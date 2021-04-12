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
    public class ModelService : BaseCRUDService<Model.Models.ModelAutomobila, Model.Requests.ModelAutomobilaSearch, Database.Model, Model.Requests.ModelAutomobilaUpsertRequest, Model.Requests.ModelAutomobilaUpsertRequest>
    {
        public ModelService(RentACarAppContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<ModelAutomobila> Get(ModelAutomobilaSearch search)
        {
            var query = _context.Set<Database.Model>().OrderBy(x=>x.Naziv).AsQueryable();

            if (search.ModelId > 0)
            {
                query = query.Where(x => x.ModelId == search.ModelId);
            }
            if (search.ProizvodjacId > 0)
            {
                query = query.Where(x => x.ProizvodjacId == search.ProizvodjacId);
            }
            if (search?.Naziv != null)
            {
                query = query.Where(x => x.Naziv == search.Naziv);
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.Models.ModelAutomobila>>(list);
    }
    }
}
