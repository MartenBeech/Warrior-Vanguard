using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<Character> friends = new();
    public List<Character> enemies = new();
    public GridManager gridManager;

    public void EndTurn() {
        friends = friends.OrderByDescending(c => c.gridPosition.x).ToList();
        foreach (Character friend in friends) {
            friend.MoveWarrior(Character.Direction.Right);
        }
    }

    public void EndEnemyTurn() {
        enemies = enemies.OrderBy(c => c.gridPosition.x).ToList();
        foreach (Character enemy in enemies) {
            enemy.MoveWarrior(Character.Direction.Left);
        }
    }

    public void RegisterCharacter(Character character, CharacterSpawner.Alignment alignment) {
        if (alignment == CharacterSpawner.Alignment.Enemy) {
            if (!enemies.Contains(character))
                enemies.Add(character);
        } else if (alignment == CharacterSpawner.Alignment.Friend) {
            if (!friends.Contains(character))
                friends.Add(character);
        }
        gridManager.RegisterCharacter(character);
    }

    public void RemoveCharacter(Character character) {
        if (friends.Contains(character))
            friends.Remove(character);
        else if (enemies.Contains(character))
            enemies.Remove(character);
    }
}
