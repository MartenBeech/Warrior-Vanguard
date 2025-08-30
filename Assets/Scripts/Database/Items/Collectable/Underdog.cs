using System.Threading.Tasks;
using UnityEngine;

public class Underdog : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When an enemy takes exactly 1 damage, it takes 2 instead";
        return this;
    }
}