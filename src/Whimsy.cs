using System;
using System.Collections.Generic;

#if EXTERNAL_EDITOR
public class Whimsy : CPHInlineBase
#else
public class CPHInline
#endif
{
    public bool Execute()
    {
        // who used the command
        CPH.TryGetArg("userId", out string userId);
        CPH.TryGetArg("userName", out string username);

        // Initialize highest whimsy, make it persistant
        int? highest = CPH.GetGlobalVar<int?>("whimsy_high", true);

        // Dictionary of people who have highest score (id, username)
        Dictionary<string,string> highest_usr = CPH.GetGlobalVar<Dictionary<string,string>>("whimsy_leaders", true);

        string outputMsg = "";

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username))
        {
            CPH.LogError("Da fuq? No userId or username");
        }

        //CPH.SendMessage("Hello @{username}!");
        CPH.SendMessage(outputMsg);
        return true;
    }
}