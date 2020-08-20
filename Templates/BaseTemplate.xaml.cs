using CardTemplateGenerator.Filters;
using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace CardTemplateGenerator.Templates
{
	public partial class BaseTemplate : Window
	{
		public BaseTemplate(string title, string description, string cost, string attack, PackIconKind damageType, string imageSource)
		{
			InitializeComponent();

			CardNameText.Text = title;

			CheckKeywords(description);

			CardCostText.Text = cost;

			if (attack.Length < 3)
			{
				CardAttack.Width = 80;
			}
			else
			{
				CardAttack.Width = 100;
			}

			CardAttackText.Text = attack;

			DamageTypeIcon.Kind = damageType;

			if (!string.IsNullOrEmpty(imageSource))
			{
				CardImage.Source = new BitmapImage(new Uri(imageSource));
			}

			// TODO: rescale to fit
		}

		private void CheckKeywords(string description)
		{
			// The idea: check Description word by word. If the word is in Keywords, make it bold. Otherwise, make it normal. (until space?)
			/*TextBlock tb = new TextBlock();
			tb.Inlines.Add("Sample text with ");
			tb.Inlines.Add(new Run("bold") { FontWeight = FontWeights.Bold });
			tb.Inlines.Add(", ");
			tb.Inlines.Add(new Run("italic ") { FontStyle = FontStyles.Italic });
			tb.Inlines.Add("and ");
			tb.Inlines.Add(new Run("underlined") { TextDecorations = TextDecorations.Underline });
			tb.Inlines.Add("words.");
			*/

			CardDescription.Text = "";

			int position = -1;

			while ((position = description.IndexOf(' ')) != -1)
			{
				// abc. <-- position 4, length 4
				string part = description.Substring(0, position);
				description = description.Remove(0, position + 1);

				// Remove dots

				string partModified = part.Replace(".", string.Empty);

				if (Keywords.KeywordArr.Contains(partModified.ToLower()))
				{
					CardDescription.Inlines.Add(new Run(part + " ") { FontWeight = FontWeights.Bold });
				}
				else
				{
					CardDescription.Inlines.Add(part + " ");
				}
			}
		}
	}
}