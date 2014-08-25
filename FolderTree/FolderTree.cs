namespace FolderTree
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class FolderTree
    {
        private Folder rootFolder;
        private string rootFolderDirectory;

        public FolderTree(string rootDirecotry)
        {
            this.rootFolder = new Folder(rootDirecotry);
            this.rootFolderDirectory = rootDirecotry;
            BuildTree(rootDirecotry);
        }

        public FolderTree(Folder rootFolder) 
        {
            this.rootFolder = rootFolder;
        }

        public string RootName { get { return this.rootFolder.Name; } }

        public FolderTree GetSubTree(string folderName)
        {
            Folder subTreeRoot = SearchForFolder(folderName, this.rootFolder);
            return new FolderTree(subTreeRoot);
        }
        
        public FolderTree GetSubTree(params string[] path) 
        {
            Folder folder = this.SearchFolderByPath(path);
            return new FolderTree(folder);
        }

        /// <summary>
        /// Calculates size of the root folder of the tree in bytes.
        /// </summary>
        /// <returns>The size.</returns>
        public long Size() 
        {
            long size = this.CalculateSize(0, this.rootFolder);
            return size;
        }

        private long CalculateSize(long size, Folder root) 
        {
            foreach (var file in root.Files)
            {
                size += file.Size;
            }

            foreach (var subFolder in root.ChildFolders)
            {
                CalculateSize(size, subFolder);
            }

            return size;
        }

        private Folder SearchForFolder(string folderName, Folder folder)
        {
            if (folder.Name == folderName) 
            {
                return folder;
            }

            foreach (Folder currFold in folder.ChildFolders)
            {
               var subFolder = SearchForFolder(folderName, currFold);
                if (subFolder != null) 
                {
                    return subFolder;
                }
            }

            return null;
        }

        private Folder SearchFolderByPath(params string[] folderPath) 
        {
            var currentFolder = this.rootFolder;

            for (int i = 0; i < folderPath.Length; i++)
            {
                var subFolders = currentFolder.ChildFolders;
                bool isFound = false;

                foreach (var folder in subFolders)
                {
                    if (folder.Name == folderPath[i])
                    {
                        currentFolder = folder;
                        break;
                    }
                }

                if (!isFound)
                {
                    throw new ArgumentException("Invalid folder path!");   
                }
            }

            return currentFolder;
        }

        private void BuildTree(string rootDirecotry)
        {
            DirectoryInfo rootDir = new DirectoryInfo(rootDirecotry);
            this.rootFolder.Name = rootDir.Name;
            GetFolderData(this.rootFolder, rootDir);
        }

        private void GetFolderData(Folder folder, DirectoryInfo dirRoot)
        {
            List<File> parsedFiles = new List<File>();
            List<Folder> parsedDirectories = new List<Folder>();

            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                files = ParseFiles(dirRoot, parsedFiles, files);
                subDirs = ParseFolders(dirRoot, parsedDirectories, subDirs);
            }
            catch (UnauthorizedAccessException e)
            {
            }

            folder.Files = parsedFiles.ToArray();
            folder.ChildFolders = parsedDirectories.ToArray();

            if (subDirs != null && subDirs.Length > 0)
            {
                for (int i = 0; i < subDirs.Length; i++)
                {
                    GetFolderData(parsedDirectories[i], subDirs[i]);
                }
            }
        }

        private static DirectoryInfo[] ParseFolders(
            DirectoryInfo dirRoot, List<Folder> parseDirectories, DirectoryInfo[] subDirs)
        {
            subDirs = dirRoot.GetDirectories();
            if (subDirs != null)
            {
                foreach (DirectoryInfo dir in subDirs)
                {
                    parseDirectories.Add(new Folder(dir.Name));
                }
            }
            return subDirs;
        }

        private static FileInfo[] ParseFiles(
            DirectoryInfo dirRoot, List<File> parsedFiles, FileInfo[] files)
        {
            files = dirRoot.GetFiles("*.*");
            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    parsedFiles.Add(new File(file.Name, file.Length));
                }
            }
            return files;
        }
    }
}
