using Microsoft.EntityFrameworkCore;
using RegistrationLoginApi.Data.DataModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace RegistrationLoginApi.Data{
    public interface IAppDbContext{
        public DbSet<User> Users {get;set;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry Entry(Object entity);
    }
}