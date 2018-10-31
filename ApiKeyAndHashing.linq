<Query Kind="Statements" />

//var apiKey = "ZDJMRjlLNlIrYUZ4c2gwWS8xT3lMZVExUjY5QjhHMXUwbS8reDUxSEhHND06VGVNeTZtWG9MdThoR3locjZFZklYbmovOGhSV0Z6M1E3VVdFVzFRakpJWT0=";
//var output = Convert.FromBase64String(apiKey);
//output.Dump();
//var encodedApiKey = Convert.ToBase64String(output);
//encodedApiKey.Dump();

var apikey = "d2LF9K6R+aFxsh0Y/1OyLeQ1R69B8G1u0m/+x51HHG4=";
var clientSecretBase64 = "onczpC/hwiS0SqjwG7Fty8sXECiiQRW7RAvdJB7EwO8=";
var clientSecretHash = Convert.FromBase64String(clientSecretBase64);
var finalEncodedApiKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(apikey + ":" + clientSecretHash));
finalEncodedApiKey.Dump();