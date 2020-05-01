using Business.Base;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using static Business.Utilities.Functions;

namespace Business.Services
{
  public class DenounceService : BaseService, IDisposable
    {
        public DenounceService() : base()
        {

        }
 
        public Ticket obtainticket(int Department_id, string Ticketcol)
        {
            Ticket ticket = new Ticket();
            DataTable table;
            string query;

            try
            {
                connection.Open();

                query = "CALL ObtainTicket ('" + Department_id + "','" + Ticketcol + "')";

                table = connection.Select(query);


                if (table == null || table.Rows.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                ticket.Ticket_id = int.Parse(table.Rows[0]["Ticket_id"].ToString());
                ticket.Department_id = int.Parse(table.Rows[0]["Department_id"].ToString());
                ticket.Ticketcol = table.Rows[0]["Ticketcol"].ToString();



                return ticket;
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

        public void saveDenounce(int denounce_id,string Description , string state,int Person_id,int User_id,int Department_id,string PPhoto,string Latitud,string Longitud)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();



                query = "CALL SaveDenounce("+ denounce_id + ",'" + Description + "'" + ",'" + state  + "','" + Person_id + "'" + ",'" + User_id + "'" + ",'" + Department_id + "','"+ PPhoto + "','"+ Latitud + "','" + Longitud + "')";

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

        public List<Denounce> ListDenouncesbyId(string user_Id)
        {
            List<Denounce> complains = new List<Denounce>();
            DataSet data;
            string query;

            try
            {
                connection.Open();

                query = "CALL Denounces_list ('" + user_Id.Trim() + "')";

                data = connection.SelectData(query);

                if (data == null || data.Tables.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    complains.Add(new Denounce()
                    {
                        Denounces_id = int.Parse(row["Denounces_id"].ToString()),
                        Description = row["Description"].ToString(),
                        Department_Id = int.Parse(row["Department_Id"].ToString()),
                        state = row["State"].ToString(),
                        Photo = row["Photo"].ToString(),
                        Longitud = row["Longitud"].ToString(),
                        Latitud = row["Latitud"].ToString(),
                        DepartmentName = row["DepartmentName"].ToString()



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





        #region Implements Interface IDisposable
        public void Dispose()
        {
            if (connection != null)
                connection.Close();

            connection = null;
        }


        public void DeleteDenounce(int Denounce_id)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "CALL DeleteDenounce" + "('" + Denounce_id + "'" + ")";

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

    }
}
