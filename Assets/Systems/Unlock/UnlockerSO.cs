using UnityEngine;

[CreateAssetMenu(fileName = "Unlocker", menuName = "ScriptableObjects/Unlocker")]
public class UnlockerSO : ScriptableObject
{
    private const string UNLOCKER_KEY = "UNLOCKER_KEY_";
    public string unlockItemName = "Car";
    public int price = 10;
    public ActorSO unlockActorSO;

    public int reqMetal => price - collectedMetal;

    public int collectedMetal
    {
        get => PlayerPrefs.GetInt(UNLOCKER_KEY + unlockItemName, 0);
        set => PlayerPrefs.SetInt(UNLOCKER_KEY + unlockItemName, value);
    }

}
