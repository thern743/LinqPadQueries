<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Xml.Serialization.dll</Reference>
  <Namespace>System.Xml.Serialization</Namespace>
</Query>

void Main()
{
	// #############
	// Three records
	var testXml = "<ROOT><ROW><ENTR_ID>2569123456789876</ENTR_ID><PSTL_CD>33603-1610</PSTL_CD><ADDR_CNTN_LINE_ONE_TX>  123  5^     </ADDR_CNTN_LINE_ONE_TX><CRDT_LINE_CHNG_DT>07-25-17</CRDT_LINE_CHNG_DT></ROW><ROW><ENTR_ID>6789876256912345</ENTR_ID><PSTL_CD>66303-9410</PSTL_CD><ADDR_CNTN_LINE_ONE_TX>123  7^       </ADDR_CNTN_LINE_ONE_TX><CRDT_LINE_CHNG_DT>12-14-16</CRDT_LINE_CHNG_DT></ROW><ROW><ENTR_ID>2569123456789876</ENTR_ID><PSTL_CD>74832-7635</PSTL_CD><ADDR_CNTN_LINE_ONE_TX>123  9^         </ADDR_CNTN_LINE_ONE_TX><CRDT_LINE_CHNG_DT>03-02-12     </CRDT_LINE_CHNG_DT></ROW></ROOT>";
	// #############
	
	// ###########
	// Scenario 1
	// ###########
	Console.WriteLine("Scenario 1:");
	var xmlDoc = XDocument.Parse(testXml, LoadOptions.None); //LoadOptions.PreserveWhitespace
	xmlDoc.Dump();

	var xmlSerializer = new XmlSerializer(typeof(FirstDataRowList<Cap>));
	var settings = new XmlReaderSettings { IgnoreWhitespace = false };

	using (var docReader = XmlReader.Create(xmlDoc.CreateReader(), settings))
	{
		var myTestEntities = xmlSerializer.Deserialize(docReader);// as FirstDataRowList<Cap>;
		myTestEntities.Dump();
	}

	// ###########
	// Scenario 2
	// ###########

	Console.WriteLine("Scenario 2:");
	var xmlSerializer2 = new XmlSerializer(typeof(FirstDataRowList<Cap>));	
	TextReader stream = new StringReader(testXml);	
	var myTestEntities2 = xmlSerializer2.Deserialize(stream);// as FirstDataRowList<Cap>;
	myTestEntities2.Dump();

	// ###########
	// Serialization
	// ###########
	var myNewTestEntities = new List<Cap> {
		new Cap { ENTR_ID = "123456", PSTL_CD = "ABC     ", ADDR_CNTN_LINE_ONE_TX = new WhitespaceXmlElement("TEST123   ") },
		new Cap { ENTR_ID = "45678", PSTL_CD = "ZXY", ADDR_CNTN_LINE_ONE_TX = new WhitespaceXmlElement("TEST456   ") },
		new Cap { ENTR_ID = "284635886", PSTL_CD = "            NMQ", ADDR_CNTN_LINE_ONE_TX = new WhitespaceXmlElement("TEST789   ") }
	};

	var fdRowList = new FirstDataRowList<Cap> { Row = myNewTestEntities };
	var newXDoc = new XDocument();
	
	using (var xmlWriter = newXDoc.CreateWriter())
	{
		xmlSerializer.Serialize(xmlWriter, fdRowList);		
	}
	
	newXDoc.Dump();
}

[XmlRoot("ROOT")]
public class FirstDataRowList<T>
{
	[XmlElement("ROW")]
	public List<T> Row { get; set;}
}

public class Cap
{
	public string ENTR_ID { get; set; }
	public int ENTR_ID_LEN { get { return ENTR_ID.Length; } }

	public WhitespaceXmlElement ADDR_CNTN_LINE_ONE_TX { get; set; }
	public int ADDR_CNTN_LINE_ONE_TX_LEN { get { return ADDR_CNTN_LINE_ONE_TX.Value.Length; } }

	public string PSTL_CD { get; set; }
	public int PSTL_CD_LEN { get { return PSTL_CD.Length; } }

	public string CRDT_LINE_CHNG_DT { get; set;}
	public int CRDT_LINE_CHNG_DT_LEN { get { return CRDT_LINE_CHNG_DT.Length; } }
}

public class WhitespaceXmlElement {
	[XmlText]
	public string Value {get; set;}
	
	[XmlAttribute("space", Namespace = "http://www.w3.org/XML/1998/namespace")]
	public string Space = "preserve";

	public WhitespaceXmlElement() { }

	public WhitespaceXmlElement(string value)
	{
		Value = value;
	}
}