using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour {

    private RayCast rayCast;
    private InputManager inputManager;
    private SavingManager savingManager;

    private bool canClick;

    void Start() {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        savingManager = GameObject.Find("SavingManager").GetComponent<SavingManager>();

        canClick = true;
    }
	
	void Update() {
        if (inputManager.IsTriggerClicked() && canClick && rayCast.Hit())
        {
            if (rayCast.GetHit().transform.name == "Load")
            {
                string selectedID = "";
                Transform idSelectorUI = transform.GetChild(0).Find("IDSelectorUI");

                for (int i = 0; i < idSelectorUI.childCount; i++)
                {
                    if (idSelectorUI.GetChild(i).name == "LetterSelector")
                    {
                        selectedID += idSelectorUI.GetChild(i).Find("LetterView").GetComponent<Text>().text;
                    }
                }

                savingManager.LoadGameObjects(selectedID); // TODO: Use return to display fail or success message                 
                canClick = false;
            }
        }

        if (!canClick)
        {
            canClick = !inputManager.IsTriggerClicked();
        }
    }
}
