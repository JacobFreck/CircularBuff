using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace CircularBuffer.Tests;

public class ArrayCopyTests : TestBase
{
	public ArrayCopyTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void ArrayCopy_Success(int size)
    {
        int[] other = new int[size];
        CircularBuffer<int> buffer = new(size);

        Enumerable.Range(0, size).ToList().ForEach(buffer.Add);

        buffer.CopyTo(other, 0);

        foreach (int index in Enumerable.Range(0, size))
            Assert.Equal(index, other[index]);
    }
}

