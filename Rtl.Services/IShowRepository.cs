using System.Collections.Generic;
using System.Threading.Tasks;
using Rtl.Domain;

namespace Rtl.Services
{
    public interface IShowRepository
    {
        Task<IEnumerable<Show>> FindAll(int pageSize, int page);
    }
}