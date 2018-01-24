using UnityEngine;

public class HomeUIHandler : MonoBehaviour
{
    private readonly Vector3 MOZART_HAUS_SPAWN = new Vector3(-2, 0, -3.5f);

    private RayCast rayCast;
    private InputManager inputManager;
    private GameObject player;
    private TeleportUI teleportUI;
    private GameObject homeUI;


    public void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        player = GameObject.Find("Player");
        teleportUI = GameObject.Find("UITeleportation").GetComponent<TeleportUI>();
        homeUI = GameObject.Find("MainMenu");
        teleportUI.DisplayMovableUIInFrontOfPlayer(homeUI);



    }

    public void Update()
    {
        teleportUI.DisplayMovableUIInFrontOfPlayer(homeUI);
        if (inputManager.IsTriggerClicked() && rayCast.Hit())
        {
            if (rayCast.GetHit().transform.name == "Mozart'Haus")
            {
                TeleportToMozartHaus();
            }
            else if (rayCast.GetHit().transform.name == "Appartments")
            {
                print("Appartment"); // Not Implemented
            }
            else if (rayCast.GetHit().transform.name == "SettingsImport")
            {
                print("SettingsImport"); // Not Implemented
            }
        }
    }

    /// <summary>
    /// Teleport Player To MozartHaus (ignore y position)
    /// </summary>
    public void TeleportToMozartHaus()
    {
        Vector3 newPlayerPosition = MOZART_HAUS_SPAWN;
        newPlayerPosition.y = player.transform.position.y;

        player.transform.position = newPlayerPosition;
    }
}
