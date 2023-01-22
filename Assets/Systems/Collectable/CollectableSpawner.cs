using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class CollectableSpawner : MonoBehaviour
{
    enum SpawnDirection
    {
        XZ,
        XYZ
    }

    [Header("General Settings")]
    [SerializeField] [Min(0f)] private float radiusOfCircle = 1f;
    [SerializeField] private GameObject spriteRendererObject;
    [SerializeField] private bool useSpriteRenderer = true;
    [SerializeField] private bool waitActorType = false;
    [SerializeField] private ActorType targetActorType = ActorType.Human;
    [SerializeField] private SpawnDirection spawnDirection = SpawnDirection.XZ;
    [Space]

    [Header("Spawn Settings")]
    [SerializeField] private GameObject metalObj;
    [SerializeField] private Vector3 spawnScaleSize = Vector3.one;
    [SerializeField] [Min(0)] private int maxObject = 15;
    [SerializeField] private float timerForSpawn = 2f;
    [SerializeField] private int numOfSpawnedObj => this.transform.childCount;


    void Start()
    {
        if(spriteRendererObject!=null && useSpriteRenderer)
            spriteRendererObject.transform.position = this.transform.position + Vector3.up * 0.01f;

        if (Application.isPlaying)
        {
            StartCoroutine("Spawning");
        }
    }

    IEnumerator Spawning()
    {
        Debug.Log("test3");
        while (true)
        {
            Debug.Log("test4");
            if (waitActorType && targetActorType != PlayerSystem.Instance.currentActorType)
            {
                Debug.Log("test5");
                yield return new WaitForSeconds(1f);
                continue;
            }

            if (numOfSpawnedObj < maxObject)
            {
                Debug.Log("test6");
                Spawn();
            }
            yield return new WaitForSeconds(timerForSpawn);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        SetSpawnerPlace();
#endif
    }

    private void SetSpawnerPlace()
    {
        if (spriteRendererObject != null && useSpriteRenderer)
        {
            if (spriteRendererObject.transform.localScale != new Vector3(radiusOfCircle, radiusOfCircle, radiusOfCircle))
            {
                spriteRendererObject.transform.localScale = new Vector3(radiusOfCircle, radiusOfCircle, radiusOfCircle);
            }

            if (spriteRendererObject.transform.position != this.transform.position + Vector3.up * 0.01f)
            {
                spriteRendererObject.transform.position = this.transform.position + Vector3.up * 0.01f;
            }
        }
    }

    private void Spawn()
    {
        float angle = Random.value * 360;

        Vector2 randomPos = Random.insideUnitCircle * radiusOfCircle;

        float YOffset = spawnDirection == SpawnDirection.XZ ? 0f : Random.RandomRange(-1f * radiusOfCircle, radiusOfCircle);

        Vector3 pos = new Vector3(
            this.transform.position.x+ randomPos.x,
            this.transform.position.y + YOffset,
            this.transform.position.z + randomPos.y
            );

        for(int a=0; a<3; a++)
        {
            Collider[] colliders = Physics.OverlapSphere(pos, 1f);

            if (colliders.Length == 0 || a == 2)
            {
                break;
            }
            else
            {
                randomPos = Random.insideUnitCircle * radiusOfCircle;
                YOffset = spawnDirection == SpawnDirection.XZ ? 0f : Random.RandomRange(-1f*radiusOfCircle, radiusOfCircle);

                pos = new Vector3(
                    this.transform.position.x + randomPos.x,
                    this.transform.position.y + YOffset,
                    this.transform.position.z + randomPos.y
                    );
            }
        }

        GameObject temp = Instantiate<GameObject>(metalObj, pos, Quaternion.Euler(new Vector3(0f, Random.Range(0, 360f), 0f)), this.transform);
        if (temp.TryGetComponent<Collectable>(out Collectable _coll))
        {
            _coll.SetScale(spawnScaleSize);
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if(spawnDirection == SpawnDirection.XZ)
        {
            Handles.DrawWireDisc(this.transform.position, Vector3.up, radiusOfCircle, 1f);
        }
        else if(spawnDirection == SpawnDirection.XYZ)
        {
            Gizmos.DrawWireSphere(this.transform.position, radiusOfCircle);
        }
#else
#endif
    }
}
