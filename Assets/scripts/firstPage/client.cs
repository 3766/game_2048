using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Text;
// namespace Client{
public class client: MonoBehaviour
{
    //创建1个客户端套接字和1个负责监听服务端请求的线程  
    static Thread ThreadClient = null;
    static Thread ThreadSend = null;

    static Socket SocketClient = null;
    public static string returnValue = null;
    
    // static void Main(string[] args)
    static client()
    {
        try
        {
            int port = 8002;
            string host = "114.55.27.162";//服务器端ip地址
            // string host = "127.0.0.1";
 
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
 
            //定义一个套接字监听  
            SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
 
            try
            {
                //客户端套接字连接到网络节点上，用的是Connect  
                SocketClient.Connect(ipe);
            }
            catch (Exception)
            {
                Console.WriteLine("连接失败！\r\n");
                Console.ReadLine();
                return;
            }
 
            ThreadClient = new Thread(Recv);
            ThreadClient.IsBackground = true;
            ThreadClient.Start();
            // 每隔10秒发送一次当前分数
            ThreadClient = new Thread(Send);
            ThreadClient.IsBackground = true;
            ThreadClient.Start();
 
            // Thread.Sleep(1000);
            // Console.WriteLine("请输入内容<按Enter键发送>：\r\n");
            // while(true)
            // {
            //     string sendStr = Console.ReadLine();
            //     ClientSendMsg(sendStr);
            // }
 
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
    }
 
    public static void init(){
        try
        {
            int port = 8002;
            string host = "114.55.27.162";//服务器端ip地址
            // string host = "127.0.0.1";
 
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
 
            //定义一个套接字监听  
            SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
 
            try
            {
                //客户端套接字连接到网络节点上，用的是Connect  
                SocketClient.Connect(ipe);
            }
            catch (Exception)
            {
                Console.WriteLine("连接失败！\r\n");
                Console.ReadLine();
                return;
            }
 
            ThreadClient = new Thread(Recv);
            ThreadClient.IsBackground = true;
            ThreadClient.Start();
 
            // Thread.Sleep(1000);
            // Console.WriteLine("请输入内容<按Enter键发送>：\r\n");
            // while(true)
            // {
            //     string sendStr = Console.ReadLine();
            //     ClientSendMsg(sendStr);
            // }
 
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
    }
    public static void Send(){
        while (true){
            Thread.Sleep(10000);
            string msg=null;
            for(int i=0;i<3;i++){
                msg += "nowscore:"+staticCreate.playScore[i].ToString()+";";
            }
            // msg=msg.Remove(msg.Length-1);
            ClientSendMsg(msg);
        }
    }
    //接收服务端发来信息的方法    
    public static void Recv()
    {
        int x = 0;
        //持续监听服务端发来的消息 
        while (true)
        {
            try
            {
                //定义一个1M的内存缓冲区，用于临时性存储接收到的消息  
                byte[] arrRecvmsg = new byte[1024 * 1024];
 
                //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
                int length = SocketClient.Receive(arrRecvmsg);
 
                //将套接字获取到的字符数组转换为人可以看懂的字符串  
                string strRevMsg = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
                if(strRevMsg=="password is true"){
                    returnValue="true";
                }
                else if(strRevMsg.StartsWith("score:")){
                    // 3个分号防止粘包
                    string[] split = strRevMsg.Split(new char[]{';'},4);
                    string []score=new string[]{"","",""};
                    for(int i=0;i<3;i++){
                        // 将获取的分数存储到用户静态变量中
                        score=split[i].Split(new char[]{':'},2);
                        int.TryParse(score[1],out User.highScore[i]);
                    }
                }
                else if(strRevMsg.StartsWith("ranking")){
                    // ranking:6;2;2001:123456;
                    string [] split = strRevMsg.Split(new char[]{';'},3);
                    string [] level = split[0].Split(new char[]{':'},2);
                    int mark=0;
                    int.TryParse(split[1],out mark);
                    string [] scores_names =split[2].Split(new char[]{';'},mark+1);
                    User.rankingScore=new string[mark];
                    User.rankingName=new string[mark];
                    // User.rankingScore =split[2].Split(new char[]{';'},mark+1);
                    for (int i=0;i<mark;i++){
                        string []score_name=scores_names[i].Split(new char[]{':'},2);
                        User.rankingScore[i]=score_name[0];
                        User.rankingName[i]=score_name[1];
                        print("ranking\n");
                        print(User.rankingScore[i]);
                        print(User.rankingName[i]);
                    }
                    

                }
                else{
                    returnValue="false";
                }
                if (x == 1)
                {
                    Console.WriteLine("\r\n服务器：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "\r\n" + strRevMsg+"\r\n");
                        
                }
                else
                {
                    Console.WriteLine(strRevMsg + "\r\n");
                    x = 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("远程服务器已经中断连接！" + ex.Message + "\r\n");
                break;
            }
        }
    }
 
    //发送字符信息到服务端的方法  
    public static void makeIdAndPassword(string username, string password){
        string IdAndPasswordMsg ="id:"+username+" password:"+password;
        ClientSendMsg(IdAndPasswordMsg);
    }
    public static void getScore(){
        ClientSendMsg("score");
        ClientSendMsg("ranking:4;");
    }
    public static void ClientSendMsg(string sendMsg)
    {
        //将输入的内容字符串转换为机器可以识别的字节数组     
        byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg);
        //调用客户端套接字发送字节数组     
        SocketClient.Send(arrClientSendMsg);
    }    
}
// }