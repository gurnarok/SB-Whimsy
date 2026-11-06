using System;

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

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username))
        {
            CPH.LogError("Da fuq? No userId or username");
        }

        CPH.SendMessage("Hello @{username}!");
        return true;
    }
}