using System.ComponentModel.DataAnnotations;

namespace RabbitMqTestConsumer.Models
{
    public class TestModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
