using System;
using Xunit.Abstractions;

namespace CircularBuffer.Tests;

public class TestBase
{
	public ITestOutputHelper Logger { get; init; }

	public TestBase(ITestOutputHelper testOutputHelper)
	{
		Logger = testOutputHelper;
	}
}

