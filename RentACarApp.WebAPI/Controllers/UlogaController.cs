using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentACarApp.Model.Models;
using RentACarApp.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentACarApp.WebAPI.Controllers
{

    public class UlogaController : BaseController<Model.Models.Uloge, object>
    {
        public UlogaController(IService<Uloge, object> service) : base(service)
        {
        }
    }
}