using Business.Base;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using static Business.Utilities.Functions;

namespace Business.Services 
{
   public class ComplainService : BaseService, IDisposable
    {

        #region Definition of Properties
        public long cantidad { get; set; }
        #endregion
        #region Definition of Constructors
        public ComplainService() : base()
        {

        }
        #endregion

        #region Definition of Public Methods
        public List<Complain> Lista(string user_Id)
        {
            List<Complain> complains = new List<Complain>();
            DataSet data;
            string query;

            try
            {
                connection.Open();

                query = "CALL Complain_list ('" + user_Id.Trim() + "')";

                data = connection.SelectData(query);

                if (data == null || data.Tables.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    complains.Add(new Complain()
                    {
                        Complain_Id = int.Parse(row["Complain_Id"].ToString()),
                        Description = row["Description"].ToString(),
                        state = int.Parse(row["state"].ToString()),
                        employee = int.Parse(row["employee"].ToString()),
                        
                    });
                }

                this.cantidad = long.Parse(data.Tables[1].Rows[0]["Cantidad"].ToString());

                return complains;
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
       



        public List<Department> TodosDespartamentos()
        {
            List<Department> departments = new List<Department>();
            DataSet data;
            string query;

            try
            {
                connection.Open();

                query = "CALL TodoDepartamento()";

                data = connection.SelectData(query);

                if (data == null || data.Tables.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    departments.Add(new Department()
                    {
                        departamento_Id = int.Parse(row["Department_Id"].ToString()),
                        nombre = row["DepartmentName"].ToString(),
                        persona_id = int.Parse(row["Person_Id"].ToString())
                    });
                }

               

                return departments;
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

        #endregion




        public List<Funcionario> TodosFuncionarios( String depSeleccion)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            DataSet data;
            string query;
            int numero = Int32.Parse(depSeleccion);

            try
            {
                connection.Open();
              
                query = "CALL FuncioXDeparta" + "('" + numero + "'" +")";
                                data = connection.SelectData(query);

                if (data == null || data.Tables.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    funcionarios.Add(new Funcionario()
                    {
                        Person_Id = int.Parse(row["Person_Id"].ToString()),
                        apellido1 = row["LastName1"].ToString(),
                        apellido2 = row["LastName2"].ToString(),
                        nombre = row["Name"].ToString()

                    });
                }



                return funcionarios;
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


        public void GuardarQueja(string descripcion,int estado,int id_persona,  int id_usuario,string empleado,string departa_empleado,int id_departa)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();



                query = "CALL ComplainInsert('"+descripcion +"'" + ",'"+estado.ToString()+ "'" + ",'"+id_persona+"'"+",'"+id_usuario+"'"+",'"+empleado+"'"+",'"+departa_empleado+"','"+id_departa+"')";

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
