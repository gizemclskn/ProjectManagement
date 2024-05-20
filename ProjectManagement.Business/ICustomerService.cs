using ProjectManagement.Core.UnitOfWorks;
using ProjectManangment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Business
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _unitOfWork.Customers.GetAll();
        }

        public Customer GetCustomer(int id)
        {
            return _unitOfWork.Customers.Get(id);
        }

        public void CreateCustomer(Customer customer)
        {
            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Complete();
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = _unitOfWork.Customers.Get(customer.CustomerId);
            if (existingCustomer == null)
                throw new Exception("Customer not found");

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Address = customer.Address;

            _unitOfWork.Complete();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _unitOfWork.Customers.Get(id);
            if (customer == null)
                throw new Exception("Customer not found");

            _unitOfWork.Customers.Remove(customer);
            _unitOfWork.Complete();
        }
    }

}
