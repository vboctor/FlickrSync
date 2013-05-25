using System.Collections;
using System.IO;

public class sortLastWriteHelper : IComparer
{
    int IComparer.Compare(object x, object y)
    {
        FileInfo f1 = (FileInfo)x;
        FileInfo f2 = (FileInfo)y;

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
    

