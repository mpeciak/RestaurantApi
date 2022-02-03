 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {

        private readonly IRestaurantService _restaurantService;
       
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        //Delete
        [HttpDelete("{id}")]
        public ActionResult Delete([FromBody] int id)
        {
            var isDelete = _restaurantService.Delete(id);
            if (isDelete)
            {
                return NoContent();
            }
            return NotFound(); 
        }
        //Create
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if(ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _restaurantService.Create(dto);
            return Created($"/api/restaurant/{id}",null);
        }
        //GetALL
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {

            var restaurantsDto = _restaurantService.GetAll();
            return Ok(restaurantsDto);
        }
        //Get
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);
            if(restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }
        //Edit
        [HttpPut("{id}")]
        public ActionResult<RestaurantDto> Update([FromBody] UpdateRestaurantDto dto,[FromRoute]int id)
        {
            if (!ModelState.IsValid)    
            {
                return BadRequest(ModelState);
            }
            var isUpdate = _restaurantService.Update(id, dto);
            if(!isUpdate)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
