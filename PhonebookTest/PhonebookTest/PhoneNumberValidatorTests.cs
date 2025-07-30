using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phonebook;

namespace PhonebookTest
{
    public class PhoneNumberValidatorTests
    {
        [Test]
        [TestCase("+1 (234) 567-8901")]     // valid US number
        [TestCase("+44 (123) 456-7890")]    // valid UK-ish number
        [TestCase("+380 (123) 456-7890")]   // valid Ukraine code
        [TestCase("+123 (999) 000-0000")]   // max 3 digit country code
        public void Validate_ValidNumber_DoesNotThrow(string validNumber)
        {
            PhoneNumber phoneNumber = new PhoneNumber(validNumber, PhoneNumberType.Personal);

            Assert.DoesNotThrow(() => PhoneNumberValidator.Validate(phoneNumber));
        }

        [Test]
        [TestCase("1 (234) 567-8901")]         // missing '+' sign
        [TestCase("+12 345 678-9012")]         // missing parentheses
        [TestCase("+123 (12) 345-6789")]       // area code less than 3 digits
        [TestCase("+123(456)789-0123")]        // no spaces at all
        [TestCase("+123 (456) 7890123")]       // missing dash
        [TestCase("++123 (456) 789-0123")]     // double '+'
        //[TestCase(null)]                       // null string
        [TestCase("")]                         // empty string
        [TestCase("   ")]                      // whitespace only
        public void Validate_InvalidNumber_ThrowsArgumentException(string invalidNumber)
        {
            PhoneNumber phoneNumber = new PhoneNumber(invalidNumber, PhoneNumberType.Personal);

            var ex = Assert.Throws<ArgumentException>(() => PhoneNumberValidator.Validate(phoneNumber));
            Assert.That(ex.Message, Is.EqualTo("Phone number is invalid"));
        }
    }
}
