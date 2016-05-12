using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MassRenamer
{
    public class EditList
    {
        // Editor
        private static string editor = PathFinder.FindProgram("gvim.exe");
        // Stores the original paths
        private string[] sources;
        // Stores the target paths
        private string[] output;
        // Path of the temporary file
        private string tempFile;

        /// <summary>
        /// Populates the temporary file with source paths.
        /// </summary>
        private void PopulateFile()
        {
            File.WriteAllLines(tempFile, sources, Encoding.UTF8);
        }

        /// <summary>
        /// Loads the output (target) paths from the temporary file.
        /// </summary>
        private void LoadOutput()
        {
            output = File.ReadAllLines(tempFile, Encoding.UTF8);
        }

        /// <summary>
        /// Starts an editor program passing it the temporary file path as 
        /// an argument. Edited list is reloaded after editor quits.
        /// </summary>
        public void Edit()
        {
            var editorProcess = Process.Start(editor, $"-f \"{tempFile}\"");
            editorProcess.WaitForExit();
            LoadOutput();
        }

        /// <summary>
        /// Renames all source paths to output. The paths that are renamed
        /// are printed out to console. The temporary file is deleted and 
        /// the tempFile property is set to null.
        /// </summary>
        public void Rename()
        {
            var pairs = Enumerable.Zip(sources, output, 
                (a, b) => new string[] { a, b });
            foreach (string[] pair in pairs)
            {
                var source = pair[0];
                var target = pair[1];
                if (source == target)
                {
                    Console.WriteLine($"{source} -x {target}");
                    continue;
                }
                File.Move(source, target);
                Console.WriteLine($"{source} -> {target}");
            }
            File.Delete(tempFile);
            tempFile = null;
        }

        public EditList(string[] paths)
        {
            sources = paths;
            tempFile = Path.GetTempFileName();
            PopulateFile();
        }   
    }
}
