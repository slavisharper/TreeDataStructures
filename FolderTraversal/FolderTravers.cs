namespace FolderTraversal
{
    using System;
    using System.IO;

    public class FolderTravers
    {
        public static void Main(string[] args)
        {
            // For smaller count of printed lines. 
            string mainDirectory = @"C:\Windows\System32";
            DirectoryInfo windowsDir = new DirectoryInfo(mainDirectory);
            WalkDirectoryTree(windowsDir, 0);
        }

        /// <summary>
        /// Walks all subfolders and files and prints the exe files.First it gets all exe
        /// files in current directory. Then For every sub directory calls itself with 
        /// enlarged padding.
        /// </summary>
        /// <param name="dirRoot">Root to start from</param>
        /// <param name="spaces">Padding from the left site. Used for printing to show nesting</param>
        private static void WalkDirectoryTree(DirectoryInfo dirRoot, int spaces) 
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;
            int leftSpaces = spaces;

            try
            {
                files = dirRoot.GetFiles("*.exe");
                if (files != null)
                {
                    // If we add space on every call the printing fails.
                    leftSpaces += 3;
                    foreach (FileInfo file in files)
                    {
                        Console.WriteLine(string.Format("{0}{1}", new string(' ', leftSpaces), file.Name));
                    }
                }

                subDirs = dirRoot.GetDirectories();
            }
            catch (UnauthorizedAccessException e)
            {
            }

            if (subDirs != null)
            {
                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    WalkDirectoryTree(dirInfo, leftSpaces);
                }
            }
        }
    }
}
