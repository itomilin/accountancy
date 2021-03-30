using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountancyCRUD.Model
{
    class MyEmployee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public decimal Salary { get; set; }

        //public string DepartmentId { get; set; }

        public string Position { get; set; }
    }
}
