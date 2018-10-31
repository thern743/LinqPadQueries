<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Security</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var chd = "1234567898761234";
	var incomingData = "{ 'PI_ID': '" + chd + "', 'CUST_NM': 'TESTGUY' }";
	var testData = JsonConvert.DeserializeObject<Cap>(incomingData);
	
	// Default: show 4 digits
	testData.Dump();

	// Override default digits
	testData.PI_ID.NumberOfDigitsToShow = 2;
	testData.Dump();

	// Override mask length
	testData.PI_ID.TotalDisplayLength = 6;
	testData.Dump();

	// Override mask char
	testData.PI_ID.MaskChar = '#';
	testData.Dump();
	
	// Override everything
	testData.PI_ID.NumberOfDigitsToShow = 6;
	testData.PI_ID.TotalDisplayLength = 10;
	testData.PI_ID.MaskChar = '-';
	testData.Dump();

	// Mask whole string
	testData.PI_ID.NumberOfDigitsToShow = 0;
	testData.PI_ID.TotalDisplayLength = 16;
	testData.PI_ID.MaskChar = '*';
	testData.Dump();

	// ToString()
	testData.PI_ID.NumberOfDigitsToShow = 4;
	var stringData = testData.PI_ID.ToString();
	stringData.Dump();
	
	// Compare values
	var areEqual = testData.PI_ID.Equals(chd);
	areEqual.Dump();

	areEqual = testData.PI_ID == chd;
	areEqual.Dump();

	areEqual = testData.PI_ID != chd;
	areEqual.Dump();
	
	// Simulate conversions from other classes with basic "strings"	
	var otherData = new MyPlainStringClass { PI_ID = chd }; // Uses basic string
	var maskedVersionOfOtherData = new Cap { PI_ID = otherData.PI_ID }; // Uses MaskedString implicit conversion from string
	var otherData2 = new MyPlainStringClass { PI_ID = maskedVersionOfOtherData.PI_ID }; // Uses string implicit conversion from MaskedString
	
	otherData.Dump();
	maskedVersionOfOtherData.Dump();
	otherData2.Dump();
}

public class Cap {
	public MaskedString PI_ID { get; set; }
	public string CUST_NM { get; set; }
}

public class MyPlainStringClass {
	public string PI_ID { get; set; }
}

// ****************************************
// Main Class Implementation
// ****************************************
public class MaskedString {
	const int defaultNumberOfCharsToShow = 4;
	private string _value;	
	public char MaskChar { get; set; } = '*';
	public int NumberOfDigitsToShow { get; set; } = 4;

	public int MaskLength { 
		get { 
			return _value.Length > NumberOfDigitsToShow 
				? TotalDisplayLength > NumberOfDigitsToShow 
					? TotalDisplayLength - NumberOfDigitsToShow 
					: _value.Length - NumberOfDigitsToShow 
				: 0; 
		} 		
	}
	
	public int TotalDisplayLength { get; set; }

	public string Value
	{
		get { 			
			return _value.Length > NumberOfDigitsToShow 
				? string.Concat(string.Empty.PadLeft(MaskLength, MaskChar), _value.Substring(_value.Length - NumberOfDigitsToShow)) 
				: _value; 
		}
		
		set { _value = value; }
	}

	public MaskedString(string value) {
		Value = value;
	}

	public MaskedString(string value, int numberOfDigitsToShow)
	{
		Value = value;
		NumberOfDigitsToShow = numberOfDigitsToShow;
	}	
	
	// ************************************************************
	// Overrides and equality implementation
	// ************************************************************
	public static implicit operator MaskedString(string obj)
	{
		var result = new MaskedString(obj);
		return result;
	}

	public static implicit operator string(MaskedString obj)
	{
		var result = obj._value;
		return result;
	}

	public static bool operator ==(MaskedString str1, string str2)
	{
		return str1._value == str2;
	}

	public static bool operator !=(MaskedString str1, string str2)
	{
		return str1._value != str2;
	}

	public override bool Equals(Object obj)
	{		
		if (obj == null || this._value.GetType() != obj.GetType()) return false;
		return obj.Equals(this._value);
	}

	public override int GetHashCode()
	{
		return _value.GetHashCode();
	}

	public override string ToString()
	{
		return Value;
	}
	// ************************************************************
}