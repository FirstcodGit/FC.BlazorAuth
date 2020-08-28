using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FC.Provider._IRepositories;
using FC.Provider.Models;
using FC.Provider.Providers;

namespace FC.Provider._Repositories
{
    public class CustomerRepositories : GenericRepositories<Customer>, ICustomerRepositories
    {
        public CustomerRepositories(DbContext context) 
            : base(context) { }

        private ApplicationDbContext _applicationDbContext => (ApplicationDbContext)_context;

        public async Task<Customer> CustomerControl(string emailAddress, byte[] password)
        {
            return await _applicationDbContext.Customers.SingleOrDefaultAsync(s => s.CustomerEmailAddress == emailAddress && s.CustomerPassword == password);
        }
    }
}
