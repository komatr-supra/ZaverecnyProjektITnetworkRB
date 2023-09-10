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

				SqlCommand commandRecordCounts = new SqlCommand();
				commandRecordCounts.Connection = connection;
				commandRecordCounts.CommandText = $"insert into [Policyholder] values({policy.Name}, {policy.Surname}, {policy.Age}, {policy.TelNumber}, {(int)policy.Gender})";
													
				int changedRowCount = commandRecordCounts.ExecuteNonQuery();
				if(changedRowCount == 0) Console.WriteLine("Writing to database FAILED!");
				else Console.WriteLine("Saved");
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
						yield return string.Format("{0,-12} {1,-12} {2,-5} {3,-15} {4, -10}", reader[1], reader[2], reader[3], reader[4], reader[5]);
					}
				}
			}
		}
		public void RemovePolicy(Policyholder policy)
		{
			using (SqlConnection connection = new(connectionString))
			{
				connection.Open();

				SqlCommand commandRecordCounts = new SqlCommand();
				commandRecordCounts.Connection = connection;
				commandRecordCounts.CommandText = $"delete from [Policyholder] where{policy.Name} = [name], {policy.Surname} = [surname], {policy.Age} = [age], {policy.TelNumber} = [telephone number], {(int)policy.Gender} = [gender])";

				int changedRowCount = commandRecordCounts.ExecuteNonQuery();
				if (changedRowCount == 0) Console.WriteLine("Writing to database FAILED!");
				else Console.WriteLine("deleted");
			}
		}

		public bool TryFindPolicyholderByName(string policyName, string policySurname, out Policyholder policyholder)
		{
			
		}
	}
}
