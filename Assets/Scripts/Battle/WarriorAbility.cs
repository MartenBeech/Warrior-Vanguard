using System.Reflection;
public class WarriorAbility {
    public enum Keywords {
        Attack,     //After it makes an attack move
        Death,      //After it dies
        Overturn,   //At the end of its turns
        Strike,     //After it strikes a warrior
        Summon,     //after it is summoned
        Kill,       //After it kills a warrior
        Aura,       //Active while it is alive
    }

    public Construct construct = new();
    public Weaken weaken = new();
    public Bloodlust bloodlust = new();
    public Revive revive = new();
    public HydraSplit hydraSplit = new();
    public Poison poison = new();
    public PoisonCloud poisonCloud = new();
    public Poisoned poisoned = new();
    public LifeSteal lifeSteal = new();
    public LifeTransfer lifeTransfer = new();
    public Retaliate retaliate = new();
    public Cannibalism cannibalism = new();
    public Stealth stealth = new();
    public PermaStealth permaStealth = new();
    public Splash splash = new();
    public RaiseDead raiseDead = new();
    public FrozenTouch frozenTouch = new();
    public Skeletal skeletal = new();
    public DeathCall deathCall = new();
    public BoneSculptor boneSculptor = new();
    public Incorporeal incorporeal = new();
    public Afterlife afterlife = new();
    public DarkTouch darkTouch = new();
    public Possess possess = new();
    public BoneSpread boneSpread = new();
    public WeakeningAura weakeningAura = new();
    public HitAndRun hitAndRun = new();
    public PoisoningAura poisoningAura = new();
    public CemeteryGates cemeteryGates = new();
    public Armor armor = new();
    public Resistance resistance = new();
    public Pierce pierce = new();
    public MassResistance massResistance = new();
    public DoubleStrike doubleStrike = new();
    public Multishot multishot = new();
    public GreedyStrike greedyStrike = new();
    public ForestStrength forestStrength = new();
    public EvilInspiration evilInspiration = new();
    public ThickSkin thickSkin = new();
    public PhoenixAshes phoenixAshes = new();
    public EternalNightmare eternalNightmare = new();
    public Rebirth rebirth = new();
    public SpellImmunity spellImmunity = new();
    public Stoneskin stoneskin = new();
    public Regeneration regeneration = new();
    public Bash bash = new();
    public Stunned stunned = new();
    public FamiliarGround familiarGround = new();
    public Rooting rooting = new();
    public Rooted rooted = new();
    public Sprout sprout = new();
    public SapEnergy sapEnergy = new();
    public Looting looting = new();
    public Heal heal = new();
    public MassHeal massHeal = new();
    public Spikes spikes = new();
    public LushGrounds lushGrounds = new();
    public ForestProtection forestProtection = new();
    public Carnivore carnivore = new();
    public Vengeance vengeance = new();
    public Flying flying = new();
    public FaeMagic faeMagic = new();
    public SapPower sapPower = new();
    public Weakened weakened = new();

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