using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JB.Controllers
{

    [Route("api/Jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        [HttpGet]
        public List<string> GetJobs()
        {
            return new List<string> { "Job1", "Job2" };
        }
    }
}