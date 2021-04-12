using AutoMapper;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarApp.WebAPI.Services
{
    public class OcjenaService : BaseCRUDService<Model.Models.Ocjena, OcjenaSearchRequest, Database.Ocjena, OcjenaUpsertRequest, OcjenaUpsertRequest>
    {
        public OcjenaService(RentACarAppContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.Models.Ocjena> Get(OcjenaSearchRequest search)
        {
            var query = _context.Set<Database.Ocjena>().Include(x=> x.RezervacijaRentanja).AsQueryable();

            if (search.VoziloId > 0)
            {
                query = query.Where(x => x.RezervacijaRentanja.AutomobilId == search.VoziloId);
            }

            if (search.RezervacijaRentanjaId > 0)
            {
                query = query.Where(x => x.RezervacijaRentanjaId == search.RezervacijaRentanjaId);
            }

            var list = query.OrderBy(x=>x.RezervacijaRentanja.KlijentId).ToList();

            List<Model.Models.Ocjena> result = _mapper.Map<List<Model.Models.Ocjena>>(list);
            foreach (var item in result)
            {
                var ocjena = _context.Ocjena.Include(y => y.RezervacijaRentanja).Where(x => x.OcjenaId == item.OcjenaId).FirstOrDefault();
                item.KlijentId = ocjena.RezervacijaRentanja.KlijentId;
                item.VoziloId = ocjena.RezervacijaRentanja.AutomobilId;
            }
            return result;
        }
    }
}
