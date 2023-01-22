using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainStash : MonoBehaviour
{
    [SerializeField] private GameObject stashObject;
    [SerializeField] private List<GameObject> stashObjects;
    [SerializeField] private int numOfMaxStash = 20;
    public int numberOfStash => this.transform.childCount;
    public bool isStashMax = false;

    [SerializeField] private float offset = 0.25f;

    private bool isStop = false;

    public static TrainStash Instance;

    private bool isSpawnPosSet = false;


    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetStash()
    {
        if (this.transform.childCount > 0)
        {
            Destroy(this.transform.GetChild(this.transform.childCount - 1).gameObject, Time.deltaTime);
            return this.transform.GetChild(this.transform.childCount - 1).gameObject;
        }
        return null;
    }

    void Update()
    {
        isStashMax = numOfMaxStash == this.transform.childCount ? true : false;

        if (isStop)
            return;

        if (TrainSystem.Instance.GetNormalizedTime() >= 0.3f && TrainSystem.Instance.GetNormalizedTime() <= 0.5f && this.transform.childCount != numOfMaxStash)
        {
            SpawnObject();
        }
        if (isSpawnPosSet == false)
        {
            SetPosOfSpawnObjects();
        }
    }

    private void SpawnObject()
    {
        for(int i = this.transform.childCount-1; i>=0; i--)
        {
            var temp = this.transform.GetChild(i);
            temp.parent = null;
            temp.gameObject.SetActive(false);
            Destroy(temp.gameObject);
        }


        for (int a = 0; a < 20; a++)
        {
            GameObject temp = Instantiate<GameObject>(stashObject, this.transform);
        }

        isSpawnPosSet = false;
    }

    private void SetPosOfSpawnObjects()
    {
        for (int a = 0; a < this.transform.childCount; a++)
        {
            Vector3 pos1 = Vector3.right * offset + Vector3.forward * offset/2 * a;
            Vector3 pos2 = Vector3.right * -1f * offset + Vector3.forward * offset/2 * a;

            this.transform.GetChild(a).transform.localPosition = pos1;

            if(a+1<this.transform.childCount)
                this.transform.GetChild(a + 1).transform.localPosition = pos2;

            a++;
        }
        isSpawnPosSet = true;
    }
}
