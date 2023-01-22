using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float speedOfRotate = 20f;
    [SerializeField][Min(0.1f)] private float timeForPopUp = 0.5f;
    private float timerForPopUp = 0.5f;
    private Vector3 metalScale = Vector3.one;

    private void Start()
    {
        timerForPopUp = timeForPopUp;
        StartCoroutine("SpawningPopUp");
        StartCoroutine("Rotate");
    }

    public void SetScale(Vector3 _scale)
    {
        metalScale = _scale;
    }

    public void Collect()
    {
        GameObject stashable = Instantiate(GameManager.Instance.GetStashablePrefab(), null);
        stashable.transform.position = this.transform.position + this.transform.up * this.transform.localScale.y * 0.5f;
        stashable.transform.localScale = metalScale;
        if (stashable.TryGetComponent<Stashable>(out Stashable _stashable))
        {
            _stashable.GoToStash(Stash.Instance.Reserve());
        }
        else
        {
            stashable.AddComponent<Stashable>();
            stashable.GetComponent<Stashable>().GoToStash(Stash.Instance.Reserve());
        }
        this.transform.GetComponent<Collider>().enabled = false;
        Destroy(this.gameObject, Time.deltaTime);
    }


    IEnumerator Rotate()
    {
        while(true)
        {
            this.transform.Rotate(this.transform.up * speedOfRotate);
            yield return null;
        }
    }

    IEnumerator SpawningPopUp()
    {
        while (timerForPopUp > 0f)
        {
            this.transform.localScale = Vector3.Lerp(Vector3.zero, metalScale, (timeForPopUp-timerForPopUp)/timeForPopUp);
            timerForPopUp -= Time.deltaTime;
            yield return null;
        }

        this.transform.localScale = metalScale;
    }
}
