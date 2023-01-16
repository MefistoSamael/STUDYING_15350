namespace ClassLibrary
{
    public class Shooter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

        public Shooter()
        {
            Id = 0;
            Name = "";
            Score = 0;
        }

        public Shooter(int Id, string Name,int Score)
        {
            this.Id = Id;
            this.Name = Name;
            this.Score = Score;
        }

        public override string ToString()
        {
            return "Id: " + Id + "\tИмя: " + Name + "\tСчёт " + Score + '\n';
        }
    }
}
