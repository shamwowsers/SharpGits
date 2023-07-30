namespace SharpGits.Tests.Extensions;

public static class StringExtensions
{
    public static byte[] AsBytes(this string theString)
    {
        // This assumes UTF8 serialization which may not always be the case for a real git repo but should be ok for our test cases
        return System.Text.Encoding.UTF8.GetBytes(theString);
    }
}