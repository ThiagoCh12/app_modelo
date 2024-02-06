using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

using System.Net.NetworkInformation;

namespace app_modelo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            lblPais.Text = txtPais.Text;
            lblUf.Text = txtUF.Text;
            lblLocal.Text = txtLocal.Text;
            lblCidade.Text = txtCidade.Text;

            try
            {
                var locations = await Geocoding.GetLocationsAsync($"{lblPais.Text}, {lblUf.Text}, {lblLocal.Text}, {lblCidade.Text}");
                if (locations != null && locations.Any())
                {
                    var firstLocation = locations.First();
                    double latitude = firstLocation.Latitude;
                    double longitude = firstLocation.Longitude;

                    var location = new Location(latitude, longitude);
                    var options = new MapLaunchOptions { Name = $"{lblPais.Text}, {lblUf.Text}, {lblLocal.Text}, {lblCidade.Text}" };
                    await Map.OpenAsync(location, options);
                }
                else
                {
                    await DisplayAlert("Erro", "Não foi possível obter as coordenadas para a localidade fornecida.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
            }
        }

    }
}
