    using System;
using System.Windows.Forms;

namespace Интерфейсы
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class Info //хранение информации методов
        {
            //без статиков не работает :(
            public static string id_selected_rows = "0";
            public static bool Employee;
            public static bool check = false;
        }
        interface IPerson //Создание интерфейса Person
        {
            //
            string Name { get; set; }
            string Address { get; set; }
            void Renc()
            {
                MessageBox.Show("Аккаунт не подтвержден. Загрузите документы.", "Уведомление");
            }
        }
        class Person
        {
            public IPerson PersonInfo { get; set; } //свойство которое будет хранить экземпляры классов
        }

        public class Employees : IPerson
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public Employees(string _name, string _address)
            {
                Name = _name;
                Address = _address;
            }
            public void Renc()
            {
                MessageBox.Show("Вы являетесь сотрудником, не пытайтесь это сделать.", "Уведомление");
            }
        }

        class User : IPerson
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public User(string _name, string _address)
            {
                Name = _name;
                Address = _address;
            }
        }
        class ConfirmUser : User, IPerson
        {
            public ConfirmUser(string _name, string _address) : base(_name, _address)
            {
                Name = _name;
                Address = _address;
            }
            public void Renc()
            {
                MessageBox.Show($"Аренда мотоцикла {Info.id_selected_rows} прошла успешно!", "Уведомление");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            if (toolStripTextBox1.Text != "" && toolStripTextBox2.Text != "") { Info.check = true; MessageBox.Show("Вы успешно подтвердили свой аккаунт!"); }
            else MessageBox.Show("Введите данные в настройках");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //апкаст
            Person person = new Person();
            if(!Info.Employee) //проверка данных, считывает сотрудников, и пользователей
            {
                if (!Info.check) person.PersonInfo = new User(toolStripTextBox1.Text, toolStripTextBox2.Text);
                else person.PersonInfo = new ConfirmUser(toolStripTextBox1.Text, toolStripTextBox2.Text);
            }
            else person.PersonInfo = new Employees(toolStripTextBox1.Text, toolStripTextBox2.Text);
            GetSelectedIDString();
            person.PersonInfo.Renc(); //один вызов для 3-ех классов
        }

        //Метод получения ID выделенной строки, для последующего вызова его в нужных методах
        public void GetSelectedIDString()
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            Info.id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //заполнение datagrid
            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.HeaderText = "Марка";
            col0.Name = "ID1";
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "Модель";
            col1.Name = "ID2";
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "Max Speed";
            col2.Name = "ID3";
            this.dataGridView1.Columns.Add(col0);
            this.dataGridView1.Columns.Add(col1);
            this.dataGridView1.Columns.Add(col2);

            DataGridViewCell cel0 = new DataGridViewTextBoxCell();
            DataGridViewCell cel1 = new DataGridViewTextBoxCell();
            DataGridViewCell cel2 = new DataGridViewTextBoxCell();
            DataGridViewRow row = new DataGridViewRow();
            cel0.Value = "Honda";
            cel1.Value = "FireStorm";
            cel2.Value = "176";
            row.Cells.AddRange(cel0, cel1, cel2);
            this.dataGridView1.Rows.Add(row);
            cel0 = new DataGridViewTextBoxCell();
            cel1 = new DataGridViewTextBoxCell();
            cel2 = new DataGridViewTextBoxCell();
            row = new DataGridViewRow();
            cel0.Value = "SUZUKI";
            cel1.Value = "GSX-R1000";
            cel2.Value = "190";
            row.Cells.AddRange(cel0, cel1, cel2);
            this.dataGridView1.Rows.Add(row);
            cel0 = new DataGridViewTextBoxCell();
            cel1 = new DataGridViewTextBoxCell();
            cel2 = new DataGridViewTextBoxCell();
            row = new DataGridViewRow();
            cel0.Value = "BMW";
            cel1.Value = "S1000RR";
            cel2.Value = "200+";
            row.Cells.AddRange(cel0, cel1, cel2);
            this.dataGridView1.Rows.Add(row);
            cel0 = new DataGridViewTextBoxCell();
            cel1 = new DataGridViewTextBoxCell();
            cel2 = new DataGridViewTextBoxCell();
            row = new DataGridViewRow();
            cel0.Value = "YAMAHA";
            cel1.Value = "YZF-R6";
            cel2.Value = "187";
            row.Cells.AddRange(cel0, cel1, cel2);
            this.dataGridView1.Rows.Add(row);
            //msgbox с получением инфы
            if (MessageBox.Show("Вы сотрудник?", "Вопрос", MessageBoxButtons.YesNo) == DialogResult.Yes) Info.Employee = true;
            else Info.Employee = false;
        }
    }
}
