using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Database;

namespace RentACarApp.WebAPI.Services
{
    public class RacunService : BaseCRUDService<Model.Models.Racun, Model.Requests.RacunSearchRequest, Database.Racun, Model.Requests.RacunUpsertRequest, Model.Requests.RacunUpsertRequest>
    {
        public RacunService(RentACarAppContext context, IMapper mapper) : base(context, mapper)
        {
        }

      
    }
}
