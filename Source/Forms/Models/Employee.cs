using System;
using SQLite;

namespace MCCForms.Models
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int Employee_ID { get; set;}

        public String FirstName { get; set;}

        public String LastName { get; set;}

        public int SAP { get; set;}

        public int PhoneNumber { get; set;}

        public String Email { get; set;} 

        public String Image { get; set;} 

        // Lege constructor voor de database
        public Employee()
        {
        }

        public Employee(String FirstName, String LastName, int SAP, int PhoneNumber, String Email, String Image)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.SAP = SAP;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Image = Image;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}

