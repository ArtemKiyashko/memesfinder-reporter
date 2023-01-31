namespace MemesFinderReporter.Managers.Reports.Extensions
{
	public static class MathExtensions
	{
		public static int RoundUpTo(this int number, int rountTo) => (int)(Math.Ceiling((decimal)number / rountTo) * rountTo);
    }
}

