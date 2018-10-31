<Query Kind="Statements">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

var secret = "9klJ8/T3MjTRoEmmGxTOdGMahiRdVH0i8j/rte8Rwj0=";
var bytes = Encoding.UTF8.GetBytes(secret);
var hashAlgorithm = SHA256.Create();
var hash = hashAlgorithm.ComputeHash(bytes);
var base64string = Convert.ToBase64String(hash);
base64string.Dump();