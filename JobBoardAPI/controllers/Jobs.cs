using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace JB.Controller

{

[ApiController]
public class JobController: ControllerBase
{
   [HttpGet]
   [Route("Job/GetJobs")]
   public List<string> GetJobs()
   {
       return new List<string> {"Job1", "Job2"};
   }

   
   [Route("Job/ParseCSVJobs")]
   [HttpPost]
   public ActionResult ParseJobList()
   {
      var csvJobs = "";
      using (StreamReader sr = new StreamReader(Request.Body))
      {
        csvJobs = sr.ReadToEnd();
      }

      var lines = csvJobs.TrimEnd().Split('\n').ToList();
      var titles = new List<string> {"A", "B", "C", "D", "E"}; //lines[0].Split(',').ToList();
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

           return this.Content(json, "application/json");
      }
    }
}