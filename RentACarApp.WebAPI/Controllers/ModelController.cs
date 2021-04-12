using RentACarApp.WebAPI.Services;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;

namespace RentACarApp.WebAPI.Controllers
{

    public class ModelController : BaseCRUDController<ModelAutomobila, Model.Requests.ModelAutomobilaSearch, Model.Requests.ModelAutomobilaUpsertRequest, Model.Requests.ModelAutomobilaUpsertRequest>
    {
        public ModelController(ICRUDService<ModelAutomobila, ModelAutomobilaSearch, ModelAutomobilaUpsertRequest, ModelAutomobilaUpsertRequest> service) : base(service)
        {
        }
    }
}