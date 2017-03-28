using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQ;
using Xunit;
using FakeItEasy;
using System.Messaging;

namespace MQTest
{
    public class Class1
    {


        [Fact]
        public void デフォルトコンストラクタで正しく初期化される事()
        {
            string name = "queue_name";

            var q = new MQ.SingleTypeQueue(name);

            Assert.Equal(name, q.QueueName);
            Assert.False(q.DeleteExistingQueueWithSending);
            Assert.False(q.DeleteExistingQueueWithReceiving);
            Assert.False(q.Transactional);
        }

        [Fact]
        public void コンストラクタで正しく初期化される事1()
        {
            string name = "queue_name1";

            var q1 = new MQ.SingleTypeQueue(name,true,false,false);

            Assert.Equal(name, q1.QueueName);
            Assert.True(q1.DeleteExistingQueueWithSending);
            Assert.False(q1.DeleteExistingQueueWithReceiving);
            Assert.False(q1.Transactional);
        }

        [Fact]
        public void コンストラクタで正しく初期化される事2()
        {
            string name = "queue_name2";

            var q2 = new MQ.SingleTypeQueue(name, false, true, false);

            Assert.Equal(name, q2.QueueName);
            Assert.False(q2.DeleteExistingQueueWithSending);
            Assert.True(q2.DeleteExistingQueueWithReceiving);
            Assert.False(q2.Transactional);

        }

        [Fact]
        public void コンストラクタで正しく初期化される事3()
        {
            string name = "queue_name3";

            var q3 = new MQ.SingleTypeQueue(name, false, false, true);

            Assert.Equal(name, q3.QueueName);
            Assert.False(q3.DeleteExistingQueueWithSending);
            Assert.False(q3.DeleteExistingQueueWithReceiving);
            Assert.True(q3.Transactional);
        }


    }
}
