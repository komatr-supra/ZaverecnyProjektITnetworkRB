namespace ZaverecnyProjektITnetworkRB
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DataManager dataManager = new DataManager();
			Controller controller = new Controller(dataManager);
			Console.WriteLine("Register of insured persons");
			while (true)
			{
				controller.Loop();
			}
		}
	}
}