using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.Web;
using RentACar.WebAplikacija.Helper;
using RentACar.WebAplikacija.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text;
using System.Security.Cryptography;

namespace RentACar.WebAplikacija.Controllers
{
    public class ProfilAdminController : Controller
    {
        private APIService _korisnikService = new APIService("Korisnik");
        private APIService _gradService = new APIService("Grad");
        
        public async Task<IActionResult> Index()
        {
            
            Korisnici logiranik=await HttpContext.GetLogiraniKorisnikAdm();
            Korisnici k = await _korisnikService.GetById<Korisnici>(logiranik.KorisnikId);

            ProfilAdminIndexVM model = new ProfilAdminIndexVM()
            {
                Adresa = k.Adresa,
                Email = k.Email,
                DatumRegistracije=k.DatumRegistracije,
                DatumRodjenja=k.DatumRodjenja,
                GradId=k.GradId,
                Ime=k.Ime,
                Prezime=k.Prezime,
                KorisnikId=k.KorisnikId,
                LozinkaHash=k.LozinkaHash,
                LozinkaSalt=k.LozinkaSalt,
                Slika=k.Slika,
                SlikaThumb=k.SlikaThumb,
                Status=k.Status,
                Telefon=k.Telefon,
                UserName=k.UserName                
            };

          var listaGradova=await  _gradService.Get<List<Grad>>(null);
            model.listaGradova = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            foreach (var item in listaGradova)
            {
                model.listaGradova.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Value = item.GradId.ToString(), Text = item.Naziv });
            }

            if (!string.IsNullOrEmpty((string)TempData["Poruka"]))
                model.Poruka = (string)TempData["Poruka"];

