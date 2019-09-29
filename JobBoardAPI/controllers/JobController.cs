using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using JB.services;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace JB.Controller

{

  [ApiController]
  public class JobController: ControllerBase
  {
    public IJobService JobService;
    public JobController(IJobService _jobService)
    {
        JobService = _jobService;
    }

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
        
        var obj = JobService.GetCSVPayload(csvJobs);

        JsonSerializer js = new JsonSerializer();
        StringBuilder json = new StringBuilder();
        var jr = new JsonTextWriter(new StringWriter(json));
        js.Serialize(jr, obj);
        
        return this.Content(json.ToString(), "application/json");
    }
  }
}