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
    public class DrzavaService : BaseCRUDService<Model.Models.Drzava, Model.Requests.DrzavaSearchRequest, Database.Drzava, Model.Requests.DrzavaUpsertRequest, Model.Requests.DrzavaUpsertRequest>
    {
        public DrzavaService(RentACarAppContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.Models.Drzava> Get(DrzavaSearchRequest search)
        {
            var query = _context.Set<Database.Drzava>().OrderBy(x=> x.Naziv).AsQueryable();

         
            if (search?.Naziv != null)
            {
                query = query.Where(x => x.Naziv == search.Naziv);
            }
            
           
            var list = query.ToList();

            return _mapper.Map<List<Model.Models.Drzava>>(list);
        }
    }
}
