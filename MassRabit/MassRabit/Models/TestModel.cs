using System.ComponentModel.DataAnnotations;

namespace MassRabit.Models
{
    public class TestModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
