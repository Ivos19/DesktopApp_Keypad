using KeyPad_UI_v3.Models;
using Newtonsoft.Json;

namespace KeyPad_UI_v3
{
	public class LectorConfiguracion
	{
		const string configFilePath = @"\Configuracion\Configuracion.json";
		public static Config Leer()
		{
			Config configFile = LeerArchivo(configFilePath);
			return configFile;
		}

		private static Config LeerArchivo(string path)
		{
			string filePath = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + path);

			string jsonFile = System.IO.File.ReadAllText(filePath);

			return JsonConvert.DeserializeObject<Config>(jsonFile);
		}
	}


}
