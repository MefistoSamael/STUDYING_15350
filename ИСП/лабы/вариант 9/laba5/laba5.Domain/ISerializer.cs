using System.Collections.Generic;

namespace laba5.Domain
{
    public interface ISerializer
    {
        IEnumerable<Computer> DeSerializeByLINQ(string fileName);
        IEnumerable<Computer> DeSerializeXML(string fileName);
        IEnumerable<Computer> DeSerializeJSON(string fileName);
        void SerializeByLINQ(IEnumerable<Computer> data, string fileName);
        void SerializeXML(IEnumerable<Computer> data, string fileName);
        void SerializeJSON(IEnumerable<Computer> data, string fileName);
    }
}
