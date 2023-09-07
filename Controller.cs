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
		private readonly InputHandler inputHandler;
		private readonly DataManager dataManager;

		public Controller(DataManager dataManager)
		{
			this.dataManager = dataManager;
			inputHandler = new InputHandler();

			inputHandler.OnQuit = ()=> { Environment.Exit(0); };
			inputHandler.OnFindPolicyholder = () => { FindPolicyholderByName(); };
			inputHandler.OnNewPolicyholder = () => { AddNewInsurance(); };
			inputHandler.OnShowAllPolicyholders = () => { ShowAllPolicyholders(); };
		}

		public void Loop() => inputHandler.ProceedCommand();

		private void ShowAllPolicyholders() => dataManager.GetPolicies().ForEach(policy => { Console.WriteLine(policy); });
		
		private void FindPolicyholderByName()
		{
            Console.WriteLine("Name");
			string name = inputHandler.GetInputText();
            Console.WriteLine("Surname");
			string surname = inputHandler.GetInputText();

			if (dataManager.TryFindPolicyholderByName(name, surname, out Policyholder policyholder)) Console.WriteLine(policyholder);
			else Console.WriteLine("NO RECORD");
        }

		private void AddNewInsurance()
		{
            Console.WriteLine("Name of new policyholder");
            string name = inputHandler.GetInputText();

			Console.WriteLine("Surname of new policyholder");
			string surname = inputHandler.GetInputText();

			Console.WriteLine("Telephone number");
			string telNumber;
            while (!TestTelephoneNumber(inputHandler.GetInputText(), out telNumber))
            {
                Console.WriteLine("Phone number is wrong. Type a real one.");
            }

            Console.WriteLine("Age");
			int age;
            while (!int.TryParse(inputHandler.GetInputText(), out age) || age < 0 || age > 120)
            {
                Console.WriteLine("This is not a valid age! Wrote a valid one.");
            }

			Policyholder policyholder = new Policyholder(name, surname, telNumber, age);
			dataManager.AddPolicy(policyholder);
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
