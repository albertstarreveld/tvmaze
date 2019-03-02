using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rtl.Domain;

namespace Rtl.Services
{
    public interface ITvGuideService
    {
        Task<IEnumerable<Show>> ListShows(int page, int pageSize);
    }

    public class TvGuideService : ITvGuideService
    {
        private readonly IShowRepository _repository;

        public TvGuideService(IShowRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Show>> ListShows(int page, int pageSize)
        {
            return await _repository.FindAll(pageSize, page);
        }
    }
}
