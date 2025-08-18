using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class Comment
    {
        public int Id { get; set; }        
        public string Description { get; set; }
        public string Author { get; set; }
        public int TaskId {  get; set; }
        public Comment (string description, string author,int taskId)
        {
            this.Description = description;
            this.Author = author;
            this.TaskId = taskId;
        }
    }
}
