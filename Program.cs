namespace ZaverecnyProjektITnetworkRB
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//IDataProvider dataManager = new DataManagerCollection();
			IDataProvider dataManager = new DataManagerDatabase();
			Controller controller = new Controller(dataManager);
			Console.WriteLine("Register of insured persons");
			while (true)
			{
				controller.Loop();
			}
		}
	}
}