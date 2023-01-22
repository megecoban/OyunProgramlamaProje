using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 rotation = new Vector3(15f, 0f, 0f);
    private Vector3 refVelocity = Vector3.zero;
    [SerializeField] [Range(0, 1f)] private float smoothTime;


    private void LateUpdate()
    {
            this.transform.rotation.SetEulerAngles(rotation);
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetTransform.position + PlayerSystem.Instance.selectedActor.ActorData.cameraOffset, ref refVelocity, smoothTime);
    }
}
