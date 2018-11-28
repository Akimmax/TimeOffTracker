using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TOT.Interfaces;
using TOT.Dto.Identity;
using TOT.Entities.IdentityEntities;

namespace TOT.Business.Services
{
    public class IdentityService : BaseService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager, IMapper mapper, IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var userQuery = _userManager.Users.Include(x => x.Position);
            var result = await userQuery.Select(x => mapper.Map<User, UserDTO>(x)).ToListAsync();
            var roles = await Task.WhenAll(result.Select(u => GetUserRolesAsync(u.Id)));

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Roles = roles[i];
            }

            return result;
        }

        public async Task RegisterAsync(UserDTO model)
        {
            var user = _mapper.Map<UserDTO, User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, model.Roles); // add role
            }
            else
                throw new AggregateException(result.Errors.Select(error => new Exception(error.Description)));
        }

        public async Task LogInAsync(string userName, string password, bool rememberMe)
        {
            User user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new Exception("The username or password provided is incorrect.");
            var currentUserRole = await _userManager.GetRolesAsync(user);
            if (user.Fired)
                throw new Exception("The user has been fired.");
            var result =
                await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
            if (!result.Succeeded)
                throw new Exception("The username or password provided is incorrect.");
        }

        public async Task<IEnumerable<User>> GetAllAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task LogoutAsync()
        {
            // delete cookies
            await _signInManager.SignOutAsync();
        }

        public async Task DeleteUserAsync(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if ("Admin" == user.UserName)
                    throw new Exception("Sorry, but you can`t delete admin.");

                user.Fired = true;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new Exception("Unexpected Error");
            }
        }

        public async Task<UserDTO> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userDTO = _mapper.Map<User, UserDTO>(user);
            userDTO.Roles = await GetUserRolesAsync(id);

            return userDTO;

        }

        public async Task<ICollection<string>> GetUserRolesAsync(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task UpdateAsync(UserDTO userNew)
        {
            User userOld = await _userManager.FindByIdAsync(userNew.Id);
            if (userOld == null)
                throw new ArgumentNullException(nameof(userOld));
            _mapper.Map(userNew, userOld);
            await _userManager.AddPasswordAsync(userOld, userNew.Password);
            var result = await _userManager.UpdateAsync(userOld);
            if (result.Succeeded == false)
            {
                throw new Exception("Some thing go wrong during update. Please try later");
            }

            var oldUserRoles = await GetUserRolesAsync(userOld.Id);
            await _userManager.RemoveFromRolesAsync(userOld, oldUserRoles);
            await _userManager.AddToRolesAsync(userOld, userNew.Roles);

        }

        public async Task UpdatePasswordAsync(UserUpdatePasswordDTO model)
        {
            User user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            var result = await _userManager.ChangePasswordAsync(user,model.Password,model.NewPassword);
            if (result.Succeeded == false)
            {
                throw new Exception("Some thing go wrong during password update. Please try again");
            }
        }
    }
}
