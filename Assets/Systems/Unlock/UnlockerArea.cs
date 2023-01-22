using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockerArea : MonoBehaviour
{
    [SerializeField] UnlockerSO unlockerData;
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI reqMetalText;
    public bool isUnlocked => (unlockerData.reqMetal <= 0) ? true : false;

    [SerializeField] private List<GameObject> unlockNextThings;
    [SerializeField] private List<GameObject> deleteAfterUnlock;
    [SerializeField] private bool dontConvertToActor = false;

    [SerializeField] private ActorSO unlockTargetActor => unlockerData.unlockActorSO;

    private void Awake()
    {
    }

    private void Start()
    {
        UpdateUI();

        if (unlockerData.reqMetal <= 0)
        {
            Unlock();
        }
    }

    public void UpdateUI()
    {
        if (unlockerData == null)
            return;

        headerText.text = "UNLOCK " + unlockerData.unlockItemName;
        headerText.text = headerText.text.ToUpperInvariant();
        reqMetalText.text = unlockerData.reqMetal.ToString();
    }

    public void DecreaseUI()
    {
        if (unlockerData == null)
            return;

        headerText.text = "UNLOCK " + unlockerData.unlockItemName;
        headerText.text = headerText.text.ToUpperInvariant();
        reqMetalText.text = ( (int.Parse(reqMetalText.text)-1)>= unlockerData.reqMetal ? (int.Parse(reqMetalText.text) - 1) : unlockerData.reqMetal).ToString();

        if (unlockerData.reqMetal <= 0)
        {
            Unlock();
        }
    }

    public void Decrease()
    {
        if (unlockerData.reqMetal <= 0)
            return;

        unlockerData.collectedMetal++;
    }


    public void Unlock()
    {
        if ((int.Parse(reqMetalText.text) - 1) >= unlockerData.reqMetal)
            return;


        if(dontConvertToActor == false)
            PlayerSystem.Instance.SetActor(unlockerData.unlockActorSO.actorType);

        if (unlockNextThings != null && unlockNextThings.Count > 0)
        {
            for(int a=0; a< unlockNextThings.Count; a++)
            {
                unlockNextThings[a].SetActive(true);
            }
        }

        if(deleteAfterUnlock != null)
        {
            for(int a=0; a< deleteAfterUnlock.Count; a++)
            {
                deleteAfterUnlock[a].SetActive(false);
            }
        }
    }
}
