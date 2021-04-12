using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace RentACarApp.WebAPI.Services
{
    public class KorisniciUlogeService : BaseService<Model.Models.KorisniciUloge, Model.Requests.KorisniciUlogeSearchRequest, Database.KorisniciUloge>
    {
        public KorisniciUlogeService(RentACarAppContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.Models.KorisniciUloge> Get(KorisniciUlogeSearchRequest search)
        {
            var query = _context.Set<Database.KorisniciUloge>().AsQueryable();


            if (search.KorisnikId > 0)
            {
                query = query.Where(x => x.KorisnikId == search.KorisnikId);
            }
            if (search.UlogaId > 0)
            {
                query = query.Where(x => x.UlogaId == search.UlogaId);
            }

            var list = query.Include(x=> x.Uloga).ToList();

            return _mapper.Map<List<Model.Models.KorisniciUloge>>(list);
        }
    }
}
