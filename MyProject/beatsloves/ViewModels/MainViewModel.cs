using beatsloves.Commands;
using beatsloves.Models;
using beatsloves.Views;
using Npgsql;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using beatsloves.Navigation;
using System.Windows.Media.Imaging;
using System.Data;
using NpgsqlTypes;

namespace beatsloves.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }
        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        private ObservableCollection<Beat> _beats;
        public ObservableCollection<Beat> Beats
        {
            get => _beats;
            set
            {
                _beats = value;
                OnPropertyChanged(nameof(Beats));
            }
        }
        private string _currentTab = "Main";
        public string CurrentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                OnPropertyChanged(nameof(CurrentTab));
                CurrentPage = 1; 
                LoadCurrentPage();
            }
        }
        public int UserID => Session.UserID;
        public string UserBalance => $"${Session.Balance:F2}";
        private int purchasesPage = 1;
        public BitmapImage UserImage => Session.imageSource;
        public ICommand OpenSoldBeatsPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand OpenAddBeatPageCommand { get; }
        public ICommand DeleteBeatCommand { get; }
        public ICommand LoadPurchasesCommand { get; }
        public ICommand LoadBeatsCommand { get; }
        public ICommand OpenBeatDetailCommand { get; }
        public ICommand LoadMyBeatsCommand { get; }
        public ICommand LoadMainBeatsCommand { get; }
        public ICommand LoadMySearchCommand { get; }
        public ICommand OpenEditBeatPageCommand { get; }

        public ICommand IncreaseBalanceCommand { get; }
        private readonly NavigationService _navigationService;
        private int allPage = 1;

        public MainViewModel(NavigationService navigationService)
        {
            NextPageCommand = new RelayCommand(NextPage);
            Session.OnBalanceChanged += () => OnPropertyChanged(nameof(UserBalance));
            Beats = new ObservableCollection<Beat>();
            LoadBeatsCommand = new RelayCommand(LoadBeats);
            _navigationService = navigationService;
            OpenBeatDetailCommand = new RelayCommand(OpenBeatDetail);
            OpenSoldBeatsPageCommand = new RelayCommand(_ => _navigationService.NavigateTo(new SoldBeatsPage()));
            OpenAddBeatPageCommand = new RelayCommand(_ => _navigationService.NavigateTo(new AddBeatPage()));
            DeleteBeatCommand = new RelayCommand(DeleteBeat);
            OpenEditBeatPageCommand = new RelayCommand(OpenEditBeatPage);
            LoadMyBeatsCommand = new RelayCommand(LoadMyBeatsCom);
            LoadMainBeatsCommand = new RelayCommand(LoadMainBeatsCom);
            LoadMySearchCommand = new RelayCommand(LoadMySearchCom);
            IncreaseBalanceCommand = new RelayCommand(IncreaseBalance);
            LoadPurchasesCommand = new RelayCommand(LoadMyPurchasesCom);
        }

        private void LoadPurchases(object obj)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM GetMyBeatsPurchases(@userId, @limitPage, @offsetPage)", connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", Session.UserID);
                        cmd.Parameters.AddWithValue("@limitPage", 5);
                        cmd.Parameters.AddWithValue("@offsetPage", CurrentPage);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var beat = new Beat
                                {
                                    Id = (int)reader["beat_id"],
                                    Title = reader["title"].ToString(),
                                    Description = reader["description"].ToString(),
                                    Price = (decimal)reader["price"],
                                    Typestring = reader["type_name"].ToString(),
                                    Beatway = reader["beat_way"].ToString(),
                                    Beatimg = reader["beat_img"].ToString(),
                                    Tags = reader["tags"].ToString(),
                                    UserId = (int)reader["user_id"]
                                };

                                Beats.Add(beat);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {

                MessageBox.Show(ex.Message);
                if (CurrentPage > 1) { CurrentPage--; }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
                if (CurrentPage > 1) { CurrentPage--; }
            }
        }

        private void IncreaseBalance(object obj)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the amount to top up your balance:",
                "Top up your balance",                    
                "0");

          

            if (!decimal.TryParse(input, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Incorrect amount");
                return;
            }

            int userId = Session.UserID;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("IncreaseBalance", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("user_id", userId);
                        cmd.Parameters.AddWithValue("amount", amount);

                        cmd.Parameters.Add(new NpgsqlParameter("success", NpgsqlDbType.Boolean) { Direction = ParameterDirection.Output });
                        cmd.Parameters.Add(new NpgsqlParameter("message", NpgsqlDbType.Text) { Direction = ParameterDirection.Output });

                        cmd.ExecuteNonQuery();

                        bool success = (bool)cmd.Parameters["success"].Value;
                        string message = (string)cmd.Parameters["message"].Value;

                        if (success)
                        {
                            MessageBox.Show("Success.");
                            Session.Balance = Session.Balance + amount;
                           
                        }
                        else
                        {
                            MessageBox.Show("Error: " + message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void NextPage(object obj)
        {
            CurrentPage++;
            LoadCurrentPage();
        }
        private void LoadMainBeatsCom(object obj)
        {
            Beats.Clear();
            CurrentTab = "Main";
        }

        private void LoadMyBeatsCom(object obj)
        {
            Beats.Clear();
            CurrentTab = "MyBeats";
        }
        private void LoadMyPurchasesCom(object obj)
        {
            Beats.Clear();
            CurrentTab = "Purchases";
        }

        private void LoadMySearchCom(object obj)
        {
            Beats.Clear();
            CurrentTab = "Search";
        }
        private void LoadCurrentPage()
        {
            switch (CurrentTab)
            {
                case "Main":
                    LoadBeats(null);
                    break;
                case "MyBeats":
                    LoadMyBeats(null);
                    break;
                case "Purchases":
                    LoadPurchases(null);
                    break;
                case "Search":
                    SearchBeats(null);
                    break;
            }
        }
        private void LoadMyBeats(object obj)
        {
            try
            {
                
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM GetMyBeats(@userId, @limitPage, @offsetPage)", connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", Session.UserID);
                        cmd.Parameters.AddWithValue("@limitPage", 5);
                        cmd.Parameters.AddWithValue("@offsetPage", CurrentPage);  

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var beat = new Beat
                                {
                                    Id = (int)reader["beat_id"],
                                    Title = reader["title"].ToString(),
                                    Description = reader["description"].ToString(),
                                    Price = (decimal)reader["price"],
                                    Typestring = reader["type_name"].ToString(),
                                    Beatway = reader["beat_way"].ToString(),
                                    Beatimg = reader["beat_img"].ToString(),
                                    Tags = reader["tags"].ToString(),
                                    UserId = (int)reader["user_id"]
                                };

                                Beats.Add(beat);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                if (CurrentPage > 1) { CurrentPage--; }
            }
            catch (Exception ex)
            {
                if (CurrentPage > 1) { CurrentPage--; }

                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteBeat(object selectedBeat)
        {
            if (selectedBeat is Beat beat && (beat.UserId == Session.UserID || Session.RoleID==1)) 
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                try
                {
                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        using (var cmd = new NpgsqlCommand("DeleteBeat", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("beat_id", beat.Id);
                            cmd.Parameters.AddWithValue("user_id", Session.UserID);

                            var status = cmd.Parameters.Add("status", NpgsqlTypes.NpgsqlDbType.Boolean);
                            status.Direction = ParameterDirection.Output;

                            var message = cmd.Parameters.Add("message", NpgsqlTypes.NpgsqlDbType.Text);
                            message.Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            bool operationStatus = (bool)status.Value;
                            string operationMessage = message.Value.ToString();

                            if (operationStatus)
                            {
                                Beats.Remove(beat);
                                MessageBox.Show("Beat is deleted");
                            }
                            else
                            {
                                MessageBox.Show(operationMessage);
                            }
                        } 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении бита: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Вы не имеете права удалять этот бит.");
            }
        }
        private void OpenEditBeatPage(object selectedBeat)
        {
            if (selectedBeat is Beat beat && beat.UserId == Session.UserID)
            {
                var addBeatPage = new AddBeatPage(new AddBeatPageViewModel(beat));
                _navigationService.NavigateTo(addBeatPage);
            }
            else
            {
                MessageBox.Show("Вы не можете редактировать этот бит.");
            }
        }
        private void MainBeats(object obj)
        {
            try
            {
                Beats.Clear();
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM GetBeatsWithTags(@limitPage, @offsetPage)", connection))
                    {
                        cmd.Parameters.AddWithValue("@limitPage", 5);
                        cmd.Parameters.AddWithValue("@offsetPage", CurrentPage);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Beat beat = new Beat
                                {
                                    Id = (int)reader["beat_id"],
                                    Title = reader["title"].ToString(),
                                    Price = (decimal)reader["price"],
                                    Tags = reader["tags"].ToString(),
                                    Beatimg = reader["beat_img"].ToString(),
                                    Typestring = reader["type_name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    Beatway = reader["beat_way"].ToString(),
                                    UserId = (int)reader["user_id"]
                                };

                                Beats.Add(beat);
                            }
                        }
                    }
                }

            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show( ex.Message);
                if (CurrentPage > 1) { CurrentPage--; }

            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
                if (CurrentPage > 1) { CurrentPage--; }

            }

        }

        private void SearchBeats(object obj)
        {
            
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM SearchBeats(@searchQuery, @limitPage, @offsetPage)", connection))
                    {
                        cmd.Parameters.AddWithValue("@searchQuery", string.IsNullOrEmpty(SearchQuery) ? (object)DBNull.Value : SearchQuery);
                        cmd.Parameters.AddWithValue("@limitPage", 5);
                        cmd.Parameters.AddWithValue("@offsetPage", CurrentPage);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var beat = new Beat
                                {
                                    Id = (int)reader["beat_id"],
                                    Title = reader["title"].ToString(),
                                    Price = (decimal)reader["price"],
                                    Tags = reader["tags"].ToString(),
                                    Beatimg = reader["beat_img"].ToString(),
                                    Typestring = reader["type_name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    Beatway = reader["beat_way"].ToString(),
                                    UserId = (int)reader["user_id"]
                                };

                                Beats.Add(beat);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                if (CurrentPage > 1) { CurrentPage--; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (CurrentPage > 1) { CurrentPage--; }
            }
        }

        private void LoadBeats(object obj)
        {
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM GetBeatsWithTags(@limitPage, @offsetPage)", connection))
                    {
                        cmd.Parameters.AddWithValue("@limitPage", 5);
                        cmd.Parameters.AddWithValue("@offsetPage", CurrentPage);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Beat beat = new Beat
                                {
                                    Id = (int)reader["beat_id"],
                                    Title = reader["title"].ToString(),
                                    Price = (decimal)reader["price"],
                                    Tags = reader["tags"].ToString(),
                                    Beatimg = reader["beat_img"].ToString(),
                                    Typestring = reader["type_name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    Beatway = reader["beat_way"].ToString(),
                                    UserId = (int)reader["user_id"]
                                };

                                Beats.Add(beat);
                            }
                        }
                    }
                }
             
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                if (CurrentPage > 1) { CurrentPage--; }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (CurrentPage>1) { CurrentPage--; }
            }
        }

        private void OpenBeatDetail(object selectedBeat)
        {
            var beat = selectedBeat as Beat;
            if (beat != null)
            {
                var beatDetailPage = new BeatDetailPage(beat);
                _navigationService.NavigateTo(beatDetailPage);
            }
        }
    }
}
