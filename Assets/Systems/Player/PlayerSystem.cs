using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSystem : MonoBehaviour
{
    public AnimSystem animSystem;
    public Stash stashSystem;
    public Collector collectorSystem;
    public MovementSystem movementSystem;
    public ActorType currentActorType;
    public PlaneSystem planeSystem;
    public NavMeshAgent agent;

    public List<GameObject> allActors;
    public Actor selectedActor;

    private BoxCollider boxCollider;

    private int activeActorIndex = -1;

    public static PlayerSystem Instance;


    private void Awake()
    {
        PlayerSystem.Instance = this;
        boxCollider = this.transform.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        if(agent==null)
        {
            if(TryGetComponent<NavMeshAgent>(out NavMeshAgent nma))
            {
                agent = nma;
            }
        }
    }

    public void Warp(Vector3 targetPos)
    {
        movementSystem.Warp(targetPos);
    }

    public void SetActor(int index = 0)
    {
        if (index == activeActorIndex)
            return;

        stashSystem.ClearStash();

        index = (index < 0) ? 0 : (index >= allActors.Count) ? allActors.Count - 1 : index;

        for (int a = 0; a < allActors.Count; a++)
        {
            allActors[a].SetActive((a != index) ? false : true);

            if (a == index)
                selectedActor = allActors[a].GetComponent<Actor>();
        }

        currentActorType = selectedActor.ActorData.actorType;

        boxCollider.center = selectedActor.ActorData.collectorColliderCenter;
        boxCollider.size = selectedActor.ActorData.collectorColliderSize;

        GameManager.Instance.SetUIForVehicles();
    }

    public void SetActor(ActorType actorType)
    {
        int val = 0;
        for (int a = 0; a < allActors.Count; a++)
        {
            if (allActors[a].GetComponent<Actor>().ActorData.actorType == actorType)
                val = a;
        }

        if (actorType != ActorType.Plane)
            agent.enabled = true;

        SetActor(val);
    }


}
