using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.Web;
using Microsoft.Extensions.Options;


namespace RentACar.WebAplikacija.Services
{
  public interface IUserService
    {
        Task<Klijent> Authenticate(string username, string password);
        Task<Korisnici> AuthenticateKor(string username, string password);
        Task<IEnumerable<Klijent>> GetAll();
        Task<IEnumerable<Korisnici>> GetAllKor();
    }

    public class UserService : IUserService
    {
        private APIService _apiService = new APIService("Klijent");
        private APIService _korisnikService = new APIService("Korisnik");

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //private List<User> _users = new List<User>
        //{ 
        //    new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" } 
        //};

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
        public async Task<Klijent> Authenticate(string username, string password)
        {
            KlijentSearchRequest searchUserName = new KlijentSearchRequest()
            {
                UserName = username,
                Status = true
            };

            var korisnik = await _apiService.Get<List<Klijent>>(searchUserName);
            var k = korisnik.FirstOrDefault();

            string passwordHash = GenerateHash(k.LozinkaSalt, password);

            KlijentSearchRequest search = new KlijentSearchRequest()
            {
                UserName = username,
                LozinkaHash = passwordHash,
                Status=true
             };

            var user = await Task.Run(() => _apiService.Get<Klijent>(search));

            //var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            //user.LozinkaHash = null;
            
            return user;
        }

        public async Task<Korisnici> AuthenticateKor(string username, string password)
        {
            KorisniciSearchRequest searchUserName = new KorisniciSearchRequest()
            {
                UserName = username,
                Status = true
            };

            var korisnik = await _korisnikService.Get<List<Korisnici>>(searchUserName);
            var k = korisnik.FirstOrDefault();

            string passwordHash = GenerateHash(k.LozinkaSalt, password);

            KorisniciSearchRequest search = new KorisniciSearchRequest()
            {
                UserName = username,
                LozinkaHash = passwordHash,
                Status = true
            };

            var user = await Task.Run(() => _korisnikService.Get<Korisnici>(search));

            //var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            //user.LozinkaHash = null;

            return user;
        }

        public async Task<IEnumerable<Klijent>> GetAll()
        {
            KlijentSearchRequest bezPassworda = new KlijentSearchRequest() { /*LozinkaHash = null;*/ };
            // return users without passwords
            return await Task.Run(() => _apiService.Get<List<Klijent>>(bezPassworda).Result.Select(x => {
                x.LozinkaHash = null;
                return x;

            }));
        }

        public async Task<IEnumerable<Korisnici>> GetAllKor()
        {
            KorisniciSearchRequest bezPassworda = new KorisniciSearchRequest() { /*LozinkaHash = null;*/ };
            // return users without passwords
            return await Task.Run(() => _korisnikService.Get<List<Korisnici>>(bezPassworda).Result.Select(x => {
                x.LozinkaHash = null;
                return x;

            }));
        }
    }
}