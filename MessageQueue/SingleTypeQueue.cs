using System;
using System.Messaging;

namespace MQ
{
    public class SingleTypeQueue
    {
        public string QueueName { get; private set; }
        public bool DeleteExistingQueueWithSending { get; private set; }
        public bool DeleteExistingQueueWithReceiving { get; private set; }
        public bool Transactional { get; private set; }
        public string QueuePath { get; set; }
        public SingleTypeQueue(string queueName)
        {
            QueueName = queueName;
            QueuePath = $"{Environment.MachineName}\\private$\\{queueName}";
        }

        public SingleTypeQueue(string queueName,bool deleteExistingQueueWithSending, bool deleteExistingQueueWithReceiving, bool transactional)
        {
            QueueName = queueName;
            DeleteExistingQueueWithSending = deleteExistingQueueWithSending;
            DeleteExistingQueueWithReceiving = deleteExistingQueueWithReceiving;
            Transactional = transactional;
            QueuePath = $"{Environment.MachineName}\\private$\\{queueName}";
        }


        public void SendMessage<T>(T messageBody) where T:class
        {
            using (var mQueue = CreateNewMessageQueue(QueueName,DeleteExistingQueueWithSending,Transactional))
            {
                var message = new Message()
                {
                    Label = "test",
                    Body = messageBody,
                };
                message.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
                mQueue.Send(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="timeout">単位は秒</param>
        /// <returns></returns>
        public T ReceiveMessage<T>(int timeout) where T:class
        {

            using (var mQueue = CreateNewMessageQueue(QueueName,DeleteExistingQueueWithReceiving,Transactional))
            {
                var message = mQueue.Receive(new System.TimeSpan(0, 0, timeout));
                message.Formatter =new XmlMessageFormatter(new Type[] { typeof(T) });
                return message.Body as T;
            }
        }


        private MessageQueue CreateNewMessageQueue(string queueName,bool deleteExistingQueue, bool transactional)
        {
            return (new Func<MessageQueue>(() =>
            {
                if (MessageQueue.Exists(QueuePath))
                {
                    if (deleteExistingQueue)
                    {
                        MessageQueue.Delete(QueuePath);
                        return MessageQueue.Create(QueuePath, transactional: transactional);
                    }
                    else
                    {
                        return new MessageQueue(QueuePath);
                    }
                }
                else
                {
                    return MessageQueue.Create(QueuePath, transactional: transactional);
                }
            })());
        }

    }
}
