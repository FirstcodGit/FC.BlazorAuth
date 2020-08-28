using System.Threading.Tasks;
using FC.Provider._IRepositories;

namespace FC.Provider
{
    public interface IUnitOfWork
    {
        ICustomerRepositories Customer { get; }

        Task SaveChangeAsync();
    }
}
