using System.Threading.Tasks;
using Rtl.Domain;

namespace Rtl.Services
{
    public interface ITvMazeRepository
    {
        Task<Announcement[]> Get(string country);
    }
}