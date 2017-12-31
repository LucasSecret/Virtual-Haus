using UnityEngine;

public class InputManager : MonoBehaviour {


    private static readonly string POINTER_TRIGGER_INPUT_NAME = "PointerTrigger";
    private static readonly string SWITCH_MENU_INPUT_NAME = "SwitchMenuButton";

    private bool canClick;
    private int canClickCooldown;
    private bool isPointerTriggerClicked;
    private bool isSwitchMenuButtonClicked;

    void Start () {
        canClick = true;
        SetInputClickedToFalse();
    }

	void Update () {
        isSwitchMenuButtonClicked = Input.GetButtonDown(SWITCH_MENU_INPUT_NAME);
        Debug.Log("Menu Button : " + isSwitchMenuButtonClicked);

        if (canClick)
        {
            isPointerTriggerClicked = (Input.GetAxis(POINTER_TRIGGER_INPUT_NAME) == 1) || (Input.GetButton(POINTER_TRIGGER_INPUT_NAME));

            canClick = false;
            canClickCooldown = 0;
        }
        else
        {
            SetInputClickedToFalse();

            canClickCooldown++;
            if (canClickCooldown > 20)
            {
                canClickCooldown = 0;
                canClick = true;
            }

            canClick = !(Input.GetAxis(POINTER_TRIGGER_INPUT_NAME) == 1) || (Input.GetButton(POINTER_TRIGGER_INPUT_NAME));
            // canClick &= !(Input.get(SWITCH_MENU_INPUT_NAME));
        }
	}

    private void SetInputClickedToFalse()
    {
        isPointerTriggerClicked = false;
        isSwitchMenuButtonClicked = false;
    }

    public bool IsPointerTriggerClicked()
    {
        return isPointerTriggerClicked;
    }
    public bool IsSwitchMenuButtonClicked()
    {
        return isSwitchMenuButtonClicked;
    }
}
