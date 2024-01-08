using Microsoft.EntityFrameworkCore;
using rpglms.src.data;
using rpglms.src.models;

namespace rpglms.Auth
{
    public class AuthRepository(DatabaseContext context)
    {
        private readonly DatabaseContext _context = context;

        public async Task<AppUser> GetUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email) ?? throw new Exception("User not found");
        }

        public async Task<bool> SaveUser(AppUser user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePassword(AppUser user, string newPassword)
        {
            user.PasswordHash = newPassword; // You should hash the password before saving it
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }
        // Database operations related to authentication

    public async Task<AppUser> GetUserById(string id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("User not found");
    }

    public async Task<bool> DeleteUser(AppUser user)
    {
        _context.Users.Remove(user);
        return await _context.SaveChangesAsync() > 0;
    }

        public async Task<bool> UpdateUser(AppUser user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<AppUser>> GetUsers(int pageNumber, int pageSize)
        {
            return await _context.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}