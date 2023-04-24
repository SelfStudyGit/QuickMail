using MimeKit;
using System;
using System.Net.Mail;
using QuickMail.Views;

namespace QuickMail;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        this.Loaded += MainPage_Loaded;

        this.Appearing += MainPage_Appearing;
    }

    private void MainPage_Loaded(object sender, EventArgs e)
    {
        this.BindingContext = _vm;
        _vmSetting = new Views.SettingViewModel();
    }
    ViewModel _vm = new ViewModel();
    Views.SettingViewModel _vmSetting;

    private void MainPage_Appearing(object sender, EventArgs e)
    {
        // Set focus on BodyTextEditor when the page appears
        this.Dispatcher.Dispatch(() =>
        {
            BodyTextEditor.Focus();
        });
    }

    private async void SendButton_ClickedAsync(Object sender, EventArgs e)
    {
        QuickMail mail = new QuickMail(_vmSetting.MailAddressFrom, _vmSetting.MailAddressTo, _vmSetting.Username, _vmSetting.Password);
        mail.BodyText = _vm.BodyText;

        SendButton.IsEnabled = false;
        _vm.BodyText = "Sending message...";
        BodyTextEditor.IsEnabled = false;
        await mail.SendMailAsync();

        Environment.Exit(0);    //close this app.
        // if the app is not closed, GUI will be reset.
        _vm.BodyText = "";
        BodyTextEditor.IsEnabled = true;
        SendButton.IsEnabled = true;
    }
}

public class ViewModel : Prism.Mvvm.BindableBase
{
    private string _bodyText;
    public string BodyText
    {
        get => _bodyText;
        set => SetProperty(ref _bodyText, value, nameof(BodyText));
    }
}