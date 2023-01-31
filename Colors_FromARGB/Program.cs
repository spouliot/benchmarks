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
		// list.Add(null); fails with NRE on uno/master
		list.Add("");
		list.Add(" ");
		for (int h = 0; h < 16; h++)
		{
			char a = hex[h];
			for (int i = 0; i < 16; i++)
			{
				char r = hex[i];
				for (int j = 0; j < 16; j++)
				{
					char g = hex[j];
					for (int k = 0; k < 16; k++)
					{
						char b = hex[k];
						list.Add($"{r}{g}{b}");		// RGB
						list.Add($"#{r}{g}{b}");	// #RGB
						list.Add($"{a}{r}{g}{b}");	// ARGB
						list.Add($"#{a}{r}{g}{b}");	// #ARGB

						for (int l = 0; l < 16; l+=4)
						{
							char a2 = hex[l];
							for (int m = 0; m < 16; m+=4)
							{
								char r2 = hex[m];
								for (int n = 0; n < 16; n+=4)
								{
									char g2 = hex[n];
									for (int o = 0; o < 16; o+=4)
									{
										char b2 = hex[o];
										list.Add($"{r}{r2}{g}{g2}{b}{b2}");			// RRGGBB
										list.Add($"#{r}{r2}{g}{g2}{b}{b2}");		// #RRGGBB
										list.Add($"{a}{a2}{r}{r2}{g}{g2}{b}{b2}");	// AARRGGBB
										list.Add($"#{a}{a2}{r}{r2}{g}{g2}{b}{b2}");	// #AARRGGBB
									}
								}
							}
						}
					}
				}
			}
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
