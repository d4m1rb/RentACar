using RentACarApp.Model.Requests;
using RentACarApp.WebAPI.Services;

namespace RentACarApp.WebAPI.Controllers
{

    public class GradController : BaseController<Model.Models.Grad, GradSearchRequest>
    {
        public GradController(IService<Model.Models.Grad, GradSearchRequest> service) : base(service)
        {
        }
    }
}