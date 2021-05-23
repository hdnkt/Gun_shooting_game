using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    int LOCA_LPORT = 12345;
    static UdpClient udp;
    Thread thread;

    public float power;//弾丸の速さ
    private bool isUpdate = false;//MakeBulletを呼び出すため使う
    private int x, y, playerID, displayID;

    public BulletManager bulletManager1;//以降プレイヤー追加することを想定

    void Start()
    {
        udp = new UdpClient(LOCA_LPORT);
        udp.Client.ReceiveTimeout = 100000;
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
    }

    void Update()
    {
        MakeBullet();
        //transform.position = new Vector3 (transform.position.x,transform.position.y,transform.position.z + 0.1f);
    }

    void OnApplicationQuit()
    {
        thread.Abort();
    }

    private void ThreadMethod()
    {
        
       
        while (true)
        {   
            IPEndPoint remoteEP = null;
            remoteEP = new IPEndPoint(IPAddress.Any, LOCA_LPORT);
            byte[] data = udp.Receive(ref remoteEP);
            string text = Encoding.ASCII.GetString(data);
            string[] datas = text.Split(' ');
            Debug.Log(datas[2]);
            playerID = int.Parse(datas[0]);
            displayID = int.Parse(datas[1]);
            x = int.Parse(datas[2]);
            y = int.Parse(datas[3]);
            isUpdate = true;
           ;
            
        }
    }



    private void MakeBullet() {

        if (Input.GetMouseButtonDown(0))//マウスデバッグ
        {
            displayID = 1;
            bulletManager1.makeBullet(Input.mousePosition.x, Input.mousePosition.y, displayID);
        }
        if (Input.GetMouseButtonDown(1))//マウスデバッグ
        {
            displayID = 2;
            bulletManager1.makeBullet(Input.mousePosition.x, Input.mousePosition.y, displayID);
        }

        if (isUpdate) {//udpが届いたら弾丸生成

            if (playerID == 1)//プレイヤーが増えることを想定
            {
                bulletManager1.makeBullet((float)x, (float)y, displayID);
                isUpdate = false;
            }

        }
    }

    
}