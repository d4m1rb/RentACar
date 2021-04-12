using AutoMapper;
using RentACarApp.Model;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarApp.WebAPI.Services
{
    public class AutomobilService : BaseCRUDService<Model.Models.Automobil, AutomobilSearchRequest, Database.Automobil, AutomobiliUPSERTtRequest, AutomobiliUPSERTtRequest>
    {
        public AutomobilService(RentACarAppContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.Models.Automobil> Get(Model.Requests.AutomobilSearchRequest search)
        {
            var query = _context.Set<Database.Automobil>().Include(y=> y.Model).AsQueryable();

            if (search.AutomobilId > 0)
            {
                query = query.Where(x => x.AutomobilId == search.AutomobilId);
            }
            if (search.ModelId > 0)
            {
                query = query.Where(x => x.ModelId == search.ModelId);
            }
            if (search.KategorijaId > 0)
            {
                query = query.Where(x => x.KategorijaId == search.KategorijaId);
            }
            if (search.ProizvodjacId > 0)
            {
                query = query.Where(x => x.Model.ProizvodjacId == search.ProizvodjacId);
            }
            if (search.GodinaProizvodnje > 0)
            {
                query = query.Where(x => x.GodinaProizvodnje == search.GodinaProizvodnje);
            }
            if (!string.IsNullOrWhiteSpace( search.RegistarskaOznaka))
            {
                var registracija = _context.RegistracijaVozila.Where(y => y.RegistarskeOznake.ToLower().StartsWith(search.RegistarskaOznaka) && y.Status==true).FirstOrDefault();
                if (registracija != null)
                {
                    query = query.Where(x => x.AutomobilId == registracija.AutomobilId);
                }
                else
                {
                    query = query.Where(x => x.AutomobilId == 0);
                }
            }
            if(search.Dostupan != null)
            query = query.Where(x => x.Dostupan == search.Dostupan);
                
            var list = query.ToList();

            List<Model.Models.Automobil> result = _mapper.Map<List<Model.Models.Automobil>>(list);
            foreach (var item in result)
            {
                var vozilo = _context.Automobil.FirstOrDefault(x => x.AutomobilId == item.AutomobilId);
                var model = _context.Model.Include(y=> y.Proizvodjac).FirstOrDefault(x => x.ModelId == item.ModelId);


                item.ProizvodjacModel = model.Proizvodjac.Naziv + " " + model.Naziv;
                item.CijenaIznajmljivanja = vozilo.CijenaIznajmljivanja;


                //Provjera da li je vozilo registrovano
                var posljednjaRegistracija = _context.RegistracijaVozila.FirstOrDefault(x => x.AutomobilId == item.AutomobilId && x.Status);
                bool isticeRegistracija = false;

                if (posljednjaRegistracija != null)
                {
                    item.RegistrovanDo = posljednjaRegistracija.VaziDo;
                    item.RegistarskaOznaka = posljednjaRegistracija.RegistarskeOznake;
                    TimeSpan timeSpan = (posljednjaRegistracija.VaziDo - DateTime.Now);
                    if (timeSpan.Days < 15)
                    {
                        item.Dostupan = false;
                        isticeRegistracija = true;
                    }
                }

                //Provjera da li je vozilo trenutno rezervisano
                //Ako rezervacija nije u toku ne uzima se u obzir
                //var rezervacije = _context.RezervacijaRentanja.Where(x => x.AutomobilId == vozilo.AutomobilId
                //                                                      && x.RezervacijaOd.Date == DateTime.Now.Date
                //                                                      && x.Otkazana==false).ToList();

                //if (rezervacije.Count > 0)
                //{
                //    item.Dostupan = false;
                //}

                item.DostupanTekst = item.Dostupan ? "Dostupno vozilo" : "Nije dostupan";
                //if (rezervacije.Count > 0)
                //{
                //    item.DostupanTekst = "Trenutno rezervisan";
                //}
                if (isticeRegistracija)
                {
                    item.DostupanTekst = "Uskoro istice registracija";
                }

            }
          
            return result;
        }

        public override Model.Models.Automobil GetById(int id)
        {
            var vozilo = _context.Automobil.FirstOrDefault(x => x.AutomobilId == id);
            var model = _context.Model.Include(y=> y.Proizvodjac).FirstOrDefault(x => x.ModelId == vozilo.ModelId);

            var result = _mapper.Map<Model.Models.Automobil>(vozilo);
            result.ProizvodjacModel = model.Proizvodjac.Naziv + " " + model.Naziv;

            var posljednjaRegistracija = _context.RegistracijaVozila.FirstOrDefault(x => x.AutomobilId == result.AutomobilId && x.Status);
            if (posljednjaRegistracija != null)
            {
                result.RegistrovanDo = posljednjaRegistracija.VaziDo;
                result.RegistarskaOznaka = posljednjaRegistracija.RegistarskeOznake;
                TimeSpan timeSpan = (posljednjaRegistracija.VaziDo - DateTime.Now);
                if (timeSpan.Days < 15)
                    result.Dostupan = false;
            }
            //var rezervacije = _context.RezervacijaRentanja.Where(x => x.AutomobilId == vozilo.AutomobilId
            //                                                         && x.RezervacijaOd.Date == DateTime.Now.Date).ToList();

            //if (rezervacije.Count>0)
            //    result.Dostupan = false;

            result.DostupanTekst = result.Dostupan ? "Dostupno vozilo" : "Nije dostupan";

            return result;
        }


    }
}
