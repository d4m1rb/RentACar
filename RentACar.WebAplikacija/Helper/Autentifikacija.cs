using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RentACar.WebAplikacija.Helper
{
    public static class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";
        private static APIService _apiService = new APIService("Klijent");
        private static APIService _korisniciService = new APIService("Korisnik");
        public static void SetLogiraniKorisnik(this HttpContext context, Klijent korisnik, bool snimiUCookie=false)
        {

            if (korisnik != null)
            {
                context.Response.SetCookieJson(LogiraniKorisnik, korisnik.UserName);
            }
            else
            {
                context.Response.SetCookieJson(LogiraniKorisnik, null);
            }
        }

        public static void SetLogiraniKorisnikAdm(this HttpContext context, Korisnici korisnik, bool snimiUCookie = false)
        {

            if (korisnik != null)
            {
                context.Response.SetCookieJson(LogiraniKorisnik, korisnik.UserName);
            }
            else
            {
                context.Response.SetCookieJson(LogiraniKorisnik, null);
            }
        }


        public static async Task<Klijent> GetLogiraniKorisnik(this HttpContext context)
       {
            

            string username = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (username == null)
                return null;

            KlijentSearchRequest search = new KlijentSearchRequest() { UserName = username, Status=true };
            var listakl=await _apiService.Get<List<Klijent>>(search);
            var kl = listakl.FirstOrDefault();

            return kl;

        }

        public static async Task<Korisnici> GetLogiraniKorisnikAdm(this HttpContext context)
        {


            string username = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (username == null)
                return null;

            KorisniciSearchRequest search = new KorisniciSearchRequest() { UserName = username, Status = true };
            var listakor = await _korisniciService.Get<List<Korisnici>>(search);
            var kor = listakor.FirstOrDefault();

            return kor;

        }

    }
}