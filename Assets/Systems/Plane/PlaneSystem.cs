using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSystem : MonoBehaviour
{
    [SerializeField] private PlaneMovement planeMovementSystem;
    [SerializeField] private PlaneRotation planeRotationSystem;
    [SerializeField] private Transform plane;
    [SerializeField] private Transform planePropeller;

    public Transform getPlane => plane;
    public Transform getPlanePropeller => planePropeller;
    public PlaneMovement movementSystem => planeMovementSystem;
    public PlaneRotation rotationSystem => planeRotationSystem;
    [HideInInspector] public float speed => planeMovementSystem.currentSpeed;
    public static PlaneSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

}
