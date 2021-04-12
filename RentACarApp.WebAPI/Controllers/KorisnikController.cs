using System.Collections.Generic;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentACarApp.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikService _service;
        public KorisnikController(IKorisnikService service)
        {
            _service = service;
        }

        //Može se i ukloniti [Authorize]
        //[Authorize(Roles = "Administrator,Menadžer,Uposlenik")]
        [HttpGet]
        public List<Korisnici> Get([FromQuery]KorisniciSearchRequest request)
        {
            return _service.Get(request);
        }

        //Može se i ukloniti [Authorize]
        [Authorize(Roles = "Administrator,Basic,Uposlenik")]
        [HttpPost]
        public Korisnici Insert(KorisniciUpsertRequest request)
        {
            return _service.Insert(request);
        }

        //Može se i ukloniti [Authorize]
        [Authorize(Roles = "Administrator,Basic,Uposlenik")]
        [HttpPut("{id}")]
        public Model.Models.Korisnici Update(int id, [FromBody]KorisniciUpsertRequest request)
        {
            var r= _service.Update(id, request);

            return r;
        }

        //Može se i ukloniti [Authorize]
        //[Authorize(Roles = "Administrator,Menadžer,Uposlenik")]
        [HttpGet("{id}")]
        public Model.Models.Korisnici GetById(int id)
        {
            return _service.GetById(id);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public Model.Models.Korisnici Delete(int id)
        {
            return _service.Delete(id);
        }

    }
}