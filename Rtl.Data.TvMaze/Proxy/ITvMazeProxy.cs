using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;

namespace Rtl.Data.TvMaze.Proxy
{
    public interface ITvMazeProxy
    {
        Task<Announcement[]> GetShows(string country);
        Task<Character[]> GetCast(int showId);
    }
}
