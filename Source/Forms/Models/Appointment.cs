using System;
using SQLite;
using System.Collections.Generic;


namespace MCCForms.Models
{
    public class Appointment
    {
        [PrimaryKey, AutoIncrement]
        public int Appointment_ID { get; set;}

        public int Employee_employee_ID { get; set; }

        [Ignore]
        public Employee EmployeeObject { get; set;}

        [Ignore]
        public List<Collector> CollectorList { get; set;}

        [Ignore]
        public List<Visitor> VisitorList { get; set; }

        public String Locatie { get; set; }

        public String Comment { get; set; }

        public DateTime Date { get; set; } // TODO: Aangepaste datum maken

        // Lege constructor voor de database
        public Appointment()
        {
        }

        public Appointment (Employee EmployeeObject, List<Collector> CollectorList, List<Visitor> VisitorList, String Locatie, DateTime Date)
        {
            this.EmployeeObject = EmployeeObject;
            this.CollectorList = CollectorList;
            this.VisitorList = VisitorList;
            this.Locatie = Locatie;
            this.Date = Date;
        }

        public override string ToString()
        {
            return string.Format("U heeft een afspraak met {0}, op {1}.", EmployeeObject, Date);
        }
    }
}

