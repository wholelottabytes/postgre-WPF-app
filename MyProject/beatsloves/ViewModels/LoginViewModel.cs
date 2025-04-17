using beatsloves.Commands;
using beatsloves.Views;
using System.Windows.Input;
using beatsloves.Navigation;
using System;
using System.IO;
using Npgsql;
using System.Windows;
using System.Windows.Media.Imaging;
using beatsloves.Models;
using System.Text.RegularExpressions;
using System.Configuration;

namespace beatsloves.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        private readonly NavigationService _navigationService;

        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            RegisterCommand = new RelayCommand(OnRegister);
            LoginCommand = new RelayCommand(OnLogin);
        }


private void OnRegister(object obj)
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            MessageBox.Show("Username and password cannot be empty.");
            return;
        }

        if (Username.Length < 4 || Password.Length < 8)
        {
            MessageBox.Show("Username and password must be at least 4, 8 characters long.");
            return;
        }

        if (!Regex.IsMatch(Password, @"[A-Za-z]") || !Regex.IsMatch(Password, @"\d"))
        {
            MessageBox.Show("Password must contain at least one letter and one digit.");
            return;
        }

        try
        {
            RegisterUser(Username, Password);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Registration error: " + ex.Message);
        }
    }


    private void RegisterUser(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT register_user(@username, @password)", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Success!");
                }
            }
        }

        private void OnLogin(object obj)
        {
            try
            {
                using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM authenticate_user(@username, @password)", conn))
                    {
                        cmd.Parameters.AddWithValue("@username", Username);
                        cmd.Parameters.AddWithValue("@password", Password);

                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            Session.UserID = Convert.ToInt32(reader["UserID"]);
                            Session.RoleID = Convert.ToInt32(reader["RoleID"]);
                            Session.Balance = Convert.ToDecimal(reader["Balance"]);

                            byte[] userImage = reader["UserImage"] as byte[];
                            if (userImage != null && userImage.Length > 0)
                            {
                                using (var stream = new MemoryStream(userImage))
                                {
                                    Session.imageSource = new BitmapImage();
                                    Session.imageSource.BeginInit();
                                    Session.imageSource.StreamSource = stream;
                                    Session.imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                    Session.imageSource.EndInit();
                                }
                            }
                            else
                            {
                                try
                                {
                                    string placeholderPath = @"..\..\..\Resources\placeholder.jpg";

                                        using (var stream = new FileStream(placeholderPath, FileMode.Open, FileAccess.Read))
                                        {
                                            Session.imageSource = new BitmapImage();
                                            Session.imageSource.BeginInit();
                                            Session.imageSource.StreamSource = stream;
                                            Session.imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                            Session.imageSource.EndInit();
                                        }
                                   
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error: {ex.Message}");
                                }
                            }




                           

                            _navigationService.NavigateTo(new MainPage(_navigationService));
                        }
                        else
                        {
                            MessageBox.Show("Incorrect login or password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in database: " + ex.Message);
            }
        }
    }
}
