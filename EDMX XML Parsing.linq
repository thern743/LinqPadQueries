<Query Kind="Statements" />

XDocument xmlDoc;

using (var file = File.OpenText($"C:\\Users\\bw6y\\Documents\\LINQPad Queries\\TestEdmx.xml"))
{
	xmlDoc = XDocument.Load(file);	
}

XNamespace edmxNs = "http://docs.oasis-open.org/odata/ns/edmx";
//
//var entityElement1 = xmlDoc.Descendants(edmxNs + "DataServices");
//
////entityElement1.Dump();
//
XNamespace viewNs = "http://docs.oasis-open.org/odata/ns/edm";
//
//var entityElement2 = entityElement1
//    .Elements(viewNs + "Schema");
//
//entityElement2.Dump();
//
//var entityElement3 = entityElement2.Where(e => e.Attributes("Namespace").First().Value == "Cpas.FirstData.Views");
//
//entityElement3.Dump();
//
//var entityElement4 = entityElement2.Where(e => e.Attributes("Namespace").First().Value == "Default");
//
//entityElement4.Dump();
//
//var entityElement5 = entityElement3
//    .Elements().FirstOrDefault(element => element.Attribute("Name").Value == "Cap");
//
//entityElement5.Dump();
//
//var entityElement6 = entityElement3
//	.Elements().FirstOrDefault(element => element.Attribute("Name").Value == "Testing123");
//
//entityElement6.Dump();

//var viewElements = xmlDoc
//				.Descendants(edmxNs + "DataServices")
//				.Elements(viewNs + "Schema")
//				.Where(e => e.Attributes("Namespace").First().Value == "Cpas.FirstData.Views")
//				.Elements();
//
//viewElements.Dump();
//
//var rpcElements = xmlDoc
//	.Descendants(edmxNs + "DataServices")
//	.Elements(viewNs + "Schema")
//	.Where(e => e.Attributes("Namespace").First().Value == "Default")
//	.Elements();
//
//rpcElements.Dump();

var entitySets = xmlDoc
	.Descendants(edmxNs + "DataServices")
	.Elements(viewNs + "Schema")
	.Where(e => e.Attributes("Namespace").First().Value == "Default")
	.Elements(viewNs + "EntityContainer")
	.Elements(viewNs + "EntitySet");

entitySets.Dump();

var actionImport = xmlDoc
	.Descendants(edmxNs + "DataServices")
	.Elements(viewNs + "Schema")
	.Where(e => e.Attributes("Namespace").First().Value == "Default")
	.Elements(viewNs + "EntityContainer")
	.Elements(viewNs + "ActionImport");

actionImport.Dump();