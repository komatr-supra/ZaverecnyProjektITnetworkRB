using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaverecnyProjektITnetworkRB
{
	//manage input from keyboard
	internal class InputHandler
	{
		#region VARIABLES
		public Action OnNewPolicyholder;
		public Action OnShowAllPolicyholders;
		public Action OnFindPolicyholder;
		public Action OnQuit;
		#endregion

		/// <summary>
		/// this is basicly app loop
		/// </summary>
		public void ProceedCommand()
		{
			ShowControls();
			int commandNumber;

			while (!TryCommand(GetInputText(), out commandNumber))
			{
                Console.WriteLine("This is not a valid command!");
				ShowControls();
            }
            
            //Console.WriteLine("DEBUG: input number = " + commandNumber);
            switch (commandNumber)
			{
				case 1: OnNewPolicyholder?.Invoke(); break;
				case 2: OnShowAllPolicyholders?.Invoke(); break;
				case 3: OnFindPolicyholder?.Invoke(); break;
				case 4: OnQuit?.Invoke(); break;
			}            
		}

		public string GetInputText() => Console.ReadLine();
		
		private void ShowControls()
		{
			//empty line is for visual separation in command line
            Console.WriteLine();
            Console.WriteLine("1 - add a new policyholder");
            Console.WriteLine("2 - show all policyholders");
            Console.WriteLine("3 - find a policyholder");
            Console.WriteLine("4 - exit");
		}

		/// <summary>
		/// Check if command string is valid
		/// </summary>
		/// <param name="commandString">string to check</param>
		/// <param name="commandNumber">command</param>
		/// <returns>true, if it is a valid command</returns>
		private bool TryCommand(string commandString, out int commandNumber)
		{
			commandNumber = 0;
			if(int.TryParse(commandString, out commandNumber) && commandNumber > 0 && commandNumber <= 4) return true;
			return false;
		}
	}
}
