using System.Threading.Tasks;
using MassRabit.Data;
using MassRabit.Models;
using MassTransit;

namespace MassRabit.Classes
{


    public class TestPublisher
    {
        private readonly IBus _bus;

        public TestPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task Publish(string message)
        {

            await _bus.Publish(new TestModel() { Title = message });
        }
    }
    public class TestConsumer : IConsumer<TestModel>
    {
        private readonly ApplicationDbContext _context;

        public TestConsumer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<TestModel> context)
        {
            await Task.Delay(3000);
            await _context.TestModels.AddAsync(new TestModel() { Title = context.Message.Title });
            await _context.SaveChangesAsync();
        }
    }
}
