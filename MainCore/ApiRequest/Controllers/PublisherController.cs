using Infrastructure.Services.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace ApiRequest.Controllers
{
    /// <summary>
    /// controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        #region Fields

        private readonly IPublisherService _publisherService;


        #endregion

        #region Methods

        /// <summary>
        /// multiply number by 2
        /// </summary>
        /// <param name="number"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("Multiply/{number}")]
        public async Task<JsonResult> Get(int number, CancellationToken token)
        {
            var result = await _publisherService.Publish(number, token);
            return new JsonResult(result);
        }

        #endregion

        #region Utilities



        #endregion

        #region Ctor

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }


        #endregion
    }
}
