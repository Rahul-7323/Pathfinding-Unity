using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding {

    private const int STRAIGHT_COST = 10;
    private const int DIAGONAL_COST = 14;
    private int width;
    private int height;
    private Grid<PathNode> grid;

    public PathFinding(int width, int height) {
        this.width = width;
        this.height = height;
        this.grid = new Grid<PathNode>(width, height, 10f, new Vector3(-10*(width/2),-10*(height/2)), (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public Grid<PathNode> GetGrid(){
        return grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY){
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);
        PQueue<PathNode, int> openList = new PQueue<PathNode, int>();
        HashSet<PathNode> closeList = new HashSet<PathNode>();
        HashSet<PathNode> visited = new HashSet<PathNode>();

        for(int i=0; i<width; i++){
            for(int j=0; j<height; j++){
                PathNode node = grid.GetGridObject(i,j);
                node.gCost = int.MaxValue;
                node.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();
        startNode.cameFromNode = null;

        openList.EnQueue(Tuple.Create(startNode.fCost, startNode));

        while(!openList.IsEmpty()){
            PathNode currNode = openList.DeQueue().Item2;
            visited.Add(currNode);

            if(currNode == endNode){
                break;
            }

            foreach(PathNode nextNode in GetNodeNeighbours(currNode)){
                if(!visited.Contains(nextNode) && nextNode.isWalkable){
                    int calculatedGCost = currNode.gCost + CalculateDistance(currNode, nextNode);
                    if(calculatedGCost < nextNode.gCost){
                        nextNode.gCost = calculatedGCost;
                        nextNode.hCost = CalculateDistance(nextNode, endNode);
                        nextNode.CalculateFCost();
                        nextNode.cameFromNode = currNode;
                    
                        if(!closeList.Contains(nextNode)){
                            closeList.Add(nextNode);
                            openList.EnQueue(Tuple.Create(nextNode.fCost, nextNode));
                        }
                    }
                }
            }
        }
        
        return CalculatePath(endNode);
    }

    public List<PathNode> CalculatePath(PathNode node){
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = node;
        while(currentNode != null){
            path.Insert(0,currentNode);
            currentNode = currentNode.cameFromNode;
        }
        return path;
    }

    public int CalculateDistance(PathNode a, PathNode b){
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + STRAIGHT_COST * remaining;
    }

    public List<PathNode> GetNodeNeighbours(PathNode node){
        List<PathNode> neighbours = new List<PathNode>();
        int x = node.x;
        int y = node.y;

        if(x+1 < width){
            neighbours.Add(grid.GetGridObject(x+1, y));
        }

        if(x-1 > -1){
            neighbours.Add(grid.GetGridObject(x-1, y));
        }

        if(y+1 < height){
            neighbours.Add(grid.GetGridObject(x, y+1));
        }

        if(y-1 > -1){
            neighbours.Add(grid.GetGridObject(x, y-1));
        }

        if(x+1 < width && y+1 < height){
            neighbours.Add(grid.GetGridObject(x+1, y+1));
        }

        if(x+1 < width && y-1 > -1){
            neighbours.Add(grid.GetGridObject(x+1, y-1));
        }

        if(x-1 > -1 && y-1 > -1){
            neighbours.Add(grid.GetGridObject(x-1, y-1));
        }

        if(x-1 > -1 && y+1 < height){
            neighbours.Add(grid.GetGridObject(x-1, y+1));
        }

        return neighbours;
    }

} 
