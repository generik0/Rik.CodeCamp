using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GAIT.Utilities.DI.Attributes;

namespace GAIT.Utilities
{
    [NoIoC]
    public static class ProjectMetadata
    {
        public static IEnumerable<string>  ProjectPrefixName = new [] { "GAIT.".ToLower(), "Rik".ToLower()} ;

        private static readonly IReadOnlyCollection<string> Paths = GetDirectories();

        private static string[] GetDirectories()
        {
            var path = Path.GetDirectoryName(new Uri(Assembly.GetCallingAssembly().CodeBase).LocalPath);
            if (path == null) return new string[0];
            var directories = Directory.GetDirectories(path, "*", SearchOption.AllDirectories).ToList();
            if (!directories.Contains(path))
            {
                directories.Add(path);
            }
            directories.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles), "Rik", "Rik.CodeCamp"));
            return directories.Distinct().OrderBy(x=>x.Length).ToArray();

        }

        public static IEnumerable<string> ProjectAssembiliesPaths => GetDistinctPaths();

        private static IEnumerable<string> GetDistinctPaths()
        {
            var paths = Paths.Distinct();
            return paths;
        }

        public static bool IsOutputted { get; set; }
    }
}
