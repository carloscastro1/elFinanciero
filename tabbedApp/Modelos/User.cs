// User.cs
//
//  Author:
//       Carlos Jonathan Castro Méndez <ccastro@elfinanciero.com.mx>
//
//  Copyright (c) 2014 Grupo Lauman
//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace tabbedApp
{
	public class User
	{
		public string IdNumero_empleado { get; set; }

		public string NumeroEmpleadoCredencial { get; set; }

		public string idGerencia { get; set; }

		public string idSubgerencia { get; set; }

		public string Nombre { get; set; }

		public string ApPaterno { get; set; }

		public string ApMaterno { get; set; }

		public string FechaNac { get; set; }

		public string edad { get; set; }

		public string RFC { get; set; }

		public string NSS { get; set; }

		public string Curp { get; set; }

		public string idEstadoCivil { get; set; }

		public string idTipoContrato { get; set; }

		public string idPuestoInicial { get; set; }

		public string idPuestoActual { get; set; }

		public string idSexo { get; set; }

		public string idBranchLocation { get; set; }

		public string idTipoSueldo { get; set; }

		public string sueldoAnterior { get; set; }

		public string sueldoActual { get; set; }

		public object sueldoMixtoTotal { get; set; }

		public object fechaUltimoAumentoSueldo { get; set; }

		public string sueldoLetra { get; set; }

		public object fechaContratacion { get; set; }

		public object fechaVencimientoContrato { get; set; }

		public object fechaContratacionIndeterminada { get; set; }

		public string idTipoUsuario { get; set; }

		public string email { get; set; }

		public string password { get; set; }
	}

	public class RootObject
	{
		public int status { get; set; }

		public List<User> info { get; set; }
	}
}

