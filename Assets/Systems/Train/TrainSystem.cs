using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TrainSystem : MonoBehaviour
{
    [SerializeField] PathCreator pathCreator;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;

    private float speed = 0;
    private float startTime = 0;

    public static TrainSystem Instance;

    float elapsedTime = 0;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        elapsedTime += speed * Time.deltaTime;
    }

    public void SetStartTime(float _startTime)
    {
        startTime = _startTime;
        elapsedTime = _startTime;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public Vector3 GetTrainPos()
    {
        return pathCreator.path.GetPointAtTime(elapsedTime, endOfPathInstruction);
    }
    public Quaternion GetTrainRot()
    {
        return pathCreator.path.GetRotation(elapsedTime, endOfPathInstruction);
    }

    public Vector3 GetVagonPos(float offset)
    {
        return pathCreator.path.GetPointAtTime(elapsedTime + offset, endOfPathInstruction);
    }

    public Quaternion GetVagonRot(float offset)
    {
        return pathCreator.path.GetRotation(elapsedTime + offset, endOfPathInstruction);
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public float GetNormalizedTime()
    {
        return (elapsedTime%1f);
    }
}
