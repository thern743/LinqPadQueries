<Query Kind="Statements" />

var xml = "<FDR version=\"1.0\">  <ODSREPLY RC=\"0061\">    <USERDATA>CAP</USERDATA>    <MESSAGE RC=\"0061\" SEVERITY=\"1\">\"ERROR\",0061,2,\"INVALID_FIELD      INVALID SELECT NAME\",1,,,,\"V2OSSEL   08\",\"CAP\",\"09/27/17 16:47:01\",\"MQC9 CRT\",,\"C$CBC27A\"</MESSAGE>  </ODSREPLY></FDR>";

var xmlDoc = XDocument.Parse(xml);

var userData = xmlDoc.Descendants("USERDATA").First().Value;
userData.Dump();

var fullError = xmlDoc.Descendants("MESSAGE").First().Value;
fullError.Dump();

var returnCode = xmlDoc.Descendants("MESSAGE").First().Attributes().First(d => d.Name == "RC").Value;
returnCode.Dump();

var message = xmlDoc.Descendants("MESSAGE").First().Value.Split(',')[3];
message.Dump();