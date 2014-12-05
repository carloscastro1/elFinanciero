using System;
using System.Collections.Generic;

namespace tabbedApp
{
	public static class ComunicadosData
	{



		public static List<ComunicadosAnterioresModel> historialComunicados = new List<ComunicadosAnterioresModel> () {
			new ComunicadosAnterioresModel { 
				TituloComunicado = "Titulo comunicado 1", 
				origenComunicado = "Recursos Humanos", 
				urlComunicado = "http://files.consumerfinance.gov/f/201212_cfpb_doj-fair-lending-mou.pdf", 
				idEmpresaGrupoProveniente = "lauman", 
				fechaDeEmision = new DateTime (2014, 4, 14, 9, 0, 0)
			},
			new ComunicadosAnterioresModel { 
				TituloComunicado = "Titulo comunicado 2", 
				origenComunicado = "Finanzas", 
				urlComunicado = "http://www.bluej.org/tutorial/tutorial-201.pdf", 
				idEmpresaGrupoProveniente = "lauman", 
				fechaDeEmision = new DateTime (2014, 4, 14, 9, 0, 0)
			},
			new ComunicadosAnterioresModel { 
				TituloComunicado = "Titulo comunicado 3", 
				origenComunicado = "Seguridad", 
				urlComunicado = "http://online.wsj.com/public/resources/documents/CantorMemo.pdf", 
				idEmpresaGrupoProveniente = "lauman", fechaDeEmision = new DateTime (2014, 05, 16, 9, 0, 0)
			},
			new ComunicadosAnterioresModel { 
				TituloComunicado = "Titulo comunicado 4", 
				origenComunicado = "TV", 
				urlComunicado = "http://www.bluej.org/tutorial/tutorial-201.pdf", 
				idEmpresaGrupoProveniente = "lauman", fechaDeEmision = new DateTime (2014, 5, 16, 9, 0, 0) 
			},
			new ComunicadosAnterioresModel { 
				TituloComunicado = "Titulo comunicado 5", 
				origenComunicado = "Recursos Humanos", 
				urlComunicado = "http://www.imagen-europe.com/images/layout/imagen-logo.gif", 
				idEmpresaGrupoProveniente = "lauman", fechaDeEmision = new DateTime (2014, 6, 17, 9, 0, 0) 
			}
		};
	}
}

