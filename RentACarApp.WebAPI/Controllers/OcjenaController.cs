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

    public class OcjenaController : BaseCRUDController<Ocjena, OcjenaSearchRequest, OcjenaUpsertRequest, OcjenaUpsertRequest>
    {
        public OcjenaController(ICRUDService<Ocjena, OcjenaSearchRequest, OcjenaUpsertRequest, OcjenaUpsertRequest> service) : base(service)
        {
        }
    }
}