using System;
using JB.models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace JB.Services
{
    
    public interface IJobService
    {
      List<Job> GetCSVPayload(string csvJobs);
    }
    
    public class JobService: IJobService
    {
        public JobService()
        {
            
        }

        public List<Job> GetCSVPayload(string csvJobs)
        {
            var lines = csvJobs.TrimEnd().Split('\n').ToList();
            var titles = new List<string> {"Title", "Description", "PostingDate", "Location", "Applicants"}; //lines[0].Split(',').ToList();
            var contents = lines.
                            Skip(1).
                            Select((s) => s.TrimEnd()).ToList();
                            

            var json = 
                contents.
                Select((line) => { 
                    if (line.EndsWith('"'))
                    {
                    var indx = line.Substring(0, line.Length - 1).LastIndexOf('"');
                    var lastCol = line.Substring(indx + 1, line.Length - indx - 2).Replace(',', ';');;
                    line = line.Remove(indx) + lastCol;
                    }
                    return line;
                }).        
                Select(
                (content) => {
                    var result = 
                        content.Split(',').
                        Select((item, i) => {
                            var res = "";
                            if (i < titles.Count - 1)
                                {
                                if (titles[i] == "PostingDate")
                                {
                                    item = DateTime.Parse(item).ToString("yyyy-MM-dd");
                                }
                                res = $"\"{titles[i]}\":\"{item}\"";
                                }
                                else
                                {
                                if (item.StartsWith('"')) {
                                    item = item.Remove(0);
                                };
                                if (item.EndsWith('"')) {
                                    item = item.Remove(item.Length - 1);
                                }
                                res = 
                                    item.Split(';').
                                        Distinct() .
                                        Select((name) => $"{{\"name\":\"{name}\"}}" ).
                                        Aggregate("_", (acc,_item) => acc == "_" ? _item : acc + "," + _item);
                                res = $"\"{titles[i]}\":[{res}]";
                                }
                                return res;
                            }).
                        Aggregate("_", (acc,_item) => (acc == "_") ? _item : acc + "," + _item);
                        return $"{{{result}}}";    
                    }
                ).
                Aggregate("_", (acc,_item) => (acc == "_") ? _item : acc + "," + _item);
                
                json = $"[{json}]";

                //json = "[{\"Title\":\"Ahmad\", \"PostingDate\":\"2019-09-29\", \"Applicants\": [{\"Name\":\"Ali\"}]}]";
                
                JsonSerializer js = new JsonSerializer();
                return js.Deserialize<List<Job>>(new JsonTextReader(new StringReader(json)));

        }
    }
}
