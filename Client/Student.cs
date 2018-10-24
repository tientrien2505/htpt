//---------------------------------------- lớp đối tượng trao đổi qua lại Student.cs ----------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    [Serializable]
    class Student
    {
        protected int id;
        protected string name;
        protected string lop;
        protected float[] diem = new float[3];
        protected float diemTB;

        public Student()
        {
            this.name = "";
            this.lop = "";
            for (int i = 0; i < 3; i++)
                this.diem[i] = 0;
            this.diemTB = 0;
        }
        public Student(string name, string lop, float[] diem, float diemTB)
        {            
            this.name = name;
            this.lop = lop;
            for (int i = 0; i < 3; i++)
            {
                this.diem[i] = diem[i];
            }
            this.diemTB = diemTB;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public string Lop
        {
            get { return lop; }
            set { lop = value; }
        }
        
        public float[] Diem
        {
            get { return diem; }
            set { diem = value; }
        }
        
        public float DiemTB
        {
            get { return diemTB; }
            set { diemTB = value; }
        }


    }
}
