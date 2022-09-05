using Dapper;
using JobAdder.Users.API.Application.Database;
using JobAdder.Users.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace JobAdder.Users.API.Application.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateAsync(string email, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            const string sql = @"
SELECT * 
  FROM dbo.ClientUser 
 WHERE [Email] = @email
";

            using (var conn = _context.CreateConnection())
            {
                var user = await conn.QuerySingleOrDefaultAsync<User>(sql, new { email });

                if (user != null)
                {
                    var passwordHasher = new PasswordHasher<IdentityUser>();
                    var result = passwordHasher.VerifyHashedPassword(user, user.AuthPasswordHash, password);
                    switch (result)
                    {
                        case PasswordVerificationResult.Success:
                        case PasswordVerificationResult.SuccessRehashNeeded:
                            return true;
                        default:
                            return false;
                    }
                   
                }
            }

            return false;
        }
    }
}
