using System;

namespace AccountancyCRUD.Model
{
    class MyProject
    {
        public int Id { get; set; }

        public string ProjectName { get; set; }

        public decimal Cost { get; set; }

        public string DepartmentId { get; set; }

        public DateTime? DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }

        public DateTime? DateEndReal { get; set; }
    }
}
