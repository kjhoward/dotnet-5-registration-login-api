using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RegistrationLoginApi.Data.DataModels;

namespace RegistrationLoginApi.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}
        public DbSet<User> Users { get; set; }
    }
}