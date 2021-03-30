using System;
using System.Linq;
using System.Windows.Forms;

namespace AccountancyCRUD.View
{
    public partial class DepartmentEmployeeForm : Form
    {
        public DepartmentEmployeeForm()
        {
            InitializeComponent();
        }

        public Button GetApplyButton => btnUpdate;

        public int Id
        {
            get => int.Parse(tbId.Text);
            set => tbId.Text = value.ToString();
        }

        public object Departments
        {
            get => cbDepartments;
            set
            {
                cbDepartments.Items.AddRange(value as object[]);
                cbDepartments.ValueMember = "Id";
                cbDepartments.DisplayMember = "Name";
            }
        }

        public object Employees
        {
            get => cbEmployeesName;
            set
            {
                cbEmployeesName.Items.AddRange(value as object[]);
                cbEmployeesName.ValueMember = "Id";
                cbEmployeesName.DisplayMember = "Name";
            }
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tbId.Text = null;
            cbDepartments.Text = null;
            cbEmployeesName.Text = null;
            cbDepartments.Items.Clear();
            cbEmployeesName.Items.Clear();
        }
    }
}
