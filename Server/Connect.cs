//------------------------------------- Connect.cs project Server -------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Threading;

namespace Server
{
    class Connect
    {
        private IPEndPoint iep = null;
        private Socket server = null;
        private List<Socket> listSocket = null;

        public Connect()
        {
            try
            {
                iep = new IPEndPoint(IPAddress.Any, 9999);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(iep);
                server.Listen(10);
            }
            catch
            {
                MessageBox.Show("SV.connect: cổng đang bị ứng dụng khác sử dụng");
                return;
            }
            listSocket = new List<Socket>();
            Thread listen = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Socket client = server.Accept();
                        MessageBox.Show("SV.connect: có client kết nối tới");
                        listSocket.Add(client);
                        Thread rec = new Thread(() =>
                        {
                            while (true)
                            {
                                if (!receiveStudent(client))
                                    break;
                            }
                        });
                        rec.IsBackground = true;
                        rec.Start();                        
                    }
                    catch
                    {
                        close();
                    }
                }
            });
            listen.IsBackground = true;
            listen.Start();
        }
        // bắt client
        //public void accept()
        //{
        //    Socket socket = server.AcceptSocket();
        //    listSocket.Add(socket);
        //}
        // đóng server
        public void close()
        {
            foreach (Socket item in listSocket)
                item.Close();
            server.Close();
        }
        // send data
        public void sendStudent(Student st)
        {
            //byte[] data = serialize(st);
            //Stream stream = client.GetStream();
            //stream.Write(data, 0, data.Length);
            //stream.Close();
        }
        // receive data
        public bool receiveStudent(Socket socket)
        {
            try
            {                
                byte[] data = new byte[1024];
                
                socket.Receive(data);

                // test gửi lại chuỗi đã gửi từ client
                socket.Send(data);
                try
                {
                    Object o = deserialize(data);
                    //Student st = (Student)deserialize(data);
                    // kiểm tra chuyển đổi byte sang object Student đúng ko?
                    Student st = new Student();
                    st = (Student)o;
                    MessageBox.Show(st.Name);
                    try
                    {
                        DAO dao = new DAO();
                        dao.add(st);
                        MessageBox.Show("SV.connect: lưu thành công");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("SV.connect: ghi không thành công");
                        Console.WriteLine(e.Message);
                    }
                    
                }
                catch
                {
                    MessageBox.Show("SV.connect: không deserialize");
                }
                return true;
            }
            catch
            {                
                listSocket.Remove(socket);
                socket.Close();
                MessageBox.Show("SV.connect: Không nhận được từ client");
                return false;
            }
        }
        // nen đối tượng thành mảng byte
        public byte[] serialize(Object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);
            return ms.ToArray();
        }
        // giải nén byte thành đối tượng
        public Object deserialize(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            BinaryFormatter bf = new BinaryFormatter();
            ms.Position = 0;
            return bf.Deserialize(ms);
        }
    }
}
