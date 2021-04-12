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

    public class RegistracijaVozilaController : BaseCRUDController<Model.Models.RegistracijaVozila, Model.Requests.RegistracijaVozilaSearchRequest, Model.Requests.RegistracijaVozilaUpsertRequest, Model.Requests.RegistracijaVozilaUpsertRequest>
    {
        public RegistracijaVozilaController(ICRUDService<RegistracijaVozila, RegistracijaVozilaSearchRequest, RegistracijaVozilaUpsertRequest, RegistracijaVozilaUpsertRequest> service) : base(service)
        {
        }
    }
}