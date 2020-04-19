using Business.Base;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using static Business.Utilities.Functions;

namespace Business.Services
{
    public class UserService : BaseService, IDisposable
    {
        #region Definition of Properties
        public long cantidad { get; set; }
        #endregion
        #region Definition of Constructors
        public UserService() : base()
        {

        }
        #endregion
        #region Definition of Public Methods
        public User VerifyCredentials(string username, string password)
        {
            User usuario = new User();
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
                usuario.persona.persona_Id = long.Parse(table.Rows[0]["Person_id"].ToString());
                usuario.persona.nombre= table.Rows[0]["Name"].ToString();
                usuario.persona.apellido1 = table.Rows[0]["LastName1"].ToString();
                usuario.persona.apellido1 = table.Rows[0]["LastName2"].ToString();
                usuario.persona.correo = table.Rows[0]["Email"].ToString();
                usuario.persona.telefono = table.Rows[0]["phoneNumber"].ToString();
                usuario.rol = int.Parse(table.Rows[0]["Role_id"].ToString());

                string departamento_id = table.Rows[0]["department_id"].ToString();
                if (departamento_id == "")
                {
                    usuario.departamento.departamento_Id = 0;
                }
                else {
                    usuario.departamento.departamento_Id = int.Parse(departamento_id);
                }

                
                usuario.departamento.nombre = table.Rows[0]["DepartmentName"].ToString();


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

        public List<User> list()
        {
            List<User> usuarios = new List<User>();
            DataSet data;
            string query;

            try
            {
                connection.Open();
                //revisar query con lainer...
                query = "select * from user;";

                data = connection.SelectData(query);

                if (data == null || data.Tables.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    usuarios.Add(new User()
                    {
                        usuario_Id = row["User_Id"].ToString(),
                        nombre = row["UserName"].ToString(),
                        rol = int.Parse(row["Role_id"].ToString()),
                        persona = new Persona() { persona_Id = int.Parse(row["Person_Id"].ToString()) },
                        departamento = new Department() { departamento_Id = int.Parse(row["department_id"].ToString()) }
                    });

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

        public void add(User usuario)
        {
            string query;
            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "CALL addUser(" + Int32.Parse(usuario.usuario_Id) + ",'" + usuario.nombre
                    + "','" + usuario.password + "'," + usuario.persona.persona_Id + "," + usuario.rol + "," + usuario.departamento.departamento_Id + ");";

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

        public void signUp(User user)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();
               // user.persona.persona_Id = long.Parse(user.persona.strId.Trim());

                query = "CALL UsuarioRegistro (" + ""+ user.persona.strId.ToString() + ",'" + user.persona.nombre.Trim() + "','" +  
                            user.persona.apellido1.Trim() + "','" + user.persona.apellido2.Trim() + "','"  + user.persona.correo.Trim() +
                            "','" + user.persona.telefono.Trim() + "'," + user.usuario_Id.ToString() + ",'" + user.nombre.Trim() + "','" + 
                             user.password.Trim() + "'," + user.rol.ToString() + ")";

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

        public void delete(string usuario_Id)
        {
            string query;
            try
            {
                connection.Open();
                connection.BeginTransaction();
                //CALL insert_studentinfo(125,'Raman','Bangalore','Computers')//
                query = "CALL deleteUser(" + Int32.Parse(usuario_Id) + ")";

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

        public void update(User usuario)
        {
            string query;
            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "CALL updateUser(" + Int32.Parse(usuario.usuario_Id) + ",'" + usuario.nombre
                    + "'," + usuario.rol + "," + usuario.departamento.departamento_Id + ");";

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
            if (connection != null )
                connection.Close();

            connection = null;
        }
        #endregion
    }
}