using System.Collections.Generic;
using System.Threading.Tasks;
using Rtl.Domain;

namespace Rtl.Services
{
    public interface IAnnouncementRepository
    {
        Task Save(IEnumerable<Announcement> announcements);
    }
}