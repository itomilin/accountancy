using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using AccountancyCRUD.View;
using System.Collections.Generic;

namespace AccountancyCRUD.Controller
{
    class MainController
    {
        private string _currentTableName;

        private readonly MainForm _mainForm;
        private readonly UpdateForm _updateForm;
        private readonly UpdateFormDepartment _updateFormDepartments;
        private readonly EmployeeForm _updateFormEmployee;
        private readonly DepartmentEmployeeForm _departmentEmployeeForm;

        #region MainController
        public MainController(params Form[] views)
        {
            foreach (var view in views)
            {
                if (view is UpdateForm)
                {
                    _updateForm = (UpdateForm)view;
                }
                else if (view is UpdateFormDepartment)
                {
                    _updateFormDepartments = (UpdateFormDepartment)view;
                }
                else if (view is EmployeeForm)
                {
                    _updateFormEmployee = (EmployeeForm)view;
                }
                else if (view is DepartmentEmployeeForm)
                {
                    _departmentEmployeeForm = (DepartmentEmployeeForm)view;
                }
                else if (view is MainForm)
                {
                    _mainForm = (MainForm)view;
                    _mainForm.GetDeleteButton.Click += Delete;
                    _mainForm.GetInsertButton.Click += Insert;

                    _mainForm.GetGrid.RowHeaderMouseDoubleClick += Update;

                    //_mainForm.Load += Read;
                    _mainForm.NetWorthButton.Click += CalculateNetWorth;
                    _mainForm.GetCombo.SelectedIndexChanged += Read;
                    _mainForm.GetCombo.SelectedIndexChanged += SetTableName;
                }
            }
        }
        #endregion

        private void SetTableName(object sender, EventArgs e)
        {
            _currentTableName = _mainForm.GetCombo.Text;
        }

        #region CalculateNetWorth
        private void CalculateNetWorth(object sender, EventArgs e)
        {
            string query =
                $"select * from get_net_worth('{_mainForm.NetWorthDate}');";
            //$"select * from get_net_worth('2015-02-02');";
            using (var context = new MyDbContext())
            {
                var result = context
                    .Database
                    .SqlQuery<decimal>(query)
                    .FirstOrDefault();
                _mainForm.SetNetWorth = result.ToString();
            }
        }
        #endregion

        #region Update
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var selected = ((ComboBox)_updateForm.Departments).SelectedItem;
            int? id = null;
            if (selected != null)
            {
                id = ((Model.MyDepartment)selected).Id;
            }

