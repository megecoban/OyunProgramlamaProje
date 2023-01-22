using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private bool canCollect => PlayerSystem.Instance.stashSystem.canCollect;

    private void OnTriggerEnter(Collider other)
    {
        if (canCollect && other.CompareTag("Collectable") && other.TryGetComponent<Collectable>(out Collectable _collectable))
        {
            _collectable.Collect();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canCollect && other.CompareTag("Collectable") && other.TryGetComponent<TrainStop>(out TrainStop _TrainStop))
        {
            _TrainStop.Collect();
        }
    }
}
