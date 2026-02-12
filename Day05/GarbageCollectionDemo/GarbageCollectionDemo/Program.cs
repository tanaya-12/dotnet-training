using System;
using System.IO;

namespace GarbageCollectionDemo
{
    public class FileManager : IDisposable
    {
        private FileStream? _filestream;
        private bool _disposed = false;

        public void OpenFile(string path, FileMode mode)
        {
            _filestream = new FileStream(path, mode);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _filestream?.Dispose();
            }

            _disposed = true;
        }

        ~FileManager()
        {
            Dispose(false);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (FileManager manager = new FileManager())
            {
                manager.OpenFile("sample.txt", FileMode.OpenOrCreate);
                Console.WriteLine("File opened successfully.");
            }

            Console.WriteLine("Resources released.");
        }
    }
}
