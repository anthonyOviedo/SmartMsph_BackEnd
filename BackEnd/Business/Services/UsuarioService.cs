using Business.Base;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using static Business.Utilities.Functions;

namespace Business.Services
{
    public class UsuarioService : BaseService, IDisposable
    {
        #region Definition of Properties
        public long cantidad { get; set; }
        #endregion
        #region Definition of Constructors
        public UsuarioService() : base()
        {

        }
        #endregion
        #region Definition of Public Methods
        public Usuario VerifyCredentials(string username, string password)
        {
            Usuario usuario = new Usuario();
            DataTable table;
            string query;

            try
            {
                connection.Open();

                query = "CALL VerifyCredentials " + "(" + "'" + username + "','" + password + "'" + ")";

                table = connection.Select(query);

                if (table == null || table.Rows.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                usuario.usuario_Id = table.Rows[0]["User_Id"].ToString();
                usuario.nombre = table.Rows[0]["UserName"].ToString();
                usuario.persona.persona_Id = int.Parse(table.Rows[0]["Person_id"].ToString());
                usuario.persona.nombre = table.Rows[0]["Name"].ToString();
                usuario.persona.apellido1 = table.Rows[0]["LastName1"].ToString();
                usuario.persona.apellido1 = table.Rows[0]["LastName2"].ToString();
                usuario.persona.correo = table.Rows[0]["Email"].ToString();
                usuario.persona.telefono = table.Rows[0]["phoneNumber"].ToString();
                usuario.rol = int.Parse(table.Rows[0]["Role_id"].ToString());

                string departamento_id = table.Rows[0]["department_id"].ToString();
                if (departamento_id == "")
                {
                    usuario.departamento.department_Id = 0;
                }
                else
                {
                    usuario.departamento.department_Id = int.Parse(departamento_id);
                }


                usuario.departamento.name = table.Rows[0]["DepartmentName"].ToString();


                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }





        public List<Usuario> Lista(int pais_Id, int emp_Id, string criterio)
        {
            List<Usuario> usuarios = new List<Usuario>();
            DataSet data;
            string query;

            try
            {
                connection.Open();

                query = "exec Usuario_Lista " + pais_Id.ToString() + "," + emp_Id.ToString() + ",'" + criterio.Trim() + "'";

                data = connection.SelectData(query);

                if (data == null || data.Tables.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    usuarios.Add(new Usuario()
                    {
                        usuario_Id = row["Usuario_Id"].ToString(),
                        nombre = row["Nombre"].ToString(),

                    });
                }

                this.cantidad = long.Parse(data.Tables[1].Rows[0]["Cantidad"].ToString());

                return usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public void Registrarse(Usuario user)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "CALL UsuarioRegistro (" + "" + user.persona.persona_Id.ToString() + ",'" + user.persona.nombre.Trim() + "','" +
                     user.persona.apellido1.Trim() + "','" + user.persona.apellido2.Trim() + "','" + user.persona.correo.Trim() +
                     "','" + user.persona.telefono.Trim() + "'," + user.usuario_Id.ToString() + ",'" + user.nombre.Trim() + "','" +
                      user.password.Trim() + "'," + user.rol.ToString() + "," + user.departamento.department_Id + ")";


                connection.Execute(query);
                connection.CommitTransaction();
            }
            catch (Exception ex)
            {
                connection.RollBackTransaction();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public void Eliminar(string usuario_Id)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "exec Usuario_Eliminar '" + usuario_Id + "'";

                connection.Execute(query);
                connection.CommitTransaction();
            }
            catch (Exception ex)
            {
                connection.RollBackTransaction();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public void ChangePassword(string usuario_Id, string password, string newPassword)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "exec Usuario_ChangePassword '" + usuario_Id + "'" +
                        ",'" + password + "','" + newPassword + "'";

                connection.Execute(query);
                connection.CommitTransaction();
            }
            catch (Exception ex)
            {
                connection.RollBackTransaction();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }



        public List<Role> ListRole()
        {
            List<Role> roles = new List<Role>();
            DataSet data;
            string query;

            try
            {
                connection.Open();

                query = "CALL ObtainRole()";

                data = connection.SelectData(query);

                if (data == null || data.Tables.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    roles.Add(new Role()
                    {
                        Role_id = int.Parse(row["Role_id"].ToString()),
                        Name = row["Name"].ToString(),

                    });
                }

                return roles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }


        public List<Usuario> UsersWithAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            Usuario usuario = new Usuario();
            DataSet data;
            string query;

            try
            {
                connection.Open();

                query = "CALL UsersWithAll()";

                data = connection.SelectData(query);

                if (data == null || data.Tables.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    usuario = new Usuario();
                    usuario.usuario_Id = row["User_Id"].ToString();
                    usuario.nombre = row["UserName"].ToString();
                    usuario.persona.persona_Id = int.Parse(row["Person_id"].ToString());
                    usuario.persona.nombre = row["Name"].ToString();
                    usuario.persona.apellido1 = row["LastName1"].ToString();
                    usuario.persona.apellido1 = row["LastName2"].ToString();
                    usuario.persona.correo = row["Email"].ToString();
                    usuario.persona.telefono = row["phoneNumber"].ToString();
                    usuario.departamento.name = row["DepartmentName"].ToString();
                    usuarios.Add(usuario);
                }

                return usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }




        public void DeleteUser(int UserId)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "CALL DeleteUser" + "('" + UserId + "'" + ")";

                connection.Execute(query);
                connection.CommitTransaction();
            }
            catch (Exception ex)
            {
                connection.RollBackTransaction();
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }








        #endregion
        #region Implements Interface IDisposable
        public void Dispose()
        {
            if (connection != null)
                connection.Close();

            connection = null;
        }
        #endregion
    }
}