using UnityEngine;

public class HomeUIHandler : MonoBehaviour
{
    private readonly Vector3 MOZART_HAUS_SPAWN = new Vector3(-2, 0.5f, -3.5f);

    private RayCast rayCast;
    private InputManager inputManager;
    private GameObject player;


    public void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        player = GameObject.Find("Player");
    }

    public void Update()
    {     
        if (inputManager.IsTriggerClicked() && rayCast.Hit())
        {
            if (rayCast.GetHit().transform.name == "Mozart'Haus")
            {
                player.transform.position = MOZART_HAUS_SPAWN;
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
}
