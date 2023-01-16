using System;

namespace laba5.Domain
{
    [Serializable]
    public class Computer
    { 
        public Computer() { }
        public Monitor monitor { get; set; }
        public Computer(Monitor monitor) => this.monitor = monitor;
        public Computer(string Name) => this.monitor = new(Name);
    }
}
