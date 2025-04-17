using beatsloves.Models;
using beatsloves.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace beatsloves.Views
{
    
    public partial class BeatDetailPage : Page
    {
        private DispatcherTimer _timer;

        public BeatDetailPage(Beat beat)
            {
                InitializeComponent();
                this.DataContext = new BeatDetailViewModel(beat);
                var viewModel = (BeatDetailViewModel)DataContext;
                viewModel.MediaPlayer = MediaPlayer;
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _timer.Tick += UpdateProgressBar;
        }
        private void MediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (MediaPlayer.NaturalDuration.HasTimeSpan)
            {
                ProgressSlider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                ProgressSlider.SmallChange = 1;
                ProgressSlider.LargeChange = 10;
                _timer.Start();
            }
        }

        // Обновление прогресс-бара
        private void UpdateProgressBar(object sender, EventArgs e)
        {
            if (MediaPlayer.NaturalDuration.HasTimeSpan && MediaPlayer.Position.TotalSeconds <= MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds)
            {
                ProgressSlider.Value = MediaPlayer.Position.TotalSeconds;
                TimeText.Text = $"{MediaPlayer.Position:mm\\:ss} / {MediaPlayer.NaturalDuration.TimeSpan:mm\\:ss}";
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
        // Перемотка при изменении значения слайдера
        private void ProgressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MediaPlayer.NaturalDuration.HasTimeSpan && Math.Abs(MediaPlayer.Position.TotalSeconds - e.NewValue) > 1)
            {
                MediaPlayer.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }
    }
}
