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
           //Метод не реализован
            /* private Phonebook.Phonebook manager;

            [SetUp]
            public void Setup()
            {
                manager = new Phonebook.Phonebook();
            }

            [Test]
            public void ContainsSubscriber_NullInput_ReturnsNull()
            {
                // Act
                bool? result = manager.ContainsSubscriber(null);

                //Assert
                Assert.IsNull(result);
            }

            [Test]
            public void ContainsSubscriber_SubscriberExists_ReturnsTrue()
            {
                // Arrange
                PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
                List<PhoneNumber> numbers = new List<PhoneNumber>();
                numbers.Add(number);
                Subscriber subscriber = new Subscriber ("Alice",numbers);                

                // Act
                var result = manager.ContainsSubscriber(subscriber);

                //Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void ContainsSubscriber_SubscriberDoesNotExist_ReturnsFalse()
            {
                // Arrange
                PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
                List<PhoneNumber> numbers = new List<PhoneNumber>();
                numbers.Add(number);
                var subscriber = new Subscriber ("Bob",numbers);

                //Act
                bool? result = manager.ContainsSubscriber(subscriber);

                //Assert
                Assert.IsFalse(result);
            }

            [Test]
            public void ContainsSubscriber_DifferentSubscriberWithSameId_ReturnsTrue()
            {
                //Arrange
                PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
                List<PhoneNumber> numbers = new List<PhoneNumber>();
                numbers.Add(number);
                var subscriber1 = new Subscriber(Guid.NewGuid(), "Charlie", numbers);
                manager.AddSubscriber(subscriber1);
                // Новый объект с таким же Id
                var subscriber2 = new Subscriber(subscriber1.Id, "Charlie Clone", numbers);
                
                //Act
                bool? result = manager.ContainsSubscriber(subscriber2);
                
                //Assert
                Assert.IsTrue(result);
            }*/
        }
    }
}
