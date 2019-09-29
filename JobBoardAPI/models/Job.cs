using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JB.models
{
    public class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        private string title;
        private string description;
        private DateTime postingDate;

        private Location location;
        private List<Applicant> applicants;

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

        public DateTime PostingDate
        {
            get { return postingDate; }
            set { postingDate = value; }
        }

        public Location Location
        {
            get { return location; }
            set { location = value; }
        }

        public List<Applicant> Applicants
        {
            get { return applicants; }
            set { applicants = value; }
        }

    }

}