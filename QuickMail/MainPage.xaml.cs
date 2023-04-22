using MimeKit;
using System;

namespace QuickMail;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void SnedButton_ClickedAsync(Object sender, EventArgs e)
    {
        QuickMail mail = new QuickMail();
        mail.SendAddress = sendAddressEntry.Text;
        mail.BodyText = BodyTextEditor.Text;

        BodyTextEditor.Text = "Sending message...";
        await mail.SendMailAsync();

        Environment.Exit(0);    //close this app. invalid in debug mode at Visual Studio
    }
}