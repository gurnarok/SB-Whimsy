using System;

#if EXTERNAL_EDITOR
public class Whimsy : CPHInlineBase
#else
public class CPHInline
#endif
{
    public bool Execute()
    {
        // Add your code here
        return true;
    }
}