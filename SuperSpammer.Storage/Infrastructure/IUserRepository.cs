using SuperSpammer.Common;

namespace SuperSpammer.Storage.Infrastructure;

public interface IUserRepository
{
    Task CreateAsync(UserDto user);
    Task<UserDto> GetByUsername(string username);
}