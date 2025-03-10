using System.Reflection;
public class WarriorAbility {
    public enum Keywords {
        Attack, Death, Overturn,
    }

    public Bloodlust bloodlust = new();
    public Revive revive = new();
    public HydraSplit hydraSplit = new();
    public Poison poison = new();
    public Poisoned poisoned = new();

    public string GetAbilityText() {
        string returnValue = "";

        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields) {
            object abilityInstance = field.GetValue(this);
            MethodInfo method = abilityInstance.GetType().GetMethod("GetTitle");

            returnValue += (string)method.Invoke(abilityInstance, null);
        }

        return returnValue;
    }

    public void DisplayAbilityTooltip(TooltipManager tooltipManager) {
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields) {
            object abilityInstance = field.GetValue(this);
            MethodInfo titleMethod = abilityInstance.GetType().GetMethod("GetTitle");
            string title = (string)titleMethod.Invoke(abilityInstance, null);
            if (title == "") continue;
            MethodInfo descriptionMethod = abilityInstance.GetType().GetMethod("GetDescription");
            string description = (string)descriptionMethod.Invoke(abilityInstance, null);

            tooltipManager.AddTooltip(title, description);
        }
    }
}