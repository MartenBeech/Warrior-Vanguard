public class WarriorAbility {


    public Bloodlust bloodlust = new();
    public Revive revive = new();
    public HydraSplit hydraSplit = new();
    public Poison poison = new();
    public Poisoned poisoned = new();

    public string GetAbilityText() {
        string returnValue = "";

        returnValue += bloodlust.GetTitle();
        returnValue += revive.GetTitle();
        returnValue += hydraSplit.GetTitle();
        returnValue += poison.GetTitle();
        returnValue += poisoned.GetTitle();

        return returnValue;
    }

    public void DisplayAbilityTooltip(TooltipManager tooltipManager, WarriorStats stats) {

    }
}