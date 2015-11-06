using System;

using Xamarin.Forms;

namespace MCCForms
{
	public partial class DatabaseHelper
	{
		static DatabaseHelper _instance;

		DatabaseConnection _connection;

		object _synclock = new object ();
		string _pincode;

		public bool IsConnected { 
			get { return _connection != null; } 
		}

		public bool IsQueryPossible { 
			get { return !IsConnected && string.IsNullOrEmpty (_pincode) ? OpenConnection (_pincode) : IsConnected; } 
		}

		public bool IsDatabaseAlreadyCreated 
		{
			get {
				return FilesystemHelper.FileExists (FilesystemHelper.DatabaseFile) ? true : false;
			}
		}

		public static DatabaseHelper Instance { 
			get {
				if (_instance == null) {
					_instance = new DatabaseHelper ();
				}
				return _instance;
			}
		}

		public bool OpenConnection ()
		{
			string hashedPincode = DependencyService.Get<ISecurity> ().GetGeneratedDatabasePincode (null);

			return OpenConnection (hashedPincode);
		}

		public bool OpenConnection (string pincode, bool onlyCheckPincode = false)
		{
			try{
				string database = FilesystemHelper.DatabaseFile;

				var connection = new DatabaseConnection (database, pincode, _synclock);

				if (connection != null) {
					if (!onlyCheckPincode) {
						_connection = connection;
						_pincode = pincode;
					}
					return true;
				}

				return false;
			}
			catch(Exception ex){
				Console.WriteLine (ex.Message);
				return false;
			}
		}

		public void CloseConnection ()
		{
			_connection = null;
		}

		public void CloseAndForgetConnection ()
		{
			_connection = null;
			_pincode = string.Empty;
		}

		public bool ChangePincode(string newPin)
		{
			try{
				int queryResponse = -1;

				queryResponse = _connection.Execute("PRAGMA rekey = '" + newPin + "';");

				if (queryResponse != -1) {
					return true;
				} else {
					return false;
				}
			}catch {
				return false;
			}
		}
	}
}