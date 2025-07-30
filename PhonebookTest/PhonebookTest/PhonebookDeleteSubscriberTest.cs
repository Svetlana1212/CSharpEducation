using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phonebook;

namespace PhonebookTest
{
    public class PhonebookDeleteSubscriberTest
    {
        private Phonebook.Phonebook manager;

        [SetUp]
        public void Setup()
        {
            manager = new Phonebook.Phonebook();
        }          
        
        [Test]
        public void DeleteSubscriberTest()
        {
            //Arrange
            PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
            List<PhoneNumber> numbers = new List<PhoneNumber>();
            numbers.Add(number);
            PhoneNumber number1 = new PhoneNumber("+7 950 555-4444", PhoneNumberType.Personal);
            List<PhoneNumber> numbers1 = new List<PhoneNumber>();
            numbers.Add(number1);
            Subscriber subscriber1 = new Subscriber("Alice", numbers);
            Subscriber subscriber2 = new Subscriber("Bob", numbers1);
            manager.AddSubscriber(subscriber1);
            manager.AddSubscriber(subscriber2);

            //Act
            manager.DeleteSubscriber(subscriber1);
            var remainingSubscribers = manager.GetAll();

            //Assept
            Assert.AreEqual(1, remainingSubscribers.Count());
            Assert.AreEqual(remainingSubscribers.ElementAt(0), subscriber2);
        }

        [Test]
        public void DeleteInvalidSubscriberTest()
        {
            //Arrange
            PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
            List<PhoneNumber> numbers = new List<PhoneNumber>();
            numbers.Add(number);
            PhoneNumber number1 = new PhoneNumber("+7 950 555-4444", PhoneNumberType.Personal);
            List<PhoneNumber> numbers1 = new List<PhoneNumber>();
            numbers.Add(number1);
            Subscriber subscriber1 = new Subscriber("Alice", numbers);
            Subscriber subscriber2 = new Subscriber("Bob", numbers1);
            manager.AddSubscriber(subscriber1);
            manager.AddSubscriber(subscriber2);

            //Act
            manager.DeleteSubscriber(subscriber1);
            
            //Удаление несуществующего пользователя
            manager.DeleteSubscriber(subscriber1);
            var remainingSubscribers = manager.GetAll();

            //Assept
            Assert.AreEqual(1, remainingSubscribers.Count());
            Assert.AreEqual(remainingSubscribers.ElementAt(0), subscriber2);
        }

    }   

    
}
