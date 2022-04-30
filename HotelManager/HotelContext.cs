using HotelManager.DataModels;
using System.Collections.Generic;
using HotelManager;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HotelManager
{
    public class HotelContext
    {
        private readonly IMongoDatabase _database;
        public readonly IMongoCollection<HotelDataModel> HotelCollection;

        public HotelContext(IOptions<HotelDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.DatabaseName);
                HotelCollection = _database.GetCollection<HotelDataModel>(settings.Value.HotelsCollectionName);
            }
        }

        
    }
}