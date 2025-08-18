using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManager.UserException;

namespace TaskManager
{
    public class CommentsManager :IDataСollection<Comment>
    {
        private const string Path = "comments.txt";      
        private static List<Comment> myComments = new List<Comment>();
        public static List<Comment> Comments
        {
            get
            {
                return myComments;
            }
            set
            {
                Comments = new List<Comment>();
            }
        }

        public bool Add(Comment comment)
        {
            comment.Id = Comments.Count()+1;
            Comments.Add(comment);
            WriteDown();
            return true;      
        }

        public Comment Search(int id)
        {
            Comment searchComment = Comments.Find(item => item.Id == id);
            if (searchComment == null)
            {
                throw new EmployeeNotFound("Комментарий не найден");
            }
            return searchComment;
        }

        
        public List<Comment> SearchTaskComment(int taskId)
        {
            List <Comment> searchComment = Comments.FindAll(item => item.TaskId == taskId);
            if (searchComment == null)
            {
                throw new EmployeeNotFound("Комментарий не найден");
            }
            return searchComment;
        }

        public bool Update(Comment comment,Dictionary <string,string> parametrs)
        {
            comment.Description = (parametrs["description"] != string.Empty) ? parametrs["description"] : comment.Description;
            WriteDown();
            return true;
        }

        public bool Delete(Comment comment)
        {
            Comments.Remove(comment);
            WriteDown();
            return true;
        }

        public static bool WriteDown()
        {
            using StreamWriter sw = new StreamWriter(Path);
            foreach (var comment in Comments)
            {
                sw.WriteLine($"id:{comment.Id},description:{comment.Description},author:{comment.Author},taskId:{comment.TaskId}");
            }
            return true;
        }

        public static bool CommentRead()
        {
            
            if (!File.Exists(Path))
                return false;
            string[] lines = File.ReadAllLines(Path);
            if (lines.Length >= 1)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    for (int i = 0; i < parts.Length; i++)
                    {
                        string[] item = parts[i].Split(':');
                        dictionary.Add(item[0], item[1]);
                    }                    
                    int Id = Int32.Parse(dictionary["id"]);
                    Comment comment = new Comment(dictionary["description"],dictionary["author"], Int32.Parse(dictionary["taskId"]));
                    comment.Id = Id;
                    comment.Author = dictionary["author"];
                    Comments.Add(comment);
                    dictionary.Clear();
                }

            }
            return true;
        }
        public CommentsManager()
        {
            CommentRead();
        }
    }
}
