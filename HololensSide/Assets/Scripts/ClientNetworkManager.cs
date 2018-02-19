using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientNetworkManager : MonoBehaviour {

    private static readonly int NETWORK_PORT = 4875;
    private static readonly string NETWORK_IP_ADDRESS = "127.0.0.1";

    private NetworkClient client;
    private ClientStatus status;

    private Transform player;

    void Start()
    {
        status = ClientStatus.DISCONNECTED;

        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (status == ClientStatus.DISCONNECTED)
            SetupClient();
    }

    public void SetupClient()
    {
        client = new NetworkClient();
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.RegisterHandler(VirtualHausMessageTypes.APPARTMENT_LOADING, LoadAppartment);
        client.RegisterHandler(VirtualHausMessageTypes.FURNITURE_LOADING, LoadFurnitures);

        client.RegisterHandler(VirtualHausMessageTypes.USER_POSITION, UpdateUserPosition);

        client.Connect(NETWORK_IP_ADDRESS, NETWORK_PORT);
        status = ClientStatus.CONNECTING;
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        client.Send(VirtualHausMessageTypes.CONNECTED, new EmptyMessage());
        status = ClientStatus.CONNECTED;
    }
    public void LoadAppartment(NetworkMessage netMsg)
    {
        AppartmentLoadingMessage msg = netMsg.ReadMessage<AppartmentLoadingMessage>();

        GameObject appartment = Instantiate(Resources.Load("Appartments/MozartHaus/MozartHaus")) as GameObject;
        appartment.transform.position = msg.appartmentPosition;
        appartment.transform.localScale = msg.appartmentScale;

        client.Send(VirtualHausMessageTypes.APPARTMENT_LOADED, new EmptyMessage());
        status = ClientStatus.APPARTMENT_LOADED;
    }

    public void LoadFurnitures(NetworkMessage netMsg)
    {
        Debug.Log("receive furnitures");
        FurnitureLoadingMessage msg = netMsg.ReadMessage<FurnitureLoadingMessage>();

        PlaceFurniture(JsonUtility.FromJson<List<SavedGameObject>>(msg.jsonFurnitures));

        client.Send(VirtualHausMessageTypes.USER_READY, new EmptyMessage());
        status = ClientStatus.READY;
    }
    private void PlaceFurniture(List<SavedGameObject> furnitures)
    {
        //TODO: Implement it
    }

    private void UpdateUserPosition(NetworkMessage netMsg)
    {
        UserPositionMessage msg = netMsg.ReadMessage<UserPositionMessage>();

        player.position = msg.userPosition;
        player.rotation = msg.userRotation;
    }


    public enum ClientStatus
    {
        DISCONNECTED,
        CONNECTING,
        CONNECTED,
        APPARTMENT_LOADED,
        READY
    }
}
