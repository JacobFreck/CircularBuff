using System;
using System.Collections.Generic;
using System.Linq;
using CircularBuff.Tests;
using Xunit;
using Xunit.Abstractions;

namespace CircularBuffer.Tests;

public class AddTests : TestBase
{
    public AddTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(6)]
    public void AddFull_Enumerate(int size)
    {
        CircularBuffer<int> buffer = new(size);

        foreach (int i in 0.To(size - 1))
            buffer.Add(i);

        int count = 0;
        foreach (int item in buffer)
        {
            Assert.Equal(count, item);
            count++;
        }
    }

    [Fact]
    public void ZeroAdd_ReturnsExpectedValue()
    {
        CircularBuffer<int> buffer = new(1);
        Assert.Equal(0, buffer.Count);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    public void Count_ReturnsExpectedValue(int size)
    {
        CircularBuffer<int> buffer = new(size);

        foreach (int i in 0.To(size - 1))
            buffer.Add(i);

        Assert.Equal(buffer.Count, size);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(3, 0)]
    [InlineData(5, 0)]
    [InlineData(6, 0)]
    [InlineData(1, 1)]
    [InlineData(3, 1)]
    [InlineData(5, 1)]
    [InlineData(6, 1)]
    [InlineData(6, 6)]
    [InlineData(6, 12)]
    [InlineData(6, 15)]
    public void AddOverflow_CorrectSequence(int size, int additional)
    {
        CircularBuffer<int> buffer = new(size);

        foreach (int i in 0.To(size + additional))
            buffer.Add(i);

        Assert.Equal((1 + additional).To(size + additional), buffer.Select(x => x));
    }
}

