using JB.models;
using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;

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

        public List<Job> GetAll()
        {
            return _jobs.Find((job) => true).ToList();
        }

        public Job Get(string id)
        {
            return _jobs.Find((job) => job.Id == id).SingleOrDefault();
        }

    }
}