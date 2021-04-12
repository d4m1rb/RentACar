using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Services;

namespace RentACarApp.WebAPI.Controllers
{

    public class AutomobilController : BaseCRUDController<Automobil, AutomobilSearchRequest, AutomobiliUPSERTtRequest, AutomobiliUPSERTtRequest>
    {
        public AutomobilController(ICRUDService<Automobil, AutomobilSearchRequest, AutomobiliUPSERTtRequest, AutomobiliUPSERTtRequest> service) : base(service)
        {
        }
    }
}