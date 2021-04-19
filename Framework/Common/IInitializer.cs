using System.Threading.Tasks;

namespace Framework.Common
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
