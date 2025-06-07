using System.Collections.Generic;
using Accounting.ViewModels.Customers;

namespace Accounting.DataLayer.Repositories
{
    public interface ICustomerRepository
    {
        List<ListCustomerViewModel> GetNameCustomers(string filter = "");
        List<Customers> GetAllCustomers();
        Customers GetCustomersById(int customerId);
        bool InsertCustomer(Customers customer);
        bool UpdateCustomer(Customers customer);
        bool DeleteCustomer(Customers customer);
        bool DeleteCustomer(int customerId);
        IEnumerable<Customers> GetCustomersByFilter(string parameter);
        int GetCustomerIdByName(string name);
        string GetCustomerNameById(int customerId);

    }
}
