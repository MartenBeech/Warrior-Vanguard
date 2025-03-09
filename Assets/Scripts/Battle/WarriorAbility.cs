public class WarriorAbility {

    public Bloodlust bloodlust = new(); //Bloodlust X: After attacking, gain X strength
    public Revive revive = new(); //Revive: First time this dies, summon a copy

    public string GetAbilityText() {
        string returnValue = "";


        returnValue += bloodlust.GetTitle();
        returnValue += revive.GetTitle();


        return returnValue;
    }
}