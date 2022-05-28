using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace CircularBuffer.Tests;

public class RemoveTests : TestBase
{
    public RemoveTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void InvalidSizeBuffer_ThrowsArgumentException(int size)
    {
        Assert.Throws<ArgumentException>(() => new CircularBuffer<int>(size));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void AddItem_Success(int size)
    {
        CircularBuffer<int> buffer = new(size);
        buffer.Add(0);
        Assert.Single(buffer);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void RemoveItem_Success(int size)
    {
        CircularBuffer<int> buffer = new(size);

        buffer.Add(0);
        buffer.Remove();

        foreach (int item in buffer)
            Console.WriteLine($"item {item}");

        Assert.Empty(buffer);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void RemoveZeroSize_Success(int size)
    {
        CircularBuffer<int> buffer = new(size);

        Assert.False(buffer.Remove());
        Assert.Empty(buffer);
    }
}
