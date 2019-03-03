using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rtl.Data.TvMaze;
using Rtl.Data.TvMaze.Mapping;
using Rtl.Data.TvMaze.Proxy;

namespace Rtl.Services.Test.AcceptanceTests
{
    [TestClass]
    public class TvMazeDownloadServiceTest
    {
        private readonly Fixture _fixture = new Fixture();

        /* Given tvMaze announces a tv show
         *  When our database is synchronized with theirs
         *  Then the show details are stored in our database
         */
        [TestMethod]
        public async Task IfTvShowOnTvMaze_ShouldSendTvShowToDatabase()
        {
            // Arrange
            var announcement = _fixture.Create<Announcement>();
            
            // Act
            var actual = await Act(announcement);

            // Assert
            Assert.AreEqual(announcement.show.name, actual.Show.Name);
        }

        /* Given tvMaze announces a tv show
         *  When our database is synchronized with theirs
         *  Then the announcement will be stored in our database
         */
        [TestMethod]
        public async Task IfTvShowAnnounced_ShouldStoreAnnouncementInDatabase()
        {
            // Arrange
            var announcement = _fixture.Create<Announcement>();

            // Act
            var actual = await Act(announcement);

            // Assert
            Assert.IsNotNull(actual);
        }

        /* Given an actor plays a character in a particular tv show
         *  When our database is synchronized with theirs
         *  Then the actors name will be listed in the cast of the show
         */
        [TestMethod]
        public async Task IfAlbertPlaysLudoInTvShow_ShouldListAlbertInTvShowCast()
        {
            // Arrange
            var announcement = _fixture.Create<Announcement>();

            // Act
            var actual = await Act(announcement);

            // Assert
            var expectedNames = announcement.show.Cast
                .Select(x => x.person.name)
                .OrderBy(x => x);

            var actualNames = actual.Show.Cast
                .Select(x => x.Name)
                .OrderBy(x => x);

            Assert.IsTrue(expectedNames.SequenceEqual(actualNames));
        }


        private async Task<Domain.Announcement> Act(Announcement announcement)
        {
            // Arrange
            var proxyMock = new Mock<ITvMazeProxy>();
            proxyMock
                .Setup(x => x.GetShows(It.IsAny<string>()))
                .Returns(Task.FromResult(new[] { announcement }));

            proxyMock
                .Setup(x => x.GetCast(It.IsAny<int>()))
                .Returns(Task.FromResult(announcement.show.Cast.ToArray()));

            Domain.Announcement actual = null;
            var announcementRepositoryMock = new Mock<IAnnouncementRepository>();
            announcementRepositoryMock
                .Setup(x => x.Save(It.IsAny<IEnumerable<Domain.Announcement>>()))
                .Callback<IEnumerable<Domain.Announcement>>(x => actual = x.FirstOrDefault())
                .Returns(Task.CompletedTask);
            
            // Act
            var tvMazeService = new TvMazeDownloadService(new TvMazeRepository(proxyMock.Object), announcementRepositoryMock.Object);
            await tvMazeService.SynchronizeOurDatabase(_fixture.Create<string>());
            
            return actual;
        }

        public TvMazeDownloadServiceTest()
        {
            _fixture
                .Customize<Announcement>(x =>
                    x.With(v => v.airtime, () => _fixture.Create<DateTime>().ToString("HH:mm")));

            Mapper.Reset();
            Mapper.Initialize(cfg => {
                cfg.AddProfile<AnnouncementProfile>();
            });

        }
    }
}
