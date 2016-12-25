using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
namespace WebApi.Controllers
{
    public class KategoriController : ApiController
    {
        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["baglantiismi"].ConnectionString);
        SqlCommand komut;

        [Route("api/Kategori/{kat_id}")]

        // GET api/<controller>/5
        public List<Kategori> Get(int kat_id)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            List<Kategori> kategori = new List<Kategori>();
            SqlCommand cmd = new SqlCommand("Select * from kategoriler where kategori_id=@kat_id ", baglanti);
            cmd.Parameters.Add("@kat_id", SqlDbType.Int).Value = kat_id;
            SqlDataReader read = cmd.ExecuteReader();
            Kategori kat = new Kategori();
            try
            {
                while (read.Read())
            {

                kat.kategori_id = read["kategori_id"].ToString();
                kat.kategori_adi = read["kategori_adi"].ToString();
                kat.ust_kategori = read["ust_kategori"].ToString();
                kategori.Add(kat);
            }



            return kategori;
            }
           
        
            catch (SqlException ex)
            {
                string hata = ex.Message;
                return null;
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