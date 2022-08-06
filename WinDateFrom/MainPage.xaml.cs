using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void calcola_Click(object sender, RoutedEventArgs e)
        {
            DateTime d = DateTime.Now;
            DateTime d1;
            try
            {
                d1 = DateTime.Parse(data.Text);
            } catch (System.FormatException ex)
            {
                risultato.Text = "La data non è stata validata.";
                return;
            }
            TimeSpan differenza = d - d1;
            risultato.Text = "Hai incontrato "+nome.Text + " circa "+differenza.Days+" fa"+".";
            
        }
    }
}
