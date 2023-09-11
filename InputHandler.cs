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
		public Action OnRemovePolicyholder;
		public Action OnQuit;

		private readonly string[] commands = { "add a new policyholder", "show all policyholders", "find a policyholder", "remove a policyholder", "exit" };
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
				case 4: OnRemovePolicyholder?.Invoke(); break;
				case 5: OnQuit?.Invoke(); break;
			}            
		}

		public string GetInputText() => Console.ReadLine();
		
		private void ShowControls()
		{
			//empty line is for visual separation in command line
            Console.WriteLine();
            for (int i = 0; i < commands.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {commands[i]}");
            }
            Console.Write("Command: ");
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
			if(int.TryParse(commandString, out commandNumber) && commandNumber > 0 && commandNumber <= commands.Length) return true;
			return false;
		}
	}
}
