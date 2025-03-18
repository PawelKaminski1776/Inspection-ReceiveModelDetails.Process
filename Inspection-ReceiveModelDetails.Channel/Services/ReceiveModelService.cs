using InspectionReceiveModelDetails.Channel.Services;
using InspectionReceiveModelDetails.Messages.Dtos;
using InspectionReceiveModelDetails.Channel.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InspectionReceiveModelDetails.Channel
{
    public class ReceiveModelDetailsService : MongoConnect
    {

        public ReceiveModelDetailsService(string ConnectionString) : base(ConnectionString)
        {
        }

        public async Task<List<BsonDocument>> GetAllModels(ReceiveModelDetailsRequest request)
        {
            try
            {
                var database = dbClient.GetDatabase("InspectionAppDatabase");
                var collection = database.GetCollection<BsonDocument>("Inspection_Models");

                var allModels = await collection.Find(Builders<BsonDocument>.Filter.Empty).ToListAsync();

                return allModels;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Retrieving All Models: {e.Message}");
                return new List<BsonDocument>(); 
            }
        }

        public async Task<string> CheckIfUserExists(string username)
        {
            var database = dbClient.GetDatabase("InspectionAppDatabase");
            var collection = database.GetCollection<BsonDocument>("Users");

            var filter = Builders<BsonDocument>.Filter.Eq("email",username);

            try
            {
                var userExists = await collection.Find(filter).AnyAsync();

                Console.WriteLine($"User Exists: {userExists}");

                return userExists ? "User Exists" : "User Not Found";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Checking For User: {e.Message}");
                return $"Error: {e.Message}";
            }
        }


    }
}