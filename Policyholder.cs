using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaverecnyProjektITnetworkRB
{
	//data about policyholder
	public struct Policyholder
	{
		private readonly string name;
		public string Name { get { return name; } }
		private readonly string surname;
		public string Surname { get { return surname; } }
		private readonly string telNumber;
		private readonly int age;

		public Policyholder(string name, string surname, string telNumber,  int age)
		{
			this.name = name;
			this.surname = surname;
			this.telNumber = telNumber;
			this.age = age;
		}
		public override string ToString()
		{
			return string.Format("{0,-12} {1,-12} {2,-5} {3,-15}", name, surname, age, telNumber);
		}
	}
}
