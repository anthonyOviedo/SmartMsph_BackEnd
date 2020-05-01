using Business.Base;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Data;
using static Business.Utilities.Functions;

namespace Business.Services
{

    public class NewService : BaseService, IDisposable
    {

        public NewService() : base()
        {

        }

        public void NewInsert(int new_id,String descripcion, string fileToUpload, string titulo)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();
          //      byte[] newBytes = Convert.FromBase64String(fileToUpload);
                



                query = "CALL NewInsert("+ new_id + ",'" + descripcion + "'" +
                        ",'" + fileToUpload + "','" + titulo + "')";

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




        public List<News> TodasLasNoticias()
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

                    News noticia = new News();
                    
                    string titulo = row["titulo"].ToString();
                 string descripcion = row["descri"].ToString();
                    string Photo = (row["photo"]).ToString();
                    int noticia_id = int.Parse(row["News_Id"].ToString());
                    //  var bitesPhoto = (row["photo"]);
                    //  string Photo = Convert.ToBase64String((byte[])bitesPhoto);
                    noticia.new_id = noticia_id;
                    noticia.titulo = titulo;
                    noticia.descripcion = descripcion;
                    noticia.fileToUpload = Photo;
                    noticias.Add(noticia); 


                        }
            


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


        public void Delete(int news_id)
        {
            string query;

            try
            {
                connection.Open();
                connection.BeginTransaction();

                query = "CALL DeleteNews" + "('" + news_id + "'" + ")";

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
//reclutamineto@crnova.com