using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SuperSpammer.Engine.Models;
using SuperSpammer.Infastructure;

namespace SuperSpammer.Storage;

public class MongoRepository : IMongoRepository
{
    public MongoRepository(IOptions<MongoDbSettings> settings)
    {
        _settings = settings.Value;
        var client = new MongoClient(_settings.ConnectionString);
        _database = client.GetDatabase(_settings.DatabaseName);
    }
    
    public IMongoCollection<T> GetCollection<T>(string name) 
        => _database.GetCollection<T>(name);
    
    async Task EnsureCollectionExists<T>(string name)
    {
        var collectionNames = await (await _database.ListCollectionNamesAsync()).ToListAsync();
        if (!collectionNames.Contains(name))
        {
            await _database.CreateCollectionAsync(name);
        }
    }
    
    readonly IMongoDatabase _database;
    readonly MongoDbSettings _settings;
}