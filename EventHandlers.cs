using System.Linq;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Scp330;

namespace CandyChances
{
    public class EventHandlers
    {
        public void OnInteractingScp330(InteractingScp330EventArgs ev)
        {
            if (!ev.IsAllowed)
                return;

            CustomRole customRole = ev.Player.GetCustomRoles()?.FirstOrDefault();
            if (customRole != null && Plugin.Instance.Config.ModifiedUseLimitsforCustomRoles.TryGetValue(customRole.Name, out int customUsageLimit))
            {
                ev.ShouldSever = ev.UsageCount >= customUsageLimit;
            }
            else if (Plugin.Instance.Config.ModifiedUseLimits.TryGetValue(ev.Player.Role.Type, out int usageLimit))
            {
                ev.ShouldSever = ev.UsageCount >= usageLimit;
            }

            if (ev.ShouldSever)
            {
                ev.Player.ShowHint(Plugin.Instance.Translation.HandsSeveredHints.RandomItem(), Plugin.Instance.Config.HandsSeveredHintTime);
            }
            else if (Plugin.Instance.Translation.GetCandyHints.TryGetValue(ev.Candy, out string[] candyHints))
            {
                ev.Player.ShowHint(candyHints.RandomItem(), Plugin.Instance.Config.CandyHintTime);
            }

            ev.ShouldPlaySound = Plugin.Instance.Config.ShouldPlayTakeSound;
        }
    }
}
