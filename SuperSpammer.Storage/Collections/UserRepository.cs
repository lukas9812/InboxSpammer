using MongoDB.Driver;
using SuperSpammer.Common;
using SuperSpammer.Infastructure;
using SuperSpammer.Storage.Infrastructure;

namespace SuperSpammer.Storage.Collections;

public class UserRepository : IUserRepository
{
    public UserRepository(IMongoRepository repository)
    {
        _collection = repository.GetCollection<UserDto>("Users");
    }

    public async Task CreateAsync(UserDto user)
    {
        await _collection.InsertOneAsync(user);
    }

    public async Task<UserDto> GetByUsername(string username)
    {
        return await _collection.Find(x => x.Username == username).FirstOrDefaultAsync();
    }
    
    readonly IMongoCollection<UserDto> _collection;

}