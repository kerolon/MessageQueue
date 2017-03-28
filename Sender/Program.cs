using MQ;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            string queueName = "test_queue_name";
            var queue = new SingleTypeQueue(queueName);

            queue.SendMessage("this_is_test_message","hogehoge");
        }
    }
}
