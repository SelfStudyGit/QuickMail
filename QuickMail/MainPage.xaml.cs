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
        _vmSetting = new Views.SettingViewModel();

        // Add the Appearing event handler
        this.Appearing += MainPage_Appearing;
    }
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
        mail.BodyText = BodyTextEditor.Text;
        
        SendButton.IsEnabled = false;
        BodyTextEditor.Text = "Sending message...";
        await mail.SendMailAsync();

        Environment.Exit(0);    //close this app. invalid in debug mode at Visual Studio
        SendButton.IsEnabled = true;
    }
}