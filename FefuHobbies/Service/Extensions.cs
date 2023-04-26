using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FefuHobbies.Service
{
	public static class Extensions
	{
		public static string CutController(this string str)
		{
			return str.Replace("Controller", "");
		}
	}
}
