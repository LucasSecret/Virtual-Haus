using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureUIHandler_Trackpad : MonoBehaviour {

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

    private int furnitureQuantity;

    private Vector2Int rightPartUISelectorPosition;
    private int leftPartUIIndex;
    private double scrollStack;

    void Start()
    {
        scrollViewHeight = scrollView.GetComponent<RectTransform>().rect.height;
        rightSideHeight = rightSide.GetComponent<RectTransform>().rect.height;

        leftPartUIItemHeight = leftPartUIItem.GetComponent<RectTransform>().rect.height;
        rightPartUIItemHeight = rightPartUIItem.GetComponent<RectTransform>().rect.height;

        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        modHandler = GameObject.Find("ModHandler").GetComponent<ModHandler>();
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        dragFurniture = GameObject.Find("EditionHandler").GetComponent<DragFurniture>();

        rightPartUISelectorPosition = new Vector2Int(0, 0);
        leftPartUIIndex = 0;
        scrollStack = 0;

        CreateUI();
    }


    void Update()
    {
        Scroll();
        Select();
    }

    private void Scroll()
    {
        scrollStack += inputManager.GetTrackpadHandler().GetMenuTrackpadRotationOffset();
        if (scrollStack >= 3) // Is Value correct ? 
        {
            scrollStack = 0;
            if (rightPartUISelectorPosition.x == 1)
            {
                rightPartUISelectorPosition.x = 0;
                rightPartUISelectorPosition.y++;
            }
            else
            {
                rightPartUISelectorPosition.x++;
            }

            if (rightPartUISelectorPosition.y * 2 - (1 - rightPartUISelectorPosition.x) > furnitureQuantity)
            {
                rightPartUISelectorPosition.x = furnitureQuantity % 2;
                rightPartUISelectorPosition.y = furnitureQuantity / 2 - 1;

                // Update cadre
                // Scroll ui
            }
        }
    }
    private void Select()
    {
        if (inputManager.IsTriggerClicked())
        {
            string furnitureName = rightSide.transform.GetChild(2 * rightPartUISelectorPosition.x + rightPartUISelectorPosition.y).GetComponentInChildren<Text>().text;
            dragFurniture.SelectObject(GameObject.Find(furnitureName));
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
        furnitureQuantity = room.childCount;

        Vector2 size = rightSide.GetComponent<RectTransform>().sizeDelta;
        rightSideHeight = size.y = furnitureQuantity / 2 + furnitureQuantity % 2;
        rightSide.GetComponent<RectTransform>().sizeDelta = size;

        for (int i = 0; i < furnitureQuantity; i++)
        {
            GameObject temp = Instantiate(rightPartUIItem, rightSide.transform);

            Vector2 position = temp.GetComponent<RectTransform>().anchoredPosition;
            position.y -= rightPartUIItemHeight * ((int)i / 2);
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
