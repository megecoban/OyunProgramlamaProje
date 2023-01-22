using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payer : MonoBehaviour
{
    private float duration = 0.2f;
    private float timer = 0f;
    private bool canPay => (PlayerSystem.Instance.stashSystem.ItemCount > 0) ? true : false;

    private void OnTriggerStay(Collider other)
    {
        if (canPay && other.CompareTag("Unlocker") && other.TryGetComponent<UnlockerArea>(out UnlockerArea _unlock))
        {
            if (_unlock.isUnlocked == false)
            {
                timer += Time.fixedDeltaTime;
                if (timer >= duration)
                {
                    PlayerSystem.Instance.stashSystem.PayStashableTo(_unlock);
                    timer = 0;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Unlocker") && other.TryGetComponent<UnlockerArea>(out UnlockerArea _unlock))
        {
            timer = 0;
        }
    }
}
