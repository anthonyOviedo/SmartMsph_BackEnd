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

        public void add(Denounce denounce)            
        {
            string query;
            try
            {
                connection.Open();
                connection.BeginTransaction();



                query = "CALL SaveDenounce('" + denounce.Description + "'" + ",'" + denounce.state + "'" + ",'" + denounce.Ticket_id + "'" + ",'" + denounce.person_Id+ "'" + ",'" + denounce.User_id + "'" + ",'" + denounce.Department_Id + "','"+ denounce.Photo + "','"+ denounce.Latitud + "','" + denounce.Longitud + "')";

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
 
        public void delete(string complainId)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();
                //todo 
                query = "";
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

        public void update(Denounce denounce)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();
                //TODO
                query = "";

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

        public List<Complain> list()
        {
            List<Complain> complains = new List<Complain>();
            DataSet data;
            string query;

            try
            {
                connection.Open();
                //todo
                query = "";
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

               // this.cantidad = long.Parse(data.Tables[1].Rows[0]["Cantidad"].ToString());

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
        #endregion

    }
}
