using System.Collections.Generic;

namespace laba6
{
    public interface IFileService<A> where A:class
    {
        IEnumerable<A> ReadFile(string fileName);
        void SaveData(IEnumerable<A> data, string fileName);
    }
}
