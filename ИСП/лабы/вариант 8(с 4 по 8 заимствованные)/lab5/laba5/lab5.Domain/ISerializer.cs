using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5.Domain
{
    public interface ISerializer
    {
        IEnumerable<Factory> DeSerializeByLINQ(string fileName);
        IEnumerable<Factory> DeSerializeXML(string fileName);
        IEnumerable<Factory> DeSerializeJSON(string fileName);
        void SerializeByLINQ(IEnumerable<Factory> factory, string fileName);
        void SerializeXML(IEnumerable<Factory> factory, string fileName);
        void SerializeJSON(IEnumerable<Factory> factory, string fileName);
    }
}
