using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainStop : MonoBehaviour
{
    public int collectableStashNumber => TrainStash.Instance.numberOfStash;
    [SerializeField][Min(0.05f)] float timeToCollectOne = 0.1f;
    private float timerForOne = 0.1f;

    private void Start()
    {
        timerForOne = timeToCollectOne;
    }

    public void Collect()
    {
        if (timerForOne <= 0f && TrainMovement.Instance.isStopped)
        {
            if (collectableStashNumber <= 0)
            {
                TrainPlay();
            }

            timerForOne = timeToCollectOne;
            GameObject stashable = Instantiate(GameManager.Instance.GetStashablePrefab(), null);
            stashable.transform.position = TrainStash.Instance.GetStash().transform.position;
            stashable.transform.localScale = Vector3.one;
            if(stashable.TryGetComponent<Stashable>(out Stashable _stashable)){
                _stashable.GoToStash(Stash.Instance.Reserve());
            }
            else
            {
                stashable.AddComponent<Stashable>();
                stashable.GetComponent<Stashable>().GoToStash(Stash.Instance.Reserve());
            }
        }
        else
        {
            timerForOne -= Time.fixedDeltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && TrainMovement.Instance.isStopped)
        {
            timerForOne = 0f;
            if(TrainStash.Instance.isStashMax == false)
            {
                TrainPlay();
            }
        }
    }

    private void TrainPlay()
    {
        TrainMovement.Instance.Play();
    }
}
