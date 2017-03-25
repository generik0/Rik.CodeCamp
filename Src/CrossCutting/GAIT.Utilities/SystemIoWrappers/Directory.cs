using System;
using SysIo = System.IO;

namespace GAIT.Utilities.SystemIoWrappers
{
    public class Directory : IDirectory
    {
        public bool Exists(string path)
        {
            return SysIo.Directory.Exists(path);
        }

        public SysIo.DirectoryInfo CreateDirectory(string path)
        {
            var dir = SysIo.Path.GetDirectoryName(path);
            if (dir == null)
            {
                throw new NullReferenceException("The path Directory is null");
            }
            return SysIo.Directory.CreateDirectory(dir);
        }

        public void Delete(string path, bool recursive=true)
        {
            if (Exists(path))
            {
                try
                {
                    SysIo.Directory.Delete(path, recursive);
                }
                catch
                {
                    ;
                }
            }
        }
    }
}
