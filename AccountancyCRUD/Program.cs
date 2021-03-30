using System;
using System.Windows.Forms;

using AccountancyCRUD.View;
using AccountancyCRUD.Controller;

namespace AccountancyCRUD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var context = new Controller.MyDbContext())
            {
                try
                {
                    context.Database.Connection.Open();
                }
                catch (Npgsql.PostgresException ex)
                {
                    MessageBox.Show($"{ex.MessageText}\n" +
                        $" check the connection string!",
                        "Error!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Invalid ConnectionString name.",
                        "Error!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
                finally
                {
                    context.Database.Connection.Close();
                }
            }

            UpdateForm updateFormProject = new UpdateForm();
            EmployeeForm updateFormEmployee = new EmployeeForm();
            UpdateFormDepartment updateFormDepartment = new UpdateFormDepartment();
            DepartmentEmployeeForm departmentEmployeeForm = new DepartmentEmployeeForm();

            LoginForm loginForm = new LoginForm();
            MainForm mainForm = new MainForm();

            

            MainController mainController = new MainController(mainForm,
                                                              updateFormProject,
                                                              updateFormDepartment,
                                                              updateFormEmployee,
                                                              departmentEmployeeForm);

            LoginController loginController = new LoginController(loginForm, mainForm);

            Application.Run(loginForm);
        }
    }
}
