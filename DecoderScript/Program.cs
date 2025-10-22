// See https://aka.ms/new-console-template for more information

using System.Text;
using DecoderScript;

var file = @"samples\en3.txt";

var fileContent = File.ReadAllText(file, Encoding.UTF8);

var output = DecoderManager.Decode(fileContent);

File.WriteAllText("output-" + Path.GetFileName(file),output);

Console.WriteLine("Done");

Console.ReadLine();

