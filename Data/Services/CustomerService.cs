using System.Linq.Expressions;
using Data.Dtos;
using Data.Entities;
using Data.Interfaces;

namespace Data.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    
    // CREATE
    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var customer = await GetCustomerEntityAsync(x => x.CustomerEmail == form.Email);
        if (customer != null)
            return false;

        customer = new CustomerEntity()
        {
            CustomerName = form.Name,
            CustomerEmail = form.Email
        };
        
        var result = await _customerRepository.CreateAsync(customer);
        return result;
     }

    // READ
    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.Select(x => new Customer(x.Id, x.CustomerName, x.CustomerEmail));
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customer = await GetCustomerEntityAsync(x => x.Id == id);
        return customer != null
            ? new Customer(customer.Id, customer.CustomerName, customer.CustomerEmail)
            : null;
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        Console.WriteLine($"Fetching customer by email: {email}");
        var customer = await GetCustomerEntityAsync(x => x.CustomerEmail == email);
        
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
        }
        
        return customer != null
            ? new Customer(customer.Id, customer.CustomerName, customer.CustomerEmail)
            : null;
    }
    
    // UPDATE
    public async Task<Customer?> UpdateCustomerAsync(CustomerUpdateForm form)
    {
        var customer = await GetCustomerEntityAsync(x => x.Id == form.Id);
        if (customer == null)
            return null;       
        
        customer.CustomerName = form.Name;
        customer.CustomerEmail = form.Email;

        await _customerRepository.UpdateOneAsync(customer);
        
        customer =  await _customerRepository.GetOneAsync(x => x.Id == form.Id);
        return customer !=null
            ? new Customer(customer.Id, customer.CustomerName, customer.CustomerEmail)
            : null;
    }
    
    // DELETE
    public async Task<bool> DeleteCustomerByIdAsync(int id)
    {
        var customer = await GetCustomerEntityAsync(x => x.Id == id);
        if (customer == null)
            return false;
        
        var result = await _customerRepository.DeleteOneAsync(x => x.Id == customer.Id);
        return result;
    }
    
    public async  Task<bool> DeleteCustomerByEmailAsync(string email)
    {
        var customer = await GetCustomerEntityAsync(x => x.CustomerEmail == email);
        if (customer == null)
            return false;
        
        var result = await _customerRepository.DeleteOneAsync(x => x.CustomerEmail == email);
        return result;
    }
    
    private async Task<CustomerEntity?> GetCustomerEntityAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        var customer = await _customerRepository.GetOneAsync(predicate);
        return customer;
    }
    
}