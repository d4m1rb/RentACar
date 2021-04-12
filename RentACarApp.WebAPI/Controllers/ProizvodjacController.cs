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

    public class ProizvodjacController : BaseCRUDController<Model.Models.Proizvodjac, Model.Requests.ProizvodjacSearchRequest, Model.Requests.ProizvodjacUpsertRequest, Model.Requests.ProizvodjacUpsertRequest>
    {
        public ProizvodjacController(ICRUDService<Proizvodjac, ProizvodjacSearchRequest, ProizvodjacUpsertRequest, ProizvodjacUpsertRequest> service) : base(service)
        {
        }
    }
}