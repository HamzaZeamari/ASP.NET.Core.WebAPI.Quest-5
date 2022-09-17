using Microsoft.AspNetCore.Mvc;
using Moq;
using SelfieAWookie.API.UI.Application.DTOs;
using SelfieAWookie.API.UI.Controllers;
using SelfieAWookie.Core.Selfies.Domain;

namespace SelfieAWookieTest
{
    public class SelfieControllerUnitTest
    {
        [Fact]
        public void ShouldReturnListOfSelfies()
        {

            //ARRANGE
            var expectedList = new List<Selfie>()
            {
                new Selfie() {Wookie = new Wookie()},
                new Selfie() {Wookie = new Wookie()}
            };

            var repositoryMock = new Mock<ISelfieRepository>();

            repositoryMock.Setup(item => item.GetAll()).Returns(expectedList);

            var controller = new SelfieController(repositoryMock.Object);

            //ACT

            var result = controller.Get();

            //ASSERT

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult okResult = result as OkObjectResult;
            Assert.IsType<List<SelfieResumeDTO>>(okResult.Value);
            Assert.NotNull(okResult.Value);

            List<SelfieResumeDTO> list = okResult.Value as List<SelfieResumeDTO>;
            Assert.True(list.Count == expectedList.Count);
            
        }
    }
}