namespace KeyPad_UI_v3.Models
{
	public class Config
	{
		public Configuraciones Configuraciones { get; set; }
		public Macros Macros { get; set; }
	}

	public class Configuraciones
	{
		public int Version { get; set; }
		public int Puerto { get; set; }
	}

	public class Macros
	{
		public GrupoUno[] GrupoUno { get; set; }
		public GrupoDos[] GrupoDos { get; set; }
		public GrupoTres[] GrupoTres { get; set; }
	}

	public class GrupoUno
	{
		public string UdpKey { get; set; }
		public string Macro { get; set; }
	}

	public class GrupoDos
	{
		public string UdpKey { get; set; }
		public string Macro { get; set; }
	}

	public class GrupoTres
	{
		public string UdpKey { get; set; }
		public string Macro { get; set; }
	}

}