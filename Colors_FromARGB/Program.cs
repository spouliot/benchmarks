using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Windows.UI;

namespace Benchmark;

class Program {
	static void Main ()
	{
		BenchmarkRunner.Run<Colors_FromARGB>();
	}
}

public class Colors_FromARGB
{
	const string hex = "01234567890abcdef";

	static readonly List<string?> list = new();

	static Colors_FromARGB()
	{
		list.Add(null);
		list.Add("");
		list.Add(" ");
		for (int i = 0; i < 16; i++) {
			char c = hex[i];
			list.Add($"{c}{c}{c}");					// RGB
			list.Add($"#{c}{c}{c}");				// #RGB
			list.Add($"{c}{c}{c}{c}");				// ARGB
			list.Add($"#{c}{c}{c}{c}");				// #ARGB
			list.Add($"{c}{c}{c}{c}{c}{c}");		// RRGGBB
			list.Add($"#{c}{c}{c}{c}{c}{c}");		// #RRGGBB
			list.Add($"{c}{c}{c}{c}{c}{c}{c}{c}");	// AARRGGBB
			list.Add($"#{c}{c}{c}{c}{c}{c}{c}{c}");	// #AARRGGBB	
		}
	}

	[Benchmark]
	public void FromARGB()
	{
		foreach (var s in list)
		{
			Colors.FromARGB(s!);
		}
	}
}
