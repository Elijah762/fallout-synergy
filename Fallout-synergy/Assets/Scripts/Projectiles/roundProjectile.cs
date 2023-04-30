using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

public class roundProjectile : baseProjectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tile"))
        {
            Debug.Log("Hello");
            Tile tile = other.gameObject.GetComponent<Tile>();
            
            if (tile.TileName == "Mountain")
            {
                Debug.Log("Pass through");
            }
            else if (tile.Solid)
            {
                Destroy(this.gameObject);
            }
        }
        
        if (other.CompareTag("Character"))
        {
            BaseUnit unit = other.gameObject.GetComponent<BaseUnit>();
            unit.UpdateHealth(-30);
            Destroy(this.gameObject);
        }
    }
}
