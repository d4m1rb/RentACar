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
    public class RezervacijaRentanjaController : BaseCRUDController<Model.Models.RezervacijaRentanja, Model.Requests.RezervacijaRentanjaSearchRequest, Model.Requests.RezervacijaRentanjaUpsertRequest, Model.Requests.RezervacijaRentanjaUpsertRequest>
    {
        public RezervacijaRentanjaController(ICRUDService<RezervacijaRentanja, RezervacijaRentanjaSearchRequest, RezervacijaRentanjaUpsertRequest, RezervacijaRentanjaUpsertRequest> service) : base(service)
        {
        }
    }
}