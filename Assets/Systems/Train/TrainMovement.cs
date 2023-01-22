using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.05f;
    private float tempSpeed = 0;
    [SerializeField] private float startedTime = 0;

    public bool isStopped = false;

    public static TrainMovement Instance;

    private float timerForNotStopping = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TrainSystem.Instance.SetStartTime(startedTime);
        TrainSystem.Instance.SetSpeed(speed);
        timerForNotStopping = startedTime;
    }

    private void Update()
    {
        if (TrainSystem.Instance == null)
            return;

        this.transform.position = TrainSystem.Instance.GetTrainPos();
        this.transform.rotation = TrainSystem.Instance.GetTrainRot();


        if (TrainSystem.Instance.GetElapsedTime() > 1f
            && TrainSystem.Instance.GetNormalizedTime() >= startedTime
            && isStopped == false)
        {
            Stop();
            isStopped = true;
        }
    }

    public void Stop()
    {
        tempSpeed = speed;
        speed = 0;
        TrainSystem.Instance.SetSpeed(speed);

    }

    public void Play()
    {
        speed = tempSpeed;
        tempSpeed = 0;
        isStopped = false;
        TrainSystem.Instance.SetStartTime(startedTime);
        TrainSystem.Instance.SetSpeed(speed);
    }

}
