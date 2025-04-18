using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class HolyLight : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Heal a random friend by 2 each turn";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override async Task UseStartOfTurn(Summoner summoner) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        List<Character> damagedFriends = gridManager.GetDamagedFriends(GameManager.turn);

        if (damagedFriends.Count == 0) return;

        int index = Rng.Range(0, damagedFriends.Count);
        Character damagedFriend = damagedFriends[index];

        await damagedFriend.Heal(damagedFriend, 2);
    }
}