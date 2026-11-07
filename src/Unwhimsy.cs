using System;

#if EXTERNAL_EDITOR
public class Unwhimsy : CPHInlineBase
#else
public class CPHInline
#endif
{
    public bool Execute()
    {
        // Do things to unset global values.
        CPH.UnsetGlobalVar("whimsy_high");
        CPH.UnsetGlobalVar("whimsy_leaders");
        CPH.UnsetAllUsersVar("whimsy_user");
        
        return true;
    }
}