using JwtAuthAspNet7WebAPI.Core.Dtos;
using JwtAuthAspNet7WebAPI.Core.Entities;
using JwtAuthAspNet7WebAPI.Core.Interface;
using JwtAuthAspNet7WebAPI.Core.OtherObjects;
using Microsoft.AspNetCore.Identity;

namespace JwtAuthAspNet7WebAPI.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public Task<AuthServiceResponseDto> loginAsynch(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<AuthServiceResponseDto> MakeAdminAsynch(UpdatePermissionDto updatePermissionDto)
        {
            throw new NotImplementedException();
        }

        public Task<AuthServiceResponseDto> MakeOwnerAsynch(UpdatePermissionDto updatePermissionDto)
        {
            throw new NotImplementedException();
        }

        public Task<AuthServiceResponseDto> registerAsynch(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthServiceResponseDto> SeedRolesAsynch()
        {
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);

            if (isOwnerRoleExists && isAdminRoleExists && isUserRoleExists)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = true,
                    Message = "Roles seeding is already done"
                };


            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.USER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.OWNER));
            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "Roles seeding Done Successfully"
            };
        }
    }
}
