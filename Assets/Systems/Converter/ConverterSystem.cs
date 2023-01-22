using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConverterSystem : MonoBehaviour
{
    [SerializeField] private ActorSO targetActorSO;
    [SerializeField] private float timerForStay = 1f;
    [SerializeField] private Slider sliderForTimer;
    [SerializeField] private UnityEvent extraUnityEventForConvert;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            timerForStay -= Time.deltaTime;
            sliderForTimer.value = 1 - timerForStay;
            if (timerForStay <= 0f)
            {
                timerForStay = 0;
                Convert();
            }
        }
        Debug.Log("STAY");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerForStay = 1;
            sliderForTimer.value = 1 - timerForStay;
        }
        Debug.Log("EXIT");
    }

    private void Convert()
    {
        PlayerSystem.Instance.SetActor(targetActorSO.actorType);

        if (extraUnityEventForConvert != null)
        {
            extraUnityEventForConvert.Invoke();
        }
        timerForStay = 1f;
        sliderForTimer.value = 0f;
    }
}
