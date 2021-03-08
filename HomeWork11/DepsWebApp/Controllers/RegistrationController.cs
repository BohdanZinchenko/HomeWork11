using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Registration controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        /// <summary>
        /// Register method, not implemented
        /// Work with ExceptionFilter
        /// </summary>
        /// <returns>NotImplementedException</returns>

        [HttpGet]
        [ExceptionFilter]
        public void Register()
        {
            throw new NotImplementedException();
        }
    }
}
