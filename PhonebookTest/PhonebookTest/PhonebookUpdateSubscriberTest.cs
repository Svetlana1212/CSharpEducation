using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phonebook;

namespace PhonebookTest
{
    public class PhonebookUpdateSubscriberTest
    {
        private Phonebook.Phonebook manager;

        [SetUp]
        public void Setup()
        {
            manager = new Phonebook.Phonebook();          
        }
        
        [Test]
        public void UpdateSubscriber_ValidSubscribers_UpdatesCorrectly()
        {
            // Arrange
            PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
            List<PhoneNumber> numbers = new List<PhoneNumber>();
            numbers.Add(number);
            PhoneNumber number1 = new PhoneNumber("+7 950 555-4444", PhoneNumberType.Personal);
            List<PhoneNumber> numbers1 = new List<PhoneNumber>();
            numbers1.Add(number1);
            Guid Id = Guid.NewGuid();

            var oldSubscriber = new Subscriber(Id, "John Doe",numbers);
            manager.AddSubscriber(oldSubscriber);
            var newSubscriber = new Subscriber(Id, "John Updated",numbers1);                        

            // Act
            manager.UpdateSubscriber(oldSubscriber, newSubscriber);

            // Assert
            var updatedSubscriber = manager.GetSubscriber(Id);
            Assert.AreEqual("John Updated", updatedSubscriber.Name);
        }

        /*[Test]        
        public void UpdateSubscriber_OldSubscriberNotFound_ThrowsException()
        {
            // Arrange
            PhoneNumber number = new PhoneNumber("+7 950 777-4444", PhoneNumberType.Personal);
            List<PhoneNumber> numbers = new List<PhoneNumber>();
            numbers.Add(number);
            PhoneNumber number1 = new PhoneNumber("+7 950 555-6666", PhoneNumberType.Personal);
            List<PhoneNumber> numbers1 = new List<PhoneNumber>();
            numbers1.Add(number1);
            Guid Id = Guid.NewGuid();
            // Абонент не существует
            var oldSubscriber = new Subscriber(Id, "Not Found",numbers);
            var newSubscriber = new Subscriber(oldSubscriber.Id, "New Name", numbers1);

            // Act
            manager.UpdateSubscriber(oldSubscriber, newSubscriber);

            //Assert
            var ex = Assert.Throws<InvalidOperationException>(() => manager.UpdateSubscriber(oldSubscriber, newSubscriber));
            Assert.That(ex.Message, Does.Contain("Элемент не найден"));          
        }*/

        
    }

    
}
