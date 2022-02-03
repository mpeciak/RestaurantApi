using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext.Restaurants
             .Include(r => r.Address)
              .Include(d => d.Dishes)
              .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) return null;
            {
                var result = _mapper.Map<RestaurantDto>(restaurant);
                return result;
            }
        }
        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();
            var restaurantDto = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantDto;
        }
        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }
        public bool Delete(int id)
        {
            _logger.LogWarning($"Restaurant with id: {id} Delete action invoke");
            var restaurant = _dbContext.Restaurants
              .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) return false;
            {
                _dbContext.Restaurants.Remove(restaurant);
                _dbContext.SaveChanges();
                return true;
            }
        }
        public bool Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant = _dbContext.Restaurants
             .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) return false;
            {
                restaurant.Name = dto.Name;
                restaurant.Description = dto.Description;
                restaurant.HasDelivery = dto.HasDelivery;
            }
            return true;
        }
    }
}