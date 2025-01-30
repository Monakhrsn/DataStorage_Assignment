using Data.Dtos;
using Data.Services;

namespace Data.Interfaces;

public interface ICustomerService
{
    Task<bool> CreateCustomerAsync(CustomerRegistrationForm form);
    Task<bool> DeleteCustomerByEmailAsync(string email);
    Task<bool> DeleteCustomerByIdAsync(int id);
    Task<Customer?> GetCustomerByEmailAsync(string email);
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<IEnumerable<Customer>?> GetCustomersAsync();
    Task<Customer?> UpdateCustomerAsync(CustomerUpdateForm form);
}