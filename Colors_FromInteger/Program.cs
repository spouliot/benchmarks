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
	public static IEnumerable<int> GetAllInt32Values()
	{
		for (int i = -100; i <= 100; i++)
		// for (int i = int.MinValue; i <= int.MaxValue; i++)
		{
			yield return i;
		}
	}

	// [ArgumentsSource(nameof(GetAllInt32Values))]
	[Benchmark]
	public void FromInteger()
	{
		for (int i = int.MinValue; i <= int.MaxValue; i++)
		{
			Colors.FromInteger(i);
		}
	}
}
