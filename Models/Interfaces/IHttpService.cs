using System.Threading.Tasks;

namespace VFMDesctop.Models.Interfaces
{
    public interface IHttpService
    {
        Task<string> PostRequest(string url, string data);
    }
}