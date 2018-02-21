using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public static class ThumbnailsHandler
{
    public static string thumbnailsPath = Application.dataPath + "/UIComponents/Thumbnails/";
    private static readonly int SIZE = 512;

    public static void CreateThumbnailsIfNotExist(GameObject furnitures)
    {
        CleanThumbnailsFolder();
        for (int i = 0; i < furnitures.transform.childCount; i++)
        {
            CreateThumbnailsIfNotExistForRoom(furnitures, i);
        }
    }

    private static void CreateThumbnailsIfNotExistForRoom(GameObject furnitures, int roomIndex)
    {
        Transform room = furnitures.transform.GetChild(roomIndex);
        int furnituresQuantity = room.childCount;

        for (int i = 0; i < furnituresQuantity; i++)
        {
            if (FurnitureThumbnailsExist(room.GetChild(i).name))
            {
                continue;
            }

            GameObject furnitureToRender = UnityEngine.Object.Instantiate(room.GetChild(i).gameObject);
            GameObject cameraGameObject = new GameObject();

            Camera camera = cameraGameObject.AddComponent<Camera>();
            camera.aspect = 1.0f;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0, 0, 0, 0);

            PlaceGameObjectForScreenShot(furnitureToRender, cameraGameObject);
            
            RenderTexture renderTexture = new RenderTexture(SIZE, SIZE, 24);
            camera.targetTexture = renderTexture;
            camera.Render();

            RenderTexture.active = renderTexture;
            Texture2D virtualPhoto = new Texture2D(SIZE, SIZE, TextureFormat.ARGB32, false);
            virtualPhoto.ReadPixels(new Rect(0, 0, SIZE, SIZE), 0, 0);

            RenderTexture.active = camera.targetTexture = null;

            File.WriteAllBytes(thumbnailsPath + room.GetChild(i).name + ".png", virtualPhoto.EncodeToPNG());

            UnityEngine.Object.DestroyImmediate(furnitureToRender);
            UnityEngine.Object.DestroyImmediate(cameraGameObject);
            UnityEngine.Object.DestroyImmediate(renderTexture);
        }
    }

    private static bool FurnitureThumbnailsExist(string name)
    {
        return File.Exists(thumbnailsPath + name + ".png");
    }
    private static void PlaceGameObjectForScreenShot(GameObject furniture, GameObject camera)
    {
        furniture.transform.position = new Vector3(-100, 0);
        Vector3 furnitureSize = furniture.GetComponent<Renderer>().bounds.size;

        float y = furnitureSize.y / 2;
        float z = (furnitureSize.y > furnitureSize.x) ? furnitureSize.y : furnitureSize.x;

        camera.transform.position = new Vector3(-100, y, -z);

        Vector3 lookAt = furniture.transform.position;
        lookAt.y += y;

        camera.transform.LookAt(lookAt);
    }

    private static void CleanThumbnailsFolder()
    {
        Directory.Delete("Assets/UIComponents/Thumbnails", true);
        Directory.CreateDirectory("Assets/UIComponents/Thumbnails");
    }
}