using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace RentACarApp.WebAPI.Services
{
    public class RezervacijaRentanjaService : BaseCRUDService<Model.Models.RezervacijaRentanja, Model.Requests.RezervacijaRentanjaSearchRequest, Database.RezervacijaRentanja, Model.Requests.RezervacijaRentanjaUpsertRequest, Model.Requests.RezervacijaRentanjaUpsertRequest>
    {
        public RezervacijaRentanjaService(RentACarAppContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.Models.RezervacijaRentanja> Get(RezervacijaRentanjaSearchRequest search)
        {
            var query = _context.Set<Database.RezervacijaRentanja>().Include(x => x.Automobil)
                                                                    .ThenInclude(y => y.Model)
                                                                    .ThenInclude(k => k.Proizvodjac)
                                                                    .Include(f => f.Klijent)
                                                                    .OrderByDescending(x => x.RezervacijaOd)
                                                                    .AsQueryable();

            #region Search
            if (search.AutomobilId > 0)
            {
                query = query.Where(x => x.AutomobilId == search.AutomobilId);
            }
            if (search.KlijentId > 0)
            {
                query = query.Where(x => x.KlijentId == search.KlijentId);
            }
            if (search.ModelId > 0)
            {
                query = query.Where(x => x.Automobil.ModelId == search.ModelId);
            }
            if (search.ProizvodjacId > 0)
            {
                query = query.Where(x => x.Automobil.Model.ProizvodjacId == search.ProizvodjacId);
            }
            if (!string.IsNullOrWhiteSpace(search.Ime))
            {
                var klijent = _context.Klijent.Where(y => y.Ime.StartsWith(search.Ime)).FirstOrDefault();
                if (klijent != null)
                {
                    query = query.Where(x => x.Klijent.Ime.StartsWith(search.Ime));

                }
            }
            if (!string.IsNullOrWhiteSpace(search.Prezime))
            {
                var klijent = _context.Klijent.Where(y => y.Prezime.StartsWith(search.Prezime)).FirstOrDefault();
                if (klijent != null)
                {
                    query = query.Where(x => x.Klijent.Prezime.StartsWith(search.Prezime));
                }
            }
            if (!string.IsNullOrWhiteSpace(search.Username))
            {
                var klijent = _context.Klijent.Where(y => y.UserName.StartsWith(search.Username)).FirstOrDefault();
                if (klijent != null)
                {
                    query = query.Where(x => x.Klijent.UserName.StartsWith(search.Username));

                }
            }
            if (!string.IsNullOrWhiteSpace(search.RegistarskaOznaka))
            {
                var registracija = _context.RegistracijaVozila.Where(y => y.RegistarskeOznake.ToLower().StartsWith(search.RegistarskaOznaka) && y.Status == true).FirstOrDefault();
                if (registracija != null)
                {
                    query = query.Where(x => x.AutomobilId == registracija.AutomobilId);

                }
                else
                    query = query.Where(x => x.AutomobilId == 0);

            }
            if (search.RezervacijaOd.HasValue)
            {
                query = query.Where(x => x.RezervacijaOd.Date >= search.RezervacijaOd.Value.Date);
            }
            if (search.RezervacijaDo.HasValue  )
            {
                query = query.Where(x => x.RezervacijaDo.Date <= search.RezervacijaDo.Value.Date);
            }
            if (search.DatumKreiranja.HasValue)
            {
                query = query.Where(x => x.DatumKreiranja.Date == search.DatumKreiranja.Value.Date);
            }
            if(search.StatusAktivna != null)
            {
                query = query.Where(x => x.RezervacijaDo.Date >= DateTime.Now.Date);

            }
            if (search.Otkazana != null)
            {
                query = query.Where(x => x.Otkazana == search.Otkazana);


            }
            if (search.VracanjeUposlovnicu != null)
            {
                query = query.Where(x => x.VracanjeUposlovnicu == search.VracanjeUposlovnicu);


            }
            if (search.KaskoOsiguranje != null)
            {
                query = query.Where(x => x.KaskoOsiguranje == search.KaskoOsiguranje);
            }

            #endregion

            var list = query.ToList();

            #region ModelPLUS attributes
            List<Model.Models.RezervacijaRentanja> result = _mapper.Map<List<Model.Models.RezervacijaRentanja>>(list);
            foreach (var item in result)
            {
                item.RezervacijaInformacije = item.DatumKreiranja.ToShortDateString() + " (" + item.VoziloInformacije + ")";
                var klijent = _context.Klijent.FirstOrDefault(x => x.KlijentId == item.KlijentId);
                item.Klijent = klijent.Ime + " " + klijent.Prezime;

                var vozilo = _context.Automobil.Include(y=> y.Model)
                                               .ThenInclude(p=>p.Proizvodjac)
                                               .FirstOrDefault(x => x.AutomobilId == item.AutomobilId);
                var posljednjaRegistracija = _context.RegistracijaVozila
                                                     .FirstOrDefault(x => x.AutomobilId == item.AutomobilId
                                                                       && x.Status);
                item.VoziloInformacije = vozilo.Model.Proizvodjac.Naziv + " " + vozilo.Model.Naziv;
                item.VoziloProizvodjacModel = vozilo.Model.Proizvodjac.Naziv + " " + vozilo.Model.Naziv;

                if (posljednjaRegistracija != null)
                    item.VoziloInformacije =item.VoziloInformacije+ "  (" + posljednjaRegistracija.RegistarskeOznake + ")";
                if(vozilo.Slika != null)
                item.SlikaThumb = vozilo.Slika;

                item.CijenaIznajmljivanja = vozilo.CijenaIznajmljivanja;
                item.RezervacijaOdDo = item.RezervacijaOd.ToString() + " - " + item.RezervacijaDo.ToString();
                item.RezervacijaBrojDana = (item.RezervacijaDo - item.RezervacijaOd).Days.ToString();
                if (item.RezervacijaBrojDana == "0")
                    item.RezervacijaBrojDana = "1";
                Database.Ocjena ocjene = _context.Ocjena.FirstOrDefault(x => x.RezervacijaRentanjaId == item.RezervacijaRentanjaId);
                if (ocjene != null)
                {
                    item.IsOcjenjena = true;
                    item.Ocjena = ocjene.Ocjena1;
                }
                else
                    item.IsOcjenjena = false;

                if (item.LokacijaPreuzimanja == "")
                    item.LokacijaPreuzimanja = "Preuzimanje u poslovnici";
            }
            #endregion 

            return result;
        }
        public override Model.Models.RezervacijaRentanja GetById(int id)
        {
            var rezervacija = _context.RezervacijaRentanja.FirstOrDefault(x => x.RezervacijaRentanjaId == id);
            var klijent = _context.Klijent.FirstOrDefault(x => x.KlijentId == rezervacija.KlijentId);
            var vozilo = _context.Automobil.Include(y=> y.Model).ThenInclude(z=>z.Proizvodjac).FirstOrDefault(x => x.AutomobilId == rezervacija.AutomobilId);

            var result = _mapper.Map<Model.Models.RezervacijaRentanja>(rezervacija);
            result.VoziloInformacije = vozilo.Model.Proizvodjac.Naziv + " " + vozilo.Model.Naziv;
            result.VoziloProizvodjacModel = vozilo.Model.Proizvodjac.Naziv + " " + vozilo.Model.Naziv;
            result.RezervacijaInformacije = result.DatumKreiranja.ToShortDateString() + " (" + result.VoziloInformacije + ")";
            result.Klijent = klijent.Ime + " " + klijent.Prezime;


            var posljednjaRegistracija = _context.RegistracijaVozila
                                                 .FirstOrDefault(x => x.AutomobilId == result.AutomobilId
                                                                   && x.Status);

            if (posljednjaRegistracija != null)
                result.VoziloInformacije = result.VoziloInformacije + "  (" + posljednjaRegistracija.RegistarskeOznake + ")";


            if (vozilo.Slika != null)
                result.SlikaThumb = vozilo.Slika;

            result.CijenaIznajmljivanja = vozilo.CijenaIznajmljivanja;
            result.RezervacijaOdDo = result.RezervacijaOd.ToString() + " - " + result.RezervacijaDo.ToString();
            result.RezervacijaBrojDana = (result.RezervacijaDo - result.RezervacijaOd).Days.ToString();
            if (result.RezervacijaBrojDana == "0")
                result.RezervacijaBrojDana = "1";

            Database.Ocjena ocjene = _context.Ocjena.FirstOrDefault(x => x.RezervacijaRentanjaId == result.RezervacijaRentanjaId);
            if (ocjene != null)
            {
                result.IsOcjenjena = true;
                result.Ocjena = ocjene.Ocjena1;
            }
            else
                result.IsOcjenjena = false;
            if (result.LokacijaPreuzimanja == "")
                result.LokacijaPreuzimanja = "Preuzimanje u poslovnici";

            return result;
        }

        public override bool Delete(int id)
        {
            
            var rezervacija=_context.RezervacijaRentanja.Find(id);
            var racun = _context.Racun.Find(rezervacija.RacunId);
            var poruke = _context.Poruka.Where(x => x.RezervacijaRentanjaId == id).ToList();

            foreach (var poruka in poruke)
            {
                _context.Poruka.Remove(poruka);
            }

            _context.RezervacijaRentanja.Remove(rezervacija);
            _context.Racun.Remove(racun);            

            _context.SaveChanges();

            var result = _mapper.Map<Model.Models.RezervacijaRentanja>(rezervacija);
            return true;
        }
    }
}
