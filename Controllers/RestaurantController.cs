using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController: ControllerBase
    {
        private readonly IRestaurantServices _restaurantServices;

        public RestaurantController(IRestaurantServices restaurantServices)
        {
            _restaurantServices = restaurantServices;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin ,Manager")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {
            _restaurantServices.Update(id, dto);

            return Ok();

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin ,Manager")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantServices.Delete(id);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin ,Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var id = _restaurantServices.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpGet]
        [Authorize(Policy = "Atleast20")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll([FromQuery] RestaurantQuery query)
        {
            var restaurantsDtos = _restaurantServices.GetAll(query);
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantServices.GetById(id);

            return Ok(restaurant);
        }
    }
}
