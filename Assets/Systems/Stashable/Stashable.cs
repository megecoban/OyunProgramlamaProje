using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stashable : MonoBehaviour
{
    [SerializeField] private Vector3 startScale = Vector3.one;
    [SerializeField] private Vector3 endScale = new Vector3(0.6f, 0.6f, 0.6f);
    [SerializeField] [Min(0.001f)] private float duration = 2f;
    private float timerForGo = 0;
    private float speed;
    private int reservedIndex = -1;
    private UnlockerArea targetUnlocker;
    private Action funcAfterPay = null;

    private void Awake()
    {
        speed = 1.0f / duration;
        startScale = this.transform.localScale;
    }

    private void Start()
    {
        startScale = this.transform.localScale;
    }

    public void GoToStash(int reservedIndex)
    {
        this.reservedIndex = reservedIndex;
        StartCoroutine("TrackStash");
    }

    public void GoToUnlocker(UnlockerArea targetUnlocker, Action someFuncs)
    {
        this.transform.parent = null;
        this.targetUnlocker = targetUnlocker;
        funcAfterPay = someFuncs;
        StartCoroutine("TrackUnlocker");
    }

    IEnumerator TrackStash()
    {
        timerForGo = 0f;

        Vector3 startPos = this.transform.position;
        Vector3 scaleStart = this.transform.localScale;

        while (timerForGo < duration)
        {
            timerForGo += Time.deltaTime;

            Vector3 midPos = Vector3.Lerp(startPos, PlayerSystem.Instance.stashSystem.GlobalPosOfReserveIndex(reservedIndex), 0.75f);
            midPos += Vector3.up * 4f;

            Vector3 targetPos = GetQuadraticBezierCurvePoint(startPos, midPos, PlayerSystem.Instance.stashSystem.GlobalPosOfReserveIndex(reservedIndex), timerForGo/duration);

            this.transform.position = Vector3.Lerp(startPos, targetPos, timerForGo/duration);
            this.transform.localScale = Vector3.Lerp(scaleStart, Vector3.one, timerForGo / duration);

            yield return null;
        }

        this.transform.parent = PlayerSystem.Instance.stashSystem.transform;
        this.transform.localRotation = Quaternion.EulerAngles(Vector3.zero);
        this.transform.localPosition = PlayerSystem.Instance.stashSystem.LocalPosOfReserveIndex(reservedIndex);
        //this.transform.localScale = Vector3.one;
    }

    IEnumerator TrackUnlocker()
    {
        timerForGo = 0f;

        this.transform.parent = null;

        Vector3 startPos = this.transform.position;

        while (timerForGo < duration)
        {
            timerForGo += Time.deltaTime;

            Vector3 midPos = Vector3.Lerp(startPos, new Vector3(targetUnlocker.transform.position.x, startPos.y, targetUnlocker.transform.position.z), 0.25f);
            midPos += Vector3.up * 4f;

            Vector3 targetPos = GetQuadraticBezierCurvePoint(startPos, midPos, new Vector3(targetUnlocker.transform.position.x, 0f, targetUnlocker.transform.position.z), timerForGo / duration);

            this.transform.position = Vector3.Lerp(startPos, targetPos, timerForGo / duration);

            yield return null;
        }


        if (funcAfterPay != null)
        {
            funcAfterPay();
        }

        Destroy(this.gameObject, Time.deltaTime);
    }


    Vector3 GetQuadraticBezierCurvePoint(Vector3 originPoint, Vector3 midPoint, Vector3 lastPoint, float time)
    {
        float u = 1 - time;
        float tt = time * time;
        float uu = u * u;

        Vector3 point = uu * originPoint;
        point += 2 * u * time * midPoint;
        point += tt * lastPoint;

        return point;
    }
}
