using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

public class pelletProjectile : baseProjectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tile"))
        {
            Tile tile = other.gameObject.GetComponent<Tile>();
            if (tile.Solid)
            {
                Destroy(this.gameObject);
            }
        }
        
        if (other.CompareTag("Character"))
        {
            BaseUnit unit = other.gameObject.GetComponent<BaseUnit>();
            unit.UpdateHealth(-5);
            Destroy(this.gameObject);
        }
    }
}
