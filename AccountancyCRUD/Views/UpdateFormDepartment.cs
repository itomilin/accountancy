using System;
using System.Linq;
using System.Windows.Forms;

namespace AccountancyCRUD.View
{
    public partial class UpdateFormDepartment : Form
    {
        public UpdateFormDepartment()
        {
            InitializeComponent();
        }

        public Button GetApplyButton => btnUpdate;

        public int GetId
        {
            get => int.Parse(tbId.Text);
            set => tbId.Text = value.ToString();
        }

        public string GetDepartmentName
        {
            get => tbDepartmentName.Text;
            set => tbDepartmentName.Text = value;
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tbId.Text = null;
            tbDepartmentName.Text = null;
        }
    }
}
