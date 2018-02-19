using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerNetworkManager : MonoBehaviour
{
    public GameObject currentAppartment;
    private Transform player;

    private static readonly int NETWORK_PORT = 4875;
    private Dictionary<int, bool> connectionsReady;

    void Start()
    {
        player = GameObject.Find("Player").transform;

        connectionsReady = new Dictionary<int, bool>();
        SetupServer();
    }

    void Update()
    {
        UserPositionMessage userPositionMessage = new UserPositionMessage()
        {
            userPosition = player.position,
            userRotation = player.rotation
        };

        foreach (KeyValuePair<int, bool> connection in connectionsReady)
        {
            if (connection.Value == true)
            {
                NetworkServer.SendToClient(connection.Key, VirtualHausMessageTypes.USER_POSITION, userPositionMessage);
            }
        }
    }

    public void SetupServer()
    {
        NetworkServer.RegisterHandler(VirtualHausMessageTypes.CONNECTED, OnClientConnect);
        NetworkServer.RegisterHandler(VirtualHausMessageTypes.APPARTMENT_LOADED, OnClientAppartmentLoaded);
        NetworkServer.RegisterHandler(VirtualHausMessageTypes.USER_READY, OnClientReady);

        NetworkServer.Listen(NETWORK_PORT);
    }

    public void OnClientConnect(NetworkMessage netMsg)
    {
        connectionsReady.Add(netMsg.conn.connectionId, false);

        AppartmentLoadingMessage appartmentLoadingMessage = new AppartmentLoadingMessage
        {
            appartmentScale = currentAppartment.transform.localScale,
            appartmentPosition = currentAppartment.transform.position,
            modelName = currentAppartment.transform.name
        };

        NetworkServer.SendToClient(netMsg.conn.connectionId, VirtualHausMessageTypes.APPARTMENT_LOADING, appartmentLoadingMessage);
    }
    public void OnClientAppartmentLoaded(NetworkMessage netMsg)
    {
        List<SavedGameObject> savedGameObjects = new List<SavedGameObject>();
        foreach (GameObject editableGameObject in GameObject.FindGameObjectsWithTag("Furniture"))
        {
            savedGameObjects.Add(new SavedGameObject(editableGameObject.name, editableGameObject.transform));
        }

        FurnitureLoadingMessage furnitureLoadingMessage = new FurnitureLoadingMessage
        {
            jsonFurnitures = JsonUtility.ToJson(savedGameObjects)
        };

        NetworkServer.SendToClient(netMsg.conn.connectionId, VirtualHausMessageTypes.FURNITURE_LOADING, furnitureLoadingMessage);
    }
    public void OnClientReady(NetworkMessage netMsg)
    {
        connectionsReady[netMsg.conn.connectionId] = true;
    }
}
