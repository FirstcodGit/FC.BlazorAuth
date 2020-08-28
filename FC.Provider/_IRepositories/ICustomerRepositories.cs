
using System.Threading.Tasks;
using FC.Provider.Models;
using FC.Provider.Providers;

namespace FC.Provider._IRepositories
{
    public interface ICustomerRepositories : IGenericRepositories<Customer>
    {
        Task<Customer> CustomerControl(string emailAddress, byte[] password);
    }
}
