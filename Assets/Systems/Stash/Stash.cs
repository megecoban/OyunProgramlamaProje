using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stash : MonoBehaviour
{
    private int itemCount = 0;
    public int ItemCount
    {
        get
        {
            return itemCount;
        }
    }

    [SerializeField] private float itemsOffset = 0.25f;
    private float maxItemSize = 0;

    public bool canCollect => (itemCount >= maxItemSize) ? false : true;
    

    public static Stash Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        maxItemSize = PlayerSystem.Instance.selectedActor.ActorData.capacity;

        SetStashPos(PlayerSystem.Instance.selectedActor.ActorData.stashPos);
    }

    public int Reserve()
    {
        itemCount++;
        return itemCount-1;
    }

    public void SetStashPos(Vector3 localPos)
    {
        this.transform.localPosition = localPos;
    }

    public Vector3 GlobalPosOfReserveIndex(int reserveIndex)
    {
        return this.transform.position + Vector3.up * itemsOffset * reserveIndex;
    }

    public Vector3 LocalPosOfReserveIndex(int reserveIndex)
    {
        return Vector3.up * itemsOffset * reserveIndex;
    }

    public void PayStashableTo(UnlockerArea unlocker)
    {
        unlocker.Decrease();
        itemCount--;
        this.transform.GetChild(this.transform.childCount - 1).GetComponent<Stashable>().GoToUnlocker(unlocker, (()=>{
            unlocker.DecreaseUI(); 
        }));
    }

    public void ClearStash()
    {
        itemCount = 0;
        if(this.transform.childCount != 0)
        {
            for (int a = this.transform.childCount - 1; a > -1; a--)
            {
                Destroy(this.transform.GetChild(a).gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        for(int a=0; a< maxItemSize; a++)
        {
            Gizmos.DrawWireCube(this.transform.position + Vector3.up * a * itemsOffset, new Vector3(0.5f, 0.25f, 0.5f));
        }
    }
}
