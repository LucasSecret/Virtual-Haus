using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackpadHandler : MonoBehaviour {

    private static readonly string MENU_TRACKPAD_X_NAME = "MenuControllerTrackpadX";
    private static readonly string MENU_TRACKPAD_Y_NAME = "MenuControllerTrackpadY";
    private static readonly string POINTER_TRACKPAD_X_NAME = "PointerControllerTrackpadX";
    private static readonly string POINTER_TRACKPAD_Y_NAME = "PointerControllerTrackpadY";

    private static readonly Vector2 DEFAULT_TRACKPAD_POSITION = new Vector2(0, 0);

    private float previousMenuTrackpadAngle;
    private float pointerTrackpadRotationOffset;

    private float previousPointerTrackpadAngle;
    private float menuTrackpadRotationOffset;

    void Start () {
        previousMenuTrackpadAngle = 0;
        menuTrackpadRotationOffset = 0;

        previousPointerTrackpadAngle = 0;
        pointerTrackpadRotationOffset = 0;
    }
	
	void Update () {
        UpdateTrackpadRotationOffset(GetMenuTrackpadPos(), ref previousMenuTrackpadAngle, ref menuTrackpadRotationOffset);
        UpdateTrackpadRotationOffset(GetPointerTrackpadPos(), ref previousPointerTrackpadAngle, ref pointerTrackpadRotationOffset);
    }

    private void UpdateTrackpadRotationOffset(Vector2 trackpadPos, ref float previousTrackpadAngle, ref float trackpadRotationOffset)
    {
        trackpadRotationOffset = 0;

        if (trackpadPos == DEFAULT_TRACKPAD_POSITION)
        {
            previousTrackpadAngle = 0;
            return;
        }

        float currentRotationAngle = GetTrackpadAngle(trackpadPos);

        if (previousTrackpadAngle == null) // Need at least one previous value to perform calculation
        {
            previousTrackpadAngle = currentRotationAngle;
            return;
        }

        trackpadRotationOffset = (currentRotationAngle - (float)previousMenuTrackpadAngle);
        previousTrackpadAngle = currentRotationAngle;
    }
    private float GetTrackpadAngle(Vector2 trackpadPos)
    {
        trackpadPos.Normalize();
        if (trackpadPos.y <= 0)
        {
            return (Mathf.Acos(trackpadPos.x) * Mathf.Rad2Deg);
        }
        else
        {
            return 360 - (Mathf.Acos(trackpadPos.x) * Mathf.Rad2Deg);
        }
    }

    private Vector2 GetMenuTrackpadPos()
    {
        return new Vector2(Input.GetAxis(MENU_TRACKPAD_X_NAME), Input.GetAxis(MENU_TRACKPAD_Y_NAME));
    }
    private Vector2 GetPointerTrackpadPos()
    {
        return new Vector2(Input.GetAxis(POINTER_TRACKPAD_X_NAME), Input.GetAxis(POINTER_TRACKPAD_Y_NAME));
    }

    // PUBLIC Interface

    public float GetMenuTrackpadRotationOffset()
    {
        return menuTrackpadRotationOffset;
    }
    public float GetPointerTrackpadRotationOffset()
    {
        return pointerTrackpadRotationOffset;
    }
}
