using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaverecnyProjektITnetworkRB
{
	internal class DBConnectionStringProvider
	{
        public string ConnectionString { get; private set; }
        public DBConnectionStringProvider()
		{
			string actualDir = Directory.GetCurrentDirectory();
			string databaseDir = actualDir;
			//go 3 levels up -> database directory
			for (int i = 0; i < 3; i++)
			{
				DirectoryInfo parentDirectory = Directory.GetParent(databaseDir);
				databaseDir = parentDirectory.FullName;
			}
			databaseDir += "\\Database1.mdf";
			SqlConnectionStringBuilder sqlonnectionString = new SqlConnectionStringBuilder();
			sqlonnectionString.DataSource = @"(LocalDB)\MSSQLLocalDB";
			sqlonnectionString.AttachDBFilename = databaseDir;
			sqlonnectionString.IntegratedSecurity = true;
			ConnectionString = sqlonnectionString.ConnectionString;
		}
	}
}
