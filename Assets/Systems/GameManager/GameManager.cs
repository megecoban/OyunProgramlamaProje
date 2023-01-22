using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Joystick")]
    [SerializeField] private Joystick activeJoystick;
    [Space]

    [Header("Stashable")]
    [SerializeField] private GameObject stashablePrefab;
    [Space]

    [Header("Spawn From Plane|Yacht")]
    [SerializeField] private Transform groundPos;
    [Space]


    [Header("Plane Movement Area Settings")]
    [SerializeField] private Vector3 maximumBorderPos;
    [SerializeField] private Vector3 minimumBorderPos;

    public Vector3 maximumBorderPositions
    {
        get
        {
            return maximumBorderPos;
        }
    }
    public Vector3 minimumBorderPositions
    {
        get
        {
            return minimumBorderPos;
        }
    }
    [Space]


    [HideInInspector] public static GameManager Instance;

    private bool usingVehicle => PlayerSystem.Instance.currentActorType == ActorType.Human ? false : true;


    [Header("UI Settings")]
    [SerializeField] private GameObject UIForVehicles;
    [Space]

    [Header("Train Area")]
    [SerializeField] GameObject trainArea;
    [Space]

    [Header("Unlocker Areas")]
    [SerializeField] List<GameObject> unlockerAreas;
    [Space]

    [Header("Converter Areas")]
    [SerializeField] List<GameObject> converterAreas;
    [SerializeField] private GameObject carConverterArea;
    [SerializeField] private GameObject humanConverterArea;
    [Space]

    [Header("Application Settings")]
    public int targetFrameRate = 60;

    private void Awake()
    {
        Instance = this;

        if (trainArea != null)
            trainArea.SetActive(false);

        if(unlockerAreas!=null && unlockerAreas.Count > 0)
        {
            for(int a=unlockerAreas.Count-1; a>-1; a--)
            {
                unlockerAreas[a].SetActive(false);
            }
            unlockerAreas[0].SetActive(true);
        }

        if (converterAreas != null && converterAreas.Count > 0)
        {
            for (int a = converterAreas.Count - 1; a > -1; a--)
            {
                converterAreas[a].SetActive(false);
            }
        }
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

    public Vector2 GetJoystickDirection()
    {
        return new Vector2(activeJoystick.Direction.x, activeJoystick.Direction.y);
    }

    public Joystick GetJoystick()
    {
        return activeJoystick;
    }

    public GameObject GetStashablePrefab()
    {
        return stashablePrefab;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Butun PlayerPrefs temizlendi");
        }

        if (PlayerSystem.Instance.currentActorType == ActorType.Car)
        {
            carConverterArea.SetActive(false);
        }
        else
        {
            if(unlockerAreas[0].active == false)
            {
                carConverterArea.SetActive(true);
            }
        }
    }

    public void SetUIForVehicles()
    {
        if (UIForVehicles != null && UIForVehicles.active != usingVehicle)
        {
            UIForVehicles.SetActive(usingVehicle);
        }
    }

    public void ExitToStickman()
    {
        if (PlayerSystem.Instance.currentActorType == ActorType.Car)
        {
            carConverterArea.transform.position = new Vector3(PlayerSystem.Instance.transform.position.x, 0.1f, PlayerSystem.Instance.transform.position.z);
            PlayerSystem.Instance.Warp(PlayerSystem.Instance.transform.position + PlayerSystem.Instance.transform.forward * 4f);

        }
        else
        {
            carConverterArea.transform.position = new Vector3(groundPos.position.x - 10f, 0.1f, groundPos.position.z);
            PlayerSystem.Instance.Warp(groundPos.position);
        }
        carConverterArea.SetActive(true);
        PlayerSystem.Instance.SetActor(ActorType.Human);
    }

    public void TransformPlayerTo(Transform targetPosTransform)
    {
        carConverterArea.SetActive(true);
        carConverterArea.transform.position = new Vector3(groundPos.position.x - 10f, 0.1f, groundPos.position.z);

        PlayerSystem.Instance.Warp(targetPosTransform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((maximumBorderPos + minimumBorderPos) / 2, maximumBorderPos - minimumBorderPos);
    }

}
