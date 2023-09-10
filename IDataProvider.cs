using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaverecnyProjektITnetworkRB
{
	public interface IDataProvider
	{
		void AddPolicy(Policyholder policy);
		void RemovePolicy(Policyholder policy);
		IEnumerable<string> GetPolicies();
		bool TryFindPolicyholderByName(string policyName, string policySurname, out Policyholder policyholder);
	}
}
