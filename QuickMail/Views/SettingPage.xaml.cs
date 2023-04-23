namespace QuickMail.Views;

public partial class SettingPage : ContentPage
{
    public SettingPage()
    {
        InitializeComponent();
        this.Loaded += (_, _) =>
        {
            _vm = new SettingViewModel();
            this.BindingContext = _vm;
        };
    }
    SettingViewModel _vm;
}

public class SettingViewModel : Prism.Mvvm.BindableBase
{
    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            SetProperty(ref _username, value, nameof(Username));
            Preferences.Set("Username", _username);
        }
    }
    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            SetProperty(ref _password, value, nameof(Password));
            Preferences.Set("Password", _password);
        }
    }
    private string _mailAddressTo;
    public string MailAddressTo
    {
        get => _mailAddressTo;
        set
        {
            SetProperty(ref _mailAddressTo, value, nameof(MailAddressTo));
            Preferences.Set("MailAddressTo", _mailAddressTo);
        }
    }
    private string _mailAddressFrom;
    public string MailAddressFrom
    {
        get => _mailAddressFrom;
        set
        {
            SetProperty(ref _mailAddressFrom, value, nameof(MailAddressFrom));
            Preferences.Set("MailAddressFrom", _mailAddressFrom);
        }
    }

    public SettingViewModel()
    {
        _username = Preferences.Get("Username", "");
        _password = Preferences.Get("Password", "");
        _mailAddressTo = Preferences.Get("MailAddressTo", "");
        _mailAddressFrom = Preferences.Get("MailAddressFrom", "");
    }
}