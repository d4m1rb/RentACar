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
    public class RegistracijaVozilaService : BaseCRUDService<Model.Models.RegistracijaVozila, Model.Requests.RegistracijaVozilaSearchRequest, Database.RegistracijaVozila, Model.Requests.RegistracijaVozilaUpsertRequest, Model.Requests.RegistracijaVozilaUpsertRequest>
    {
        public RegistracijaVozilaService(RentACarAppContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.Models.RegistracijaVozila> Get(RegistracijaVozilaSearchRequest search)
        {

            var query = _context.Set<Database.RegistracijaVozila>().AsQueryable();

            if (search.AutomobilId > 0)
            {
                query = query.Where(x => x.AutomobilId == search.AutomobilId);
            }
            if (search.UposlenikId > 0)
            {
                query = query.Where(x => x.UposlenikId == search.UposlenikId);
            }
            if (search.DatumRegistracije.Year != 0001)
            {
                query = query.Where(x => x.DatumRegistracije == search.DatumRegistracije);
            }
            if (!string.IsNullOrWhiteSpace( search.Trajanje))
            {
                query = query.Where(x => x.Trajanje == search.Trajanje);
            }
            query = query.Where(x => x.Status == search.Status);


            var list = query.ToList();

            return _mapper.Map<List<Model.Models.RegistracijaVozila>>(list);
        }
    }
}
