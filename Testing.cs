using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class Testing : MonoBehaviour
{
    private PathFinding pathFinding;

    private void Start() {
        this.pathFinding = new PathFinding(16, 10);
    }    

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            int endX, endY;
            pathFinding.GetGrid().GetXY(mouseWorldPosition, out endX, out endY);
            List<PathNode> path = pathFinding.FindPath(0, 0, endX, endY);

            for(int i=0; i<path.Count-1; i++){
                Vector3 startPoint = pathFinding.GetGrid().GetWorldPosition(path[i].x, path[i].y) + Vector3.one*5f;
                Vector3 endPoint = pathFinding.GetGrid().GetWorldPosition(path[i+1].x, path[i+1].y) + Vector3.one*5f;
                Debug.DrawLine(startPoint, endPoint, Color.green, 5f);
            }
        }
        if(Input.GetMouseButtonDown(1)){
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            PathNode node = pathFinding.GetGrid().GetGridObject(mouseWorldPosition);
            node.isWalkable = !node.isWalkable;
            if(!node.isWalkable){
                node.showObstacle();
            }
            else{
                node.hideObstacle();
            }
        }
    }
}
