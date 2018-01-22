using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private static readonly string MOD_CHANGE_BUTTON_NAME = "ChangeModButton";
    private static readonly string CLICKED_TRIGGER_NAME = "PointerTrigger";

    private static readonly Vector2 DEFAULT_TRACKPAD_POSITION = new Vector2(0, 0);

    private TrackpadHandler trackpadHandler;

    void Update()
    {
        trackpadHandler = GameObject.Find("InputManager").GetComponent<TrackpadHandler>();
    }

    // PUBLIC Interface

    public TrackpadHandler GetTrackpadHandler()
    {
        return trackpadHandler;
    }

    public bool IsModChangeButtonClicked()
    {
        return Input.GetButtonDown(MOD_CHANGE_BUTTON_NAME);
    }
    public bool IsTriggerClicked()
    {
        return (Input.GetAxis(CLICKED_TRIGGER_NAME) == 1 || Input.GetButton(CLICKED_TRIGGER_NAME));
    }
}
