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
        public DepartmentService() : base()
        {

        }


        public void saveDepartment(Department department)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();



                query = "CALL SaveDepartment(" + department.department_Id.ToString() + ",'" + department.name + "'" + "," + department.person_id.ToString() + ")";

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


        public void DeleteDepartment(int Department_id)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "CALL DeleteDepartment" + "('" + Department_id + "'" + ")";

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


        public void Dispose()
        {
            if (connection != null)
                connection.Close();

            connection = null;
        }
    }
}
