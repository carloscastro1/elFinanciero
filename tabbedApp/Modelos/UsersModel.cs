using System;

namespace tabbedApp
{
	public class UsersModel
	{

		public string Nombre { get; set;}
		public string ApPaterno { get; set;}
		public string ApMaterno { get; set;}
		public DateTime FecNac { get; set;}
		public string Departamento { get; set;}
		public string subDepartamento { get; set;}
		public string JefeDirecto { get; set;}
		public string NumeroExtensionTelefono{ get; set;}
		public string correoElectronico { get; set;}
		public string empresaNombre{ get; set;}
		public int empresaId { get; set;}
		public string HorarioLaboral { get; set;}


		public UsersModel ()
		{
		}
	}
}

