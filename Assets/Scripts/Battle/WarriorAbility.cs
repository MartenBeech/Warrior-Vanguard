using System.Reflection;
public class WarriorAbility {
    public enum Keywords {
        Attack,     //After it makes an attack move
        Death,      //After it dies
        Overturn,   //At the end of its turns
        Strike,     //After it strikes a warrior
        Summon,     //after it is summoned
        Kill,       //After it kills a warrior
    }

    public Weaken weaken = new();
    public Bloodlust bloodlust = new();
    public Revive revive = new();
    public HydraSplit hydraSplit = new();
    public Poison poison = new();
    public Poisoned poisoned = new();
    public LifeSteal lifeSteal = new();
    public Retaliate retaliate = new();
    public Cannibalism cannibalism = new();
    public Stealth stealth = new();
    public PermaStealth permaStealth = new();
    public Splash splash = new();
    public RaiseDead raiseDead = new();
    public FrozenTouch frozenTouch = new();
    public Skeletal skeletal = new();
    public DeathCall deathCall = new();
    public BoneToughener boneToughener = new();
    public Incorporeal incorporeal = new();
    public Afterlife afterlife = new();
    public DarkTouch darkTouch = new();
    public Possess possess = new();

    public string GetAbilityText(WarriorStats stats) {
        string returnValue = "";

        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields) {
            object abilityInstance = field.GetValue(this);
            object[] parameters = { stats };
            MethodInfo method = abilityInstance.GetType().GetMethod("GetTitle");

            returnValue += (string)method.Invoke(abilityInstance, parameters);
        }

        return returnValue;
    }

    public void DisplayAbilityTooltip(TooltipManager tooltipManager, WarriorStats stats) {
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (FieldInfo field in fields) {
            object abilityInstance = field.GetValue(this);
            object[] parameters = { stats };
            MethodInfo titleMethod = abilityInstance.GetType().GetMethod("GetTitle");
            string title = (string)titleMethod.Invoke(abilityInstance, parameters);
            if (title == "") continue;
            MethodInfo descriptionMethod = abilityInstance.GetType().GetMethod("GetDescription");
            string description = (string)descriptionMethod.Invoke(abilityInstance, parameters);

            tooltipManager.AddTooltip(title, description, 0.5f);
        }
    }
}