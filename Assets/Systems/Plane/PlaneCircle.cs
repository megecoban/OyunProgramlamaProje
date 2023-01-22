using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCircle : MonoBehaviour
{
    private Transform plane;
    private Transform planePropeller;
    [SerializeField] private Transform centerPoint;
    [SerializeField] [Min(0)] float radius = 10f;
    [SerializeField] [Min(0)] float speedOfPlane = 10f;
    float angle = 0;

    private void Start()
    {
        plane = PlaneSystem.Instance.getPlane;
        planePropeller = PlaneSystem.Instance.getPlanePropeller;

        plane.position = new Vector3(this.transform.position.x - radius, this.transform.position.y, this.transform.position.z - radius);
    }

    void Update()
    {
        angle += Time.deltaTime;

        if (angle > 2 * Mathf.PI)
            angle = 2 * Mathf.PI - angle;

        Vector3 offset = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * radius;

        this.transform.LookAt(centerPoint.transform.position + offset);
        this.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);

        planePropeller.Rotate(new Vector3(0f, 0f, speedOfPlane * 30f * Time.deltaTime));

        this.transform.position = Vector3.Lerp(this.transform.position, centerPoint.transform.position + offset, Time.deltaTime * speedOfPlane);
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(centerPoint.transform.position, 1f);

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(centerPoint.transform.position + new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * radius, 1f);
        }
    }


    /*
     * void MethodTwoForPlane(){
     * plane.position = Vector3.Lerp(plane.position, plane.position + plane.forward * speedOfPlane, Time.deltaTime);
        plane.eulerAngles = new Vector3(0f,
                Mathf.LerpAngle(plane.eulerAngles.y, plane.eulerAngles.y + 45f, Time.deltaTime * (speedOfPlane/radius)),
                0f);

        planePropeller.Rotate(new Vector3(0f, 0f, speedOfPlane * 30f * Time.deltaTime));
     * }
     */


    /*
    void MethodOneForPlane()
    {
        Vector3 pos = new Vector3();
        angle = (angle + (Time.deltaTime * speedOfPlane));

        if (angle >= 360f)
            angle = angle - 360f;

        plane.transform.LookAt(pos);
        plane.transform.eulerAngles = new Vector3(0f, plane.transform.eulerAngles.y, 0f);

        pos.x = this.transform.position.x + (radius * Mathf.Cos(angle / (180f / Mathf.PI)));
        pos.y = this.transform.position.y;
        pos.z = this.transform.position.z + (radius * Mathf.Sin(angle / (180f / Mathf.PI)));

        plane.position = pos;
    }

    float ClampAngle(float current, float min, float max)
    {
        float dtAngle = Mathf.Abs(((min - max) + 180) % 360 - 180);
        float hdtAngle = dtAngle * 0.5f;
        float midAngle = min + hdtAngle;

        float offset = Mathf.Abs(Mathf.DeltaAngle(current, midAngle)) - hdtAngle;
        if (offset > 0)
            current = Mathf.MoveTowardsAngle(current, midAngle, offset);
        return current;
    }*/


}
