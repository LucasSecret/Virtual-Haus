using UnityEngine;

public class HomeUIHandler : MonoBehaviour
{
    private readonly Vector3 MOZART_HAUS_SPAWN = new Vector3(-2, 0.5f, -3.5f);

    private static Vector2 MOZART_HAUS_MENU_BUTTON_POSITION;
    private static Vector2 APPARTEMENTS_MENU_BUTTON_POSITION;
    private static Vector2 PARAMETRES_MENU_BUTTON_POSITION;

    private RayCast rayCast;
    private InputManager inputManager;
    private GameObject player;
    public GameObject selecter;

    public void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        player = GameObject.Find("Player");
        selecter = GameObject.Find("Selecter");

        MOZART_HAUS_MENU_BUTTON_POSITION = 
            GameObject.Find("MozartHausMenuButton").GetComponent<RectTransform>().anchoredPosition;
        APPARTEMENTS_MENU_BUTTON_POSITION = 
            GameObject.Find("AppartementsMenuButton").GetComponent<RectTransform>().anchoredPosition;
        PARAMETRES_MENU_BUTTON_POSITION = 
            GameObject.Find("ParametresMenuButton").GetComponent<RectTransform>().anchoredPosition;
        ;
    }

    public void Update()
    {
        selecter.SetActive(false);

        if (rayCast.Hit())
        {
            if (rayCast.GetHit().transform.name == "MozartHausMenuButton")
            {
                selecter.SetActive(true);
                selecter.GetComponent<RectTransform>().anchoredPosition = MOZART_HAUS_MENU_BUTTON_POSITION;
                
                if (inputManager.IsTriggerClicked())
                {
                    player.transform.position = MOZART_HAUS_SPAWN;
                }
            }
            else if (rayCast.GetHit().transform.name == "AppartementsMenuButton")
            {
                selecter.SetActive(true);
                selecter.GetComponent<RectTransform>().anchoredPosition = APPARTEMENTS_MENU_BUTTON_POSITION;

                //todo implement function when IsTriggerClicked() == true
            }
            else if (rayCast.GetHit().transform.name == "ParametresMenuButton")
            {
                selecter.SetActive(true);
                selecter.GetComponent<RectTransform>().anchoredPosition = PARAMETRES_MENU_BUTTON_POSITION;
                
                //todo implement function when IsTriggerClicked() == true
            }
        }
    }
}
