using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Services;

namespace RentACarApp.WebAPI.Controllers
{

    public class DrzavaController : BaseCRUDController<Model.Models.Drzava, Model.Requests.DrzavaSearchRequest, Model.Requests.DrzavaUpsertRequest, Model.Requests.DrzavaUpsertRequest>
    {
        public DrzavaController(ICRUDService<Drzava, DrzavaSearchRequest, DrzavaUpsertRequest, DrzavaUpsertRequest> service) : base(service)
        {
        }
    }
}