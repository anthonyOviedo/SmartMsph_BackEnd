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
                        state = row["state"].ToString(),
                        employee = row["employee"].ToString(),
                        
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
                        department_Id = int.Parse(row["Department_Id"].ToString()),
                        name = row["DepartmentName"].ToString(),
                        person_id = row["Person_Id"].ToString(),
                        personname = row["Name"].ToString(),
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


        public List<Funcionario> allfuncionary()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            DataSet data;
            string query;
   

            try
            {
                connection.Open();

                query = "CALL funcionaryall()";
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


        public void GuardarQueja(string descripcion,string estado,int id_persona,  int id_usuario,string empleado,string empleado_name, int id_departa,string fecha)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();



                query = "CALL ComplainInsert('"+descripcion +"'" + ",'"+estado+ "'" + ",'"+id_persona+"'"+",'"+id_usuario+"'"+",'"+ empleado_name + "'"+",'"+ empleado + "','"+id_departa+ "','" + fecha + "')";

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




        public List<Complain> ListComplainsbyId(int User_id)    
        {
            List<Complain> complains = new List<Complain>();
            DataSet data;
            string query;

            try
            {
                connection.Open();

                query = "CALL ObtainComplainXid" + "('" + User_id + "'" + ")";
                data = connection.SelectData(query);

                if (data == null || data.Tables.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    complains.Add(new Complain()
                    {


                        Complain_Id = int.Parse(row["Complain_id"].ToString()),
                        Description = row["Description"].ToString(),
                        state = row["state"].ToString(),
                        person_Id = int.Parse(row["Person_Id"].ToString()),
                        employee = row["employee"].ToString(),
                        employee_name = row["employee_name"].ToString(),
                        department_id = int.Parse(row["Department_Id"].ToString()),
                        User_id = int.Parse(row["User_id"].ToString()),
                        fecha = row["fecha"].ToString(),
                        departmentname = row["DepartmentName"].ToString()


                    }) ;
                }



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



        public void UpdateComplain(int Complain_id, string Description ,string employee ,string employee_name ,int Department_Id,string fecha)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();



                query = "CALL updateComplain('" + Complain_id + "'" + ",'" + Description + "'" + ",'" + employee + "'" + ",'" + employee_name + "'" + ",'" + Department_Id + "'" + ",'"  + fecha + "')";

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


        public void DeleteComplain(int Complain_id)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "CALL DeleteComplains" + "('" + Complain_id + "'" + ")";

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
