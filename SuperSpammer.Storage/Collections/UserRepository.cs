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

    public async Task Create(UserDto user)
    {
        await _collection.InsertOneAsync(user);
    }

    public async Task<UserDto> GetByUsername(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    readonly IMongoCollection<UserDto> _collection;

}