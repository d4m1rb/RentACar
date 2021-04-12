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
    public class ProizvodjacService : BaseCRUDService<Model.Models.Proizvodjac, Model.Requests.ProizvodjacSearchRequest, Database.Proizvodjac, Model.Requests.ProizvodjacUpsertRequest, Model.Requests.ProizvodjacUpsertRequest>
    {
        public ProizvodjacService(RentACarAppContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.Models.Proizvodjac> Get(ProizvodjacSearchRequest search)
        {
            var query = _context.Set<Database.Proizvodjac>().OrderBy(x=> x.Naziv).AsQueryable();

         
            if (search?.Naziv != null)
            {
                query = query.Where(x => x.Naziv == search.Naziv);
            }
            if (search.ProizvodjacId > 0)
            {
                query = query.Where(x => x.ProizvodjacId == search.ProizvodjacId);
            }
           
            var list = query.ToList();

            return _mapper.Map<List<Model.Models.Proizvodjac>>(list);
        }
    }
}
