// Comunicado.cs
//
//  Author:
//       Carlos Jonathan Castro Méndez <ccastro@elfinanciero.com.mx>
//
//  Copyright (c) 2014 Grupo Lauman
//
using System;
using System.Collections.Generic;

namespace tabbedApp
{
	public class Comunicado
	{
		public string fechaEmision { get; set; }

		public string idEmpresa { get; set; }

		public string origenComunicado { get; set; }

		public string TituloComunicado { get; set; }

		public string urlComunicado { get; set; }
	}

	public class Comunicados
	{
		public List<Comunicado> Comunicado { get; set; }
	}

	public class RootObjectComunicado
	{
		public Comunicados Comunicados { get; set; }
	}
}

