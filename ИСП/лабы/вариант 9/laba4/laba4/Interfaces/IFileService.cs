using System.Collections.Generic;

namespace laba4.Interfaces
{
    interface IFileService<A>
    {
        IEnumerable<A> ReadFile(string fileName);
        void SaveData(IEnumerable<A> data, string fileName);
    }
}
