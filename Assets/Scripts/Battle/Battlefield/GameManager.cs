using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Character> players = new List<Character>();
    public List<Character> enemies = new List<Character>();
    public GridManager gridManager;

    public void EndTurn()
    {
        Debug.Log("🔄 Player Turn: Moving all characters to the right!");

        foreach (Character character in players)
        {
            character.MoveRight();
        }
    }

    public void EndEnemyTurn()
    {
        Debug.Log("🔄 Enemy Turn: Moving all enemies to the left!");

        foreach (Character enemy in enemies)
        {
            enemy.MoveLeft();
        }
    }

    public void RegisterCharacter(Character character, bool isEnemy)
    {
        if (isEnemy)
        {
            if (!enemies.Contains(character))
                enemies.Add(character);
        }
        else
        {
            if (!players.Contains(character))
                players.Add(character);
        }
        gridManager.RegisterCharacter(character);
    }
}
