using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagement.Core.UnitOfWorks;
using ProjectManangment.Model;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("list")]
        public IActionResult ListCustomers()
        {
            var customers = _unitOfWork.Customers.GetAll();
            return Ok(customers);
        }
        [HttpPost("detail")]
        public async Task<IActionResult> GetCustomerDetail()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Dictionary<string, int>>(body);

                if (data == null || !data.ContainsKey("CustomerId"))
                {
                    return BadRequest("Customer ID is required.");
                }

                int customerId = data["CustomerId"];
                var customer = _unitOfWork.Customers.Get(customerId);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(body);

                if (customer == null || string.IsNullOrEmpty(customer.Name))
                {
                    return BadRequest("Invalid customer data.");
                }

                _unitOfWork.Customers.Add(customer);
                _unitOfWork.Complete();

                return Created("", customer);
            }
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateCustomer()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(body);

                if (customer == null || customer.CustomerId == 0)
                {
                    return BadRequest("Invalid customer data.");
                }

                var existingCustomer = _unitOfWork.Customers.Get(customer.CustomerId);

                if (existingCustomer == null)
                {
                    return NotFound();
                }

                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Address = customer.Address;

                _unitOfWork.Complete();

                return NoContent();
            }
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteCustomer()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<Dictionary<string, int>>(body);

                if (data == null || !data.ContainsKey("CustomerId"))
                {
                    return BadRequest("Customer ID is required.");
                }

                int customerId = data["CustomerId"];
                var customer = _unitOfWork.Customers.Get(customerId);

                if (customer == null)
                {
                    return NotFound();
                }

                _unitOfWork.Customers.Remove(customer);
                _unitOfWork.Complete();

                return NoContent();
            }
        }
        }
}
