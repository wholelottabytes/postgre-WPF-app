using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using beatsloves.ViewModels;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace beatsloves.Views
{
    public partial class AddBeatPage : Page
    {
        public AddBeatPage()
        {
            InitializeComponent();
            DataContext = new AddBeatPageViewModel();
        }
        public AddBeatPage(AddBeatPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void SelectBeatFile_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddBeatPageViewModel;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Audio Files (*.mp3;*.wav)|*.mp3;*.wav|All Files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                viewModel.Beatway = openFileDialog.FileName;
            }
        }

        private void SelectImageFile_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AddBeatPageViewModel;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All Files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                viewModel.Beatimg = openFileDialog.FileName;
            }
        }



        private void PriceTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string text = ((TextBox)sender).Text + e.Text;
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9.]$") || (e.Text == "." && text.Contains(".")))
            {
                e.Handled = true; 
            }
        }

        private void PriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0";
                textBox.CaretIndex = textBox.Text.Length;
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var navService = System.Windows.Navigation.NavigationService.GetNavigationService(this);
            if (navService != null && navService.CanGoBack)
            {
                navService.GoBack();
            }
        }

            private void TagSuggestionSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string selectedTag = e.AddedItems[0] as string;

                if (DataContext is AddBeatPageViewModel viewModel && selectedTag != null)
                {
                    if (string.IsNullOrWhiteSpace(viewModel.Tags))
                    {
                        viewModel.Tags = selectedTag;  
                    }
                    else
                    {
                        viewModel.Tags += " " + selectedTag;  
                    }

                    // Очищаем поле ввода, но не очищаем список тегов
                    viewModel.CurrentTagInput = string.Empty;
                    viewModel.SuggestedTags.Clear();  // Очищаем список предложенных тегов
                }
            }
        }

    }
}
