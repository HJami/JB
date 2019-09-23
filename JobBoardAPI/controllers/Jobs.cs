using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


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
   public void ParseJobList(string csvJobs)
   {
      foreach (var obj in csvJobs.Split('\n'))
      {
        foreach (var item in obj.Split(','))    
        {

        }
      }
      
   }
}