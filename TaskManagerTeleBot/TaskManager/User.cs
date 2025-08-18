using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public string Effectiveness {  get; set; }
        public string Email {  get; set; }
        public string JobTitle {  get; set; }
        public string Role {  get; set; }
        public List<string> Message { get; set; }
        public User (int id, string name,string surname,string email)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
        }        
    }
}
