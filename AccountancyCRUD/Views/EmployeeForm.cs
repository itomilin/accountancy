using System;
using System.Linq;
using System.Windows.Forms;

namespace AccountancyCRUD.View
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }

        public Button GetApplyButton => btnApply;

        public decimal Salary
        {
            get => !decimal.TryParse(tbSalary.Text, out decimal salary) ?
                    0 : salary;
            set => tbSalary.Text = value.ToString();
        }

        public int Id
        {
            get => int.Parse(tbId.Text);
            set => tbId.Text = value.ToString();
        }

        public string EmployeelName
        {
            get => tbName.Text;
            set => tbName.Text = value;
        }

        public string Surname
        {
            get => tbSurname.Text;
            set => tbSurname.Text = value;
        }

        public string Patronymic
        {
            get => tbPatronymic.Text;
            set => tbPatronymic.Text = value;
        }

        public string Position
        {
            get => tbPosition.Text;
            set => tbPosition.Text = value;
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tbId.Text = null;
            tbName.Text = null;
            tbSurname.Text = null;
            tbPatronymic.Text = null;
            tbPosition.Text = null;
            tbSalary.Text = null;
        }
    }
}
