using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding {
    private int width;
    private int height;
    private Grid<PathNode> grid;

    public PathFinding(int width, int height) {
        this.width = width;
        this.height = height;
        this.grid = new Grid<PathNode>(width, height, 10f, new Vector3(0,0), (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY){
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);
        PQueue<PathNode, int> openList = new PQueue<PathNode, int>();
        
        // openList.Enqueue(startNode, 10);
        return new List<PathNode>();
    }
} 
