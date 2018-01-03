using UnityEngine;

public class HomeUIHandler : MonoBehaviour
{
    private readonly Vector3 MOZART_HAUS_SPAWN = new Vector3(-2, 0.574f, -3.555f);

    private const string CLICKED_TRIGGER_NAME = "PointerTrigger";
    private bool canClick = true;

    private RayCast rayCast;
    private GameObject player;


    public void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        player = GameObject.Find("Player");
    }

    public void Update()
    {     
        if (IsTriggerClicked() && canClick)
        {
            if (rayCast.Hit())
            {
                if (rayCast.GetHit().transform.name == "MozartHaus")
                {
                    player.transform.position = MOZART_HAUS_SPAWN;
                    canClick = false;
                }
                else if (rayCast.GetHit().transform.name == "Appartment")
                {
                    print("Appartment");
                }
                else if (rayCast.GetHit().transform.name == "SettingsImport")
                {
                    print("SettingsImport");
                }
            }

        }
        else if (!IsTriggerClicked())
        {
            canClick = true;
        }
    }

    private bool IsTriggerClicked()
    {
        return (Input.GetAxis(CLICKED_TRIGGER_NAME) == 1 || Input.GetButton(CLICKED_TRIGGER_NAME));
    }

}
