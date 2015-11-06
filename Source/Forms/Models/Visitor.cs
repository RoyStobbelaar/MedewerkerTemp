using System;
using SQLite;

namespace MCCForms.Models
{
	public class Visitor
    {
        [PrimaryKey, AutoIncrement]
        public int Visitor_ID { get; set;}

        public int Appointment_appointment_ID { get; set; }

        public String FirstName { get; set;}

        public String LastName { get; set;}

        public String Email { get; set;}

        public int PhoneNumber { get; set;}

        public String Company { get; set;}

        public String Image { get; set;}

        // Lege constructor voor de database
        public Visitor()
        {
        }

        public Visitor(String FirstName, String LastName, String Email, int PhoneNumber, String Company, String Image)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.Company = Company;
            this.Image = Image;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}

