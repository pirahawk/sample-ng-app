using System.Threading.Tasks;

namespace AgentPortal.Domain.Store
{
    public interface IImageStore
    {
        Task<bool> TryCreateContainer(string container);
        Task<string> PersistArticleEntryMedia(string imageFileName, byte[] mediaContent, string contentType);
    }
}