using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class Testing : MonoBehaviour
{
    private PathFinding pathFinding;

    private void Start() {
        this.pathFinding = new PathFinding(6, 6);
        pathFinding.FindPath(1,1,2,2);
    }    
}
