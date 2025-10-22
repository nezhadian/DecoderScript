using System.Text;
using DecoderScript;

namespace Tests;

public class DecodingTests
{
    [Fact]
    public void TestExampleFile1()
    {
        var encodedFilePath = @"samples\en1.txt";
        var decodedFilePath = @"samples\de1.txt";
        
        var encodedContent = File.ReadAllText(encodedFilePath, Encoding.UTF8);
        var expectedDecodeFile = File.ReadAllText(decodedFilePath, Encoding.UTF8);
        
        var decoded = DecoderManager.DecodeString(encodedContent);

        Assert.Equal(expectedDecodeFile, decoded);
    }
    
    [Fact]
    public void TestExampleFile2()
    {
        var encodedFilePath = @"samples\en2.txt";
        var decodedFilePath = @"samples\de2.txt";
        
        var encodedContent = File.ReadAllText(encodedFilePath, Encoding.UTF8);
        var expectedDecodeFile = File.ReadAllText(decodedFilePath, Encoding.UTF8);
        
        var decoded = DecoderManager.DecodeString(encodedContent);

        Assert.Equal(expectedDecodeFile, decoded);
    }
    
    [Fact]
    public void TestExampleFile3_JustOutput()
    {
        var encodedFilePath = @"samples\en3.txt";
        
        var encodedContent = File.ReadAllText(encodedFilePath, Encoding.UTF8);
        
        var decoded = DecoderManager.DecodeString(encodedContent);
        
        File.WriteAllText("output-" + Path.GetFileName(encodedFilePath),decoded);

    }
}