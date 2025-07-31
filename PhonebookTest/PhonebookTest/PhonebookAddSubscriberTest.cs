using NUnit.Framework;
using Phonebook;

namespace PhonebookTest
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
        public void AddSubscriber_NewSubscriber_AddsSuccessfully()
        {
            // Arrange
            PhoneNumber number = new PhoneNumber ("+7 950 333-4444", PhoneNumberType.Personal);
            List<PhoneNumber> numbers = new List<PhoneNumber> ();
            numbers.Add(number);
            var subscriber = new Subscriber("John Doe",numbers);
            
            //Act
            manager.AddSubscriber(subscriber);

            //Assert
            IEnumerable<Subscriber> listSubscriber = manager.GetAll();
            Assert.AreEqual(listSubscriber.Count(),1);
        }

        [Test]
        public void AddSubscriber_ExistingSubscriber_ThrowsInvalidOperationException()
        {
            // Arrange
            PhoneNumber number = new PhoneNumber("+7 950 333-4444", PhoneNumberType.Personal);
            List<PhoneNumber> numbers = new List<PhoneNumber>();
            numbers.Add(number);
            var subscriber = new Subscriber("John Doe", numbers);

            //Act
            manager.AddSubscriber(subscriber);
            
            //Пользователь уже есть
            var ex = Assert.Throws<InvalidOperationException>(() => manager.AddSubscriber(subscriber));

            //Assert            
            Assert.That(ex.Message, Is.EqualTo("Unable to add subscriber. Subscriber exists"));
        }

       /*[Test]
        public void AddSubscriber_InvalidPhoneNumbers_ThrowsArgumentException()
        {
            // Arrange
            //Пустой телефонный номер
            PhoneNumber number = new PhoneNumber(string.Empty, PhoneNumberType.Personal);
            List<PhoneNumber> numbers = new List<PhoneNumber>();
            numbers.Add(number);
            var subscriber = new Subscriber("Bob Roy", numbers);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => manager.AddSubscriber(subscriber));
             
            //Assert
            Assert.That(ex.Message, Is.EqualTo("Invalid phone numbers list"));
        }*/
    
    }
}