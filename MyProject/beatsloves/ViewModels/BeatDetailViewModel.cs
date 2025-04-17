using beatsloves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using beatsloves.Commands;
using Npgsql;
using System.Windows.Controls;

namespace beatsloves.ViewModels
{
 

    public class BeatDetailViewModel : BaseViewModel
    {
        public MediaElement MediaPlayer { get; set; }

        public ICommand PlayCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand StopCommand { get; }

        public int UserID => Session.UserID;
        public Beat SelectedBeat { get; set; }

        private int _userRating;
        public int UserRating
        {
            get => _userRating;
            set
            {
                _userRating = value;
                OnPropertyChanged();
            }
        }

        private double _averageRating;
        public double AverageRating
        {
            get => _averageRating;
            set
            {
                _averageRating = value;
                OnPropertyChanged();
            }
        }
        public ICommand PurchaseCommand { get; }
        public ICommand RateBeatCommand { get; }
        public ICommand LoadAverageRatingCommand { get; }

        public BeatDetailViewModel(Beat selectedBeat)
        {
            SelectedBeat = selectedBeat;

            RateBeatCommand = new RelayCommand(ExecuteRateBeat);
            LoadAverageRatingCommand = new RelayCommand(ExecuteLoadAverageRating);
            PurchaseCommand = new RelayCommand(ExecutePurchase, CanExecutePurchase);
            PlayCommand = new RelayCommand(ExecutePlay);
            PauseCommand = new RelayCommand(ExecutePause);
            StopCommand = new RelayCommand(ExecuteStop);

            ExecuteLoadAverageRating();
        }
        private void ExecutePlay(object obj)
        {
            if (MediaPlayer.Source != null)
            {
                MediaPlayer.Play();
            }
            else
            {
                MessageBox.Show("Audio source is not set.");
            }
        }

        private void ExecutePause(object obj)
        {
            MediaPlayer?.Pause();
        }

        private void ExecuteStop(object obj)
        {
            MediaPlayer?.Stop();
        }
        private bool CanExecutePurchase(object parameter)
        {
            return SelectedBeat != null && Session.Balance >= SelectedBeat.Price;
        }
        private void ExecutePurchase(object parameter)
        {
            if (SelectedBeat == null)
            {
                MessageBox.Show("No beat selected.");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var cmd = new NpgsqlCommand("SELECT make_purchase(@beatId, @buyerId)", conn);
                        cmd.Parameters.AddWithValue("beatId", SelectedBeat.Id);
                        cmd.Parameters.AddWithValue("buyerId", Session.UserID);
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();

                        // Обновляем баланс пользователя
                        Session.Balance -= SelectedBeat.Price;

                        tran.Commit();
                        MessageBox.Show("Purchase successful!");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
            }
        }
        private void ExecuteRateBeat(object obj)
        {
            try
            {
                
                using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT SetBeatRating(@p_BeatID, @p_UserID, @p_RatingValue)", conn))
                    {
                        cmd.Parameters.AddWithValue("@p_BeatID", SelectedBeat.Id);
                        cmd.Parameters.AddWithValue("@p_UserID", Session.UserID);
                        cmd.Parameters.AddWithValue("@p_RatingValue", UserRating); 
                        cmd.ExecuteNonQuery();
                       
                    }
                }

      
                ExecuteLoadAverageRating();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оценке: {ex.Message}");
            }
        }


        private void ExecuteLoadAverageRating(object obj)
        {
            try
            {
                using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT CalculateAverageRatingForBeat(@beatId)", conn))
                    {
                        cmd.Parameters.AddWithValue("@beatId", SelectedBeat.Id);
                        AverageRating = Convert.ToDouble(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }
        private void ExecuteLoadAverageRating()
        {
            try
            {
                using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT CalculateAverageRatingForBeat(@beatId)", conn))
                    {
                        cmd.Parameters.AddWithValue("@beatId", SelectedBeat.Id);
                        AverageRating = Convert.ToDouble(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}