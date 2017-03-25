using System.IO;

namespace GAIT.Utilities.SystemIoWrappers
{
    public interface IDirectory
    {
        DirectoryInfo CreateDirectory(string path);
        bool Exists(string path);
        void Delete(string path, bool recursive = true);
    }
}