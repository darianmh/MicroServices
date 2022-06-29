namespace Domain.Models
{
    public class PublishMessage
    {
        public PublishMessage(int number)
        {
            Number = number;
        }

        public PublishMessage()
        {

        }

        public int Number { get; set; }
    }
}
