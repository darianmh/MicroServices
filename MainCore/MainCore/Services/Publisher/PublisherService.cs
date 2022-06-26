using MainCore.Models;
using MassTransit;

namespace MainCore.Services.Publisher
{
    public class PublisherService
    {
        #region Fields

        private readonly IPublishEndpoint _publishEndpoint;

        #endregion

        #region Methods

        public async Task<bool> Publish(int number)
        {
            var model = new PublishMessage(number);
            await _publishEndpoint.Publish<PublishMessage>(model);
            return true;
        }

        #endregion

        #region Utilities



        #endregion

        #region Ctor

        public PublisherService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }


        #endregion

    }
}
