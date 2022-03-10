using Microsoft.EntityFrameworkCore;
using RegistrationLoginApi.Data.DataModels;
using System.Threading;
using System.Threading.Tasks;
namespace RegistrationLoginApi.Data{
    public interface IAppDbContext{
        public DbSet<User> Users {get;set;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}