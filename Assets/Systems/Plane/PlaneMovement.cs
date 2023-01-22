using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlaneMovement : MonoBehaviour
{
    private float speed => PlayerSystem.Instance.selectedActor.ActorData.speed;


    public float getSpeed => speed;

    private Vector3 previousPos;

    public float currentSpeed = 0;


    private void Start()
    {
        previousPos = PlayerSystem.Instance.transform.position;
    }


    public void Move()
    {
        MoveWithSpeed(PlayerSystem.Instance.transform.position, PlayerSystem.Instance.transform.forward, speed);
    }

    void MoveWithSpeed(Vector3 currentPos, Vector3 dir, float speed)
    {
        Vector3 endPos = currentPos + dir * speed;
        previousPos = PlayerSystem.Instance.transform.position;

        for (int i = 0; i < 3; i++)
        {
            endPos[i] = Mathf.Clamp(endPos[i], GameManager.Instance.minimumBorderPositions[i], GameManager.Instance.maximumBorderPositions[i]);
        }

        PlayerSystem.Instance.transform.position = Vector3.Lerp(currentPos, endPos, Time.deltaTime);
        currentSpeed = MyUtils.CalculateCurrentSpeed(PlayerSystem.Instance.transform.position, previousPos);
    }

}
