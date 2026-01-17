using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace TrainMate;

public class ProfilePageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private readonly FirebaseClient _firebase;

    public ProfilePageViewModel()
    {
        _firebase = new FirebaseClient("https://mobilne-45354-default-rtdb.europe-west1.firebasedatabase.app/");
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set { _isBusy = value; OnPropertyChanged(); }
    }

    private string? _name;
    public string? Name { get => _name; set { _name = value; OnPropertyChanged(); } }

    private string? _status;
    public string? Status { get => _status; set { _status = value; OnPropertyChanged(); } }

    private string? _place;
    public string? Place { get => _place; set { _place = value; OnPropertyChanged(); } }

    private string? _quote;
    public string? Quote { get => _quote; set { _quote = value; OnPropertyChanged(); } }

    private string? _email;
    public string? Email { get => _email; set { _email = value; OnPropertyChanged(); } }

    public ObservableCollection<Recipe> Recepti { get; } = new();

    public Recipe? Recipe1 => Recepti.Count > 0 ? Recepti[0] : null;
    public Recipe? Recipe2 => Recepti.Count > 1 ? Recepti[1] : null;

    public async Task LoadAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var user = await _firebase
                .Child("Users")
                .Child("1")
                .OnceSingleAsync<UserProfile>();

            Name = user?.Name;
            Status = user?.Status;
            Place = user?.Place;
            Quote = user?.Quote;

            Email = user?.Email;

            Recepti.Clear();

            var list = await _firebase
                .Child("Recepti")
                .OnceSingleAsync<List<Recipe>>();

            var firstTwo = (list ?? new List<Recipe>())
                .Where(r => r != null)
                .Take(2)
                .ToList();

            foreach (var r in firstTwo)
                Recepti.Add(r);

            OnPropertyChanged(nameof(Recipe1));
            OnPropertyChanged(nameof(Recipe2));
        }
        finally
        {
            IsBusy = false;
        }
    }
}
