using System;
using SQLite;

namespace MCCForms.Models
{
    public class Collector
    {
        [PrimaryKey, AutoIncrement]
        public int Collector_ID { get; set;}
        public int Appointment_appointment_ID { get; set; }


        public Collector()
        {
        }
    }
}

