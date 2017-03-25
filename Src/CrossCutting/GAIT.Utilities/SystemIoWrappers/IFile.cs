using System;
using System.Collections.Generic;
using System.IO;

namespace GAIT.Utilities.SystemIoWrappers
{
    public interface IFile
    {
        bool Exists(string path);
        void Create(string file);
        FileStream OpenWrite(string file);
        void Copy(string source, string destination);
        void WriteAllLines(string file, IEnumerable<string> lines);
        string[] ReadAllLines(string path);
        string ReadAllText(string path);
        void WriteAllText(string path, string text);
        DateTime GetLastWriteTime(string path);
        DateTime GetLastWriteTimeLocal(string path);
        Tuple<DateTime, DateTime> ModifiedDateTime(string path, params string[] excludes);
        StreamWriter CreateText(string path);
        StreamReader OpenText(string path);
    }
}