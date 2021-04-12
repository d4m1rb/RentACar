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

    public class KategorijaVozilaController : BaseCRUDController<Model.Models.KategorijaVozila, Model.Requests.KategorijaSearchRequest, Model.Requests.KategorijaUpsertRequest, Model.Requests.KategorijaUpsertRequest>
    {
        public KategorijaVozilaController(ICRUDService<KategorijaVozila, KategorijaSearchRequest, KategorijaUpsertRequest, KategorijaUpsertRequest> service) : base(service)
        {
        }
    }
}