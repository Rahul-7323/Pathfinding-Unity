using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class PathNode {

    private Grid<PathNode> grid;
    public int x;
    public int y;
    public int gCost;
    public int hCost;
    public int fCost;
    public bool isWalkable;
    public GameObject obstacle;
    public PathNode cameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int y) {
        this.x = x;
        this.y = y;
        this.grid = grid;
        this.isWalkable = true;
        this.gCost = int.MaxValue;
        obstacle = GameObject.Instantiate(GameObject.Find("Obstacle"));
        obstacle.transform.position = grid.GetWorldPosition(x, y) + new Vector3(1,1,0)*5f + new Vector3(0,0,-1);
    }

    public void CalculateFCost(){
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return x+","+y;
    }

    public void showObstacle(){
        obstacle.transform.position += new Vector3(0,0,1);
    }

    public void hideObstacle(){
        obstacle.transform.position -= new Vector3(0,0,1);
    }
}
