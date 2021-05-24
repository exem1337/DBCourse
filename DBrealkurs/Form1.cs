using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DBrealkurs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); cbs.Add(comboBox1); cbs.Add(comboBox2); cbs.Add(comboBox3); cbs.Add(comboBox4);
        }
       
        string Credentials =
            "Server = localhost;" +
            "Integrated security = SSPI;" +
            "database = elCourseBD;";

        List<ComboBox> cbs = new List<ComboBox>();

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Works works = new Works(Credentials);
            switch (tabControl1.SelectedIndex)
            {
                case 0: { dataGridViewTest.DataSource = works.dataSet("*", "Курс", null).Tables[0].DefaultView; break; }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //Works works = new Works(Credentials);
            //addCourseCourseCB.DataSource = works.dataSet("Фамилия", "Пользователь", null).Tables[0].DefaultView;
            //addCourseCourseCB.DisplayMember = "Пользователь_Фамилия";
        }

        string selectedTable;

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            sa();
        }

        List<string> BufferListUpdate(int Index)
        {
            Works database = new Works(Credentials);
            List<string> Temp = new List<string>();
            switch (Index)
            {
                case 0: // Заполнение курсов
                    dataGridViewListReturner.DataSource = database.ReturnTable("Название", "Курс", null).Tables[0].DefaultView;
                    for (int i = 0; i < dataGridViewListReturner.Rows.Count - 1; i++)
                    {
                        Temp.Add(dataGridViewListReturner.Rows[i].Cells[0].Value.ToString());
                    }
                    break;
                case 1: // пользователей
                    dataGridViewListReturner.DataSource = database.ReturnTable("Фамилия", "Пользователь", null).Tables[0].DefaultView;
                    for (int i = 0; i < dataGridViewListReturner.Rows.Count - 1; i++)
                    {
                        Temp.Add(dataGridViewListReturner.Rows[i].Cells[0].Value.ToString());
                    }
                    break;
                case 2:
                    dataGridViewListReturner.DataSource = database.ReturnTable("Фамилия", "Автор", null).Tables[0].DefaultView;
                    for (int i = 0; i < dataGridViewListReturner.Rows.Count - 1; i++)
                    {
                        Temp.Add(dataGridViewListReturner.Rows[i].Cells[0].Value.ToString());
                    }
                    break;
            } 
            return Temp;
        }

        void ComboUpdates()
        {
            addCourseCourseCB.Items.Clear(); // При добавлении нас.пункта
            addCourseStudentCB.Items.Clear(); 
            comboBox1.Items.Clear(); comboBox2.Items.Clear(); comboBox3.Items.Clear(); comboBox4.Items.Clear();
            foreach (string i in BufferListUpdate(0))
            {
                addCourseCourseCB.Items.Add(i);
            }
            foreach (string i in BufferListUpdate(1))
            {
                addCourseStudentCB.Items.Add(i);
            }
            foreach(string i in BufferListUpdate(2))
            {
                comboBox1.Items.Add(i);
                comboBox2.Items.Add(i);
                comboBox3.Items.Add(i);
                comboBox4.Items.Add(i);
            }
        }

        

        int GetDirCode(string Table, string ToFind, int cell) // Вернуть код (итератор) из справочника
        {
            Works database = new Works(Credentials);
            dataGridViewListReturner.DataSource = database.ReturnTable("*", Table, null).Tables[0].DefaultView;
            for (int i = 0; i < dataGridViewListReturner.Rows.Count - 1; i++)
            {
                if (dataGridViewListReturner.Rows[i].Cells[cell].Value.ToString() == ToFind)
                {
                    return Convert.ToInt32(dataGridViewListReturner.Rows[i].Cells[0].Value);
                }
            }
            return -1;
        }

        void sa ()
        {
            Works works = new Works(Credentials);
            switch (tabControl2.SelectedIndex)
            {
                case 0: BufferListUpdate(0); selectedTable = "Список студентов курса"; ComboUpdates(); dataGridViewTest.DataSource = works.courseAndStudents().Tables[0].DefaultView; dataGridTemp.DataSource = works.dataSet("Пользователь.userID, Курс.ID", "Пользователь, Курс, Список_Студентов_Курса ", "WHERE CourseID = ID AND Список_Студентов_Курса.userID = Пользователь.userID;").Tables[0].DefaultView; break;
                case 2: BufferListUpdate(1); selectedTable = "Пользователь"; dataGridTemp.DataSource = works.dataSet("*", "Пользователь", null).Tables[0].DefaultView; dataGridViewTest.DataSource = works.dataSet("Фамилия, Имя, Отчество", "Пользователь", null).Tables[0].DefaultView; break;
                case 3: BufferListUpdate(2); selectedTable = "Автор"; dataGridTemp.DataSource = works.dataSet("*", "Автор", null).Tables[0].DefaultView; dataGridViewTest.DataSource = works.dataSet("Фамилия, Имя, Отчество", "Автор", null).Tables[0].DefaultView; break;
                case 1: selectedTable = "Курс"; dataGridViewTest.DataSource = works.dataSet("Название, Продолжительность, [Требуемый уровень], [Дата начала], [Дата окончания]", "Курс", null).Tables[0].DefaultView; dataGridTemp.DataSource = works.dataSet("*", "Курс", null).Tables[0].DefaultView; ComboUpdates(); break;
            }
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Works works = new Works(Credentials);
            switch (tabControl3.SelectedIndex)
            {
                case 0: break;
                case 1: dataGridViewTest.DataSource = works.dataSet("*", "Список_Студентов_Курса", null).Tables[0].DefaultView; break;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = false; comboBox2.Visible = false; comboBox3.Visible = false; comboBox4.Visible = false;
            for(int i = 0; i < numericUpDown1.Value; i++)
            {
                cbs[i].Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Works database = new Works(Credentials);
            listBox1.Items.Add(database.newAuthor(authorLastNameTB.Text, authorNameTB.Text, authorSurNameTB.Text));
        }

        

        private void sqlExecuteBTN_Click(object sender, EventArgs e)
        {
            Works database = new Works(Credentials);
            listBox1.Items.Add(database.sqlExecute(sqlTB.Text));
            dataGridViewListReturner.DataSource = database.ReturnQuery(sqlTB.Text).Tables[0].DefaultView;
            sqlTB.Text = String.Empty;
        }

        private void tabControl4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        int tempeID = - 1; int tempStudID = - 1;
        private void dataGridViewTest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewTest.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tempeID = Convert.ToInt32(dataGridTemp.Rows[e.RowIndex].Cells[0].Value.ToString());
            Works database = new Works(Credentials);
            switch (selectedTable)
            {
                case "Пользователь": { addStudentLastNameTB.Text = dataGridViewTest.Rows[e.RowIndex].Cells[0].Value.ToString(); addStudentNameTB.Text = dataGridViewTest.Rows[e.RowIndex].Cells[1].Value.ToString(); addStudentSurnameTB.Text = dataGridViewTest.Rows[e.RowIndex].Cells[2].Value.ToString(); break; }
                case "Автор": { authorLastNameTB.Text = dataGridViewTest.Rows[e.RowIndex].Cells[0].Value.ToString(); authorNameTB.Text = dataGridViewTest.Rows[e.RowIndex].Cells[1].Value.ToString(); authorSurNameTB.Text = dataGridViewTest.Rows[e.RowIndex].Cells[2].Value.ToString(); break; }
                case "Курс":
                    {
                        courseRequiredSkill.Value = Convert.ToInt32(dataGridViewTest.Rows[e.RowIndex].Cells[2].Value);
                        addCourseNameTB.Text = dataGridViewTest.Rows[e.RowIndex].Cells[0].Value.ToString();
                        courseHours.Value = Convert.ToInt32(dataGridViewTest.Rows[e.RowIndex].Cells[1].Value);
                        beginCourseDTP.Value = Convert.ToDateTime(dataGridViewTest.Rows[e.RowIndex].Cells[3].Value);
                        endCourseDTP.Value = Convert.ToDateTime(dataGridViewTest.Rows[e.RowIndex].Cells[4].Value);
                        dataGridViewListReturner.DataSource = database.returnAuthorsCount(tempeID).Tables[0].DefaultView;
                        int authorsCount = Convert.ToInt32(dataGridViewListReturner.Rows.Count-1);
                        foreach (ComboBox cb in cbs) cb.Visible = false; int i = 0;
                        foreach(ComboBox cb in cbs) { if (i != authorsCount) { cb.Visible = true; i++; } } 
                        for(i = 0; i < dataGridViewListReturner.Rows.Count-1; i++) { cbs[i].SelectedItem = dataGridViewListReturner.Rows[i].Cells[1].Value.ToString(); }
                        break;
                    }
                case "Список студентов курса": { tempeID = Convert.ToInt32(dataGridTemp.Rows[e.RowIndex].Cells[1].Value); tempStudID = Convert.ToInt32(dataGridTemp.Rows[e.RowIndex].Cells[0].Value); break; }
            }
        }
        #region кнопки

        private void addStudentBTN_Click(object sender, EventArgs e)
        {
            Works database = new Works(Credentials);
            listBox1.Items.Add(database.addUser(addStudentLastNameTB.Text, addStudentNameTB.Text, addStudentSurnameTB.Text));

        }

        private void button2_Click(object sender, EventArgs e)
            {
                Works database = new Works(Credentials);
                listBox1.Items.Add(database.newCourse(Convert.ToInt32(courseHours.Value), Convert.ToInt32(courseRequiredSkill.Value), beginCourseDTP.Value, endCourseDTP.Value, addCourseNameTB.Text));
                int newCourseID = GetDirCode("Курс", $"{addCourseNameTB.Text}", 3), author;
                foreach (ComboBox cb in cbs)
                {
                    if (cb.Visible)
                    {
                        Works database1 = new Works(Credentials);
                        author = GetDirCode("Автор", $"{cb.SelectedItem.ToString()}", 1);
                        listBox1.Items.Add(database1.newAuthorAndCourseRecord(author, newCourseID));
                    }
                }
            }
            private void newCourseStudentBTN_Click(object sender, EventArgs e)
            {
                Works database = new Works(Credentials);
                int studentID = GetDirCode("Пользователь", $"{addCourseStudentCB.SelectedItem.ToString()}", 1);
                int courseID = GetDirCode("Курс", $"{addCourseCourseCB.SelectedItem.ToString()}", 3);
                listBox1.Items.Add(database.newCourseStudent(studentID, courseID, trackBar1.Value));
                sa();
            }
            private void deleteCourseBTN_Click(object sender, EventArgs e)
            {
                Works database = new Works(Credentials);
                if(tempeID != -1) { listBox1.Items.Add(database.deleteCourse(tempeID)); tempeID = -1; }
            }

            private void updateStudentBTN_Click(object sender, EventArgs e)
            {
                Works database = new Works(Credentials);
                if (tempeID != -1) { listBox1.Items.Add(database.updateUser(addStudentLastNameTB.Text, addStudentNameTB.Text, addStudentSurnameTB.Text, tempeID)); tempeID = -1; }
            }

            private void deleteStudentBTN_Click(object sender, EventArgs e)
            {
                Works database = new Works(Credentials);
                if (tempeID != -1) { listBox1.Items.Add(database.deleteUser(tempeID)); tempeID = -1; }
            }

            private void button3_Click(object sender, EventArgs e)
            {
                Works database = new Works(Credentials);
                if (tempeID != -1) { listBox1.Items.Add(database.updateAuthor(authorLastNameTB.Text, authorNameTB.Text, authorSurNameTB.Text, tempeID)); tempeID = -1; }
            }

            private void deleteAuthorBTN_Click(object sender, EventArgs e)
            {
                Works database = new Works(Credentials);
                if (tempeID != -1) { listBox1.Items.Add(database.deleteAuthor(tempeID)); tempeID = -1; }
            }

            private void editCourseBTN_Click(object sender, EventArgs e)
            {
                Works database = new Works(Credentials); int author;
                if (tempeID != -1) { listBox1.Items.Add(database.updateCourse(tempeID, Convert.ToInt32(courseHours.Value), Convert.ToInt32(courseRequiredSkill.Value), beginCourseDTP.Value, endCourseDTP.Value, addCourseNameTB.Text));  }
                for (int i = 0; i < dataGridViewListReturner.Rows.Count - 1; i++)
                    listBox1.Items.Add(database.deleteAuthorFromAuthorsAndCourse(Convert.ToInt32(dataGridViewListReturner.Rows[i].Cells[0].Value)));
                foreach (ComboBox cb in cbs)
                {
                    if (cb.Visible)
                    {
                        Works database1 = new Works(Credentials);
                        author = GetDirCode("Автор", $"{cb.SelectedItem.ToString()}", 1);
                        listBox1.Items.Add(database1.newAuthorAndCourseRecord(author, tempeID));
                    }
                }
                tempeID = -1;
            }

                private void deleteStudentFromCourseBTN_Click(object sender, EventArgs e)
                {
                    Works database = new Works(Credentials);
                    if (tempeID != -1 && tempStudID != -1) { listBox1.Items.Add(database.deleteStudentFromCourse(tempeID, tempStudID)); tempeID = -1; tempStudID = -1; }
                }
        #endregion
        
    }
}
