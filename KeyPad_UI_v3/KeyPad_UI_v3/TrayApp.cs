using System.Diagnostics;

namespace KeyPad_UI_v3
{
	public class TrayApp : ApplicationContext
	{
		private NotifyIcon trayIcon;
		private string lastReceivedMessage = string.Empty;

		public TrayApp()
		{
			// Crear el ícono en la bandeja del sistema
			trayIcon = new NotifyIcon()
			{
				Icon = new Icon(Path.Combine(Application.StartupPath, "keypad_icon.ico")), // Icono predeterminado
				Text = "KeyPad!",
				Visible = true
			};

			// Agregar un menú contextual al ícono
			var contextMenu = new ContextMenuStrip();

			var showMenuItem = new ToolStripMenuItem("Conectar!");
			showMenuItem.Click += SendMenuItem_Click;

			var exitMenuItem = new ToolStripMenuItem("Salir!");
			exitMenuItem.Click += Exit;

			var openJsonFileMenuItem = new ToolStripMenuItem("Configuracion");
			openJsonFileMenuItem.Click += OpenJsonFileMenuItem_Click;


			contextMenu.Items.Add(openJsonFileMenuItem);
			contextMenu.Items.Add(showMenuItem);
			contextMenu.Items.Add(exitMenuItem);

			trayIcon.ContextMenuStrip = contextMenu;

			//Config configFile = LectorConfiguracion.Leer();

			// Iniciar el receptor como tarea asincrónica
			Task receptorTask = ReceptorEmisor.Receptor();
		}

		private void ShowMessage(object sender, EventArgs e)
		{
			MessageBox.Show("Hola desde la bandeja del sistema!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void Exit(object sender, EventArgs e)
		{
			// Limpiar recursos antes de salir
			trayIcon.Visible = false;
			Application.Exit();
		}

		private static void SendMenuItem_Click(object sender, EventArgs e)
		{
			ReceptorEmisor.enviarMensaje_Click();
		}

		private void OpenJsonFileMenuItem_Click(object sender, EventArgs e)
		{
			string jsonFilePath = Path.Combine(Application.StartupPath, "config.json");

			if (File.Exists(jsonFilePath))
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = jsonFilePath,
					UseShellExecute = true
				});
			}
			else
			{
				MessageBox.Show("El archivo JSON no se encontró.", "Archivo No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}
