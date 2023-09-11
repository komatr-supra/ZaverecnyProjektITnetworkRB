using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaverecnyProjektITnetworkRB
{
	public struct Policyholder
	{
		public string Name { get; private set; }
		public string Surname { get; private set; }
		public string TelNumber { get; private set; }

		public int Age { get; private set; }
		public Sex Gender { get; private set; }

		public Policyholder(string name, string surname, string telNumber,  int age, Sex sex)
		{
			this.Name = name;
			this.Surname = surname;
			this.TelNumber = telNumber;
			this.Age = age;
			this.Gender = sex;
		}

		public override string ToString()
		{
			return string.Format("{0,-12} {1,-12} {2,-5} {3,-15} {4, -10}", Name, Surname, Age, TelNumber, Gender);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj is not Policyholder) return false;

			Policyholder otherPolicyholder = (Policyholder)obj;
			return this.Name == otherPolicyholder.Name && this.Surname == otherPolicyholder.Surname && this.Age == otherPolicyholder.Age;

		}
		public override int GetHashCode()
		{
			int hashCode = 17;

			hashCode = hashCode * 23 + (Name != null ? Name.GetHashCode() : 0);
			hashCode = hashCode * 23 + (Surname != null ? Surname.GetHashCode() : 0);
			hashCode = hashCode * 23 + (TelNumber != null ? TelNumber.GetHashCode() : 0);
			hashCode = hashCode * 23 + Age.GetHashCode();
			hashCode = hashCode * 23 + Gender.GetHashCode();

			return hashCode;
		}
		public enum Sex { [Description("Male")] MALE, [Description("Female")] FEMALE }
	}
}
