namespace JB.models
{
    public class JobView
    {
        private string title;
        private string description;

        private Location location;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public Location Location
        {
            get { return location; }
            set { location = value; }
        }

    }

}