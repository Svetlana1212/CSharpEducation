namespace Test
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Class2 Julia = new Class2 ();            
            Class3 Julia1 = new Class3 ();
            
            List<Class1> users = new List<Class1>();
            users.Add(Julia);
            users.Add(Julia1);
            //Console.WriteLine(users[0].Name);
            //Julia.SalaryType = Class1.SalaryType.HourlyRate;
            //using StreamWriter sw = File.CreateText(Path);
            foreach (var item in users)
            {
                //Console.WriteLine(item.Name);
                //string newSalary = salary.ToString("g");
                //sw.WriteLine($"{item.Id}|{item.Name}|{item.BaseSalary}|{item.Post}|{newSalary}");
            }

            Console.WriteLine(users[0].Class2.SalaryType.ToString());
        }
    }
}
