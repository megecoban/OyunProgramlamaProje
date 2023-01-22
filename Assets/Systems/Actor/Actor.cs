using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private ActorSO actorData;
    public ActorSO ActorData
    {
        get
        {
            return actorData;
        }
    }
}
