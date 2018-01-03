﻿using System;
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

    private float scrollViewHeight;
    private float rightSideHeight;
    private float leftPartUIItemHeight;
    private float rightPartUIItemHeight;

    void Start() {
		scrollViewHeight = scrollView.GetComponent<RectTransform>().rect.height;
        rightSideHeight = rightSide.GetComponent<RectTransform>().rect.height;

        leftPartUIItemHeight = leftPartUIItem.GetComponent<RectTransform>().rect.height;
        rightPartUIItemHeight = rightPartUIItem.GetComponent<RectTransform>().rect.height;

        CreateUI();
    }


    void Update()
    {
        Scroll();
    }

    private void Scroll()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (rightSide.GetComponent<RectTransform>().anchoredPosition.y > 0)
            {
                Vector2 pos = rightSide.GetComponent<RectTransform>().anchoredPosition;
                pos.y -= 0.1f;

                if (pos.y < 0)
                    pos.y = 0;

                rightSide.GetComponent<RectTransform>().anchoredPosition = pos;
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
            }

            temp.GetComponentInChildren<Text>().text = room.GetChild(i).name;
        }
    }
}