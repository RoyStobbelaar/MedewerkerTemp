using System;
using System.Linq;
using System.Diagnostics;
using MCCForms.Models;

using SQLite;
using Xamarin.Forms;

namespace MCCForms
{
	public partial class DatabaseHelper
	{
		class DatabaseConnection : SQLiteConnection
		{
			readonly object _synclock;

			public const string LatestDbVersion = "1";

			public DatabaseConnection (string dbPath, string pincode, object synclock) : base (dbPath, pincode, storeDateTimeAsTicks: true)
			{   
				_synclock = synclock;

				string dbVersion;

				try {
					Setting setting = TransactionWithResult<Setting> (() => 
						(from e in Table<Setting> () 
							where e.Key == StorageConstants.DbSettingVersionKey
							select e).FirstOrDefault<Setting> ());

					dbVersion = setting.Value;
				} 
				catch {
					dbVersion = string.Empty;
				}

				if (!string.IsNullOrEmpty (dbVersion) && !LatestDbVersion.Equals (dbVersion)) {
					UpgradeDatabase ();
				}

				Transaction (Initialize);

				if (string.IsNullOrEmpty (dbVersion)) {
					Setting setting = new Setting { Key = StorageConstants.DbSettingVersionKey, Value = LatestDbVersion };
					Transaction (() => InsertOrReplace (setting));

					//Store IV and salt to local db - important in case you change the pincode, these values are needed to decrypt the existing files
					Setting settingIV = new Setting { Key = StorageConstants.HashedPincodeKey, Value = pincode };
					Transaction (() => InsertOrReplace (settingIV));

					var salt = DependencyService.Get<ISecurity> ().GetValueFromKeyChain (StorageConstants.SaltStorageKey);
					Setting settingSalt = new Setting { Key = StorageConstants.SaltStorageKey, Value = salt };
					Transaction (() => InsertOrReplace (settingSalt));
				}
			}

			void Initialize ()
			{
				CreateTable<Setting> ();
				CreateTable<FilePathIV> ();
				CreateTable<PasswordObject> ();
				CreateTable<Appointment> ();
				CreateTable<Employee> ();
				CreateTable<Visitor> ();
			}

			void UpgradeDatabase ()
			{
				//Implement when needed in the future
			}

			public TResult TransactionWithResult<TResult> (Func<TResult> action)
			{
				lock (_synclock) {
					bool beganTransaction = false;
					try {
						if (!IsInTransaction) {
							beganTransaction = true;
							BeginTransaction ();
						}

						TResult result = action ();

						if (beganTransaction) {
							Commit ();
						}

						return result;

					} catch (Exception ex) {
						Debug.WriteLine ("Exception in " + ex.Source + ": " + ex.Message);
						if (beganTransaction) {
							Rollback ();
						}
						throw;
					}
				}
			}

			public void Transaction (Action action)
			{
				lock (_synclock) {
					bool beganTransaction = false;
					try {
						if (!IsInTransaction) {
							beganTransaction = true;
							BeginTransaction ();
						}

						action ();

						if (beganTransaction) {
							Commit ();
						}

					} catch (Exception ex) {
						Debug.WriteLine ("Exception in " + ex.Source + ": " + ex.Message);
						if (beganTransaction) {
							Rollback ();
						}
						throw;
					}
				}
			}
		}
	}
}