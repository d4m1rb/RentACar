using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace RentACarApp.WebAPI.Services
{
    public class KlijentService : IKlijentService
    {
        private readonly RentACarAppContext _context;
        private readonly IMapper _mapper;
        public KlijentService(RentACarAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Model.Models.Klijent Autentificiraj(string username, string password)
        {
            var user = _context.Klijent.FirstOrDefault(x => x.UserName == username);
            if (user != null)
            {
                var newHash = GenerateHash(user.LozinkaSalt, password);

                if (newHash == user.LozinkaHash)
                {
                    return _mapper.Map<Model.Models.Klijent>(user);
                }
            }
            return null;
        }

        public List<Model.Models.Klijent> Get(KlijentSearchRequest search)
        {
            var query = _context.Set<Database.Klijent>().AsQueryable();

            if (search.GradId > 0)
            {
                query = query.Where(x => x.GradId == search.GradId);
            }
            if (!string.IsNullOrWhiteSpace(search?.Ime))
            {
                query = query.Where(x => x.Ime.ToLower().StartsWith(search.Ime));
            }
            if (!string.IsNullOrWhiteSpace(search?.Prezime))
            {
                query = query.Where(x => x.Prezime.ToLower().StartsWith(search.Prezime));
            }
            if (!string.IsNullOrWhiteSpace(search?.Email))
            {
                query = query.Where(x => x.Email.ToLower()==search.Email.ToLower());
            }
            if (!string.IsNullOrWhiteSpace(search?.UserName))
            {
                query = query.Where(x => x.UserName.ToLower()==search.UserName.ToLower());
            }
            if (!string.IsNullOrWhiteSpace(search?.LozinkaHash))
            {
                query = query.Where(x => x.LozinkaHash.ToLower() == search.LozinkaHash.ToLower());
            }

            if (search.DatumRegistracijeOd.HasValue)
            {
                query = query.Where(x => x.DatumRegistracije.Date >= search.DatumRegistracijeOd.Value.Date);
            }
            if (search.DatumRegistracijeDo.HasValue)
            {
                query = query.Where(x => x.DatumRegistracije.Date <= search.DatumRegistracijeDo.Value.Date);
            }
            query = query.Where(x => x.Status == search.Status);

            var list = query.ToList();

            List<Model.Models.Klijent> result = _mapper.Map<List<Model.Models.Klijent>>(list);

            return result;
        }

        public Model.Models.Klijent GetById(int Id)
        {
            var entity = _context.Klijent.Find(Id);

            var result= _mapper.Map<Model.Models.Klijent>(entity);

            return result;
        }

        public Model.Models.Klijent Delete(int Id)
        {
            var klijent = _context.Klijent.Find(Id);


            List<Database.RezervacijaRentanja> rezervacije = _context.RezervacijaRentanja.Where(id => id.KlijentId == Id).ToList();

            foreach (var x in rezervacije)
            {
                var RacunId = x.RacunId;

                var poruke = _context.Poruka.Where(por => por.RezervacijaRentanjaId == x.RezervacijaRentanjaId).ToList();
                foreach (var poruka in poruke)
                {
                    _context.Poruka.Remove(poruka);
                }

                _context.RezervacijaRentanja.Remove(x);

                var racun=_context.Racun.Where(id => id.RacunId == RacunId).FirstOrDefault();
                _context.Racun.Remove(racun);
            }

            _context.Klijent.Remove(klijent);

            _context.SaveChanges();

            return _mapper.Map<Model.Models.Klijent>(klijent);
        }

        public Model.Models.Klijent Insert(KlijentUpsertRequest request)
        {
            var entity = _mapper.Map<Database.Klijent>(request);

            if (request.Password != request.PasswordPotvrda)
            {
                throw new Exception("Passwordi se ne slažu");
            }

            entity.LozinkaSalt = GenerateSalt();
            entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, request.Password);

            _context.Klijent.Add(entity);
            _context.SaveChanges();

          

            return _mapper.Map<Model.Models.Klijent>(entity);
        }

      
        public Model.Models.Klijent Update(int Id, KlijentUpsertRequest request)
        {
            var entity = _context.Klijent.FirstOrDefault(x => x.KlijentId == Id);
            _context.Klijent.Attach(entity);
            _context.Klijent.Update(entity);
            request.KlijentId = entity.KlijentId;

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (request.Password != request.PasswordPotvrda)
                {
                    throw new Exception("Passwordi se ne slažu");
                }

                entity.LozinkaSalt = GenerateSalt();
                entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, request.Password);
            }

        
            _mapper.Map(request, entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Models.Klijent>(entity);
        }

        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
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
