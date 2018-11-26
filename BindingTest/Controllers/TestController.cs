using BindingTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace BindingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // POST api/test
        [HttpPost]
        public MyComplexModel Post([FromBody] MyComplexModel model)
        {
            return model;
        }
    }
}