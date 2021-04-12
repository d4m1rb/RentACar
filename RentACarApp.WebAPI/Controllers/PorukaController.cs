using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentACarApp.WebAPI.Controllers
{

    public class PorukaController : BaseCRUDController<Model.Models.Poruka, Model.Requests.PorukaSearchRequest, Model.Requests.PorukaUpsertRequest, Model.Requests.PorukaUpsertRequest>
    {
        public PorukaController(ICRUDService<Poruka, PorukaSearchRequest, PorukaUpsertRequest, PorukaUpsertRequest> service) : base(service)
        {
        }
    }
}