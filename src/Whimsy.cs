using System;
using System.Collections.Generic;
using System.Windows.Automation;

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
            return false;
        }

        // Initialize highest whimsy, make it persistant
        int? whimsy_high = CPH.GetGlobalVar<int?>("whimsy_high", true) ?? 0;
        
        // Dictionary of people who have highest score (id, username)
        Dictionary<string,string> whimsy_leaders = CPH.GetGlobalVar<Dictionary<string,string>>("whimsy_leaders", true) ?? [];

        string outputMsg = "";

        // Get users value if they have already used it
        int? users_score = CPH.GetTwitchUserVarById<int?>(userId,"whimsy_user", true);

        // Check if the user has a score already
        if (users_score.HasValue)
        {
            outputMsg = $"@{username}, your are still {users_score.Value}% whimsical gurnaroKLove";
        }
        else
        {
            int whimsy_score = CPH.Between(0,100);
            CPH.SetTwitchUserVarById(userId,"whimsy_user", whimsy_score, true);

            if(!whimsy_high.HasValue || whimsy_leaders.Count == 0)
            {
                // First score to be added
                whimsy_high = whimsy_score;
                whimsy_leaders[userId] = username;

                outputMsg = $"Yo @{username}, you are the most whimsiest with {whimsy_score}%! gurnaroKSpin";
            }
            else if( whimsy_score > whimsy_high.Value)
            {
                // New score is higher than previous
                whimsy_high = whimsy_score;
                whimsy_leaders.Clear();
                whimsy_leaders[userId] = username;

                outputMsg = $"What! @{username} just beat everybody else with {whimsy_score}% and is the most whimsiest now! gurnaroKSpin";
            }
            else if(whimsy_score == whimsy_high.Value)
            {
                // There is a tie
                string highest_names = "@" + string.Join(", @", whimsy_leaders.Values);
                // Add user to the leaderboard after getting names, so they don't get double mention
                whimsy_leaders[userId] = username;

                outputMsg = $"Nice! @{username} just tied with {highest_names}";
            }
            else
            {
                // Final case, if the score is lower than highest value
                string highest_names = "@" + string.Join(", @", whimsy_leaders.Values);
                if (whimsy_leaders.Count < 2)
                {
                    outputMsg = $"It's not your day today @{username}, your whimsy is {whimsy_score}%, but {highest_names} has a {whimsy_high.Value}% whimsy rating.";
                }
                else
                {
                    outputMsg = $"It's not your day today @{username}, your whimsy is {whimsy_score}%, but {highest_names} all have a {whimsy_high.Value}% whimsy rating.";
                }
            }
        }

        // Save updated data
        CPH.SetGlobalVar("whimsy_high", whimsy_high.Value, true);
        CPH.SetGlobalVar("whimsy_leaders", whimsy_leaders, true);

        //CPH.SendMessage($"Hello @{username}!");
        CPH.SendMessage(outputMsg, false);
        return true;
    }
}