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

    // Identity abilities
    public Construct construct = new();
    public Flying flying = new();
    public Revive revive = new();
    public LifeSteal lifeSteal = new();
    public LifeTransfer lifeTransfer = new();
    public RaiseDead raiseDead = new();
    public Skeletal skeletal = new();
    public Cannibalism cannibalism = new();
    public Afterlife afterlife = new();
    public Stoneskin stoneskin = new();
    public Rooting rooting = new();
    public FamiliarGround familiarGround = new();
    public FaeMagic faeMagic = new();
    public SapPower sapPower = new();
    public Bleed bleed = new();
    public Joust joust = new();
    public LightningBolt lightningBolt = new();

    // Common abilities
    public Armor armor = new();
    public Resistance resistance = new();
    public Guard guard = new();
    public Weaken weaken = new();
    public Bloodlust bloodlust = new();
    public Vengeance vengeance = new();
    public Poison poison = new();
    public Retaliate retaliate = new();
    public FirstStrike firstStrike = new();
    public Stealth stealth = new();
    public Splash splash = new();
    public Incorporeal incorporeal = new();
    public FrozenTouch frozenTouch = new();
    public DarkTouch darkTouch = new();
    public HitAndRun hitAndRun = new();
    public Pierce pierce = new();
    public DoubleStrike doubleStrike = new();
    public Multishot multishot = new();
    public Regeneration regeneration = new();
    public Bash bash = new();
    public Heal heal = new();
    public Spikes spikes = new();
    public Carnivore carnivore = new();
    public Backstab backstab = new();

    // Unique abilities
    public HydraSplit hydraSplit = new();
    public PermaStealth permaStealth = new();
    public DeathCall deathCall = new();
    public BoneSculptor boneSculptor = new();
    public PoisonCloud poisonCloud = new();
    public Possess possess = new();
    public BoneSpread boneSpread = new();
    public WeakeningAura weakeningAura = new();
    public PoisoningAura poisoningAura = new();
    public CemeteryGates cemeteryGates = new();
    public MassResistance massResistance = new();
    public GreedyStrike greedyStrike = new();
    public ForestStrength forestStrength = new();
    public EvilInspiration evilInspiration = new();
    public ThickSkin thickSkin = new();
    public PhoenixAshes phoenixAshes = new();
    public EternalNightmare eternalNightmare = new();
    public Rebirth rebirth = new();
    public SpellImmunity spellImmunity = new();
    public Sprout sprout = new();
    public SapEnergy sapEnergy = new();
    public Looting looting = new();
    public MassHeal massHeal = new();
    public LushGrounds lushGrounds = new();
    public ForestProtection forestProtection = new();
    public Thunderstorm thunderstorm = new();
    public StaticEntrance staticEntrance = new();

    // Buffs and Debuffs
    public Poisoned poisoned = new();
    public Stunned stunned = new();
    public Rooted rooted = new();
    public Weakened weakened = new();
    public Bleeding bleeding = new();
    public HumanShield humanShield = new();

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