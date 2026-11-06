using System;
using System.Collections.Generic;

#if EXTERNAL_EDITOR
public class Unwhimsy : CPHInlineBase
#else
public class CPHInline
#endif
{
    public bool Execute()
    {
        // Do things to unset global values.
        return true;
    }
}