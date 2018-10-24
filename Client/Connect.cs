using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;

namespace Client
{
    class Connect
    {
        private IPEndPoint iep = null;
        private Socket client = null;      

        public Connect()
        {
            iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            conn();
        }
        // connect
        public void conn()
        {
            try
            {
                client.Connect(iep);
            }
            catch
            {
                MessageBox.Show("Client: không thể kết nối đến server");
            }
        }
        // disconnect
        public void disConn()
        {
            client.Close();
        }
        // send data
        public void sendStudent(Student s)
        {            
            byte[] data = serialize(s);
            client.Send(data);            
            MessageBox.Show("Client: gửi yêu cầu thành công");
            Student st = (Student)deserialize(data);
            //MessageBox.Show("Client: đã gửi tên: " + st.Name + "\nlớp: " + st.Lop + "\nđiểm 1: " + st.Diem[0] + "\nđiểm 2: " + st.Diem[1] + "\nđiểm 3: " + st.Diem[2] + "\nđiểm tb: " + st.DiemTB);
            byte[] d = new byte[1024];
            client.Receive(d);
            st = (Student)deserialize(d);
            MessageBox.Show("Client: đã gửi tên: " + st.Name + "\nlớp: " + st.Lop + "\nđiểm 1: " + st.Diem[0] + "\nđiểm 2: " + st.Diem[1] + "\nđiểm 3: " + st.Diem[2] + "\nđiểm tb: " + st.DiemTB);
        }
        // receive data
        public Student receiveStudent()
        {            
            byte[] data = new byte[1024];
            client.Receive(data);
            return (Student)deserialize(data);
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
