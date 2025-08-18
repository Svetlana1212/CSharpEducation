using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class WorkTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime СreationDate 
        { 
            get 
            { 
                return DateTime.Today;
            }
            set { }
        }
        public string Status { get; set; }              
        public string Priority { get; set; }
        public List<User> Responsible = new List<User>();
        private string v1;
        private string v2;
        private DateTime dateTime;

        public List<int> Comments { get; set; }  
        //public double Progress {  get; set; }
        public static int DifficultyFactor { get; set; }
        public int CalculatePenalty(int actualDays)
        {
            int plannedDays = Deadline.Day - СreationDate.Day;
            int delayDays = (plannedDays >= actualDays) ? 0 : actualDays - plannedDays;                 
            return delayDays;
        }
        public static int CalculateEfficiencyBall (int penalty)
        {            
            return DifficultyFactor/penalty;
        }
        public WorkTask(int id, string name, string description, DateTime deadline) 
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Deadline = deadline;
        }

        public WorkTask(string v1, string v2, DateTime dateTime)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.dateTime = dateTime;
        }
    }
}
