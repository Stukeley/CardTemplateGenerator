using System.IO;

namespace CardTemplateGenerator.Filters
{
	public static class Keywords
	{
		public static string[] KeywordArr { get; set; }

		static Keywords()
		{
			var dir = Directory.GetCurrentDirectory() + @"\Filters\Keywords.txt";
			if (File.Exists(dir))
			{
				KeywordArr = File.ReadAllLines(dir);
			}
			else
			{
				KeywordArr = new string[0];
			}

			for (int i = 0; i < KeywordArr.Length; i++)
			{
				KeywordArr[i] = KeywordArr[i].ToLower();
			}
		}
	}
}
