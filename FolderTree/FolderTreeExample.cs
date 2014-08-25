namespace FolderTree
{
    using System;

    public class FolderTreeExample
    {
        public static void Main(string[] args)
        {
            string system32Root = @"C:\Windows\System32";

            FolderTree system32 = new FolderTree(system32Root);
            long system32Size = system32.Size();
            Console.WriteLine(string.Format("{0} - {1}",
                system32.RootName, SizeForPrint(system32Size)));

            FolderTree advancedInstallers = system32.GetSubTree("AdvancedInstallers");
            long advancedInstallersSize = advancedInstallers.Size();
            Console.WriteLine(string.Format("{0} - {1}",
                advancedInstallers.RootName, SizeForPrint(advancedInstallersSize)));
        }

        private static string SizeForPrint(long size) 
        {
            if (size < 1024)
            {
                return size.ToString() + " bytes";
            }

            double roundedSize = size / 1024;
            int typeNumber = 1;

            while (roundedSize > 1024)
            {
                roundedSize = roundedSize / 1024;
                typeNumber++;
            }

            string sizeForPrint = Math.Round(roundedSize, 2).ToString();
            switch (typeNumber)
            {
                case 1: sizeForPrint += " Kb"; break;
                case 2: sizeForPrint += " Mb"; break;
                case 3: sizeForPrint += " Gb"; break;
                default: throw new ArgumentException();
            }

            return sizeForPrint;
        }
    }
}
