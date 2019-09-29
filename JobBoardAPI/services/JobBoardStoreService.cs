using JB.models;
using MongoDB.Driver;

namespace JB.services
{
    public class JobBoardStoreService
    {
        private readonly IMongoCollection<Job> _jobs;

        public JobBoardStoreService(IJobBoardDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _jobs = database.GetCollection<Job>(settings.JobsCollectionName);
        }

        public Job Create(Job job)
        {
            _jobs.InsertOne(job);
            return job;
        }
    }
}