            return View(model);
        }
        public async Task<IActionResult> SnimiSliku(IFormFile profile_avatar)
        {
            if (profile_avatar != null)
            {
                var filename = System.IO.Path.GetFileName(profile_avatar.FileName);
                Image image = null;

                //using (var localFile = System.IO.File.OpenWrite(filename))
                using (var uploadedFile = profile_avatar.OpenReadStream())
                {
                    //uploadedFile.CopyTo(localFile);
                    image = Image.FromStream(uploadedFile);

                }
                var img = new Bitmap(image);

                MemoryStream msImg = new MemoryStream();
                img.Save(msImg, System.Drawing.Imaging.ImageFormat.Jpeg);
                var SlikaNew = msImg.ToArray();


                Image thumb = image.GetThumbnailImage(75, 75, () => false, IntPtr.Zero);
                MemoryStream ms = new MemoryStream();
                thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                var SlikaThumbNew = ms.ToArray();

                Korisnici logiranik = await HttpContext.GetLogiraniKorisnikAdm();
                Korisnici k = await _korisnikService.GetById<Korisnici>(logiranik.KorisnikId);
                var uloge = k.KorisniciUloge.Select(x => x.UlogaId).ToList();

                KorisniciUpsertRequest request = new KorisniciUpsertRequest()
                {
                    Adresa = logiranik.Adresa,
                    DatumRegistracije = logiranik.DatumRegistracije,
                    DatumRodjenja = logiranik.DatumRodjenja.Value,
                    Email = logiranik.Email,
                    GradId = logiranik.GradId,
                    Ime = logiranik.Ime,
                    KorisnikId = logiranik.KorisnikId,
                    Prezime = logiranik.Prezime,
                    Status = true,
                    Telefon = logiranik.Telefon,
                    UserName = logiranik.UserName,
                    Slika = SlikaNew,
                    SlikaThumb = SlikaThumbNew,
                    Uloge=uloge
                };

                await _korisnikService.Update<Korisnici>(logiranik.KorisnikId, request);
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Snimi(ProfilAdminIndexVM model)
        {
            Korisnici k = await _korisnikService.GetById<Korisnici>(model.KorisnikId);
            var uloge = k.KorisniciUloge.Select(x => x.UlogaId).ToList();

            string dan = "", mjesec = "", godina = "";
            DateTime datumRodjenja = DateTime.MinValue, datumRegistracije = DateTime.MinValue;

            if (model.DatumRegistracijeStr != null)
            {
                var datumRegistracijeStr = model.DatumRegistracijeStr;
                dan = datumRegistracijeStr.Substring(0, 2);
                mjesec = datumRegistracijeStr.Substring(3, 2);
                godina = datumRegistracijeStr.Substring(6, 4);
                datumRegistracije = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));
            }

            if (model.DatumRodjenjaStr != null)
            {
                dan = ""; mjesec = ""; godina = "";
                var datumRodjenjaStr = model.DatumRodjenjaStr;
                dan = datumRodjenjaStr.Substring(0, 2);
                mjesec = datumRodjenjaStr.Substring(3, 2);
                godina = datumRodjenjaStr.Substring(6, 4);
                datumRodjenja = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));
            }

            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Adresa = model.Adresa,
                DatumRegistracije=datumRegistracije,
                DatumRodjenja=datumRodjenja,
                Email=model.Email,
                GradId=model.GradId,
                Ime=model.Ime,
                KorisnikId=model.KorisnikId,              
                Prezime=model.Prezime,               
                Status=true,
                Telefon=model.Telefon,
                UserName=model.UserName,
                Uloge=uloge
            };


            model.DatumRegistracije = datumRegistracije;
            model.DatumRodjenja = datumRodjenja;

            //var filename = model.SlikaPath;
            //var file = System.IO.File.ReadAllBytes(filename);
            //model.Slika = file;
            //if (model.SlikaPath != null)
            //{


            //    MemoryStream memoryStream = new MemoryStream(model.Slika);
            //    Image image = Image.FromStream(memoryStream);
            //    Image thumb = image.GetThumbnailImage(75, 75, () => false, IntPtr.Zero);

            //    MemoryStream ms = new MemoryStream();
            //    thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    model.SlikaThumb = ms.ToArray();
            //}
            //else

          request.Slika = k.Slika;
          request.SlikaThumb = k.SlikaThumb;
            
            
                           
            await _korisnikService.Update<Korisnici>(model.KorisnikId, request);
            //if (entity != null)
            //{
            //    MessageBox.Show("Uspješno izmjenjeni podaci o klijentu");
            //}



                return RedirectToAction("Index");
        }

        public async Task<IActionResult> Password()
        {

            Korisnici logiranik = await HttpContext.GetLogiraniKorisnikAdm();
            Korisnici k = await _korisnikService.GetById<Korisnici>(logiranik.KorisnikId);

            ProfilAdminPasswordVM model = new ProfilAdminPasswordVM()
            {
               
                Email = k.Email,                
                Ime = k.Ime,
                Prezime = k.Prezime,
                KorisnikId = k.KorisnikId,               
                Slika = k.Slika,
                SlikaThumb = k.SlikaThumb,
                Status = k.Status,
                Telefon = k.Telefon,
                UserName = k.UserName               
            };

            if(!string.IsNullOrEmpty((string)TempData["Poruka"]))
                model.Poruka = (string)TempData["Poruka"];

            return View(model);
        }

        public async Task<IActionResult> SnimiPassword(ProfilAdminPasswordVM input)
        {

            Korisnici logiranik = await HttpContext.GetLogiraniKorisnikAdm();
            Korisnici k = await _korisnikService.GetById<Korisnici>(logiranik.KorisnikId);
            var uloge = k.KorisniciUloge.Select(x => x.UlogaId).ToList();

            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Adresa = k.Adresa,
                Email = k.Email,
                DatumRegistracije = k.DatumRegistracije,
                DatumRodjenja = k.DatumRodjenja.Value,
                GradId = k.GradId,
                Ime = k.Ime,
                Prezime = k.Prezime,
                KorisnikId = k.KorisnikId,               
                Slika = k.Slika,
                SlikaThumb = k.SlikaThumb,
                Status = k.Status,
                Telefon = k.Telefon,
                UserName = k.UserName,
                Uloge=uloge

            };

            if(!String.IsNullOrEmpty(input.Password) && !String.IsNullOrEmpty(input.PasswordPotvrda) && !String.IsNullOrEmpty(input.StariPassword))
            {
                if (input.Password == input.PasswordPotvrda)
                {
                    request.Password = input.Password;
                    request.PasswordPotvrda = input.PasswordPotvrda;
                }
                else
                {
                    TempData["Poruka"] = "Polje PASSWORD i polje PASSWORDPOTVRDA se ne smiju razlikovati!";
                    return RedirectToAction("Password");
                    //("Polje PASSWORD i polje PASSWORDPOTVRDA se ne smiju razlikovati!");

                }

                string Hash = GenerateHash(k.LozinkaSalt, input.StariPassword);
                if (k.LozinkaHash==Hash)
                {
                    if(input.StariPassword==input.Password)
                    {
                        TempData["Poruka"] = "NOVI password ne može biti isti kao TRENUTNI password.";
                        return RedirectToAction("Password");
                    }
                    await _korisnikService.Update<Korisnici>(k.KorisnikId, request);
                    TempData["Poruka"] = "Uspješno ste izmjenili lozinku.";
                    //("Obavijest", "Uspješno ste izmjenili lozinku", "OK");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Poruka"] = "Stari password je netačan.";
                    return RedirectToAction("Password");
                }
            }
            else
            {
                //throw new Exception("Obavezno je unijeti sva polja!");
                TempData["Poruka"] = "Obavezno je unijeti sva polja.";
                return RedirectToAction("Password");
               
            }
           

            return RedirectToAction("Index");
        }

        public static string GenerateHash(string salt, string password)
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
    }
}