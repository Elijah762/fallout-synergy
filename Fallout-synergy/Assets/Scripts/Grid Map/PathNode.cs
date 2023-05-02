using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> grid;
    public  int x;
    public int y;

    public bool isWalkable;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode lastVisit;
    
    public PathNode(Grid<PathNode>grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.isWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    
    public override string ToString()
    {
        return x + ", " + y;
    }
}
