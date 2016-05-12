using System;
using System.IO;

namespace MassRenamer
{
    class PathFinder
    {
        static private string[] paths = Environment.GetEnvironmentVariable(
                "PATH", EnvironmentVariableTarget.Machine).Split(';');

        static public string FindProgram(string program)
        {
            foreach (string path in paths)
            {
                var full_path = Path.Combine(path, program);
                if (File.Exists(full_path.ToLower()))
                {
                    return full_path;
                }
            }
            return null;
        }
    }
}
