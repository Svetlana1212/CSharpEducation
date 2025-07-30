using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phonebook;

namespace PhonebookTest
{
    public class PhonebookRenameSubscriberTest
    {
        private Phonebook.Phonebook manager;

        [SetUp]
        public void Setup()
        {
            manager = new Phonebook.Phonebook();          
        }        

        [Test]
        public void RenameSubscriber_ShouldUpdateName()
        {
            // Arrange
            PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
            var subscriber = new Subscriber(Guid.NewGuid(), "John Doe", new List<PhoneNumber> { number});
            
            // Act
            manager.RenameSubscriber(subscriber, "Jane Doe");
            Guid id = subscriber.Id;
            Subscriber updatedSubscriber = manager.GetSubscriber(id);

            // Assert
            Assert.Equals("Jane Doe", updatedSubscriber.Name);
            Assert.Equals(subscriber.Id, updatedSubscriber.Id);
            Assert.Equals(subscriber.PhoneNumbers, updatedSubscriber.PhoneNumbers);
        }        

        [Test]
        public void RenameSubscriber_ShouldKeepOriginalIdAndPhoneNumbers()
        {
            // Arrange
            PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
            var subscriber = new Subscriber("John Doe", new List<PhoneNumber> { number});
            Guid id = subscriber.Id;
            Subscriber updatedSubscriber = manager.GetSubscriber(id);

            // Act
            manager.RenameSubscriber(subscriber, "Jane Doe");

            // Assert
            Assert.Equals(subscriber.Id, updatedSubscriber.Id);
            Assert.Equals(subscriber.PhoneNumbers, updatedSubscriber.PhoneNumbers);
        }

        [Test]
        public void RenameSubscriber_ShouldNotModifyOriginalSubscriber()
        {
            // Arrange
            PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
            List<PhoneNumber> numbers = new List<PhoneNumber>();
            numbers.Add(number);
            var subscriber = new Subscriber(Guid.NewGuid(), "John Doe", new List<PhoneNumber>{ number });
            
            // Act
            manager.RenameSubscriber(subscriber, "Jane Doe");

            // Assert
            Assert.Equals("John Doe", subscriber.Name);

            //Assert.(subscriber.PhoneNumbers);
            Assert.Equals(numbers, subscriber.PhoneNumbers[0].Number);
        }
        
    }
}
