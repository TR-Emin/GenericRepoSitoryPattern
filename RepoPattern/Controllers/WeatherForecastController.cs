using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Repository;

namespace RepoPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase 
    {

        //private readonly ILogger<WeatherForecastController> _logger;
        private IRepositoryWrapper _repoWrapper;
        public WeatherForecastController(IRepositoryWrapper repoWrapper)
        {
            //_logger = logger;
            _repoWrapper = repoWrapper;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            //_logger.LogInfo("Here is info message from the controller.");
            //_logger.LogDebug("Here is debug message from the controller.");
            //_logger.LogWarn("Here is warn message from the controller.");
            //_logger.LogError("Here is error message from the controller.");

           
            var domesticAccounts = _repoWrapper.Account.FindByCondition(x => x.AccountType.Equals("Domestic"));
            //var owners = _repoWrapper.Owner.FindAll();
            return new string[] { "value1", "value2" };
        }
    }
}
