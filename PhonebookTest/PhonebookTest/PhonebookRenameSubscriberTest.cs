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
            var subscriber = new Subscriber("John Doe", new List<PhoneNumber> { number});
            manager.AddSubscriber(subscriber);
            Subscriber updatedSubscriber = manager.GetSubscriber(subscriber.Id);

            // Act
            manager.RenameSubscriber(updatedSubscriber, "Jane Doe");

            // Assert
            Subscriber newSubscriber = manager.GetSubscriber(subscriber.Id);
            Assert.AreEqual("Jane Doe", newSubscriber.Name);
            Assert.AreEqual(subscriber.Id, updatedSubscriber.Id);            
            Assert.AreEqual(subscriber.PhoneNumbers[0].Number, updatedSubscriber.PhoneNumbers[0].Number);

        } 
      

        [Test]
        public void RenameSubscriber_ShouldNotModifyOriginalSubscriber()
        {
            // Arrange
            PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
            List<PhoneNumber> numbers = new List<PhoneNumber>();
            numbers.Add(number);
            var subscriber = new Subscriber("John Doe", new List<PhoneNumber>{ number });
            manager.AddSubscriber(subscriber);
            Subscriber updatedSubscriber = manager.GetSubscriber(subscriber.Id);

            // Act
            manager.RenameSubscriber(updatedSubscriber, "John Doe");

            // Assert
            Subscriber newSubscriber = manager.GetSubscriber(subscriber.Id);
            Assert.AreEqual("John Doe", newSubscriber.Name);

            //Assert.(subscriber.PhoneNumbers);
            Assert.AreEqual(numbers.ElementAt(0).Number, newSubscriber.PhoneNumbers[0].Number);
        }
        
    }
}
