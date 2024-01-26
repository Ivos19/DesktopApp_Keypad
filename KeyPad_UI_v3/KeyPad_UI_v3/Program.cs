namespace KeyPad_UI_v3
{
	internal static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			ReceptorEmisor.enviarMensaje_Click();
			Application.Run(new TrayApp());
		}
	}
}