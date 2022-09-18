using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.API.UI.Application.DTOs;
using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookie.Core.Selfies.Infrastructures.Data;

namespace SelfieAWookie.API.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelfieController : ControllerBase
    {
        private readonly ISelfieRepository repository = null;
        public SelfieController(ISelfieRepository slf)
        {
            repository = slf;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //var model = Enumerable.Range(1, 10).Select(item => new Selfie() { Id = item });

            var model = this.repository.GetAll();

            var res = model.Select(item =>
            new SelfieResumeDTO() { Title = item.Title, WookieId = item.Wookie.Id, nbSelfiesFromWookie = (item.Wookie?.Selfies?.Count).GetValueOrDefault(0) })
                .ToList();


            return this.Ok(res);
        }

        [HttpPost]
        public IActionResult AddOne(SelfieDTO item)
        {
            IActionResult res = this.BadRequest();

            Selfie addedSelfie = this.repository.AddOne(new Selfie()
            {
                ImagePath = item.ImagePath,
                Title = item.Title
            });

            this.repository.UnitOfWork.SaveChanges();
            if(addedSelfie != null)
            {
                item.Id = addedSelfie.Id;
                res = this.Ok(item);
            }
            
            
            return res;
        }
    }
}
