using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class VirtualHausMessageTypes : MsgType
{
    public static short CONNECTED = 1010;
    public static short APPARTMENT_LOADING = 1020;
    public static short APPARTMENT_LOADED = 1021;
    public static short FURNITURE_LOADING = 1030;

    public static short USER_READY = 1040;

    public static short USER_POSITION = 1050;
    public static short NEW_FURNITURE_POSITION = 1070;
}

public class EmptyMessage : MessageBase
{ }
public class UserPositionMessage : MessageBase
{
    public Vector3 userPosition;
    public Quaternion userRotation;
}
public class NewFurniturePositionMessage : MessageBase
{
    public string furnitureName;
    public Vector3 furniturePosition;
    public Quaternion furnitureRotation;
}
public class AppartmentLoadingMessage : MessageBase
{
    public Vector3 appartmentScale;
    public Vector3 appartmentPosition;
    public string modelName;
}
public class FurnitureLoadingMessage : MessageBase
{
    public string jsonFurnitures;
}

[Serializable]
public class NewFurnitureInformations
{
    public string furnitureName;
    public string prefabName;

    public Vector3 furniturePosition;
    public Quaternion furnitureRotation;
}

[Serializable]
public class NewFurnituresInformations
{
    public List<NewFurnitureInformations> furnitures;
}
