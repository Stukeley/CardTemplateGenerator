using CardTemplateGenerator.Templates;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
			var card = GenerateCard();

			card.WindowStyle = WindowStyle.None;

			card.Show();
			card.Focus();

			var bmp = GetImage(card);

			SaveAsPng(bmp);

			card.Close();
		}

		private BaseTemplate GenerateCard()
		{
			var item = ((ComboBoxItem)CardDamageTypeBox.SelectedItem).Content.ToString();

			var damageType = item switch
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

		private RenderTargetBitmap GetImage(BaseTemplate card)
		{
			var size = new Size(card.Width, card.Height);

			var result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);

			var visual = new DrawingVisual();

			using (var context = visual.RenderOpen())
			{
				context.DrawRectangle(new VisualBrush(card), null, new Rect(new Point(), size));
				context.Close();
			}

			result.Render(visual);

			return result;
		}

		private void SaveAsPng(RenderTargetBitmap bmp)
		{
			var encoder = new PngBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(bmp));

			using (var stream = File.Create(Directory.GetCurrentDirectory() + "Card " + DateTime.Now.ToString("ddMM HHmmss") + ".png"))
			{
				encoder.Save(stream);
			}
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
