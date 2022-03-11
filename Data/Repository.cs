using AutoMapper;
using System.Threading.Tasks;
using DevConsulting.RegistrationLoginApi.Models;
using RegistrationLoginApi.Data.DataModels;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RegistrationLoginApi.Data{
    public class Repository : IRepository{
        private readonly IMapper _mapper;
        private readonly IAppDbContext _dbContext;

        public Repository(IAppDbContext context, IMapper mapper){
            _dbContext = context;
            _mapper = mapper;
        }

        public  QueryResult<UserResource> GetUser(long Id){
            var query = _dbContext.Users.AsNoTracking().AsQueryable();
            query = query.Where(x=>x.Id == Id);

            var result = new QueryResult<UserResource>();
            result.TotalItems = query.Count();

            var items = query.ToList();
            result.Items = _mapper.Map<List<User>, List<UserResource>>(items);
            return result; 
        }

        public  QueryResult<UserResource> GetUser(string username){
            var query = _dbContext.Users.AsNoTracking().AsQueryable();
            query = query.Where(x=>x.Username == username);

            var result = new QueryResult<UserResource>();
            result.TotalItems = query.Count();

            var items = query.ToList();
            result.Items = _mapper.Map<List<User>, List<UserResource>>(items);
            return result; 
        }

        public  QueryResult<UserResource> GetAllUsers(){
            var query = _dbContext.Users.AsNoTracking().AsQueryable();

            var result = new QueryResult<UserResource>();
            result.TotalItems = query.Count();

            var items = query.ToList();
            result.Items = _mapper.Map<List<User>, List<UserResource>>(items);
            return result; 
        }

        public async Task AddUserAsync(UserResource user){
             var dbUser = _mapper.Map<UserResource, User>(user);
             _dbContext.Users.Add(dbUser);
             await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken());
        }

        public async Task UpdateUserAsync(UserResource user){
             var dbUser = _mapper.Map<UserResource, User>(user);
             _dbContext.Users.Update(dbUser);
             await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken());
        }

        public async Task RemoveUserAsync(UserResource user){
             var dbUser = _mapper.Map<UserResource, User>(user);
             _dbContext.Users.Remove(dbUser);
             await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken());
        }
    }
}