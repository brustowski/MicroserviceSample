namespace Apollo.Bp.Net.Card.Core.Extensions
{
	public static class StringExtensions
	{
		public static string TrimEndingSlashes(this string input)
		{
			return input.TrimEnd('/', '\\');
		}
	}
}