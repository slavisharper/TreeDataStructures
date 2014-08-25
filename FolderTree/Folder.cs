namespace FolderTree
{
    public class Folder
    {
        public Folder(string name, File[] files = null, Folder[] folders = null) 
        {
            this.Name = name;
            this.ChildFolders = folders;
            this.Files = files;
        }

        public string Name { get; set; }

        public File[] Files { get; set; }

        public Folder[] ChildFolders { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
