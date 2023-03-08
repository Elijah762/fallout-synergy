using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	[SerializedField]
	private int width;
	private int height;
	
	[SerializedField]
	private Tile tilePrefab;

    [SerializedField]
    private Transform cam;
	
    // Start is called before the first frame update
    void Start()
    {
        generateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void generateGrid() {
		for(int x = 0; x < width; x++) {
			for(int y = 0; y < width; y++) {
                		var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternian.identity);
                		spawnedTile.name = $"Tile {x} {y}";
            		}
		}
        	cam.transform.position = new Vector3((float) width / 2 - .5f, (float) height / 2 - .5f, -10);
	}
}

