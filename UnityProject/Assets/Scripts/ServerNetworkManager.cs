using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        SendPlayerPosUpdate();
    }

    private void SetupServer()
    {
        NetworkServer.RegisterHandler(VirtualHausMessageTypes.CONNECTED, OnClientConnect);
        NetworkServer.RegisterHandler(VirtualHausMessageTypes.APPARTMENT_LOADED, OnClientAppartmentLoaded);
        NetworkServer.RegisterHandler(VirtualHausMessageTypes.USER_READY, OnClientReady);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnClientDisconnect);

        NetworkServer.Listen(NETWORK_PORT);
    }

    public void OnClientDisconnect(NetworkMessage netMsg)
    {
        connectionsReady.Remove(netMsg.conn.connectionId);
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
        NewFurnituresInformations newFurnituresInformations = new NewFurnituresInformations();

        FurnitureLoadingMessage furnitureLoadingMessage = new FurnitureLoadingMessage
        {
            jsonFurnitures = JsonUtility.ToJson(newFurnituresInformations)
        };
        
        NetworkServer.SendToClient(netMsg.conn.connectionId, VirtualHausMessageTypes.FURNITURE_LOADING, furnitureLoadingMessage);
    }
    public void OnClientReady(NetworkMessage netMsg)
    {
        connectionsReady[netMsg.conn.connectionId] = true;
    }

    public void SendFurniturePosUpdate(GameObject furniture)
    {
        NewFurniturePositionMessage furniturePositionMessage = new NewFurniturePositionMessage()
        {
            furnitureName = furniture.transform.name,
            furniturePosition = furniture.transform.position,
            furnitureRotation = furniture.transform.rotation
        };

        SendMessageToAllClientReady(VirtualHausMessageTypes.NEW_FURNITURE_POSITION, furniturePositionMessage);
    }
    private void SendPlayerPosUpdate()
    {
        UserPositionMessage userPositionMessage = new UserPositionMessage()
        {
            userPosition = player.position,
            userRotation = player.rotation
        };
        SendMessageToAllClientReady(VirtualHausMessageTypes.USER_POSITION, userPositionMessage);
    }

    private void SendMessageToAllClientReady(short msgType, MessageBase message)
    {
        foreach (KeyValuePair<int, bool> connection in connectionsReady)
        {
            if (connection.Value == true)
            {
                NetworkServer.SendToClient(connection.Key, msgType, message);
            }
        }
    }
}
