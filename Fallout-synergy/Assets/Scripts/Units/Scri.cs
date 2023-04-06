using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "scriptable unit")]
    public class Scri : ScriptableObject
    {
        public Faction Faction;
        public BaseUnit unitPrefab;

    }
    public enum Faction
    {
        Champ = 0,
        Enemy = 1
    }
}