using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TOT.Interfaces;
using TOT.Dto.Identity;
using TOT.Entities.IdentityEntities;
using TOT.Dto.Identity.Models;

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

        public async Task<IEnumerable<UserDTO>> GetFilteredUsersAsync(UserFilterModel model)
        {
            if (model == null)
            {
                return await GetAllUsersAsync();
            }
            IQueryable<User> userQuery = _userManager.Users.Include(x => x.Position);
            if (!String.IsNullOrEmpty(model.Email))
            {
                userQuery = userQuery.Where(x => x.Email.Contains(model.Email));
            }
            if (!String.IsNullOrEmpty(model.Name))
            {
                userQuery = userQuery.Where(x => x.Name.Contains(model.Name));
            }
            if (!String.IsNullOrEmpty(model.Surname))
            {
                userQuery = userQuery.Where(x => x.Surname.Contains(model.Surname));
            }
            if (!String.IsNullOrEmpty(model.Patronymic))
            {
                userQuery = userQuery.Where(x => x.Patronymic.Contains(model.Patronymic));
            }
            if (model.toHireDate != null)
            {
                if (model.fromHireDate != null)
                {
                    userQuery = userQuery.Where(x =>
                        DateTime.Compare(x.HireDate, (DateTime)model.fromHireDate) >= 0 &&
                        DateTime.Compare(x.HireDate, (DateTime)model.toHireDate) <= 0);
                }
                else
                {
                    userQuery = userQuery.Where(x =>
                        DateTime.Compare(x.HireDate, (DateTime)model.toHireDate) <= 0);
                }
            }
            else
            {
                if (model.fromHireDate != null)
                {
                    userQuery = userQuery.Where(x =>
                        DateTime.Compare(x.HireDate, (DateTime)model.fromHireDate) >= 0);
                }
            }
            if (model.Fired != null)
            {
                userQuery = userQuery.Where(x => x.Fired == model.Fired);
            }
            if (model.Position!=null && model.Position.Any())
            {
                userQuery = userQuery.Where(x => model.Position.Any(c=>c == x.PositionId));
            }

            var result = await userQuery.Select(x => mapper.Map<User, UserDTO>(x)).ToListAsync();
            var roles = await Task.WhenAll(result.Select(u => GetUserRolesAsync(u.Id)));

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Roles = roles[i];
            }

            if (model.Roles != null && model.Roles.Any())
            {
                model.Roles.Remove(null);
                if (model.Roles.Count != 0)
                {
                    result = result.Where(x => model.Roles.Any(c => x.Roles.Any(z => z.Equals(c)))).ToList();
                }
            }

            return result;
        }

        public IEnumerable<UserDTO> SortUsers(IEnumerable<UserDTO> Users, UserSortState sortOrder = UserSortState.NameAsc)
        {
            switch (sortOrder)
            {
                case UserSortState.NameDesc:
                    Users = Users.OrderByDescending(s => s.Name);
                    break;

                case UserSortState.SurnameAsc:
                    Users = Users.OrderBy(s => s.Surname);
                    break;
                case UserSortState.SurnameDesc:
                    Users = Users.OrderByDescending(s => s.Surname);
                    break;

                case UserSortState.PatronymicAsc:
                    Users = Users.OrderBy(s => s.Patronymic);
                    break;
                case UserSortState.PatronymicDesc:
                    Users = Users.OrderByDescending(s => s.Patronymic);
                    break;

                case UserSortState.EmailAsc:
                    Users = Users.OrderBy(s => s.Email);
                    break;
                case UserSortState.EmailDesc:
                    Users = Users.OrderByDescending(s => s.Email);
                    break;

                case UserSortState.PositionAsc:
                    Users = Users.OrderBy(s => s.Position.Title);
                    break;
                case UserSortState.PositionDesc:
                    Users = Users.OrderByDescending(s => s.Position.Title);
                    break;

                case UserSortState.HireDateAsc:
                    Users = Users.OrderBy(s => s.HireDate);
                    break;
                case UserSortState.HireDateDesc:
                    Users = Users.OrderByDescending(s => s.HireDate);
                    break;

                case UserSortState.RolesAsc:
                    Users = Users.OrderBy(s => s.Roles);
                    break;
                case UserSortState.RolesDesc:
                    Users = Users.OrderByDescending(s => s.Roles);
                    break;

                case UserSortState.FiredAsc:
                    Users = Users.OrderBy(s => s.Fired);
                    break;
                case UserSortState.FiredDesc:
                    Users = Users.OrderByDescending(s => s.Fired);
                    break;

                default:
                    Users = Users.OrderBy(s => s.Name);
                    break;
            }

            return Users;
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
