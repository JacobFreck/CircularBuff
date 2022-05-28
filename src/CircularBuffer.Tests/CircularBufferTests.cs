using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace CircularBuffer.Tests;

public class CircularBufferTests
{
    ITestOutputHelper Logger { get; init; }

    public CircularBufferTests(ITestOutputHelper testOutputHelper)
    {
        Logger = testOutputHelper;
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

        foreach (int i in Enumerable.Range(0, size+1))
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
}
