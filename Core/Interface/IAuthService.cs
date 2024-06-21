using JwtAuthAspNet7WebAPI.Core.Dtos;
using System.Data;

namespace JwtAuthAspNet7WebAPI.Core.Interface
{
    public interface IAuthService
    {
        Task<AuthServiceResponseDto> SeedRolesAsynch();
        Task<AuthServiceResponseDto> registerAsynch(RegisterDto registerDto);
        Task<AuthServiceResponseDto> loginAsynch(LoginDto loginDto);
        Task<AuthServiceResponseDto> MakeAdminAsynch(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> MakeOwnerAsynch(UpdatePermissionDto updatePermissionDto);


    }
}
