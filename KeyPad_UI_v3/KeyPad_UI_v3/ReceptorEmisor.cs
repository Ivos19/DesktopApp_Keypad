
using KeyPad_UI_v3.Models;
using System.Net.Sockets;
using System.Text;
using WindowsInput;


namespace KeyPad_UI_v3
{
	internal class ReceptorEmisor
	{
		private const int listenPort = 4211;
		private const int sendPort = 4210;
		private static UdpClient listener = new UdpClient(listenPort);
		private static InputSimulator inputSimulator = new InputSimulator();
		private static Config configFile = LectorConfiguracion.Leer();


		public static async Task Receptor()
		{
			try
			{
				while (true)
				{
					UdpReceiveResult result = await listener.ReceiveAsync();
					byte[] entrada = result.Buffer;
					string mensaje = Encoding.ASCII.GetString(entrada, 0, entrada.Length);

					SimularPulsacionDeTecla(mensaje);
				}
			}
			catch (SocketException ex)
			{
				Console.WriteLine(ex);
			}
			finally
			{
				listener.Close();
			}
		}
		public static void enviarMensaje_Click()
		{
			using (UdpClient udpClient = new UdpClient())
			{
				string mensaje = "Mensaje enviado desde la opcion del menu";
				byte[] data = Encoding.ASCII.GetBytes(mensaje);
				udpClient.Send(data, data.Length, "192.168.100.90", sendPort);
			}
		}

		private static void SimularPulsacionDeTecla(string udpKey)
		{
			// Buscar en GrupoUno
			foreach (var grupo in configFile.Macros.GrupoUno)
			{
				if (udpKey.Equals(grupo.UdpKey, StringComparison.OrdinalIgnoreCase))
				{
					SimularMacro(grupo.Macro);
					return;
				}
			}

			// Buscar en GrupoDos
			foreach (var grupo in configFile.Macros.GrupoDos)
			{
				if (udpKey.Equals(grupo.UdpKey, StringComparison.OrdinalIgnoreCase))
				{
					SimularMacro(grupo.Macro);
					return;
				}
			}

			// Buscar en GrupoTres
			foreach (var grupo in configFile.Macros.GrupoTres)
			{
				if (udpKey.Equals(grupo.UdpKey, StringComparison.OrdinalIgnoreCase))
				{
					SimularMacro(grupo.Macro);
					return;
				}
			}
		}

		/*private static void SimularMacro(string macro)
		{
			inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.MEDIA_PLAY_PAUSE);
		}*/

		private static void SimularMacro(string macro)
		{
			Dictionary<string, WindowsInput.Native.VirtualKeyCode> keyMappings = new Dictionary<string, WindowsInput.Native.VirtualKeyCode>
			{
				// Letras minúsculas
				{"a", WindowsInput.Native.VirtualKeyCode.VK_A},
				{"b", WindowsInput.Native.VirtualKeyCode.VK_B},
				{"c", WindowsInput.Native.VirtualKeyCode.VK_C},
				{"d", WindowsInput.Native.VirtualKeyCode.VK_D},
				{"e", WindowsInput.Native.VirtualKeyCode.VK_E},
				{"f", WindowsInput.Native.VirtualKeyCode.VK_F},
				{"g", WindowsInput.Native.VirtualKeyCode.VK_G},
				{"h", WindowsInput.Native.VirtualKeyCode.VK_H},
				{"i", WindowsInput.Native.VirtualKeyCode.VK_I},
				{"j", WindowsInput.Native.VirtualKeyCode.VK_J},
				{"k", WindowsInput.Native.VirtualKeyCode.VK_K},

				{"v", WindowsInput.Native.VirtualKeyCode.VK_V},
				// ... Agrega el resto de las letras minúsculas

				// Letras mayúsculas
				{"A", WindowsInput.Native.VirtualKeyCode.VK_A},
				{"B", WindowsInput.Native.VirtualKeyCode.VK_B},
				{"C", WindowsInput.Native.VirtualKeyCode.VK_C},
				{"D", WindowsInput.Native.VirtualKeyCode.VK_D},
				{"E", WindowsInput.Native.VirtualKeyCode.VK_E},
				{"F", WindowsInput.Native.VirtualKeyCode.VK_F},
				{"G", WindowsInput.Native.VirtualKeyCode.VK_G},
				{"H", WindowsInput.Native.VirtualKeyCode.VK_H},
				{"I", WindowsInput.Native.VirtualKeyCode.VK_I},
				{"J", WindowsInput.Native.VirtualKeyCode.VK_J},
				{"K", WindowsInput.Native.VirtualKeyCode.VK_K},

				{"V", WindowsInput.Native.VirtualKeyCode.VK_V},

				// Numeros
				{"1", WindowsInput.Native.VirtualKeyCode.VK_1},
				{"2", WindowsInput.Native.VirtualKeyCode.VK_2},
				{"3", WindowsInput.Native.VirtualKeyCode.VK_3},

				//Especiales
				{"VU", WindowsInput.Native.VirtualKeyCode.VOLUME_UP},
				{"VD", WindowsInput.Native.VirtualKeyCode.VOLUME_DOWN},



				// ... Agrega el resto de las letras mayúsculas

				// Símbolos básicos y otros caracteres
				{"+", WindowsInput.Native.VirtualKeyCode.OEM_PLUS},
				{"-", WindowsInput.Native.VirtualKeyCode.OEM_MINUS},
				// ... Agrega más símbolos y caracteres especiales

				// Teclas multimedia
				{"PlayPause", WindowsInput.Native.VirtualKeyCode.MEDIA_PLAY_PAUSE},
				{"VolumeUp", WindowsInput.Native.VirtualKeyCode.VOLUME_UP},
				{"VolumeDown", WindowsInput.Native.VirtualKeyCode.VOLUME_DOWN},
				// ... Agrega más teclas multimedia
			};

			var keysToPress = macro.Split(',');

			foreach (var key in keysToPress)
			{
				var keyPressed = keyMappings.TryGetValue(key, out WindowsInput.Native.VirtualKeyCode virtualKeyCode);
				if (char.IsUpper(key[0]) && keyPressed)
				{
					inputSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.SHIFT); // Mantener presionada la tecla SHIFT
					Thread.Sleep(50);
					inputSimulator.Keyboard.KeyPress(virtualKeyCode); // Simular la pulsación de la tecla
					Thread.Sleep(50);
					inputSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.SHIFT); // Soltar SHIFT
					Thread.Sleep(50);
				}
				else if (keyPressed)
				{
					inputSimulator.Keyboard.KeyPress(virtualKeyCode); // Simular la pulsación de la tecla 
					Thread.Sleep(100); // Pausa breve entre pulsaciones (ajusta según necesites)
				}
			}
		}
	}

}
