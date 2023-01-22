using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRotation : MonoBehaviour
{
    [SerializeField] private GameObject propeller;
    [SerializeField] private float multiplerForVecX = 40f;
    [SerializeField] private float multiplerForVecY = 40f;
    [SerializeField] [Range(1f, 90f)] private float maximumY = 60f;
    float speed => PlaneSystem.Instance.speed;


    public void PropellerRotation()
    {
        propeller.transform.Rotate(new Vector3(0f, 0f, PlaneSystem.Instance.speed * 30f * Time.deltaTime < 20f ? 20f : PlaneSystem.Instance.speed * 30f * Time.deltaTime));
    }

    public void Rotation()
    {
        PlayerSystem.Instance.transform.eulerAngles += new Vector3(
            GameManager.Instance.GetJoystickDirection().y * -1f * multiplerForVecX * Time.deltaTime,
            GameManager.Instance.GetJoystickDirection().x * multiplerForVecY * Time.deltaTime,
            0f
            );

        if (GameManager.Instance.GetJoystickDirection().y == 0f)
        {
            PlayerSystem.Instance.transform.eulerAngles = new Vector3(Mathf.LerpAngle(PlayerSystem.Instance.transform.eulerAngles.x, 0f, Time.deltaTime * (speed / 20)),
                PlayerSystem.Instance.transform.eulerAngles.y,
                PlayerSystem.Instance.transform.eulerAngles.z);
        }

        PlayerSystem.Instance.transform.eulerAngles = new Vector3(
        MyUtils.ClampAngle(PlayerSystem.Instance.transform.eulerAngles.x, -1 * maximumY, maximumY), PlayerSystem.Instance.transform.eulerAngles.y, PlayerSystem.Instance.transform.eulerAngles.z);
    }
}
