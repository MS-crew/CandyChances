using System.Linq;
using Exiled.CustomRoles.API;
using Interactables.Interobjects;
using Exiled.Events.EventArgs.Scp330;
using Exiled.CustomRoles.API.Features;

namespace CandyChances
{
    public class EventHandlers
    {
        public void OnInteractingScp330(InteractingScp330EventArgs ev)
        {
            if (!ev.IsAllowed)
                return;

            int UsageLimit;
            CustomRole customRole = ev.Player.GetCustomRoles()?.FirstOrDefault();
            
            if (customRole != null && Plugin.Instance.Config.ModifiedUseLimitsforCustomRoles.TryGetValue(customRole.Name, out UsageLimit))
            {
                ev.ShouldSever = ev.UsageCount >= UsageLimit;
            }
            else if (Plugin.Instance.Config.ModifiedUseLimits.TryGetValue(ev.Player.Role.Type, out UsageLimit))
            {
                ev.ShouldSever = ev.UsageCount >= UsageLimit;
            }
            else
            {
                UsageLimit = Scp330Interobject.MaxAmountPerLife;
            }
                
            ev.ShouldPlaySound = Plugin.Instance.Config.ShouldPlayTakeSound;

            if (ev.ShouldSever)
            {
                if (!Plugin.Instance.Config.HandsSeveredHint)
                    return;

                ev.Player.ShowHint(Plugin.Instance.Translation.HandsSeveredHints.RandomItem(), Plugin.Instance.Config.HandsSeveredHintTime);
                return;
            }

            string hint = string.Empty;

            if (Plugin.Instance.Translation.GetCandyHints.TryGetValue(ev.Candy, out string[] candyHints))
            {
                hint = candyHints.RandomItem();
            }

            if (Plugin.Instance.Config.ShowRemainingUse)
            {
                int remaining = UsageLimit - ev.UsageCount - 1;
                string remainingHint = Plugin.Instance.Translation.RemainingUse.Replace("{0}", remaining.ToString());

                if (!string.IsNullOrEmpty(hint))
                    hint += "\n";

                hint += remainingHint;
            }

            ev.Player.ShowHint(hint, Plugin.Instance.Config.CandyHintTime);
        }
    }
}
