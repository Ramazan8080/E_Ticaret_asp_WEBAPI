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


    public class UyelerController : ApiController
    {
        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["baglantiismi"].ConnectionString);
        SqlCommand komut;

        [Route("api/Uyeler/{kul_adi}/{sifre}")]

        // GET api/<controller>/5
        public Kullanicilar Get(string kul_adi, string sifre)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            List<Kullanicilar> Kullanicilar = new List<Kullanicilar>();
            SqlCommand cmd = new SqlCommand("Select * from Uyeler where kullanici_adi=@kul_adi and sifre=@sifre ", baglanti);
            cmd.Parameters.Add("@kul_adi", SqlDbType.VarChar).Value = kul_adi;
            cmd.Parameters.Add("@sifre", SqlDbType.VarChar).Value = sifre;
            SqlDataReader read = cmd.ExecuteReader();
            Kullanicilar kullanici = new Kullanicilar();
            try
            {

                while (read.Read())
                {
                    
                    kullanici.kullanici_adi = read["kullanici_adi"].ToString();
                    kullanici.adres = read["adres"].ToString();
                    kullanici.eposta = read["eposta"].ToString();
                    kullanici.telefon = read["telefon"].ToString();
                    kullanici.sifre = read["sifre"].ToString();
                    kullanici.sifre_tekrar = read["sifre_tekrar"].ToString();
                    Kullanicilar.Add(kullanici);
                }



                return kullanici;
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