using System.Text;
using SharpGits.Console.GitObjects;

namespace SharpGits.Console.Data;

public class BlobSerializer
{
    private const string BlobPrefix = "blob ";

    public byte[] Serialize(Blob blob)
    {
        string blobbedString = $"{BlobPrefix}{blob.Content.Length}" + char.MinValue;
        return Encoding.UTF8.GetBytes(blobbedString).Concat(blob.Content).ToArray();
    }

    public Blob Deserialize(byte[] blobBytes)
    {
        ThrowIfNotBlobPrefixed(blobBytes);

        var contentStartIndex = getContentStartIndexAndValidateLength(blobBytes);
        return new Blob() { Content = blobBytes.Skip(contentStartIndex).ToArray() };
    }

    private int getContentStartIndexAndValidateLength(byte[] blobBytes)
    {
        StringBuilder sb = new StringBuilder();
        int i = BlobPrefix.Length;
        byte nullAsciiByte = (byte)char.MinValue;
        for (; i < blobBytes.Length; i++)
        {
            var currentByte = blobBytes[i];
            if (currentByte == nullAsciiByte)
            {
                //we now have length
                break;
            }
            sb.Append(Encoding.ASCII.GetString(new[] { currentByte }));
        }

        var statedLengthAsString = sb.ToString();
        if (string.IsNullOrWhiteSpace(statedLengthAsString))
        {
            throw new ArgumentException("Invalid length content in blob");
        }
        var contentStartIndex = i + 1;
        var blobContentStatedLength = int.Parse(statedLengthAsString);
        var blobContentActualLength = blobBytes.Length - contentStartIndex;

        if (blobContentActualLength != blobContentStatedLength)
        {
            throw new ArgumentException($"Mismatch between specced blob content length ({blobContentStatedLength}) and actual content length ({blobContentActualLength})");
        }
        return contentStartIndex;
    }

    private static void ThrowIfNotBlobPrefixed(byte[] blobBytes)
    {
        var blobbedStringBytes = blobBytes.Take(BlobPrefix.Length);
        var expectedBlobBytes = Encoding.ASCII.GetBytes(BlobPrefix);

        if (false == blobbedStringBytes.SequenceEqual(expectedBlobBytes))
        {
            throw new ArgumentException("Encoding does not match expected blob format");
        }
    }
}