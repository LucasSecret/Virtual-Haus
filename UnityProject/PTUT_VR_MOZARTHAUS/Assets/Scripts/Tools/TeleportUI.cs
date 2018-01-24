using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportUI : MonoBehaviour {

    public GameObject movableUI;
    private GameObject player;
    private RayCast rayCast;
		
	void Start()
    {
        player = GameObject.Find("Player");
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
    }
	
	void Update () {
		
	}

    public void DisplayMovableUIInFrontOfPlayer(GameObject movableUI)
    {
        movableUI.GetComponent<RectTransform>().anchoredPosition3D = player.transform.position;
        movableUI.GetComponent<RectTransform>().LookAt(rayCast.source.transform);
        Quaternion playerRotation = player.transform.rotation;

        Vector3 newpos = player.transform.position + (player.transform.forward - 0.75f * player.transform.right);
        newpos.y = 1.5f;


        Vector3 newScale;
        newScale.x = 0.025f;
        newScale.y = 0.025f;
        newScale.z = 1;

        movableUI.GetComponent<RectTransform>().anchoredPosition3D = newpos;
        movableUI.GetComponent<RectTransform>().transform.position = newpos;
        movableUI.GetComponent<RectTransform>().transform.localScale = newScale;
        movableUI.GetComponent<RectTransform>().transform.rotation = playerRotation;
        print(movableUI.transform.position.ToString());
        print(movableUI.transform.name);
        print(player.transform.position.ToString());
    }
}
