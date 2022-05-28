using System;
using System.Collections.Generic;
using System.Linq;
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
    public void AddFull_Enumerate(int size)
    {
        CircularBuffer<int> buffer = new(size);

        foreach (int i in Enumerable.Range(0, size))
            buffer.Add(i);

        int count = 0;
        foreach (int item in buffer)
        {
            Assert.Equal(count, item);
            count++;
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(6)]
    public void AddOverflow(int size)
    {
        CircularBuffer<int> buffer = new(size);

        foreach (int i in Enumerable.Range(0, size + 1))
            buffer.Add(i);

        foreach ((int value, int index) in buffer.Select((int i, int index) => (i, index)))
        {
            if (index == 0)
                Assert.Equal(size, value);
            else
                Assert.Equal(index, value);

            Logger.WriteLine($"value={value}, index={index}");
        }

        Assert.Equal(size, buffer.Count);
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

        if (size > 0)
            Enumerable.Range(0, size).ToList().ForEach(buffer.Add);

        Assert.Equal(buffer.Count, size);
    }
}

