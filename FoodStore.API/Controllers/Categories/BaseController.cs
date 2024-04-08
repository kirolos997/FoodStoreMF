using FoodStore.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers.Categories
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [TypeFilter(typeof(ControllerLogger))]
    [TypeFilter(typeof(ActionLogger))]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
