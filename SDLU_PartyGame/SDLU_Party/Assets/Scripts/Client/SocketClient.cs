using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SocketClient : MonoSingleton<SocketClient>
{
    private WebSocket ws;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Debug.LogWarning("## DEBUG START FUNCTION AT SocketClient ##");
        // Connect();
    }


    /// <summary>
    /// 서버와 연결함
    /// </summary>
    /// <param name="ip">IP</param>
    /// <param name="port">PORT</param>
    public void     Connect(string ip = "183.101.5.142", ushort port = 32000)
    {
        if(ws != null && ws.IsAlive)
        {
            Debug.Log("이미 서버에 연결되어 있습니다.");
            return;
        }


        ws = new WebSocket($"ws://{ip}:{port}");

        try
        {
            ws.Connect();

            ws.OnMessage += (s, e) => {
                RecvData((WebSocket)s, e);
            };

        }
        catch(System.Exception e)
        {
            Debug.LogError($"서버에 연결하는 중 오류가 발생했어요.\r\n{e.Message}"); 
        }
    }

    private void RecvData(WebSocket socket, MessageEventArgs message)
    {
        BufferHandler.Instance.Handle(message.Data);
    }


    /// <summary>
    /// 서버와 연결을 끊음
    /// </summary>
    public void Disconnect()
    {
        ws.Close();
        ws = default(WebSocket);
    }

    /// <summary>
    /// DataVO 를 Json 으로 변환 후 보냄
    /// </summary>
    /// <param name="vo">DataVO</param>
    public void Send(DataVO vo)
    {
        try
        {
            ws.Send(JsonUtility.ToJson(vo));
        }
        catch(System.Exception ex)
        {
            Debug.LogError($"서버에 메세지를 보내는 중 오류가 발생했어요.\r\n{ex.Message}\r\n{ex.StackTrace}");
        }
    }

    private void OnDestroy()
    {
        try {
            Disconnect();
        } catch { }
    }
}
