using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Windows.UI;

namespace Benchmark;

class Program {
	static void Main ()
	{
		BenchmarkRunner.Run<Colors_FromInteger>();
	}
}

public class Colors_FromInteger
{
	[Benchmark]
	public void FromInteger()
	{
		for (int i = 0; i < 1024 * 1024; i++)
		{
			Colors.FromInteger(i);
		}
	}
}
