using System.Linq;
using System.Windows.Forms;

namespace AccountancyCRUD.View
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            AcceptButton = btnLogin;
        }

        public Label MyLabel
        {
            get { return lblStatus; }
            set { lblStatus = value; }
        }

        public TextBox GetLogin
        {
            get
            {
                return tbLogin;
            }
        }

        public TextBox GetPassword
        {
            get
            {
                return tbPassword;
            }
        }

        public Button GetButton
        {
            get
            {
                return btnLogin;
            }
        }
    }
}
