using BTTuan8.SchoolDBDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTTuan8
{
    public partial class Form1 : Form
    {
        public SchoolDBDataSet context =  new SchoolDBDataSet();
        private BindingSource bindingSource1 = new BindingSource();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'schoolDBDataSet.Students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter.Fill(this.schoolDBDataSet.Students);
           bindingSource1.DataSource = schoolDBDataSet.Students;
            dgvStudent.DataSource = bindingSource1;
            txtFullName.DataBindings.Add("Text", bindingSource1, "FullName");
            txtAge.DataBindings.Add("Text", bindingSource1, "Age");
            cmbNganh.DataSource = new BindingSource(GetNganh(), null);
            cmbNganh.DisplayMember = "Value"; // Tên hiển thị trong ComboBox
            cmbNganh.ValueMember = "Key"; // Giá trị thực sự sẽ được lưu vào cơ sở dữ liệu
            cmbNganh.DataBindings.Add("SelectedValue", bindingSource1, "Major"); // Liên kết với trường Major



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow newStudent = schoolDBDataSet.Students.NewRow();
            newStudent["FullName"] = txtFullName.Text;
            newStudent["Age"] = int.Parse(txtAge.Text);
            newStudent["Major"] = cmbNganh.SelectedValue.ToString();
            
            schoolDBDataSet.Students.Rows.Add(newStudent);
            studentsTableAdapter.Update(schoolDBDataSet.Students);
            bindingSource1.ResetBindings(false);


        }
        private Dictionary<string, string> GetNganh()
        {
            return new Dictionary<string, string>
            {
                { "CNTT", "Công nghệ Thông tin" },
                { "CK", "Cơ khí" },
                { "MK", "Marketing" }
            };
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current != null)
            {
                DataRowView currentRow = (DataRowView)bindingSource1.Current;
                currentRow["FullName"] = txtFullName.Text;
                currentRow["Age"] = int.Parse(txtAge.Text);
                currentRow["Major"] = cmbNganh.SelectedValue; 

                
                studentsTableAdapter.Update(schoolDBDataSet.Students);
                bindingSource1.ResetBindings(false);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current != null)
            {
                DataRowView currentRow = (DataRowView)bindingSource1.Current;
                currentRow.Delete();

                
                studentsTableAdapter.Update(schoolDBDataSet.Students);

              
                bindingSource1.ResetBindings(false);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
        }

        private void btnlui_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
        }
    }
}
