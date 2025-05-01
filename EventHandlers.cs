using System.Linq;
using Exiled.CustomRoles.API;
using Exiled.Events.EventArgs.Scp330;
using Exiled.CustomRoles.API.Features;
using InventorySystem.Items.Usables.Scp330;
using Interactables.Interobjects;

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
                ev.ShouldSever = ev.UsageCount >= UsageLimit;
            else if (Plugin.Instance.Config.ModifiedUseLimits.TryGetValue(ev.Player.Role.Type, out UsageLimit))
                ev.ShouldSever = ev.UsageCount >= UsageLimit;
            else
                UsageLimit = Scp330Interobject.MaxAmountPerLife;


            ev.ShouldPlaySound = Plugin.Instance.Config.ShouldPlayTakeSound;

            if (ev.ShouldSever)
            {
                if (!Plugin.Instance.Config.HandsSeveredHint)
                    return;

                ev.Player.ShowHint(Plugin.Instance.Translation.HandsSeveredHints.RandomItem(), Plugin.Instance.Config.HandsSeveredHintTime);
                return;
            }

            if (!Plugin.Instance.Translation.GetCandyHints.TryGetValue(ev.Candy, out string[] candyHints))
                return;

            string hint = candyHints.RandomItem();

            if (Plugin.Instance.Config.ShowRemainingUse)
            {
                int remaining = UsageLimit - ev.UsageCount - 1;
                hint += Plugin.Instance.Translation.RemainingUse.Replace("{0}", remaining.ToString());
            }

            ev.Player.ShowHint(hint, Plugin.Instance.Config.CandyHintTime);
        }
    }
}
