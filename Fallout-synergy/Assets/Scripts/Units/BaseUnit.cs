using UnityEngine;

namespace Units
{
    public class BaseUnit : MonoBehaviour
    {
        public Tile OccupiedTile;
        public Faction Faction;
        public string UnitName;
        public int health;

        public void UpdateHealth(int damage)
        {
            health += damage;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}