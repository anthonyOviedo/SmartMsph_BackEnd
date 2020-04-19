using Business.Base;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using static Business.Utilities.Functions;

namespace Business.Services
{
    public class DepartmentService : BaseService, IDisposable
    {
        #region Definition of Properties
        public long cantidad { get; set; }
        #endregion
        #region Definition of Constructors
        public DepartmentService() : base()
        {

        }
        #endregion
       
        #region Definition of Public Methods
       
        public List<Department> list()
        {
            List<Department> departamentos = new List<Department>();
            DataSet data;
            string query;

            try
            {
                connection.Open();
                //por hacer en la base de datos..... stop procedure.
                query = "select * from department";

                data = connection.SelectData(query);

                if (data == null || data.Tables.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    departamentos.Add(new Department()
                    {
                        //revisar en el baclog o la base para hacer esto
                        departamento_Id = Int32.Parse(row["Department_Id"].ToString()),
                        nombre = row["DepartmentName"].ToString(),
                        persona_id = Int32.Parse(row["Person_Id"].ToString()),
                    });
                }

                return departamentos;
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

        public void add(Department dep){
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "CALL DepartamentoRegistro( " + dep.departamento_Id + ",'" + dep.nombre + "'," + dep.persona_id+");";

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

        public void delete(string departmentId) {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "CALL deleteDepartment(" + departmentId + ");";
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

        public void update(Department dep) {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "CALL updateDepartment(" + dep.departamento_Id + ",'" + dep.nombre.Trim() + "'," + dep.persona_id + ");";

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