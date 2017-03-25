using System;
using System.Collections.Generic;
using System.Linq;
using SysIo = System.IO;

namespace GAIT.Utilities.SystemIoWrappers
{
    public class File : IFile
    {
        public bool Exists(string path)
        {
            return SysIo.File.Exists(path);
        }

        public void Create(string file)
        {
           using (SysIo.File.Create(file)) { };
        }

        public SysIo.FileStream OpenWrite(string file)
        {
            return SysIo.File.OpenWrite(file);
        }

        public void Copy(string source, string destination)
        {
            SysIo.File.Copy(source,destination, true);
        }
        public void WriteAllLines(string file, IEnumerable<string> lines )
        {
            SysIo.File.WriteAllLines(file, lines);
            
        }
        public string[] ReadAllLines(string path)
        {
            return SysIo.File.ReadAllLines(path);
        }

        public string ReadAllText(string path)
        {
            if (Exists(path))
            {
                return SysIo.File.ReadAllText(path);
            }
            return string.Empty;
        }

        public void WriteAllText(string path,string text)
        {
            SysIo.File.WriteAllText(path, text);
        }

        public DateTime GetLastWriteTime(string path)
        {
            return Exists(path) ? SysIo.File.GetLastWriteTimeUtc(path) : new DateTime();
        }

        public DateTime GetLastWriteTimeLocal(string path)
        {
            return Exists(path) ? SysIo.File.GetLastWriteTime(path) : new DateTime();
        }

        public Tuple<DateTime,DateTime> ModifiedDateTime(string path, params string[] excludes )
        {
            var files = SysIo.Directory.GetFiles(path)
                .Where(x => !excludes.Any(y => x.Equals(SysIo.Path.Combine(path,y), StringComparison.InvariantCultureIgnoreCase))).ToArray();
            return new Tuple<DateTime, DateTime>(files.Max(x => SysIo.File.GetLastWriteTimeUtc(x)), files.Max(x => SysIo.File.GetLastWriteTime(x)));
        }

        public SysIo.StreamWriter CreateText(string path)
        {
            return SysIo.File.CreateText(path);
        }

        public SysIo.StreamReader OpenText(string path)
        {
            return SysIo.File.OpenText(path);
        }
    }
}
