using CustomersApi.Interfaces;
using CustomersApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomer _customerService;
        public CustomersController(ICustomer customerService)
        {
            _customerService = customerService; 
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task Post([FromBody] Customer customer)
        {
            await _customerService.AddCustomer(customer);
        }

    }
}
