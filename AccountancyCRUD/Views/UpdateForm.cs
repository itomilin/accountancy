using System;
using System.Linq;
using System.Windows.Forms;

namespace AccountancyCRUD.View
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        public Button GetApplyButton => btnUpdate;

        public decimal GetCost
        {
            get => numCost.Value;
            set => numCost.Value = value;
        }

        public int GetId
        {
            get => int.Parse(tbId.Text);
            set => tbId.Text = value.ToString();
        }

        public DateTime? GetBeginDate
        {
            get => dtpBeginDate.Value;
            set => dtpBeginDate.Value = (DateTime)value;
        }

        public DateTime? GetEndDate
        {
            get => dtpEndDate.Value;
            set => dtpEndDate.Value = (DateTime)value;
        }

        public DateTime? GetEndRealDate
        {
            get => dtpEndRealDate.Value;
            set => dtpEndRealDate.Value = (DateTime)value;
        }

        public string GetProjectName
        {
            get => tbProjectName.Text;
            set => tbProjectName.Text = value;
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

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tbId.Text = null;
            tbProjectName.Text = null;
            numCost.Value = 0;
            dtpBeginDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            dtpEndRealDate.Value = DateTime.Now;
            cbDepartments.Items.Clear();
            cbDepartments.Text = null;
        }
    }
}
