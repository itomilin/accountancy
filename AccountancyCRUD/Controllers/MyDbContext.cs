using System.Data.Entity;

namespace AccountancyCRUD.Controller
{
    class MyDbContext : DbContext
    {
        private const string CONNECTION_NAME = "AccountancyEntities";
        public MyDbContext()
            : base(CONNECTION_NAME)
        {
        }

        // Свойства для работы с таблицами бд.
        public DbSet<Model.user> Users { get; set; }

        public DbSet<Model.project> Projects { get; set; }

        public DbSet<Model.department> Departments { get; set; }

        public DbSet<Model.departments_employees> DepartmentsEmployees { get; set; }

        public DbSet<Model.employee> Employees { get; set; }
    }
}
