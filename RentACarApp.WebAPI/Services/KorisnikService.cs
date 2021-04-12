using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RentACarApp.Model;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace RentACarApp.WebAPI.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly RentACarAppContext _context;
        private readonly IMapper _mapper;
        public KorisnikService(RentACarAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Model.Models.Korisnici Autentificiraj(string username, string password)
        {
            var user = _context.Korisnici.Include("KorisniciUloge.Uloga").FirstOrDefault(x => x.UserName == username);
            if(user != null)
            {
                var newHash = GenerateHash(user.LozinkaSalt, password);

                if(newHash== user.LozinkaHash)
                {
                    return _mapper.Map<Model.Models.Korisnici>(user);
                }
            }
            return null;
        }

        public List<Model.Models.Korisnici> Get(KorisniciSearchRequest search)
        {
            var query = _context.Set<Database.Korisnici>().AsQueryable();

            if (search.KorisnikId > 0)
            {
                query = query.Where(x => x.KorisnikId == search.KorisnikId);
            }
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
            if (!string.IsNullOrWhiteSpace(search?.LozinkaHash))
            {
                query = query.Where(x => x.LozinkaHash.ToLower() == search.LozinkaHash.ToLower());
            }
            if (!string.IsNullOrWhiteSpace(search?.UserName))
            {
                query = query.Where(x => x.UserName.ToLower()==search.UserName.ToLower());
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

            List<Database.Korisnici> korisnici=new List<Database.Korisnici>();
            if (search.uloge != null)
            {
                List<Database.KorisniciUloge> korisniciSaUlogama = new List<Database.KorisniciUloge>();

                foreach (var u in search.uloge)
                {
                    korisniciSaUlogama.AddRange(_context.KorisniciUloge.Include(y=>y.Uloga).Where(x => x.Uloga.Naziv==u).ToList());
                }
                foreach (var k in query.ToList())
                {
                    bool postoji = false;
                    foreach (var ku in korisniciSaUlogama)
                    {
                        if (k.KorisnikId == ku.KorisnikId)
                            korisnici.Add(k);
                    }

                }

            }

            else
            korisnici= query.ToList();
            
            List<Model.Models.Korisnici> result = _mapper.Map<List<Model.Models.Korisnici>>(korisnici);
            foreach (var item in result)
            {
                item.ImePrezime = item.Ime + " " + item.Prezime;

            }

            return result;
        }

        public Model.Models.Korisnici GetById(int Id)
        {
            var entity = _context.Korisnici.Find(Id);
                
            return _mapper.Map<Model.Models.Korisnici>(entity);
        }

        public Model.Models.Korisnici Delete(int Id)
        {
            var korisnik = _context.Korisnici.Find(Id);
            

            List<Database.KorisniciUloge> uloge = _context.KorisniciUloge.Where(id => id.KorisnikId == Id).ToList();

            foreach (var x in uloge)
            {
                _context.KorisniciUloge.Remove(x);
            }

            _context.Korisnici.Remove(korisnik);
                        
            _context.SaveChanges();

            return _mapper.Map<Model.Models.Korisnici>(korisnik);
        }
        public Model.Models.Korisnici Insert(KorisniciUpsertRequest request)
        {
            var entity = _mapper.Map<Database.Korisnici>(request);

            if (request.Password != request.PasswordPotvrda)
            {
                throw new Exception("Passwordi se ne slažu");
            }

            entity.LozinkaSalt = GenerateSalt();
            entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, request.Password);

            _context.Korisnici.Add(entity);
            _context.SaveChanges();

            foreach (var uloga in request.Uloge)
            {
                Database.KorisniciUloge korisniciUloge = new Database.KorisniciUloge();
                Database.Uloge u = _context.Uloge.FirstOrDefault(x => x.UlogaId == uloga);
                korisniciUloge.UlogaId = u.UlogaId;
                korisniciUloge.KorisnikId = entity.KorisnikId;
                korisniciUloge.DatumIzmjene = DateTime.Now;
                _context.KorisniciUloge.Add(korisniciUloge);
            }
            _context.SaveChanges();

            return _mapper.Map<Model.Models.Korisnici>(entity);
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
        public Model.Models.Korisnici Update(int Id,KorisniciUpsertRequest request)
        {
            var entity = _context.Korisnici.Include(x=> x.KorisniciUloge).FirstOrDefault(x=>x.KorisnikId==Id);
            _context.Korisnici.Attach(entity);
            _context.Korisnici.Update(entity);
            request.KorisnikId = entity.KorisnikId;

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (request.Password != request.PasswordPotvrda)
                {
                    throw new Exception("Passwordi se ne slažu");
                }

                entity.LozinkaSalt = GenerateSalt();
                entity.LozinkaHash = GenerateHash(entity.LozinkaSalt, request.Password);
            }


            var trenutneUloge = _context.KorisniciUloge.Where(x => x.KorisnikId == entity.KorisnikId);

            foreach (var uloga in trenutneUloge)
            {
                bool postoji = false;
                List<int> sveSelectovane = request.Uloge;
                foreach (var odabrana in sveSelectovane)
                {
                    if (uloga.UlogaId == odabrana)
                        postoji = true;
                }
                if (!postoji)
                {
                    _context.KorisniciUloge.Remove(uloga);
                }

            }


            foreach (var uloga in request.Uloge)
            {
                var u = _context.KorisniciUloge.FirstOrDefault(x => x.UlogaId == uloga && x.KorisnikId == entity.KorisnikId);

                if (u == null)
                {
                    Database.KorisniciUloge korisniciUloge = new Database.KorisniciUloge();
                    Database.Uloge ul = _context.Uloge.FirstOrDefault(x => x.UlogaId == uloga);
                    korisniciUloge.UlogaId = ul.UlogaId;
                    korisniciUloge.KorisnikId = entity.KorisnikId;
                    korisniciUloge.DatumIzmjene = DateTime.Now;
                    _context.KorisniciUloge.Add(korisniciUloge);
                }
            }
            _context.SaveChanges();
            _mapper.Map(request, entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Models.Korisnici>(entity);
        }

        
        
    }
}
