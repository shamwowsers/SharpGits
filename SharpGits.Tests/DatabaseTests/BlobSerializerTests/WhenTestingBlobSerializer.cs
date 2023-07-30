using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit.Composable;
using SharpGits.Console.Data;

namespace SharpGits.Tests.DatabaseTests.BlobSerializerTests;

public abstract class WhenTestingBlobSerializer : ComposableTestingTheBehaviourOf
{
    [ItemUnderTest]
    public BlobSerializer Serializer { get; set; }
}
