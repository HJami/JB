namespace JB.models
{
    public class JobBoardDatabaseSettings : IJobBoardDatabaseSettings
    {
        public string JobsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IJobBoardDatabaseSettings
    {
        string JobsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}    