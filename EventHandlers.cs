using System.Linq;
using Exiled.CustomRoles.API;
using Interactables.Interobjects;
using Exiled.Events.EventArgs.Scp330;
using Exiled.CustomRoles.API.Features;
using Exiled.API.Features;

namespace CandyChances
{
    public class EventHandlers
    {
        public void OnInteractingScp330(InteractingScp330EventArgs ev)
        {
            if (!ev.IsAllowed)
                return;

            int UsageLimit = 2;
            CustomRole customRole = ev.Player.GetCustomRoles()?.FirstOrDefault();
            
            if (customRole != null && Plugin.Instance.Config.ModifiedUseLimitsforCustomRoles.TryGetValue(customRole.Name, out UsageLimit))
            {
                ev.ShouldSever = ev.UsageCount >= UsageLimit;
            }
            else if (Plugin.Instance.Config.ModifiedUseLimits.TryGetValue(ev.Player.Role.Type, out UsageLimit))
            {
                ev.ShouldSever = ev.UsageCount >= UsageLimit;
            }


            if (ev.ShouldSever)
            {
                ev.Player.ShowHint(Plugin.Instance.Translation.HandsSeveredHints.RandomItem(), 
                                   Plugin.Instance.Config.HandsSeveredHintTime);
            }
            else if (Plugin.Instance.Translation.GetCandyHints.TryGetValue(ev.Candy, out string[] candyHints))
            {
                string hint= candyHints.RandomItem();

                if (Plugin.Instance.Config.ShowRemainingUse)
                {
                    int remaining = UsageLimit - ev.UsageCount;
                    hint += Plugin.Instance.Translation.RemainingUse.Replace("{0}", remaining.ToString());
                } 

                ev.Player.ShowHint(hint, Plugin.Instance.Config.CandyHintTime);
            }

            ev.ShouldPlaySound = Plugin.Instance.Config.ShouldPlayTakeSound;
        }
    }
}
