using System.Collections.Generic;
using System.Collections.ObjectModel;
using beatsloves.Commands;
using beatsloves.Models;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Npgsql;

namespace beatsloves.ViewModels
{
    public class AddBeatPageViewModel : BaseViewModel
    {
        private string _beatimg;
        private string _beatway;
        private bool _isEditMode;
        private int _beatId;

        public ObservableCollection<TypeModel> LicenseTypes { get; set; } = new ObservableCollection<TypeModel>();

        private TypeModel _selectedLicenseType;
        public TypeModel SelectedLicenseType
        {
            get => _selectedLicenseType;
            set
            {
                _selectedLicenseType = value;
                OnPropertyChanged(nameof(SelectedLicenseType));
            }
        }
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                _isEditMode = value;
                OnPropertyChanged(nameof(IsEditMode));
                OnPropertyChanged(nameof(PageTitle));
                OnPropertyChanged(nameof(ButtonText));
            }
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string Beatimg
        {
            get => _beatimg;
            set
            {
                _beatimg = value;
                OnPropertyChanged(nameof(Beatimg));
            }
        }

        public string Beatway
        {
            get => _beatway;
            set
            {
                _beatway = value;
                OnPropertyChanged(nameof(Beatway));
            }
        }

        private string _tags;
        public string Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }
        public ICommand AddOrEditBeatCommand { get; }

        public ObservableCollection<string> SuggestedTags { get; set; } = new ObservableCollection<string>();
        private List<string> _allTags = new List<string>();

        private string _currentTagInput;
        public string CurrentTagInput
        {
            get => _currentTagInput;
            set
            {
                _currentTagInput = value;
                OnPropertyChanged(nameof(CurrentTagInput));
                UpdateTagSuggestions();
            }
        }

        // Конструктор для добавления нового бита
        public AddBeatPageViewModel()
        {
            LoadLicenseTypes();
            LoadTags();
            AddOrEditBeatCommand = new RelayCommand(AddOrEditBeat);
        }

        // Конструктор для редактирования
        public AddBeatPageViewModel(Beat beat)
        {
            _isEditMode = true;
            _beatId = beat.Id;
            Title = beat.Title;
            Description = beat.Description;
            Price = beat.Price;
            Beatimg = beat.Beatimg;
            Beatway = beat.Beatway;
            Tags = beat.Tags;

            LoadLicenseTypes();
            LoadTags();

            SelectedLicenseType = LicenseTypes.FirstOrDefault(t => t.Name == beat.Typestring);

            AddOrEditBeatCommand = new RelayCommand(AddOrEditBeat);
        }
        public string PageTitle => _isEditMode ? "Edit Beat" : "Add a New Beat";

        public string ButtonText => _isEditMode ? "Update Beat" : "Add Beat";
        private void LoadLicenseTypes()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("SELECT id, typename FROM types;", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LicenseTypes.Add(new TypeModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
        }

        private void LoadTags()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("SELECT tagname FROM tags;", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _allTags.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }

        private void UpdateTagSuggestions()
        {
            SuggestedTags.Clear();
            if (string.IsNullOrWhiteSpace(CurrentTagInput))
                return;

            var filteredTags = _allTags.Where(tag => tag.StartsWith(CurrentTagInput, System.StringComparison.OrdinalIgnoreCase));
            foreach (var tag in filteredTags)
            {
                SuggestedTags.Add(tag);
            }
        }

        private void AddOrEditBeat(object obj)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                MessageBox.Show("Title field cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                MessageBox.Show("Description field cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Beatway))
            {
                MessageBox.Show("Beatway field cannot be empty.");
                return;
            }

            if (SelectedLicenseType == null || SelectedLicenseType.Id <= 0)
            {
                MessageBox.Show("Please select a valid license type.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Beatimg))
            {
                MessageBox.Show("Beat image path cannot be empty.");
                return;
            }
            if (Price <= 0)
            {
                MessageBox.Show("Price cannot be empty or zero.");
                return;
            }




            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string procedureName = _isEditMode ? "UpdateBeat" : "AddBeat";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var cmd = new NpgsqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (_isEditMode)
                        {
                            cmd.Parameters.AddWithValue("beat_id", _beatId);
                        }

                        cmd.Parameters.AddWithValue("title", Title);
                        cmd.Parameters.AddWithValue("description", Description);
                        cmd.Parameters.AddWithValue("price", Price); 
                        cmd.Parameters.AddWithValue("typeid", SelectedLicenseType.Id);
                        cmd.Parameters.AddWithValue("beatway", Beatway);
                        cmd.Parameters.AddWithValue("beatimg", Beatimg);
                        cmd.Parameters.AddWithValue("tags", Tags);
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
                            MessageBox.Show(_isEditMode ? "Beat successfully updated." : "Beat successfully added.");
                        }
                        else
                        {
                            MessageBox.Show("Error: " + operationMessage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while adding/updating the beat: " + ex.Message);
            }
        }
    }
    }
