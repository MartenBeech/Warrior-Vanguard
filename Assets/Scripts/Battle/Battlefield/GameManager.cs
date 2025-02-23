using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Character> characters = new List<Character>();
    public GridManager gridManager;

    public void EndTurn()
    {
        Debug.Log("🔄 End Turn: Moving all characters to the right!");

        foreach (Character character in characters)
        {
            character.MoveRight();
        }
    }

    public void RegisterCharacter(Character character)
    {
        if (!characters.Contains(character))
        {
            characters.Add(character);
            gridManager.RegisterCharacter(character); // Register in GridManager too
        }
    }
}