            using (var context = new MyDbContext())
            {
                if (_currentTableName == "Projects")
                {
                    var entry = context.Projects
                        .Where(it => it.id == _updateForm.GetId)
                        .FirstOrDefault();
                    dynamic employee = ((ComboBox)_updateForm.Departments).SelectedItem;
                    if (employee != null)
                    {
                        entry.department_id = employee.Id;
                    }
                    entry.project_name = _updateForm.GetProjectName;
                    entry.date_begin = _updateForm.GetBeginDate;
                    entry.date_end = _updateForm.GetEndDate;
                    entry.date_end_real = _updateForm.GetEndRealDate;
                    entry.cost = _updateForm.GetCost;
                    //_updateForm.GetApplyButton.Click -= UpdateButton_Click;
                    _updateForm.Close();
                }
                else if (_currentTableName == "Departments")
                {
                    var entry = context.Departments
                        .Where(it => it.id == _updateFormDepartments.GetId)
                        .FirstOrDefault();
                    entry.name = _updateFormDepartments.GetDepartmentName;
                    //_updateFormDepartments.GetApplyButton.Click -= UpdateButton_Click;
                    _updateFormDepartments.Close();
                }
                else if (_currentTableName == "Employees")
                {
                    var entry = context.Employees
                        .Where(it => it.id == _updateFormEmployee.Id)
                        .FirstOrDefault();
                    entry.name = _updateFormEmployee.EmployeelName;
                    entry.surname = _updateFormEmployee.Surname;
                    entry.patronymic = _updateFormEmployee.Patronymic;
                    entry.position = _updateFormEmployee.Position;
                    entry.salary = _updateFormEmployee.Salary;
                    //_updateFormEmployee.GetApplyButton.Click -= UpdateButton_Click;
                    _updateFormEmployee.Close();
                }
                else if (_currentTableName == "DepartmentsEmployees")
                {
                    var entry = context.DepartmentsEmployees
                        .Where(it => it.id == _departmentEmployeeForm.Id)
                        .FirstOrDefault();
                    dynamic employee = ((ComboBox)_departmentEmployeeForm.Employees).SelectedItem;
                    entry.employee_id = employee.Id;
                    dynamic department = ((ComboBox)_departmentEmployeeForm.Departments).SelectedItem;
                    entry.department_id = department.Id;
                    //_departmentEmployeeForm.GetApplyButton.Click -= UpdateButton_Click;
                    _departmentEmployeeForm.Close();
                }

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var x = ex;
                    MessageBox.Show("handle trigger");
                }
            }
            Read(this, new EventArgs());
        }

        private void Update(object sender, EventArgs e)
        {
            string selectedTable = _mainForm.GetCombo.Text;
            using (var context = new MyDbContext())
            {
                if (selectedTable == "Projects") //+
                {
                    var tableRow = (Model.MyProject)_mainForm.GetGrid.SelectedRows[0].DataBoundItem;
                    _updateForm.Departments = context.Departments.Select(
                        it => new Model.MyDepartment()
                        {
                            Id = it.id,
                            Name = it.name
                        }).ToArray();
                    ((ComboBox)_updateForm.Departments).Text = tableRow.DepartmentId;
                    _updateForm.GetId = tableRow.Id;
                    _updateForm.GetProjectName = tableRow.ProjectName;
                    _updateForm.GetCost = tableRow.Cost;
                    _updateForm.GetEndDate = tableRow.DateEnd;
                    _updateForm.GetBeginDate = tableRow.DateBegin;
                    _updateForm.GetEndRealDate = tableRow.DateEndReal;

                    _updateForm.Text = "Update Form";
                    _updateForm.GetApplyButton.Text = "Update entry";
                    _updateForm.GetApplyButton.Click += UpdateButton_Click;
                    _updateForm.ShowDialog();
                    _updateForm.GetApplyButton.Click -= UpdateButton_Click;
                }
                else if (selectedTable == "Departments") //+
                {
                    var tableRow = (Model.MyDepartment)_mainForm.GetGrid.SelectedRows[0].DataBoundItem;
                    _updateFormDepartments.GetId = tableRow.Id;
                    _updateFormDepartments.GetDepartmentName = tableRow.Name;

                    _updateFormDepartments.Text = "Update Form";
                    _updateFormDepartments.GetApplyButton.Text = "Update entry";
                    _updateFormDepartments.GetApplyButton.Click += UpdateButton_Click;
                    _updateFormDepartments.ShowDialog();
                    _updateFormDepartments.GetApplyButton.Click -= UpdateButton_Click;
                }
                else if (selectedTable == "Employees")
                {
                    var tableRow = (Model.MyEmployee)_mainForm.GetGrid
                        .SelectedRows[0].DataBoundItem;
                    _updateFormEmployee.Id = tableRow.Id;
                    _updateFormEmployee.EmployeelName = tableRow.Name;
                    _updateFormEmployee.Surname = tableRow.Surname;
                    _updateFormEmployee.Patronymic = tableRow.Patronymic;
                    _updateFormEmployee.Position = tableRow.Position;
                    _updateFormEmployee.Salary = tableRow.Salary;

                    _updateFormEmployee.Text = "Update Form";
                    _updateFormEmployee.GetApplyButton.Text = "Update entry";
                    _updateFormEmployee.GetApplyButton.Click += UpdateButton_Click;
                    _updateFormEmployee.ShowDialog();
                    _updateFormEmployee.GetApplyButton.Click -= UpdateButton_Click;
                }
                else if (selectedTable == "DepartmentsEmployees") //+
                {
                    var tableRow = (Model.MyDepartmentEmployee)_mainForm.GetGrid
                        .SelectedRows[0].DataBoundItem;
                    _departmentEmployeeForm.Id = tableRow.Id;
                    _departmentEmployeeForm.Departments = context.Departments.Select(
                        it => new Model.MyDepartment()
                        {
                            Id = it.id,
                            Name = it.name
                        }).ToArray();
                    ((ComboBox)_departmentEmployeeForm.Departments).Text = tableRow.DepartmentId;
                    _departmentEmployeeForm.Employees = context.Employees.Select(
                        it => new Model.MyEmployee()
                        {
                            // С использованием интерполяции строк, entity НЕ может построить запрос.
                            Id = it.id,
                            Name = it.surname + " " +
                                   it.name + " " +
                                   it.patronymic
                        }).ToArray();
                    ((ComboBox)_departmentEmployeeForm.Employees).Text = tableRow.EmployeeName;

                    _departmentEmployeeForm.Text = "Update Form";
                    _departmentEmployeeForm.GetApplyButton.Text = "Update entry";
                    _departmentEmployeeForm.GetApplyButton.Click += UpdateButton_Click;
                    _departmentEmployeeForm.ShowDialog();
                    _departmentEmployeeForm.GetApplyButton.Click -= UpdateButton_Click;
                }
            }
        }
        #endregion

        #region Delete
        private void Delete(object sender, EventArgs e)
        {
            if (_mainForm.GetGrid.SelectedRows.Count == 0)
            {
                return;
            }
            int id = int.Parse(_mainForm.GetGrid.SelectedRows[0].Cells[0].Value.ToString());
            using (var context = new MyDbContext())
            {
                if (_currentTableName == "Projects")
                {
                    context.Projects.Remove(context
                        .Projects.Where(it => it.id == id).FirstOrDefault());
                }
                else if (_currentTableName == "Departments")
                {
                    context.Departments.Remove(context
                        .Departments.Where(it => it.id == id).FirstOrDefault());
                }
                else if (_currentTableName == "Employees")
                {
                    context.Employees.Remove(context
                        .Employees.Where(it => it.id == id).FirstOrDefault());
                }
                else if (_currentTableName == "DepartmentsEmployees")
                {
                    context.DepartmentsEmployees.Remove(context
                        .DepartmentsEmployees.Where(it => it.id == id).FirstOrDefault());
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("handle trigger");
                }
            }
            Read(this, new EventArgs());
        }
        #endregion

        #region Insert

        private void InsertButton_Click(object sender, EventArgs e)
        {
            if (_currentTableName == "Projects")
            {
                var selected = ((ComboBox)_updateForm.Departments).SelectedItem;
                int? id = null;
                if (selected != null)
                {
                    id = ((Model.MyDepartment)selected).Id;
                }
                using (var context = new MyDbContext())
                {
                    context.Projects.Add(new Model.project
                    {
                        project_name = _updateForm.GetProjectName,
                        date_begin = _updateForm.GetBeginDate,
                        date_end = _updateForm.GetEndDate,
                        date_end_real = _updateForm.GetEndRealDate,
                        cost = _updateForm.GetCost,
                        department_id = id
                    });
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("handle trigger");
                    }
                }
                _updateForm.Close();
                //_updateForm.GetApplyButton.Click -= InsertButton_Click;
            }
            else if (_currentTableName == "Departments")
            {
                using (var context = new MyDbContext())
                {
                    context.Departments.Add(new Model.department
                    {
                        name = _updateFormDepartments.GetDepartmentName
                    });
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("handle trigger");
                    }
                }
                _updateFormDepartments.Close();
                //_updateFormDepartments.GetApplyButton.Click -= InsertButton_Click;
            }
            else if (_currentTableName == "Employees")
            {
                using (var context = new MyDbContext())
                {
                    context.Employees.Add(new Model.employee
                    {
                        name = _updateFormEmployee.EmployeelName,
                        surname = _updateFormEmployee.Surname,
                        patronymic = _updateFormEmployee.Patronymic,
                        position = _updateFormEmployee.Position,
                        salary = _updateFormEmployee.Salary
                    });
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("handle trigger");
                    }
                }
                _updateFormEmployee.Close();
                //_updateFormEmployee.GetApplyButton.Click -= InsertButton_Click;
            }
            else if (_currentTableName == "DepartmentsEmployees")
            {
                var selDep = ((ComboBox)_departmentEmployeeForm.Departments).SelectedItem;
                var selEmpl = ((ComboBox)_departmentEmployeeForm.Employees).SelectedItem;
                using (var context = new MyDbContext())
                {
                    try
                    {
                        context.DepartmentsEmployees.Add(new Model.departments_employees
                        {
                            employee_id = ((Model.MyEmployee)selEmpl).Id,
                            department_id = ((Model.MyDepartment)selDep).Id
                        });
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("handle trigger");
                    }
                }
                _departmentEmployeeForm.Close();
                //_departmentEmployeeForm.GetApplyButton.Click -= InsertButton_Click;
            }

            Read(this, new EventArgs());
        }

        private void Insert(object sender, EventArgs e)
        {
            if (_currentTableName == "Projects")
            {
                _updateForm.GetApplyButton.Click += InsertButton_Click;
                using (var context = new MyDbContext())
                {
                    _updateForm.Departments = context.Departments.Select(
                        it => new Model.MyDepartment()
                        {
                            Id = it.id,
                            Name = it.name
                        }).ToArray();
                }
                _updateForm.Text = "Insert Form";
                _updateForm.GetApplyButton.Text = "Insert new entry";
                _updateForm.ShowDialog();
            }
            else if (_currentTableName == "Departments") // +
            {
                _updateFormDepartments.Text = "Insert Form";
                _updateFormDepartments.GetApplyButton.Text = "Insert new entry";
                _updateFormDepartments.GetApplyButton.Click += InsertButton_Click;
                _updateFormDepartments.ShowDialog();
                _updateFormDepartments.GetApplyButton.Click -= InsertButton_Click;
            }
            else if (_currentTableName == "Employees")
            {
                _updateFormEmployee.Text = "Insert Form";
                _updateFormEmployee.GetApplyButton.Text = "Insert new entry";
                _updateFormEmployee.GetApplyButton.Click += InsertButton_Click;
                _updateFormEmployee.ShowDialog();
                _updateFormEmployee.GetApplyButton.Click -= InsertButton_Click;
            }
            else if (_currentTableName == "DepartmentsEmployees")
            {
                _departmentEmployeeForm.Text = "Insert Form";
                _departmentEmployeeForm.GetApplyButton.Text = "Insert new entry";
                _departmentEmployeeForm.GetApplyButton.Click += InsertButton_Click;
                using (var context = new MyDbContext())
                {
                    _departmentEmployeeForm.Departments = context.Departments.Select(
                        it => new Model.MyDepartment()
                        {
                            Id = it.id,
                            Name = it.name
                        }).ToArray();
                    ((ComboBox)_departmentEmployeeForm.Departments).SelectedIndex = 0;
                }
                using (var context = new MyDbContext())
                {
                    _departmentEmployeeForm.Employees = context.Employees.Select(
                        it => new Model.MyEmployee()
                        {
                            // С использованием интерполяции строк, entity НЕ может построить запрос.
                            Id = it.id,
                            Name = it.surname + " " +
                                   it.name + " " +
                                   it.patronymic
                        }).ToArray();
                    ((ComboBox)_departmentEmployeeForm.Employees).SelectedIndex = 0;
                }
                _departmentEmployeeForm.ShowDialog();
                _departmentEmployeeForm.GetApplyButton.Click -= InsertButton_Click;
            }
        }
        #endregion

        #region Read
        private void Read(object sender, EventArgs e)
        {
            string selectedTable = _mainForm.GetCombo.Text;
            using (var context = new MyDbContext())
            {
                if (selectedTable == "Projects")
                {
                    _mainForm.GetGrid.DataSource = context.Projects
                   .ToList().Select(item => new Model.MyProject
                   {
                       Id = item.id,
                       ProjectName = item.project_name,
                       Cost = item.cost,
                       DepartmentId = item.department_id is null ? "" : item.department.name,
                       DateBegin = item.date_begin,
                       DateEnd = item.date_end,
                       DateEndReal = item.date_end_real
                   }).ToList();

                }
                else if (selectedTable == "Departments")
                {
                    _mainForm.GetGrid.DataSource = context.Departments
                        .ToList().Select(item => new Model.MyDepartment
                        {
                            Id = item.id,
                            Name = item.name
                        }).ToList();
                }
                else if (selectedTable == "Employees")
                {
                    _mainForm.GetGrid.DataSource = context.Employees
                        .ToList().Select(item => new Model.MyEmployee
                        {
                            Id = item.id,
                            Name = item.name,
                            Surname = item.surname,
                            Patronymic = item.patronymic,
                            Position = item.position,
                            Salary = item.salary
                        }).ToList();
                }
                else if (selectedTable == "DepartmentsEmployees")
                {
                    _mainForm.GetGrid.DataSource = context.DepartmentsEmployees
                        .ToList().Select(item => new Model.MyDepartmentEmployee
                        {
                            Id = item.id,
                            DepartmentId = item.department.name,
                            EmployeeName = $"{item.employee.surname} " +
                                       $"{item.employee.name} " +
                                       $"{item.employee.patronymic}".Trim()
                        }).ToList();
                }
            }
        }
        #endregion
    }
}
