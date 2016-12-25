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
    public class UrunlerController : ApiController
    {

        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["baglantiismi"].ConnectionString);
        SqlCommand komut;

        // GET api/<controller>
        public IEnumerable<Urunler> Get()
        {

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            List<Urunler> urunlers = new List<Urunler>();
            SqlCommand cmd = new SqlCommand("Select * from D_urunler ", baglanti);
            SqlDataReader read = cmd.ExecuteReader();
            try
            {

                while (read.Read())
                {
                    Urunler urun = new Urunler();
                    urun.urun_id = read["urun_id"].ToString();
                    urun.urun_adi = read["urun_adi"].ToString();
                    urun.fiyat = read["fiyat"].ToString();
                    urun.aciklama = read["aciklama"].ToString();
                    urun.eklenme_tarihi = read["eklenme_tarihi"].ToString();
                    urun.begeni_sayisi = read["begeni_sayisi"].ToString();
                    urun.adet = read["adet"].ToString();
                    urunlers.Add(urun);

                }
                return urunlers;
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

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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