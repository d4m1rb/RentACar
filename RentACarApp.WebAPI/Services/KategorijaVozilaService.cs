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
    public class KategorijaVozilaService : BaseCRUDService<Model.Models.KategorijaVozila, Model.Requests.KategorijaSearchRequest, Database.KategorijaVozila, Model.Requests.KategorijaUpsertRequest, Model.Requests.KategorijaUpsertRequest>
    {
        public KategorijaVozilaService(RentACarAppContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.Models.KategorijaVozila> Get(KategorijaSearchRequest search)
        {
            var query = _context.Set<Database.KategorijaVozila>().OrderBy(x=>x.Naziv).AsQueryable();

            if (search.KategorijaId > 0)
            {
                query = query.Where(x => x.KategorijaId == search.KategorijaId);
            }            
            if (search?.Naziv != null)
            {
                query = query.Where(x => x.Naziv.ToLower() == search.Naziv.ToLower());
            }
            if (search?.Opis != null)
            {
                query = query.Where(x => x.Opis == search.Opis);
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.Models.KategorijaVozila>>(list);
    }
    }
}
