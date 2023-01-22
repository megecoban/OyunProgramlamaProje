using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathCreator))]
public class TrainPath : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    public bool closedLoop = true;
    public Transform[] waypoints;

    private void Awake()
    {
        if (pathCreator == null)
        {
            pathCreator = this.GetComponent<PathCreator>();
        }
    }

    [ContextMenu("GenerateBezierPath")]
    private void GenerateBezierPath()
    {
        if (waypoints.Length > 0)
        {
            BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
            if(this.pathCreator == null)
            {
                this.GetComponent<PathCreator>().bezierPath = bezierPath;
            }
            else
            {
                this.pathCreator.bezierPath = bezierPath;
            }
        }
    }
}
