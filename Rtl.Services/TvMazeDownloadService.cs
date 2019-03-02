using System;
using System.Threading.Tasks;

namespace Rtl.Services
{
    public class TvMazeDownloadService
    {
        private readonly ITvMazeRepository _tvMazeRepository;
        private readonly IAnnouncementRepository _announcementRepository;

        public TvMazeDownloadService(ITvMazeRepository tvMazeRepository, IAnnouncementRepository announcementRepository)
        {
            _tvMazeRepository = tvMazeRepository;
            _announcementRepository = announcementRepository;
        }

        public async Task SynchronizeOurDatabase(string countryCode)
        {
            var announcements = await _tvMazeRepository.Get(countryCode);
            await _announcementRepository.Save(announcements);
        }
    }
}
