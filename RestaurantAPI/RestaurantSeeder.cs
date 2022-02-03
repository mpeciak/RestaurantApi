using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if(!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurant();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var role = new List<Role>()
            {
                new  Role()
                {
                    Name="Admin"
                },
                new Role()
                {
                    Name="User"
                },
                   new Role()
                {
                    Name="Menager"
                }
            };
            return role;

        }
        private IEnumerable<Restaurant> GetRestaurant()
        {
            var restaurant = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name="KFC",
                    Category="Fast Food",
                    Description=" zalozone w ameryce",
                    ContactEmail="mirek@mirek.pl",
                    ContactNumber="33445533",
                    HasDelivery=true,
                    Dishes =new List<Dish>()
                    {
                        new Dish()
                        {
                            Name="Chicker",
                            Price=8.4M,
                        },
                    },
                    Address=new Address()
                    {
                        City="Tarnow",
                        Street="Kollataja",
                        PostCode="33-100",
                    },
                },
                new Restaurant()
                {
                    Name="KC",
                    Category="Fast Fd",
                    Description=" zalozccccone w ameryce",
                    ContactEmail="mircssdek@mirek.pl",
                    ContactNumber="3cc3445533",
                    HasDelivery=true,
                    Dishes =new List<Dish>()
                    {
                        new Dish()
                        {
                            Name="Chiken",
                            Price=10M,
                        },
                    },
                    Address=new Address()
                    {
                        City="Tar",
                        Street="llataja",
                        PostCode="33-100",
                    },
                }
            };
            return restaurant;
        }
    }
}
