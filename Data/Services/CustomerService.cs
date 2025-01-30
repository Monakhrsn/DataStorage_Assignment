using System.Linq.Expressions;
using Data.Dtos;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Data.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    
    // CREATE
    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var customer = await GetCustomerEntityAsync(x => x.Email == form.CustomerEmail);
        if (customer != null)
            return false;

        customer = new CustomerEntity()
        {
            Name = form.CustomerName,
            Email = form.CustomerEmail
        };
        
        var result = await _customerRepository.CreateAsync(customer);
        return result;
     }

    // READ
    public async Task<IEnumerable<Customer>?> GetCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.Select(x => new Customer(x.Id, x.Name, x.Email));
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customer = await GetCustomerEntityAsync(x => x.Id == id);
        return customer != null
            ? new Customer(customer.Id, customer.Name, customer.Email)
            : null;
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        var customer = await GetCustomerEntityAsync(x => x.Email == email);
        return customer != null
            ? new Customer(customer.Id, customer.Name, customer.Email)
            : null;
    }
    
    // UPDATE
    public async Task<Customer?> UpdateCustomerAsync(CustomerUpdateForm form)
    {
        var customer = await GetCustomerEntityAsync(x => x.Id == form.CustomerId);
        if (customer == null)
            return null;
        
        customer.Name = form.CustomerName;
        customer.Email = form.CustomerEmail;

        await _customerRepository.UpdateOneAsync(customer);
        
        customer =  await _customerRepository.GetOneAsync(x => x.Id == form.CustomerId);
        return customer !=null
            ? new Customer(customer.Id, customer.Name, customer.Email)
            : null;
    }
    
    // DELETE
    public async Task<bool> DeleteCustomerByIdAsync(int id)
    {
        var customer = await GetCustomerEntityAsync(x => x.Id == id);
        if (customer == null)
            return false;
        
        var result = await _customerRepository.DeleteOneAsync(x => x.Id == id);
        return result;
    }
    
    public async  Task<bool> DeleteCustomerByEmailAsync(string email)
    {
        var customer = await GetCustomerEntityAsync(x => x.Email == email);
        if (customer == null)
            return false;
        
        var result = await _customerRepository.DeleteOneAsync(x => x.Email == email);
        return result;
    }
    
    private async Task<CustomerEntity?> GetCustomerEntityAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        var customer = await _customerRepository.GetOneAsync(predicate);
        return customer;
    }
    
}