using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class GirisController : ApiController
    {

        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["baglantiismi"].ConnectionString);
        SqlCommand komut;
       

        public bool Session { get; private set; }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [Route("api/Giris/{kul_adi}/{sifre}")]
        // GET api/<controller>/5
        public bool Get(string kul_adi, string sifre)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
           
            SqlCommand cmd = new SqlCommand("Select * from Uyeler where kullanici_adi=@kul_adi and sifre=@sifre ", baglanti);
            cmd.Parameters.Add("@kul_adi", SqlDbType.VarChar).Value = kul_adi;
            cmd.Parameters.Add("@sifre", SqlDbType.VarChar).Value = sifre;
            SqlDataReader read = cmd.ExecuteReader();
           
            try
            {

                if (read.Read())
                {
                    return true;
                }else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                return false;
            }
            finally
            {
                baglanti.Dispose();
                baglanti.Close();

            }
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}