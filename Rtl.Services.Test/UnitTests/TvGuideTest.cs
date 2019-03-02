using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Rtl.Services.Test.UnitTests
{
    [TestClass]
    public class TvGuideTest
    {
        private readonly Fixture _fixture = new Fixture();

        [TestMethod]
        public async Task IfGetShowsCalled_ShouldInvokeFindAllOnRepository()
        {
            // arrange
            var page = _fixture.Create<int>();
            var pageSize = _fixture.Create<int>();

            var repositoryMock = new Mock<IShowRepository>();
            var unitUnderTest = new TvGuideService(repositoryMock.Object);

            // act
            await unitUnderTest.ListShows(page, pageSize);

            // assert
            repositoryMock
                .Verify(x => x.FindAll(It.Is<int>(a => a == pageSize), It.Is<int>(a => a == page)));
        }
    }
}
