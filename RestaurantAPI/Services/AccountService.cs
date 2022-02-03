using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountService(RestaurantDbContext context,IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;

        }

        public RestaurantDbContext Context { get; }

        public void RegisterUser(RegisterUserDto dto)
        {

            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nactionality = dto.Nationality,
                RoleId = dto.RoleId,
            };
            var hashPassword=_passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
