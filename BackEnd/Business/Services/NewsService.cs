using Business.Base;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using static Business.Utilities.Functions;

namespace Business.Services
{

    public class NewsService : BaseService, IDisposable
    {

        public NewsService() : base()
        {

        }

        public void add(News news)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();
                byte[] newBytes = Convert.FromBase64String(news.fileToUpload);
                



                query = "CALL NewInsert('" + news.descripcion + "'" +
                        ",'" + newBytes + "','" + news.titulo + "')";

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

        public List<News> list()
        {
            List<News> noticias = new List<News>();
            DataSet data;
            string query;

            try
            {
                connection.Open();

                query = "CALL ObtainNews()";

                data = connection.SelectData(query);

                if (data == null || data.Tables.Count == 0)
                    VerifyMessage("Ocurrió un error durante la transacción por favor inténtelo de nuevo");

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    //falta el id de la noticia
                    News noticia = new News();
                    string titulo = row["titulo"].ToString();
                    string descripcion = row["descri"].ToString();
                    var bitesPhoto = (row["photo"]);
                    string Photo =Convert.ToBase64String((byte[])bitesPhoto);
                    noticia.titulo = titulo;
                    noticia.descripcion = descripcion;
                    noticia.fileToUpload = Photo;
                    noticias.Add(noticia); 


                }
                Console.WriteLine(noticias[0].fileToUpload);


                return noticias;
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

        public void delete(string newsId)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

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

        public void update(News news)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

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
        
        //todo
            //SEARCH

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
//reclutamineto@crnova.com