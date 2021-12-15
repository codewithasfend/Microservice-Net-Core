using CustomersApi.Models;

namespace CustomersApi.Interfaces
{
    public interface ICustomer
    {
        Task AddCustomer(Customer customer);
    }
}
