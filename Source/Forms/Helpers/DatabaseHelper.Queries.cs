using System.Linq;
using System.Collections.Generic;
using System;

using MCCForms.Models;

namespace MCCForms
{
	public partial class DatabaseHelper
	{
		#region setting
	
		public Setting GetSetting (string key)
		{
			if (IsQueryPossible) {
				var result = _connection.TransactionWithResult<Setting> (() => 
					(from e in _connection.Table<Setting> ()
						where e.Key == key
						select e).FirstOrDefault<Setting> ());	
				
				return result;
			}

			return null;
		}

		public void AddSetting(Setting setting)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.Insert (setting));
			}
		}

		public void UpdateSetting (Setting setting)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.InsertOrReplace (setting));
			}
		}

		public void DeleteSetting (Setting setting)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.Delete<Setting> (setting.Key));
			}
		}
		#endregion

		#region filepathiv

		public string GetIVForFilePath (string filePath)
		{
			try{
				if (IsQueryPossible) {
					var result = _connection.TransactionWithResult<FilePathIV> (() => 
						(from e in _connection.Table<FilePathIV> ()
							where e.FilePath == filePath
							select e).FirstOrDefault<FilePathIV> ());	

					return result.IV;
				}

				return null;
			}catch {
				return null;
			}
		}

		public void AddFilePathIV(FilePathIV filePathIV)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.Insert (filePathIV));
			}
		}

		public void DeleteIVsForFilePaths ()
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => {
					_connection.DropTable<FilePathIV>();
					_connection.CreateTable<FilePathIV>();
				});
			}
		}
		#endregion

		#region passwordobject
		public void InsertAppointmentObject(Appointment appointmentObject){
			
			if (appointmentObject.EmployeeObject != null)
			{
				InsertEmployee(appointmentObject.EmployeeObject);
			}

			if (appointmentObject.VisitorList != null)
			{
				foreach (Visitor v in appointmentObject.VisitorList)
				{
					InsertVisitor(v);
				}
			}
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.Insert (appointmentObject));
			}

		}

		public void UpdateAppointmentObject(Appointment appointmentObject){
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.InsertOrReplace (appointmentObject));
			}
		}

		public void DeleteAppointmentObject(Appointment appointmentObject){
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.Delete (appointmentObject));
			}
		}
			
		/*
		public void InsertPasswordObject (PasswordObject passwordObject)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.Insert (passwordObject));
			}
		}

		public void UpdatePasswordObject (PasswordObject passwordObject)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.InsertOrReplace (passwordObject));
			}
		}

		public void DeletePasswordObject (PasswordObject passwordObject)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.Delete (passwordObject));
			}
		}
		*/

		public List<Appointment>GetAppointments(){
			List<Appointment> appointments = null;

			if (IsQueryPossible) {
				_connection.Transaction (() => {
					//Get appointment
					//appointments = _connection.Query<Appointment> ("SELECT * FROM Appointment").ToList ();
					//Get visitors/employees/collectors

					appointments = (from i in _connection.Table<Appointment>()
						select i).ToList();

					foreach(Appointment appointment in appointments){

						appointment.EmployeeObject = GetEmployee(appointment.Employee_employee_ID);
						//appointment.VisitorList = GetVisitors(appointment.VisitorList);
					}


					appointments.Sort ((x, y) => DateTime.Compare (x.Date, y.Date));
				});
				return appointments;
			}
			return appointments;
		}

		public void InsertEmployee (Employee employee)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.Insert (employee));
			}
		}

		public void UpdateEmployee (Employee employee)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.InsertOrReplace (employee));
			}
		}

		public void DeleteEmployee (Employee employee)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.Delete (employee));
			}
		}

		public Employee GetEmployee(int id)
		{
			Employee e = new Employee();
			if (IsQueryPossible)
			{
				e =  _connection.Table<Employee>().FirstOrDefault(x => x.Employee_ID == id);
			}
			return e;
		}

		public void InsertVisitor (Visitor visitor)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.Insert (visitor));
			}
		}

		public void UpdateVisitor (Visitor visitor)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.InsertOrReplace (visitor));
			}
		}

		public void DeleteVisitor (Visitor visitor)
		{
			if (IsQueryPossible) {
				_connection.Transaction (() => _connection.Delete (visitor));
			}
		}

		//Should be list
		public List<Visitor> GetVisitors(List<Visitor> visitors){
			List<Visitor> visitorList = null;
			if (IsQueryPossible) {
				_connection.Transaction (() => {
					foreach(Visitor v in visitors){
						visitorList.Add(_connection.Table<Visitor>().FirstOrDefault(x => x.Visitor_ID == v.Visitor_ID));
					}
				});
			}
			return visitorList;
		}

		/*
		public List<PasswordObject> GetPasswordObjects()
		{
			List<PasswordObject> passwordObjects = null;

			if (IsQueryPossible) {
				_connection.Transaction (() => {
					passwordObjects = _connection.Query<PasswordObject> ("SELECT * FROM PasswordObject").ToList ();
					passwordObjects.Sort((x, y) => string.Compare(x.Name, y.Name));
				});
				return passwordObjects;
			}
			return passwordObjects;
		}
		*/
		#endregion
	}
}