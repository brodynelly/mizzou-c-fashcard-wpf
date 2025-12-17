using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FlashcardGame.Models;

namespace FlashcardGame
{
    /// <summary>
    /// Interaction logic for FlashcardCreation.xaml
    /// </summary>
    public partial class FlashcardCreation : Window
    {
        // save a list of terms and definitions
        List<TextBox> terms = new List<TextBox>();
        List<TextBox> definitions = new List<TextBox>();
        StudySet _studySet;
        public FlashcardCreation(StudySet flashSet)
        {
            _studySet = flashSet;
            InitializeComponent();
        }
        
        // when go is clicked create text boxes that they can change
        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            // ensure size is a number
            int size = 0;
            try
            {
                size = int.Parse(amount.Text);
            }
            catch
            {
                MessageBox.Show("Invalid input. Only input numbers.");
            }

            // for each size create a text box for terms and one for definitions and add them to the screen
            for (int i = 0; i < size; i++)
            {
                Grid grid = new Grid();
                grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b1b47"));
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.MaxWidth = 800;


                TextBox textBox = new TextBox();
                textBox.Margin = new Thickness(5);
                textBox.Text = "Term " + (i + 1);
                textBox.Height = 60;
                textBox.TextWrapping = TextWrapping.Wrap;
                textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                textBox.FontSize = 20;
                textBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A3C57"));
                textBox.Foreground = Brushes.White;
                textBox.BorderThickness = new Thickness(0);
                Grid.SetColumn(textBox, 0);
                terms.Add(textBox);

                TextBox textBox2 = new TextBox();
                textBox2.Margin = new Thickness(5);
                textBox2.Text = "Definiton " + (i + 1);
                textBox2.Height = 60;
                textBox2.TextWrapping = TextWrapping.Wrap;
                textBox2.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                textBox2.AcceptsReturn = true;
                textBox2.FontSize = 20;
                textBox2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A3C57"));
                textBox2.Foreground = Brushes.White;
                textBox2.BorderThickness = new Thickness(0);
                Grid.SetColumn(textBox2, 1);
                definitions.Add(textBox2);

                grid.Children.Add(textBox);
                grid.Children.Add(textBox2);

                textBoxesStack.Children.Add(grid);
            }
        }

        // on save button click create a new flash card for each term and definition combo and close the dialog
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(terms.Count < 1)
            {
                Close();
                return;
            }
            for(int i=0;i<terms.Count;i++)
            {
                _studySet.AddNewFlashcard(terms[i].Text, definitions[i].Text);
            }
            _studySet.Name = nameOfCardSet.Text;
            DialogResult = true;
            Close();
        }
    }
}
