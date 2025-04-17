using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using beatsloves.Commands;
using beatsloves.Models;
using Npgsql;
using NpgsqlTypes;

namespace beatsloves.ViewModels
{
    public class SoldBeatsPageViewModel : BaseViewModel
    {
        private ObservableCollection<Beat> _soldBeats;
        public ObservableCollection<Beat> SoldBeats
        {
            get => _soldBeats;
            set
            {
                _soldBeats = value;
                OnPropertyChanged(nameof(SoldBeats));
            }
        }

        private BitmapImage _imageSource;  
        public BitmapImage ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource)); 
            }
        }

        public ICommand BackCommand { get; }
        public ICommand LoadUserBeatsSoldCommand { get; }
        public ICommand UpdateUserImageCommand { get; }

        public SoldBeatsPageViewModel()
        {
            // Инициализация команд
            LoadUserBeatsSoldCommand = new RelayCommand(LoadSoldBeats);
            UpdateUserImageCommand = new RelayCommand(UpdateUserImage);

            SoldBeats = new ObservableCollection<Beat>();

            if (Session.imageSource == null)
            {
                Session.imageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/placeholder.png"));
            }
            else
            {
                ImageSource = Session.imageSource;
            }
        }

        private void LoadSoldBeats(object obj)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    int userId = Session.UserID;

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM GetUserSales(@userId);", connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SoldBeats.Add(new Beat
                                {
                                    Id = (int)reader["beat_id"],
                                    Title = reader["title"].ToString(),
                                    PurchaseDate = reader["purchase_date"] != DBNull.Value ? (DateTime?)reader["purchase_date"] : null,
                                    Price = (decimal)reader["purchase_amount"]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateUserImage(object obj)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);

                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        int userId = Session.UserID; 

                       
                        using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT insert_user_image(@userId, @imageData);", connection))
                        {
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.Parameters.AddWithValue("@imageData", imageBytes);

                            cmd.ExecuteNonQuery(); 
                        }
                    }

                    ImageSource = LoadImageFromFile(filePath);
                    Session.imageSource = ImageSource; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении изображения: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    

        private BitmapImage LoadImageFromFile(string filePath)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(filePath);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            return image;
        }
    }
}
