using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

//#if !UNITY_BUILD
//using Quobject.SocketIoClientDotNet.Client;
//#endif


public class Conversation : MonoBehaviour {
    public GameObject Conversations;

    private string converstaionText { get; set; }
    private string addedString { get; set; }

    private int sourcePressCounter { get; set; }

	// Use this for initialization
	void Start () {
        converstaionText = "Initializing";
        Conversations.GetComponent<TextMesh>().text = converstaionText;
        InteractionManager.InteractionSourcePressed += SourcePressed;


//#if !UNITY_BUILD
//        SetSocket();
//#endif
    }

    private void SourcePressed(InteractionSourcePressedEventArgs obj)
    {
        this.sourcePressCounter = this.sourcePressCounter + 1;
//#if !UNITY_BUILD
//        this.socket.Emit("chat", "counter: " + this.sourcePressCounter);
//#endif
    }

    // Update is called once per frame
    void Update () {
        Conversations.GetComponent<TextMesh>().text = converstaionText;
    }

//#if !UNITY_BUILD
//    private Socket socket;
//    private void SetSocket()
//    {
//        this.socket = IO.Socket("http://10.104.54.88:3000"); // Your Socket server URL please!
//        socket.On(Socket.EVENT_CONNECT, () =>
//        {
//            converstaionText = "Connected";
//        });
//        socket.On("chat", async (data) => 
//        {
//            converationText = conversationText + "\n" + data;
//        });
//
//    }
//
//#endif

}
