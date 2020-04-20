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

        public void add(string Description , string state ,int Ticket_id,int Person_id,int User_id,int Department_id,string PPhoto,string Latitud,string Longitud)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();



                query = "CALL SaveDenounce('" + Description + "'" + ",'" + state + "'" + ",'" + Ticket_id + "'" + ",'" + Person_id + "'" + ",'" + User_id + "'" + ",'" + Department_id + "','"+ PPhoto + "','"+ Latitud + "','" + Longitud + "')";

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

        //to do 

        //delete / cancel

        //update

        //list


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
