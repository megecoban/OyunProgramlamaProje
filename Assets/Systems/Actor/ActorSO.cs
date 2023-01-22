using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName = "ScriptableObjects/Actor")]
public class ActorSO : ScriptableObject
{
    public string name;
    public int capacity;
    public Vector3 stashPos;
    public Vector3 cameraOffset;
    public Vector3 collectorColliderCenter;
    public Vector3 collectorColliderSize;
    public float speed;
    public float angularSpeed;
    public ActorType actorType = ActorType.Human;
}

public enum ActorType
{
    Human,
    Car,
    Plane,
    Yacht
}
