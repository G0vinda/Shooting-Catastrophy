using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObstacle : Obstacle
{
    public override void OnHit()
    {
        Debug.Log("HIT");
    }
}
