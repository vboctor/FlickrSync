using System.Collections.Generic;
using System.IO;

public class SortLastWriteHelper : IComparer<FileInfo>
{
    public int Compare(FileInfo f1, FileInfo f2)
    {
        if (f1.LastWriteTime > f2.LastWriteTime)
        {
            return 1;
        }
        else if (f1.LastWriteTime < f2.LastWriteTime)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
    

