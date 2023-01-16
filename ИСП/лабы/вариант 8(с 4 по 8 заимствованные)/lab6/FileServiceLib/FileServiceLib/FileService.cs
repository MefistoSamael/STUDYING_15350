using System.Text.Json;
using lab6;

namespace FileServiceLib
{
    public class FileService<T> : IFileService<T> where T : class
    {
        public IEnumerable<T> ReadFile(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                List<T>? list = JsonSerializer.Deserialize<List<T>>(fs);
                if (list == null)
                    throw new Exception("Некорректный файл");
                return list;
            }
        }

        public void SaveData(IEnumerable<T> data, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                JsonSerializer.SerializeAsync(fs, data, options);
            }
        }
    }
}