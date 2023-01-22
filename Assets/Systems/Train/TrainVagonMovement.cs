using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainVagonMovement : MonoBehaviour
{
    [SerializeField] private float offset = -0.0225f;

    private void Update()
    {
        if (TrainSystem.Instance == null)
            return;

        this.transform.position = TrainSystem.Instance.GetVagonPos(offset);
        this.transform.rotation = TrainSystem.Instance.GetVagonRot(offset);
    }
}
