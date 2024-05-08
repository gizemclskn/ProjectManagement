using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetCustomer([FromBody] int customerId)
        {
            var customer = _unitOfWork.Customers.Get(customerId);
            return customer != null ? Ok(customer) : NotFound();
        }

        [HttpPost("create")]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Complete();
            return Created("", customer);
        }
        [HttpPost("update")]
        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            var existingCustomer = _unitOfWork.Customers.Get(customer.CustomerId);
            if (existingCustomer == null)
                return NotFound();

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Address = customer.Address;

            _unitOfWork.Complete();
            return NoContent();
        }
        [HttpPost("delete")]
        public IActionResult DeleteCustomer([FromBody] int customerId)
        {
            var customer = _unitOfWork.Customers.Get(customerId);
            if (customer == null)
                return NotFound();

            _unitOfWork.Customers.Remove(customer);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}
