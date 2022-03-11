using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using RegistrationLoginApi.Authorization;
using RegistrationLoginApi.Data;
using RegistrationLoginApi.Data.DataModels;
using RegistrationLoginApi.Helpers;
using RegistrationLoginApi.Models.Users;
using System.Threading;
using System.Threading.Tasks;
using DevConsulting.RegistrationLoginApi.Models;

namespace RegistrationLoginApi.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<UserResource> GetAll();
        UserResource GetById(long id);
        Task Register(RegisterRequest model);
        Task Update(long id, UpdateRequest model);
        Task Delete(long id);
    }

    public class UserService : IUserService
    {
        private IRepository _repository;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(
            IRepository repo,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _repository = repo;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var userResult = _repository.GetUser(model.Username);
            var user = userResult.Items.FirstOrDefault();

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public IEnumerable<UserResource> GetAll()
        {
            return _repository.GetAllUsers().Items;
        }

        public UserResource GetById(long id)
        {
            return _repository.GetUser(id).Items.FirstOrDefault();
        }

        public async Task Register(RegisterRequest model)
        {
            // validate
            var userResult = _repository.GetUser(model.Username);
            var userVal = userResult.Items.FirstOrDefault();
            if (userVal!=null)
                throw new AppException("Username '" + model.Username + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<UserResource>(model);

            // hash password
            user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // save user
            await _repository.AddUserAsync(user);
        }

        public async Task Update(long id, UpdateRequest model)
        {
            var user = _repository.GetUser(id).Items.FirstOrDefault();
            var userResult = _repository.GetUser(model.Username);
            var userVal = userResult.Items.FirstOrDefault();
            // validate
            if (model.Username != user.Username && userVal != null)
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            await _repository.UpdateUserAsync(user);
        }

        public async Task Delete(long id)
        {
            var user = _repository.GetUser(id).Items.FirstOrDefault();
            await _repository.RemoveUserAsync(user);
        }
    }
}