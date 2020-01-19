using System.Threading.Tasks;

namespace AgentPortal.Domain.Store
{
    public interface IImageStore
    {
        Task<bool> TryCreateContainer(string container);
    }
}