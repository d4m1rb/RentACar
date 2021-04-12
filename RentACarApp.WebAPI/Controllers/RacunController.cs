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

    public class RacunController : BaseCRUDController<Model.Models.Racun, Model.Requests.RacunSearchRequest, Model.Requests.RacunUpsertRequest, Model.Requests.RacunUpsertRequest>
    {
        public RacunController(ICRUDService<Racun, RacunSearchRequest, RacunUpsertRequest, RacunUpsertRequest> service) : base(service)
        {
        }
    }
}