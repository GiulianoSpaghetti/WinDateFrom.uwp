using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.System.Profile.SystemManufacturers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace WinDateFrom
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        private Windows.Storage.ApplicationDataContainer container;
        private static string anniversario = "";
        public MainPage()
        {
            MessageDialog d;
            this.InitializeComponent();
            container = localSettings.CreateContainer("WinDateFrom", Windows.Storage.ApplicationDataCreateDisposition.Always);
            int month, years, days;
            String s, s1, s2, s3;
            s = localSettings.Containers["WinDateFrom"].Values["month"] as string;
            s1 = localSettings.Containers["WinDateFrom"].Values["years"] as string;
            s2 = localSettings.Containers["WinDateFrom"].Values["days"] as string;
            s3 = localSettings.Containers["WinDateFrom"].Values["name"] as string;
            try
            {
                month = int.Parse(s);
                years = int.Parse(s1);
                days = int.Parse(s2);
                Data.Date = new DateTime(years, month, days);
            }
            catch (Exception ex)
            {
                Data.Date = DateTime.Now;
            }
            if (s3 != null)
                nome.Text = s3;
            if (!SystemSupportInfo.LocalDeviceInfo.SystemProductName.Contains("Xbox"))
            {
                d = new MessageDialog("Unsupported platform");
                d.Commands.Add(new UICommand("Exit", new UICommandInvokedHandler(exit)));
                IAsyncOperation<IUICommand> asyncOperation = d.ShowAsync();
            }

        }
        private async void augurio_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri($"https://twitter.com/intent/tweet?text=Happy%20{anniversario}%20my%20love"));
            augurio.Visibility = Visibility.Collapsed;

        }
        private void info_Click(object sender, RoutedEventArgs e)
        {
            GApp.Visibility = Visibility.Collapsed;
            GInfo.Visibility = Visibility.Visible;
        }
        private void app_Click(object sender, RoutedEventArgs e)
        {
            GInfo.Visibility = Visibility.Collapsed;
            GApp.Visibility = Visibility.Visible;
        }

        private void app_Delete(object sender, RoutedEventArgs e)
        {
            localSettings.Containers["WinDateFrom"].Dispose();
        }
        private void exit(IUICommand command)
        {
            Application.Current.Exit();
        }
        private void calcola_Click(object sender, RoutedEventArgs e)
        {
            DateTime d = DateTime.Now;

            TimeSpan differenza = d - Data.Date;
            anniversario = "";
            augurio.Visibility= Visibility.Collapsed;
            if (differenza.TotalMinutes < 0)
                risultato.Text = "Invalid rvalue";
            else
            {
                localSettings.Containers["WinDateFrom"].Values["month"] = Data.Date.Month.ToString();
                localSettings.Containers["WinDateFrom"].Values["years"] = Data.Date.Year.ToString();
                localSettings.Containers["WinDateFrom"].Values["days"] = Data.Date.Day.ToString();
                localSettings.Containers["WinDateFrom"].Values["name"] = nome.Text;
                if (nome.Text == "")
                    risultato.Text = $"{differenza.Days} days have passed";
                else
                    risultato.Text = risultato.Text = $"You meet {nome.Text} about {differenza.Days} days ago.";

                if (d.Day == Data.Date.Day && differenza.TotalDays > 1)
                {
                    if (d.Month == Data.Date.Month)
                    {
                        mesiversary.Text = "Is your Anniversary";
                        anniversario = "Anniversary";
                    }
                    else
                    {
                        mesiversary.Text = "Is your mesiversary";
                        anniversario = "mesiversary";
                    }
                    if (nome.Text!="" && anniversario!="")
                        augurio.Visibility = Visibility.Visible;
                }
                else
                    mesiversary.Text = "";
            }
        }
    }
}
