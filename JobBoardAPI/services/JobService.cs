using System;
using JB.models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace JB.services
{

    public interface IJobService
    {
        IEnumerable<Job> GetCSVPayload(string csvJobs);
        IEnumerable<JobView> GetJobs();
        
        Job GetJob(string id);
    }

    public class JobService : IJobService
    {

        public JobBoardStoreService JobBoardStoreService { get; set; }
        public JobService(JobBoardStoreService _jobBoardStoreService)
        {
            JobBoardStoreService = _jobBoardStoreService;
        }

        public IEnumerable<JobView> GetJobs()
        {
            return JobBoardStoreService.GetAll().
                   ToList().
                   Select((item) => 
                              new JobView {Title = item.Title, 
                                           Description = item.Description, 
                                           Location = item.Location});
        }

        public Job GetJob(string id)
        {
            return JobBoardStoreService.Get(id);
        }

        public IEnumerable<Job> GetCSVPayload(string csvJobs)
        {
            var lines = csvJobs.TrimEnd().Split('\n').ToList();
            var titles = new List<string> { "Title", "Description", "PostingDate", "Location", "Applicants" }; //lines[0].Split(',').ToList();
            var contents = lines.
                            Skip(1).
                            Select((s) => s.TrimEnd()).ToList();


            var json =
                contents.
                Select((line) =>
                {
                    if (line.EndsWith('"'))
                    {
                        var indx = line.Substring(0, line.Length - 1).LastIndexOf('"');
                        var lastCol = line.Substring(indx + 1, line.Length - indx - 2).Replace(',', ';'); ;
                        line = line.Remove(indx) + lastCol;
                    }
                    return line;
                }).
                Select(
                (content) =>
                {
                    var result =
                        content.Split(',').
                        Select((item, i) =>
                        {
                            var res = "";
                            if (i < titles.Count - 1)
                            {
                                if (titles[i] == "PostingDate")
                                {
                                    //item = DateTime.Parse(item).ToString("yyyy-MM-dd");
                                    item = new DateTime(int.Parse(item.Substring(6, 4)),
                                                        int.Parse(item.Substring(3, 2)),
                                                        int.Parse(item.Substring(0, 2)))
                                              .ToString("yyyy-MM-dd");
                                }
                                res = $"\"{titles[i]}\":\"{item}\"";
                            }
                            else
                            {
                                if (item.StartsWith('"'))
                                {
                                    item = item.Remove(0);
                                };
                                if (item.EndsWith('"'))
                                {
                                    item = item.Remove(item.Length - 1);
                                }
                                res =
                                    item.Split(';').
                                        Distinct().
                                        Select((name) => $"{{\"name\":\"{name}\"}}").
                                        Aggregate("_", (acc, _item) => acc == "_" ? _item : acc + "," + _item);
                                res = $"\"{titles[i]}\":[{res}]";
                            }
                            return res;
                        }).
                        Aggregate("_", (acc, _item) => (acc == "_") ? _item : acc + "," + _item);
                    return $"{{{result}}}";
                }
                ).
                Aggregate("_", (acc, _item) => (acc == "_") ? _item : acc + "," + _item);

            json = $"[{json}]";

            //json = "[{\"Title\":\"Ahmad\", \"PostingDate\":\"2019-09-29\", \"Applicants\": [{\"Name\":\"Ali\"}]}]";

            JsonSerializer js = new JsonSerializer();
            var objs = js.Deserialize<List<Job>>(new JsonTextReader(new StringReader(json)));

            foreach (var obj in objs)
            {
                JobBoardStoreService.Create(obj);
            }
            return objs;

        }
    }
}
