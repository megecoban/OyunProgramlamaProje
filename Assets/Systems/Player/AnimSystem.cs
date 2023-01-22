using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSystem : MonoBehaviour
{
    [Header("For Human")]
    [SerializeField] private Animator animator;
    private float speed = 0;

    [Header("For Car")]
    [SerializeField] List<GameObject> frontWheelsForYRotation;
    [SerializeField] List<GameObject> allWheelsForXRotation;
    [SerializeField] float maxDegree = 10f;

    public static AnimSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }


    private void Update()
    {
        if (animator != null)
        {
            animator.SetFloat("WalkSpeed", speed);
        }

        if (allWheelsForXRotation != null)
        {
            Vector3 xzDirection = new Vector3(GameManager.Instance.GetJoystickDirection().x, 0f, GameManager.Instance.GetJoystickDirection().y);
            float angle = Mathf.Abs(Vector3.Angle(this.transform.forward, xzDirection));
            Vector3 crossAngle = Vector3.Cross(this.transform.forward, xzDirection);
            angle = crossAngle.y < 0 ? angle * -1f : angle;
            angle = Mathf.Clamp(angle, -30f, 30f);


            if (speed != 0f)
            {
                for(int a=0; a<allWheelsForXRotation.Count; a++)
                {
                    allWheelsForXRotation[a].transform.Rotate(Vector3.right * speed * 7f);
                }
                
                for(int a=0; a<frontWheelsForYRotation.Count; a++)
                {
                    frontWheelsForYRotation[a].transform.localEulerAngles = new Vector3(0f, angle, 0f);
                }
            }
        }
    }
}
