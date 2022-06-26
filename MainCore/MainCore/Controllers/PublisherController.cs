using MainCore.Services.Publisher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        #region Fields

        private readonly PublisherService _publisherService;


        #endregion

        #region Methods


        /// <summary>
        /// multiply number by 2
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [Route("Multiply")]
        public async Task<JsonResult> Get(int number)
        {
            var result = await _publisherService.Publish(number);
            return new JsonResult(result);
        }

        #endregion

        #region Utilities



        #endregion

        #region Ctor

        public PublisherController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }


        #endregion
    }
}
