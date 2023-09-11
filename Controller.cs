using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaverecnyProjektITnetworkRB
{
	//control flow of the program
	internal class Controller
	{
		private readonly InputHandler _inputHandler;
		private readonly IDataProvider _dataManager;
		public Controller(IDataProvider dataManager)
		{
			this._dataManager = dataManager;
			_inputHandler = new InputHandler();

			_inputHandler.OnQuit = ()=> { Environment.Exit(0); };
			_inputHandler.OnFindPolicyholder = () => { FindPolicyholderByName(false); };
			_inputHandler.OnNewPolicyholder = () => { AddNewInsurance(); };
			_inputHandler.OnShowAllPolicyholders = () => { ShowAllPolicyholders(); };
			_inputHandler.OnRemovePolicyholder = () => { FindPolicyholderByName(true); };
		}

		public void Loop() => _inputHandler.ProceedCommand();

		private void ShowAllPolicyholders()
		{
			var policyholder = _dataManager.GetPolicies().ToArray();//ForEach(policy => { Console.WriteLine(policy); });
			Console.WriteLine(string.Format("{0,-12} {1,-12} {2,-5} {3,-15} {4, -10}", "Name", "Surname", "Age", "TelNumber", "Gender"));

			foreach (var policy in policyholder)
            {
				Console.WriteLine(policy);
			}
        }
		
		private void FindPolicyholderByName(bool remove)
		{
            Console.WriteLine("Name");
			string name = _inputHandler.GetInputText();
            Console.WriteLine("Surname");
			string surname = _inputHandler.GetInputText();
			if (!_dataManager.TryFindPolicyholderByName(name, surname, out Policyholder policyholder)) Console.WriteLine("NO RECORD");
			if(remove)
			{
				_dataManager.RemovePolicy(policyholder);
				Console.WriteLine("Policyholder was removed");
				return;
			}
			Console.WriteLine(policyholder);
			
        }

		private void AddNewInsurance()
		{
            Console.WriteLine("Name of new policyholder");
            string name = _inputHandler.GetInputText();

			Console.WriteLine("Surname of new policyholder");
			string surname = _inputHandler.GetInputText();

			Console.WriteLine("Telephone number");
			string telNumber;
            while (!TestTelephoneNumber(_inputHandler.GetInputText(), out telNumber))
            {
                Console.WriteLine("Phone number is wrong. Type a real one.");
            }

            Console.WriteLine("Age");
			int age;
            while (!int.TryParse(_inputHandler.GetInputText(), out age) || age < 0 || age > 120)
            {
                Console.WriteLine("This is not a valid age! Wrote a valid one.");
            }

			string sexString;
			Console.WriteLine("Sex (male, female)");
			sexString = _inputHandler.GetInputText().Trim().ToLower();
			while (!(sexString == "male") && !(sexString == "female"))
			{
				Console.WriteLine("This is not a valid sex! Wrote a valid one.");
				sexString = _inputHandler.GetInputText().ToLower();
			}
			Policyholder.Sex sex = sexString == "male" ? Policyholder.Sex.MALE : Policyholder.Sex.FEMALE;

			Policyholder policyholder = new Policyholder(name, surname, telNumber, age, sex);
			_dataManager.AddPolicy(policyholder);
            Console.WriteLine("New policyholder saved");
        }		

		/// <summary>
		/// check if telephone number is in right format(input and output string are equals)
		/// </summary>
		/// <param name="telNumberText">input tel number as string</param>
		/// <param name="telNumber">output tel number as string</param>
		/// <returns>true if telephone number is valid</returns>
		private bool TestTelephoneNumber(string telNumberText, out string telNumber)
		{
			telNumber = telNumberText;
			//check if number length is right and if first char is number or '+'
			if (telNumberText.Length < 9 && telNumberText.Length > 13 && (!(telNumberText[0] == '+') || !IsANumber(telNumberText[0])))
			{
                Console.WriteLine("first check in phone fail");
                return false;
			}

            foreach (char character in telNumberText.Substring(1)) if (!IsANumber(character)) return false;
            
			return true;

			bool IsANumber(char character) => character >= '0' && character <= '9';
        }
		
	}
}
