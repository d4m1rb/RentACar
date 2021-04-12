using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.Web;
using RentACar.WebAplikacija.Helper;
using RentACar.WebAplikacija.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace RentACar.WebAplikacija.Controllers
{
   
    public class LoginController : Controller
    {
        APIService _apiService = new APIService("Klijent");
        APIService _korisnikService = new APIService("Korisnik");
        public async Task<IActionResult> Index()
        {
            LoginVM model=null;
            Klijent k = await HttpContext.GetLogiraniKorisnik();
            if (k!=null)
            {
                model = new LoginVM() { username = k.UserName };
                return View(model);
            }

            return View();
        }


        private static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        public IActionResult Logout()
        {
           

                HttpContext.SetLogiraniKorisnik(null, true);
                HttpContext.Session.Set<string>("UserName", null);


                return RedirectToAction("Index", "Login");
            }

            public async Task<IActionResult> Login(LoginVM input)
        {
            if (input.username==null)
            {
                return RedirectToAction("Index", "Login");
            }
            
            //KorisnickiNalog korisnik = _db.KorisnickiNalog
            //    .SingleOrDefault(x => x.KorisnickoIme == input.username && x.Lozinka == input.password);
            //APIService.Username = input.username;
            //APIService.Password = input.password;

            KlijentSearchRequest userNameSearch = new KlijentSearchRequest() { UserName = input.username, Status = true };
            var kl2 = await _apiService.Get<List<Klijent>>(userNameSearch);
            var kl = kl2.FirstOrDefault();

            KorisniciSearchRequest userNSearch = new KorisniciSearchRequest() { UserName = input.username, Status = true };
            var kor2 = await _korisnikService.Get<List<Korisnici>>(userNSearch);
            var kor = kor2.FirstOrDefault();

            if (kl == null && kor==null)
            {
                TempData["error_login"] = "Pogrešan username ili password!";
                return RedirectToAction("Index", "Login");
            }
            else if(kl!=null)
            {
                var pwHash = GenerateHash(kl.LozinkaSalt, input.password);
                KlijentSearchRequest search = new KlijentSearchRequest() { LozinkaHash = pwHash, UserName = input.username, Status = true };
                var klijenti = await _apiService.Get<List<Klijent>>(search);
                var klijent = klijenti.FirstOrDefault();

                if (klijent == null)
                {
                    TempData["error_poruka"] = "Pogrešan username ili password!";
                    return RedirectToAction("Index");
                }                              

                HttpContext.SetLogiraniKorisnik(klijent, true);
                HttpContext.Session.Set<string>("UserName",klijent.UserName);                               

                return RedirectToAction("Index", "Home");
            }
            else /*if(kor!=null)*/
            {
                var pwHash = GenerateHash(kor.LozinkaSalt, input.password);
                KorisniciSearchRequest search = new KorisniciSearchRequest() {LozinkaHash = pwHash, UserName = input.username, Status = true };
                var korisnici = await _korisnikService.Get<List<Korisnici>>(search);
                var korisnik = korisnici.FirstOrDefault();

                if (korisnik == null)
                {
                    TempData["error_poruka"] = "Pogrešan username ili password!";
                    return RedirectToAction("Index");
                }                             

                HttpContext.SetLogiraniKorisnikAdm(korisnik, true);
                HttpContext.Session.Set<string>("UserName", korisnik.UserName);
                APIService.Username = korisnik.UserName;
                APIService.Password = input.password;

                return RedirectToAction("Administrator", "Home");
            }
                       
            //HttpContext.SetLogiraniKorisnik(korisnik, input.ZapamtiPassword); 
        }
    }
}