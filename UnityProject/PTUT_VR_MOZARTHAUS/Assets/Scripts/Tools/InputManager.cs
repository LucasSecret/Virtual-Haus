using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private static readonly string MOD_CHANGE_BUTTON_NAME = "ChangeModButton";
    private static readonly string CLICKED_TRIGGER_NAME = "PointerTrigger";
    private static readonly string MENU_TRACKPAD_X_NAME = "MenuControllerTrackpadX";
    private static readonly string MENU_TRACKPAD_Y_NAME = "MenuControllerTrackpadY";

    public bool IsModChangeButtonClicked()
    {
        return Input.GetButtonDown(MOD_CHANGE_BUTTON_NAME);
    }
    public bool IsTriggerClicked()
    {
        return (Input.GetAxis(CLICKED_TRIGGER_NAME) == 1 || Input.GetButton(CLICKED_TRIGGER_NAME));
    }
    public Vector2 GetMenuTrackpadPos()
    {
        return new Vector2(Input.GetAxis(MENU_TRACKPAD_X_NAME), Input.GetAxis(MENU_TRACKPAD_Y_NAME));
    }
}
