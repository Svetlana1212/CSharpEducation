using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Phonebook;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PhonebookTest
{
    public class AddNumberToSubscriberTest
    {
        public class PhonebookAddSubscriberTest
        {
            private Phonebook.Phonebook manager;

            [SetUp]
            public void Setup()
            {
                manager = new Phonebook.Phonebook();
            }

            [Test]
            public void AddNumberToSubscriber_ShouldAddNewNumber()
            {
                // Arrange
                PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
                var subscriber = new Subscriber("John Doe", new List<PhoneNumber> { number });
                PhoneNumber newNumber = new PhoneNumber("+7 950 333-5555", PhoneNumberType.Personal);

                // Act
                manager.AddNumberToSubscriber(subscriber, newNumber);

                // Assert
                Subscriber updateSubscriber = manager.GetSubscriber(subscriber.Id);
                Assert.AreEqual(updateSubscriber.PhoneNumbers.Count(), 2);
                Assert.AreEqual(updateSubscriber.PhoneNumbers.ElementAt(1), newNumber);

            }

            [Test]
            public void AddNumberToSubscriber_ShouldNotModifyOriginalSubscriber()
            {
                // Arrange
                PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
                var subscriber = new Subscriber("John Doe", new List<PhoneNumber> { number });
                PhoneNumber newNumber = new PhoneNumber("+7 950 333-5555", PhoneNumberType.Personal);
                List<PhoneNumber> numbers = new List<PhoneNumber>();
                numbers.Add(newNumber);
                // Act
                manager.AddNumberToSubscriber(subscriber, newNumber);

                // Assert                
                Assert.Equals(newNumber, subscriber.PhoneNumbers.ElementAt(0).Number);
            }

            [Test]
            public void AddNumberToSubscriber_ShouldUpdateSubscriberWithSameId()
            {
                // Arrange
                PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
                var subscriber = new Subscriber("John Doe", new List<PhoneNumber> { number });
                PhoneNumber newNumber = new PhoneNumber("+7 950 333-5555", PhoneNumberType.Personal);
                
                // Act
                manager.AddNumberToSubscriber(subscriber, newNumber);

                // Assert
                Subscriber updatedSubscriber = manager.GetSubscriber(subscriber.Id);
                
                Assert.AreEqual(subscriber,updatedSubscriber);
            }
        }
    }
}
