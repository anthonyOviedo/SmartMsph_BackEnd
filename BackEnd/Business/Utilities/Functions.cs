using Business.Models;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using static Business.Utilities.Declarations;

namespace Business.Utilities
{
    public static class Functions
    {
        #region Definition of Private Methods
        private static SecurityKey GetSymmetricSecurityKey(string secretkey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
        }

        private static TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = GetSymmetricSecurityKey(GetAppSetting(nameTagSecret))
            };
        }
        #endregion
        #region Definition of Public Methods
        public static bool FormatRegionalOptions()
        {
            try
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Base64Encode(string pCadena)
        {
            byte[] cadenaByte = new byte[pCadena.Length];
            cadenaByte = Encoding.UTF8.GetBytes(pCadena);
            string encodedCadena = Convert.ToBase64String(cadenaByte);
            return encodedCadena;
        }

        public static string Base64Decode(string pCadena)
        {
            var encoder = new UTF8Encoding();
            var utf8Decode = encoder.GetDecoder();

            byte[] cadenaByte = Convert.FromBase64String(pCadena);
            int charCount = utf8Decode.GetCharCount(cadenaByte, 0, cadenaByte.Length);
            char[] decodedChar = new char[charCount];
            utf8Decode.GetChars(cadenaByte, 0, cadenaByte.Length, decodedChar, 0);
            string result = new string(decodedChar);
            return result;
        }

        public static string EncodeMD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            StringBuilder strBuilder = new StringBuilder();

            try
            {
                md5.ComputeHash(Encoding.ASCII.GetBytes(text));

                byte[] result = md5.Hash;

                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("x2"));
                }

                return strBuilder.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetConnectionString(string connection)
        {
            try
            {
                return Base64Decode(ConfigurationManager.ConnectionStrings[connection].ConnectionString);
            }
            catch
            {
                return string.Empty;
            }
        }

        internal static string GetAppSetting(string key)
        {
            try
            {
                return Base64Decode(ConfigurationManager.AppSettings[key]);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static void VerifyMessage(string message)
        {
            if (message.Trim() != string.Empty)
                throw new Exception(message.Trim());
        }

        public static HttpContent CreateContent(object body)
        {
            StringContent content;

            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = Int32.MaxValue;

                content = new StringContent(serializer.Serialize(body));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return content;
            }
            catch
            {
                return null;
            }
        }

        public static RestError NewError(Exception ex, string title)
        {
            try
            {
                if (title == string.Empty)
                    title = "Error";

                return new RestError() { code = ex.HResult, title = title, message = ex.Message, data = ex.InnerException };
            }
            catch
            {
                return null;
            }
        }

        public static ResponseConfig VerifyAuthorization(HttpRequestHeaders headers)
        {
            ResponseConfig response = new ResponseConfig();
            User usuario;

            try
            {
                if (headers.Authorization == null || headers.Authorization.ToString().Trim() == string.Empty)
                    VerifyMessage("El método requiere autenticación");

                string scheme = headers.Authorization.Scheme;
                string token = headers.Authorization.Parameter;

                if (scheme == null || scheme.ToLower().Trim() != "bearer")
                    VerifyMessage("Debe de enviar el esquema de autenticación");

                if (token == null || token.Trim() == string.Empty)
                    VerifyMessage("Debe de enviar el token");

                if (!VerifyToken(token, out usuario))
                    VerifyMessage("El token no es válido");

                response.usuario = usuario;
            }
            catch (Exception ex)
            {
                response.isAuthenticated = false;
                response.errorMessage = ex.Message;
                response.usuario = null;
            }

            return response;
        }

        public static string GenerateToken(User usuario)
        {
            string token;

            try
            {
                if (usuario == null || usuario.usuario_Id.Trim() == string.Empty)
                    throw new ArgumentException("Arguments to create token are not valid.");

                ClaimsIdentity claims = new ClaimsIdentity();

                claims.AddClaim(new Claim("usuario_Id", usuario.usuario_Id));
                claims.AddClaim(new Claim("nombre", usuario.nombre));

                IdentityModelEventSource.ShowPII = true;

                SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();
                securityTokenDescriptor.Subject = new ClaimsIdentity(claims);
                securityTokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(GetAppSetting(nameTagTimeSession)));
                securityTokenDescriptor.SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(GetAppSetting(nameTagSecret)), GetAppSetting(nameTagAlgorithm));

                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

                token = jwtSecurityTokenHandler.WriteToken(securityToken);

                return token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool VerifyToken(string token, out User usuario)
        {
            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken;

            try
            {
                if (string.IsNullOrEmpty(token))
                    throw new ArgumentException("No se ha enviado el token");

                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                jwtToken = (JwtSecurityToken)validatedToken;

                usuario = new User();
                usuario.usuario_Id = jwtToken.Payload["usuario_Id"].ToString();
                usuario.nombre = jwtToken.Payload["nombre"].ToString();

                return true;
            }
            catch
            {
                usuario = null;
                return false;
            }
        }
        #endregion
    }
}