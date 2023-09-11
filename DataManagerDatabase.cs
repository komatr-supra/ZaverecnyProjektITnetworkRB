using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZaverecnyProjektITnetworkRB
{
	internal class DataManagerDatabase : IDataProvider
	{
		
		private readonly DBConnectionStringProvider _connectionStringProvider;
		private string connectionString => _connectionStringProvider.ConnectionString;
		public DataManagerDatabase()
		{
			_connectionStringProvider = new DBConnectionStringProvider();
		}

		public void AddPolicy(Policyholder policy)
		{
			using (SqlConnection connection = new(connectionString))
			{
				connection.Open();

				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.Parameters.AddWithValue("@Name", policy.Name);
				command.Parameters.AddWithValue("@Surname", policy.Surname);
				command.Parameters.AddWithValue("@Age", policy.Age);
				command.Parameters.AddWithValue("@TelNumber", policy.TelNumber);
				command.Parameters.AddWithValue("@Gender", (int)policy.Gender);
				command.CommandText = $"insert into [Policyholder]([name], [surname], [age], [telephone number], [gender]) values(@Name, @surname, @Age, @TelNumber, @Gender)";
													
				int changedRowCount = command.ExecuteNonQuery();
				if(changedRowCount == 0) Console.WriteLine("Writing to database FAILED!");
				//else Console.WriteLine("Saved");
            }
		}

		public IEnumerable<string> GetPolicies()
		{
			using (SqlConnection connection = new(connectionString))
			{
				connection.Open();

				SqlCommand commandRecordCounts = new SqlCommand();
				commandRecordCounts.Connection = connection;
				commandRecordCounts.CommandText = $"select * from [Policyholder]";

				using (var reader = commandRecordCounts.ExecuteReader())
				{
					while (reader.Read())
					{
						//pass index 0 -> its ID... dont care
						yield return string.Format("{0,-12} {1,-12} {2,-5} {3,-15} {4, -10}", reader[1], reader[2], reader[3], reader[4], (Policyholder.Sex)(byte)reader[5]);
					}
				}
			}
		}
		public void RemovePolicy(Policyholder policy)
		{
			using (SqlConnection connection = new(connectionString))
			{
				connection.Open();

				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.Parameters.AddWithValue("@Name", policy.Name);
				command.Parameters.AddWithValue("@Surname", policy.Surname);
				command.CommandText = $"delete from [Policyholder] where @Name = [name] and @surname = [surname]";

				int changedRowCount = command.ExecuteNonQuery();
				if (changedRowCount == 0) Console.WriteLine("Writing to database FAILED!");
				//else Console.WriteLine("deleted");
			}
		}

		public bool TryFindPolicyholderByName(string policyName, string policySurname, out Policyholder policyholder)
		{
			using (SqlConnection connection = new(connectionString))
			{
				connection.Open();

				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.Parameters.AddWithValue("@Name", policyName);
				command.Parameters.AddWithValue("@Surname", policySurname);
				command.CommandText = $"select * from [Policyholder] where @Name = [name] and @Surname = [surname]";

				var reader = command.ExecuteReader();
				if (reader.Read())
				{
					policyholder = new Policyholder(reader[1].ToString(), reader[2].ToString(), reader[4].ToString(), (byte)reader[3], (Policyholder.Sex)(byte)reader[5]);
					return true;
				}
				policyholder = default;
				return false;
			}
		}
	}
}
