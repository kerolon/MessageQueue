using System;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string queueName = "test_queue_name";
            var queue = new MQ.SingleTypeQueue(queueName);

            var message = queue.ReceiveMessage<string>(10);

            Console.WriteLine("キューからのメッセージ:" + message);

            
            Console.ReadLine();
        }
    }
}
