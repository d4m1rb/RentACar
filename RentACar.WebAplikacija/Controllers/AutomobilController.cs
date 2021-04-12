using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.Web;
using RentACar.WebAplikacija.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace RentACar.WebAplikacija.Controllers
{
    public class AutomobilController : Controller
    {
        private APIService _automobilService = new APIService("Automobil");
        private APIService _ocjenaService = new APIService("Ocjena");
        public async Task<IActionResult> Index(string generalSearch)
        {
            AutomobilIndexVM model = new AutomobilIndexVM();
            var automobili=await _automobilService.Get<List<Automobil>>(new AutomobilSearchRequest() { Dostupan = true });

            

            foreach (var item in automobili)
            {
                var ocjene = await _ocjenaService.Get<List<Ocjena>>(new OcjenaSearchRequest() { VoziloId = item.AutomobilId });
                decimal prosjecnaOcjena = 0;

                if (ocjene.Count() != 0)
                {
                    var sumaOcjena = 0; var brojOcjena = 0;
                    foreach (var ocjena in ocjene)
                    {
                        sumaOcjena += ocjena.Ocjena1;
                        brojOcjena++;
                    }

                    prosjecnaOcjena = sumaOcjena / (decimal)brojOcjena;
                }

                AutomobilIndexVM.Row x = new AutomobilIndexVM.Row();

                x.AutomobilId = item.AutomobilId;
                x.Boja = item.Boja;
                x.BrojSjedista = item.BrojSjedista;
                x.BrojVrata = item.BrojVrata;
                x.CijenaIznajmljivanja = item.CijenaIznajmljivanja;
                x.CijenaKaskoOsiguranja = item.CijenaKaskoOsiguranja;
                x.Dostupan = item.Dostupan;
                x.DostupanTekst = item.DostupanTekst;
                x.EmisioniStandard = item.EmisioniStandard;
                x.GodinaProizvodnje = item.GodinaProizvodnje;
                x.Gorivo = item.Gorivo;
                x.KategorijaId = item.KategorijaId;
                x.Kubikaza = item.Kubikaza;
                x.ModelId = item.ModelId;
                x.Novo = item.Novo;
                x.Potrosnja = item.Potrosnja;
                x.ProizvodjacModel = item.ProizvodjacModel;
                x.RegistarskaOznaka = item.RegistarskaOznaka;
                x.RegistrovanDo = item.RegistrovanDo;
                x.Slika = item.Slika;
                x.SlikaThumb = item.SlikaThumb;
                x.SnagaMotora = item.SnagaMotora;
                x.Transmisija = item.Transmisija;
                x.ProsjecnaOcjena = prosjecnaOcjena;
                x.KubikazaString = (Decimal.Parse(item.Kubikaza) / 100 * 100).ToString("0.00");

                if(generalSearch!=null)
                {
                    if (item.ProizvodjacModel.ToUpper().Contains(generalSearch.ToUpper()))
                    {
                        model.listaAutomobila.Add(x);
                    }
                }
                else
                {
                    model.listaAutomobila.Add(x);
                }                
            }

            var listatemp = model.listaAutomobila.OrderBy(x => x.ProizvodjacModel).ToList();
            model.listaAutomobila.Clear();
            model.listaAutomobila = listatemp;

            return View(model);
        }
    }
}