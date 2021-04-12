using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Services;

namespace RentACarApp.WebAPI.Controllers
{

    public class KorisniciUlogaController : BaseController<Model.Models.KorisniciUloge, Model.Requests.KorisniciUlogeSearchRequest>
    {
        public KorisniciUlogaController(IService<KorisniciUloge, KorisniciUlogeSearchRequest> service) : base(service)
        {
        }
    }
}