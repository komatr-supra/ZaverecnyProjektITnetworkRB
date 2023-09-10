using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ZaverecnyProjektITnetworkRB
{
	internal class DataManagerCollection : IDataProvider
	{
		private List<Policyholder> _policies;
		public DataManagerCollection() 
		{
			_policies = new List<Policyholder>();
		}
		public void AddPolicy(Policyholder policy)
		{
			_policies.Add(policy);
		}
		public void RemovePolicy(Policyholder policy)
		{
			_policies.Remove(policy);
		}
		public IEnumerable<string> GetPolicies()
		{
            foreach (var item in _policies)
            {
                yield return item.ToString();
            }
		}
		/// <summary>
		/// find policyholder in collection
		/// </summary>
		/// <param name="policyName">name of policyholder</param>
		/// <param name="policySurname">surname of policyholder</param>
		/// <param name="policyholder">struct of policyholder</param>
		/// <returns>true if policyholder was found</returns>
		public bool TryFindPolicyholderByName(string policyName, string policySurname, out Policyholder policyholder)
		{
			
			foreach (Policyholder policy in _policies)
			{
				if(policy.Name == policyName && policy.Surname == policySurname)
				{
					policyholder = policy;
					return true;
				}
			}
			policyholder = default;
			return false;
		}
	}
}
