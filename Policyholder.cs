using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaverecnyProjektITnetworkRB
{
	//data about policyholder
	public class Policyholder
	{
		private readonly string name;
		public string Name { get { return name; } }
		private readonly string surname;
		public string Surname { get { return surname; } }
		private readonly string telNumber;
		private readonly int age;
		private readonly Sex sex;

		public Policyholder(string name, string surname, string telNumber,  int age, Sex sex)
		{
			this.name = name;
			this.surname = surname;
			this.telNumber = telNumber;
			this.age = age;
			this.sex = sex;
		}

		public override string ToString()
		{
			return string.Format("{0,-12} {1,-12} {2,-5} {3,-15}", name, surname, age, telNumber);
		}

		public override bool Equals(object? obj)
		{
			if (obj == null || obj.GetType() != GetType()) return false;

			Policyholder otherPolicyholder = obj as Policyholder;
			if (this.name != otherPolicyholder.Name || this.surname != otherPolicyholder.Surname || this.age != otherPolicyholder.age) return false;

			return true;
		}
		
		public enum Sex { MALE, FEMALE}
	}
}
