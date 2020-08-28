using System.Threading.Tasks;
using FC.Provider._IRepositories;
using FC.Provider._Repositories;

namespace FC.Provider
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        ICustomerRepositories _customerRepositories;
        public ICustomerRepositories Customer
        {
            get
            {
                if (_customerRepositories == null)
                    _customerRepositories = new CustomerRepositories(_context);

                return _customerRepositories;
            }
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
