using CardTemplateGenerator.Templates;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System.Windows;

namespace CardTemplateGenerator
{
	public partial class MainWindow : Window
	{
		private string _imageSource = "";

		public MainWindow()
		{
			InitializeComponent();
		}

		private void PreviewButton_Click(object sender, RoutedEventArgs e)
		{
			var card = GenerateCard();

			card.Show();
		}

		private void GenerateButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private BaseTemplate GenerateCard()
		{
			var damageType = CardDamageTypeBox.SelectedItem switch
			{
				"Physical" => PackIconKind.Sword,
				"Fire" => PackIconKind.Fire,
				"Ice" => PackIconKind.Water,
				"Lightning" => PackIconKind.Flash,
				"Poison" => PackIconKind.SkullCrossbones,
				_ => PackIconKind.Sword,
			};

			var card = new BaseTemplate(title: CardNameBox.Text, description: CardDescriptionBox.Text, cost: CardCostBox.Text,
				attack: CardAttackBox.Text, damageType: damageType, imageSource: _imageSource);

			return card;
		}

		private void SelectImageButton_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new OpenFileDialog()
			{
				DefaultExt = ".png",
				Filter = "PNG images|*.png|All files|*.*"
			};

			var result = dialog.ShowDialog();

			if (result.Value)
			{
				_imageSource = dialog.FileName;
			}
		}
	}
}
