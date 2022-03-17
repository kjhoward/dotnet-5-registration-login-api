using System.Threading.Tasks;
using DevConsulting.Common.Models;
using DevConsulting.RegistrationLoginApi.Client;
using RegistrationLoginApi.Data.DataModels;
namespace RegistrationLoginApi.Data{
    public interface IRepository{
        public QueryResult<UserResource> GetUser(long Id);
        public QueryResult<UserResource> GetUser(string username);
        public QueryResult<UserResource> GetAllUsers();

        public Task AddUserAsync(UserResource user);
        public Task UpdateUserAsync(UserResource user);
        public Task RemoveUserAsync(UserResource user);
    }
}