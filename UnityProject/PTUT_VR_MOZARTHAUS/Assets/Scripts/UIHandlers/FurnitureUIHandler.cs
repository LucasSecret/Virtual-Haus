using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureUIHandler : MonoBehaviour {

    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject scrollView;
    public GameObject furnitures;

    public GameObject leftPartUIItem;
    public GameObject rightPartUIItem;


    private InputManager inputManager;
    private ModHandler modHandler;
    private RayCast rayCast;
    private DragFurniture dragFurniture;


    private float scrollViewHeight;
    private float rightSideHeight;
    private float leftPartUIItemHeight;
    private float rightPartUIItemHeight;


    /// <summary>
    /// For Test
    /// </summary>

    private static readonly Vector2 DEFAULT_TRACKPAD_POSITION = new Vector2(0, 0);
    private Vector2 previousMenuTrackpadPosition = DEFAULT_TRACKPAD_POSITION;


    private void TestRotation()
    {
        Vector2 trackpadPos = inputManager.GetMenuTrackpadPos();

        if (trackpadPos == DEFAULT_TRACKPAD_POSITION)
            return;

        if (previousMenuTrackpadPosition == DEFAULT_TRACKPAD_POSITION)
        {
            previousMenuTrackpadPosition = trackpadPos;
            return;
        }

        double rotationAngle = GetTrackpadAngle(trackpadPos);
    }

    private double GetTrackpadAngle(Vector2 trackpadPos)
    {
        return (360 + (Math.Acos(trackpadPos.x) * Mathf.Rad2Deg + Math.Asin(trackpadPos.y) * Mathf.Rad2Deg) / 2) % 360;
    }




    void Start() {
		scrollViewHeight = scrollView.GetComponent<RectTransform>().rect.height;
        rightSideHeight = rightSide.GetComponent<RectTransform>().rect.height;

        leftPartUIItemHeight = leftPartUIItem.GetComponent<RectTransform>().rect.height;
        rightPartUIItemHeight = rightPartUIItem.GetComponent<RectTransform>().rect.height;

        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        modHandler = GameObject.Find("ModHandler").GetComponent<ModHandler>();
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        dragFurniture = GameObject.Find("EditionHandler").GetComponent<DragFurniture>();

        CreateUI();
    }


    void Update()
    {

        TestRotation();

        Scroll();
    }

    private void Scroll()
    {
        if (modHandler.IsInEditionMod() && inputManager.IsTriggerClicked())
        {
            if (rayCast.Hit())
            {
                Transform hitObject = rayCast.GetHit().transform;

                Debug.Log(hitObject.parent);
                if (hitObject.parent == leftSide.transform)
                {
                    UpdateRightUIPart(hitObject.GetSiblingIndex());
                }
                else if (hitObject.parent == rightSide.transform)
                {
                    dragFurniture.SelectObject(GameObject.Find((rayCast.GetHit().transform.GetChild(0).GetComponent<Text>().text)));
                }
            }
        }




        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (rightSide.GetComponent<RectTransform>().anchoredPosition.y > 0)
            {
                Vector2 pos = rightSide.GetComponent<RectTransform>().anchoredPosition;
                pos.y -= 0.1f;

                if (pos.y < 0)
                    pos.y = 0;

                rightSide.GetComponent<RectTransform>().anchoredPosition = pos;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (scrollViewHeight + rightSide.GetComponent<RectTransform>().rect.y < rightSideHeight)
            {
                Vector2 pos = rightSide.GetComponent<RectTransform>().anchoredPosition;
                pos.y += 0.1f;

                if (scrollViewHeight + pos.y > rightSideHeight)
                    pos.y = rightSideHeight - scrollViewHeight;

                rightSide.GetComponent<RectTransform>().anchoredPosition = pos;
            }
        }
    }


    private void CreateUI()
    {
        UpdateLeftUIPart();
        UpdateRightUIPart(0);
    }
    private void UpdateLeftUIPart()
    {
        for (int i = 0; i < furnitures.transform.childCount; i++)
        {
            GameObject temp = Instantiate(leftPartUIItem, leftSide.transform);
            Vector2 position = temp.GetComponent<RectTransform>().anchoredPosition;
            position.y -= leftPartUIItemHeight * i;

            temp.GetComponent<RectTransform>().anchoredPosition = position;
            temp.GetComponentInChildren<Text>().text = furnitures.transform.GetChild(i).name;
        }
    }
    private void UpdateRightUIPart(int index)
    {
        foreach (Transform child in rightSide.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Transform room = furnitures.transform.GetChild(index);
        int furnitureQuantity = room.childCount;

        Vector2 size = rightSide.GetComponent<RectTransform>().sizeDelta;
        rightSideHeight = size.y = furnitureQuantity / 2 + furnitureQuantity % 2;
        rightSide.GetComponent<RectTransform>().sizeDelta = size;

        for (int i = 0; i < furnitureQuantity; i++)
        {
            GameObject temp = Instantiate(rightPartUIItem, rightSide.transform);

            Vector2 position = temp.GetComponent<RectTransform>().anchoredPosition;
            position.y -= rightPartUIItemHeight * ((int) i / 2);
            temp.GetComponent<RectTransform>().anchoredPosition = position;

            if (i % 2 == 1)
            {
                Vector2 pivot = temp.GetComponent<RectTransform>().pivot;
                pivot.x = -1;
                temp.GetComponent<RectTransform>().pivot = pivot;

                Vector3 center = temp.GetComponent<BoxCollider>().center;
                center.x += 1;
                temp.GetComponent<BoxCollider>().center = center;
            }

            temp.GetComponentInChildren<Text>().text = room.GetChild(i).name;
        }
    }
}
