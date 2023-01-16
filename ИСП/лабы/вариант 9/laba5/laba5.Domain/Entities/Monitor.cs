using System;

namespace laba5.Domain
{
    [Serializable]
    public class Monitor
    {
        public string Name { get; set; }
        public Monitor() { }
        public Monitor(string Name) => this.Name = Name;
    }
}