using System.Text;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmarks;

public class LinkerHintsHelpersBench
{
	// From uno/src/SourceGenerators/Uno.UI.SourceGenerators/BindableTypeProviders/LinkerHintsHelpers.cs
	internal static string GetPropertyAvailableName_Original(string name)
		=> "Is_" + name
			.Replace("`", "_")
			.Replace("<", "_")
			.Replace(">", "_")
			.Replace("+", "_")
			.Replace("[", "_")
			.Replace("]", "_")
			.Replace(".", "_")
			.Replace(",", "_")
		+ "_Available";

	internal static string GetPropertyAvailableName_Char(string name)
		=> "Is_" + name
			.Replace('`', '_')
			.Replace('<', '_')
			.Replace('>', '_')
			.Replace('+', '_')
			.Replace('[', '_')
			.Replace(']', '_')
			.Replace('.', '_')
			.Replace(',', '_')
		+ "_Available";

	internal static string GetPropertyAvailableName_StringBuilder(string name)
	{
		// output length remains unchanged whatever the input data is
		StringBuilder builder = new(name.Length + "Is_".Length + "_Available".Length);
		builder.Append("Is_").Append(name)
			.Replace('`', '_')
			.Replace('<', '_')
			.Replace('>', '_')
			.Replace('+', '_')
			.Replace('[', '_')
			.Replace(']', '_')
			.Replace('.', '_')
			.Replace(',', '_')
			.Append("_Available");
		return builder.ToString();
	}

	internal static string GetPropertyAvailableName_Custom(string name)
	{
		// output length remains unchanged whatever the input data is
		StringBuilder builder = new(name.Length + "Is_".Length + "_Available".Length);
		builder.Append("Is_");
		// single iteration over the chars
		foreach (var c in name) {
			switch (c) {
			case '`':
			case '<':
			case '>':
			case '+':
			case '[':
			case ']':
			case '.':
			case ',':
				builder.Append('_');
				break;
			default:
				builder.Append(c);
				break;
			}
		}
		builder.Append("_Available");
		return builder.ToString();
	}

	[Benchmark]
	public void Original()
	{
		foreach (var line in data)
		{
			GetPropertyAvailableName_Original(line);
		}
	}

	[Benchmark]
	public void Char()
	{
		foreach (var line in data)
		{
			GetPropertyAvailableName_Char(line);
		}
	}

	[Benchmark]
	public void Builder()
	{
		foreach (var line in data)
		{
			GetPropertyAvailableName_StringBuilder(line);
		}
	}

	[Benchmark]
	public void Custom()
	{
		foreach (var line in data)
		{
			GetPropertyAvailableName_Custom(line);
		}
	}

	static string[] data = Array.Empty<string>();

	public static void Main(string[] args)
	{
		data = File.ReadAllLines(args[0]);
		BenchmarkRunner.Run<LinkerHintsHelpersBench>();
	}
}
