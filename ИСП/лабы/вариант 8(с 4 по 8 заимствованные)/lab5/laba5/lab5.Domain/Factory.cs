using System.Runtime.Serialization;

namespace lab5.Domain
{
    [Serializable]
    public class Storage
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Roominess { get; set; }

        public Storage()
        {
            Name = "NoName";
            Address = "NoName";
            Roominess = 0;
        }

        public Storage(string name, string address, int roominess)
        {
            Name = name;
            Address= address;
            Roominess = roominess;
        }

        public override string ToString()
        {
            return $"Склад {Name}, адрес: {Address}, вместительность: {Roominess}";
        }
    }

    [Serializable]
    public class Factory
    {
        public List<Storage> Storages { get; set; }
        public string Name { get; set; }
        
        public Factory()
        {
            Storages = new List<Storage>();
            Name = "NoName";
        }

        public Factory(string name, List<Storage> storages)
        {
            Name = name;
            Storages = storages;
        }

        public void AllStorage()
        {
            foreach(Storage storage in Storages)
            {
                Console.WriteLine(storage);
            }
        }
    }
}