using AccountancyCRUD.View;
using AccountancyCRUD.Model;
using System.Linq;

namespace AccountancyCRUD.Controller
{
    class LoginController
    {
        private readonly LoginForm _loginForm;
        private readonly MainForm _mainForm;

        public LoginController(LoginForm loginForm,
            MainForm mainForm)
        {
            _loginForm = loginForm;
            _mainForm = mainForm;
            _loginForm.GetButton.Click += new System.EventHandler(Validation);
        }

        private void Validation(object sender, System.EventArgs e)
        {
            // if user exist in table, then show the new form
            using (var context = new MyDbContext())
            {
                var userLogin = context
                    .Users
                    .FirstOrDefault(x => x.user_name == _loginForm.GetLogin.Text);
                if (userLogin is null)
                {
                    _loginForm.MyLabel.Visible = true;
                    _loginForm.MyLabel.Text = "Invalid login.";
                    return;
                }
                if (!SecurePasswordHasher.Verify(_loginForm.GetPassword.Text, userLogin.password))
                {
                    _loginForm.MyLabel.Visible = true;
                    _loginForm.MyLabel.Text = "Invalid password.";
                    return;
                }
                _loginForm.MyLabel.Visible = false;
                if (userLogin.role == "user")
                {
                    _mainForm.GetInsertButton.Visible = false;
                    _mainForm.GetDeleteButton.Visible = false;
                    _mainForm.GetGrid.RowHeadersVisible = false;
                }

                _loginForm.Hide();
                _mainForm.Show();
            }
        }
    }
}
