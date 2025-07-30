using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phonebook;

namespace PhonebookTest
{
    public class PhonebookContainsSubscriberTest
    {

        [TestFixture]
        public class SubscriberManagerTests
        {
            private Phonebook.Phonebook manager;

            [SetUp]
            public void Setup()
            {
                manager = new Phonebook.Phonebook();
            }

            [Test]
            public void ContainsSubscriber_NullInput_ReturnsNull()
            {
                bool? result = manager.ContainsSubscriber(null);

                Assert.IsNull(result);
            }

            [Test]
            public void ContainsSubscriber_SubscriberExists_ReturnsTrue()
            {
                PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
                List<PhoneNumber> numbers = new List<PhoneNumber>();
                numbers.Add(number);
                var subscriber = new Subscriber (Guid.NewGuid(),"Alice",numbers);
                manager.AddSubscriber(subscriber);

                bool? result = manager.ContainsSubscriber(subscriber);

                Assert.IsTrue(result);
            }

            [Test]
            public void ContainsSubscriber_SubscriberDoesNotExist_ReturnsFalse()
            {
                PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
                List<PhoneNumber> numbers = new List<PhoneNumber>();
                numbers.Add(number);
                var subscriber = new Subscriber (Guid.NewGuid(), "Bob",numbers);
                bool? result = manager.ContainsSubscriber(subscriber);
                Assert.IsFalse(result);
            }

            [Test]
            public void ContainsSubscriber_DifferentSubscriberWithSameId_ReturnsTrue()
            {
                PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
                List<PhoneNumber> numbers = new List<PhoneNumber>();
                numbers.Add(number);
                var subscriber1 = new Subscriber(Guid.NewGuid(), "Charlie", numbers);
                manager.AddSubscriber(subscriber1);

                // Новый объект с таким же Id
                var subscriber2 = new Subscriber(subscriber1.Id, "Charlie Clone", numbers );
                bool? result = manager.ContainsSubscriber(subscriber2);
                Assert.IsTrue(result);
            }
        }
    }
}
