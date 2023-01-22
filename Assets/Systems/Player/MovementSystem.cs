using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementSystem : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private GameManager gameManager;

    private PlaneSystem planeSystem => PlayerSystem.Instance.planeSystem;

    private float angularTime => PlayerSystem.Instance.selectedActor.ActorData.angularSpeed;

    public void Warp(Vector3 targetPos)
    {
        agent.Warp(targetPos);
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if(PlayerSystem.Instance.currentActorType == ActorType.Plane)
        {
            if (agent.enabled == true)
                agent.enabled = false;

            planeSystem.movementSystem.Move();
            planeSystem.rotationSystem.Rotation();
            planeSystem.rotationSystem.PropellerRotation();
            return;
        }

        if (agent.enabled == false)
            agent.enabled = true;

        if (agent.speed != PlayerSystem.Instance.selectedActor.ActorData.speed)
            agent.speed = PlayerSystem.Instance.selectedActor.ActorData.speed;

        if (gameManager.GetJoystickDirection().magnitude != 0f)
        {
            Vector3 dir = new Vector3(gameManager.GetJoystickDirection().x, 0f, gameManager.GetJoystickDirection().y);
            /*
            // DOT
            float dot = 1;
            if(PlayerSystem.Instance.selectedActor.ActorData.actorType == ActorType.Human)
            {
                dot = 1;
            }
            else
            {
                dot = Mathf.Clamp(Mathf.Abs(Vector3.Dot(this.transform.forward, dir)), 0.5f, 1f);
            }
            // DOT
            */
            Vector3 dest = Vector3.Lerp(Vector3.zero, dir * agent.speed, Time.deltaTime);

            agent.Move(dest);


            this.transform.forward = Vector3.Lerp(this.transform.forward, gameManager.GetJoystickDirection().x * Vector3.right + gameManager.GetJoystickDirection().y * Vector3.forward, Time.deltaTime / angularTime);
        }


        AnimSystem.Instance.SetSpeed(gameManager.GetJoystickDirection().magnitude);
    }
}
