using FoodStore.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [TypeFilter(typeof(ControllerLogger))]
    [TypeFilter(typeof(ActionLogger))]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
