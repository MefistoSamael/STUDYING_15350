namespace LibraryLab8
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Employee()
        {
            Id = 0;
            Age = 0;
            Name = "NONAME";
        }

        public Employee(int id, int age, string name)
        {
            Id = id;
            Age = age;
            Name = name == null ? "NONAME" : name;
        }

        public override string ToString()
        {
            return "Работник: " + Name + "; Id: " + Id + "; Возраст " + Age;
        }
    }
}