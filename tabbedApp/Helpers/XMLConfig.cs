// XMLConfig.cs
//
//  Author:
//       Carlos Jonathan Castro Méndez <ccastro@elfinanciero.com.mx>
//
//  Copyright (c) 2014 Grupo Lauman
//
using System;
using MonoTouch.Foundation;
using System.Xml.Serialization;
using System.IO;


namespace tabbedApp
{

	public class XMLConfig
	{
		[XmlAttribute ("url")]
		public string url { get; set; }

		[XmlAttribute ("id")]
		public string id { get; set; }



		public XMLConfig ()
		{
			url = "";
			id = "";
		}


		public  static XMLConfig FromXmlString (string xmlString)
		{
			try {
				var reader = new StringReader (xmlString);
				var serializer = new XmlSerializer (typeof(XMLConfig));
				var instance = (XMLConfig)serializer.Deserialize (reader);

				return instance;
			} catch (Exception ex) {

				Console.WriteLine (ex.Message);
				return null;
			}
		}
	}
}
