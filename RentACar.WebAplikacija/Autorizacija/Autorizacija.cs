using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.Web;
using RentACar.WebAplikacija.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



namespace RentACar.WebAplikacija.Autorizacija
{

    
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool klijent, bool administrator)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { klijent, administrator };
        }
    }


    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        private APIService _klijentService = new APIService("Klijent");
        private APIService _korisnikService = new APIService("Korisnik");
        public MyAuthorizeImpl(bool klijent, bool administrator)
        {
            _klijent = klijent;
            _administrator = administrator;
        }
        private readonly bool _klijent;
        private readonly bool _administrator;

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            // var k = filterContext.HttpContext.User.Identity;
           
            Klijent k = await filterContext.HttpContext.GetLogiraniKorisnik();
            Korisnici kor = null;

            if (k==null)
            {
                kor = await filterContext.HttpContext.GetLogiraniKorisnikAdm();
            }


            var sesija=filterContext.HttpContext.Session.Get<string>("UserName");

            if ((k==null && kor==null) || sesija == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }
                
                filterContext.Result = new RedirectToActionResult("Index", "Login", new { @area = "" });
                return;
            }

            //provjera da li klijent postoji u bazi
            #region Provjera da li postoji Klijent u bazi
            bool postojiKlijent = false;
            if (k != null)
            {          
                Klijent klijBaza = await _klijentService.GetById<Klijent>(k.KlijentId);
                if (klijBaza != null)
                {
                    postojiKlijent = true;
                }
            }
            #endregion

            if (_klijent && postojiKlijent)
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            //provjera da li korisnik postoji u bazi
            #region Provjera da li postoji Korisnik u bazi
            bool postojiKorisnik = false;
            if (kor != null)
            {
                Korisnici korBaza = await _korisnikService.GetById<Korisnici>(kor.KorisnikId);
                if (korBaza != null)
                {
                    postojiKorisnik = true;
                }
            }
            #endregion

            if (_administrator && postojiKorisnik)
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.ViewData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Login", new { @area = "" });

            //if(k.IsAuthenticated)
            //if (k!=null)
            //{
            //    await next();
            //    return;
            //}

            //if (filterContext.Controller is Controller c1)
            //{
            //    c1.ViewData["error_poruka"] = "Nemate pravo pristupa";
            //}
            //filterContext.Result = new RedirectToActionResult("Index", "Home", new { @area = "" });

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}

