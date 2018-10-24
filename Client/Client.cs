//--------------------------------Client.cs project Client ----------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class Client : Form
    {
        Connect connect = null;
        Student st = new Student();
        public Client()
        {
            InitializeComponent();
            connect = new Connect();
            updateDiemTb();
        }
        public void updateDiemTb()
        {
            double d = (st.Diem[0]+st.Diem[1]+st.Diem[2])/3;
            st.DiemTB = (float)Math.Round(d,2);
            textBoxDiemTb.Text = st.DiemTB.ToString();
        }

        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            connect.disConn();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            st.Name = textBoxName.Text;
            st.Lop = textBoxLop.Text;
            if (st.Name == "" || st.Lop == "")
            {
                MessageBox.Show("nhập đầy đủ thông tin");
                return;
            }
            connect.sendStudent(st);
            //MessageBox.Show("Client: đã gửi tên: " + st.Name + "\nlớp: " + st.Lop + "\nđiểm 1: " + st.Diem[0] + "\nđiểm 2: " + st.Diem[1] + "\nđiểm 3: " + st.Diem[2] + "\nđiểm tb: " + st.DiemTB);
        }

        private void comboBoxDiem_SelectedIndexChanged(object sender, EventArgs e)
        {                        
            string diem = comboBoxDiem.SelectedItem.ToString();            
            if (diem=="Điểm 1")
            {                
                textBoxDiem.Text = st.Diem[0].ToString();
            }
            else if (diem=="Điểm 2")
            {
                textBoxDiem.Text = st.Diem[1].ToString();
            }
            else if (diem=="Điểm 3")
            {
                textBoxDiem.Text = st.Diem[2].ToString();
            }            
        }

        private void textBoxDiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string diem = textBoxDiem.Text;
                float d = float.Parse(diem);
                string dauDiem = comboBoxDiem.SelectedItem.ToString();
                if (dauDiem == "Điểm 1")
                {
                    st.Diem[0] = d;
                }
                else if (dauDiem == "Điểm 2")
                {
                    st.Diem[1] = d;
                }
                else if (dauDiem == "Điểm 3")
                {
                    st.Diem[2] = d;
                }
                updateDiemTb();
            }
            catch
            {
                MessageBox.Show("nhập sai dữ liệu");
            }
        }

        private void textBoxDiem_Click(object sender, EventArgs e)
        {
            textBoxDiem.SelectAll();
        }     
    }
}
