using System;

using SQLite;

namespace MCCForms
{
	public class PasswordObject
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[NotNull]
		public string Name { get; set; }

		[NotNull]
		public string Password { get; set; }

		public string Category { get; set; } //to demonstrate picker

		public string PathToImage1 { get; set; }

		public string PathToImage2 { get; set; }

		public string PathToImage3 { get; set; }

		public string LocationName { get; set; } //to demonstrate location picker

		public string LocationAddress { get; set; }

		public string LocationZipAndCity { get; set; }

		[NotNull]
		public DateTime AccountSince { get; set; } = DateTime.Now; //to demonstrate date picker - today as default value

		[NotNull]
		public bool IsActive { get; set; } = true; //to demonstrate switch - true as default value

		[NotNull]
		public double ImportanceLevel { get; set; } //to demonstrate slider

		public override string ToString (){
			return "Values of changed object = Name: " + Name + " Password: " + Password; 
		}
	}
}