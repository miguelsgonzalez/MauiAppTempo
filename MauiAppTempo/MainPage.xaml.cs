using MauiAppTempo.Models;
using MauiAppTempo.Services;
using System.Net.Http;

namespace MauiAppTempo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = $"Latitude: {t.lat} \n" +
                                                $"Longitude: {t.lon} \n" +
                                                $"Nascer do Sol: {t.sunrise} \n" +
                                                $"Pôr do Sol: {t.sunset} \n" +
                                                $"Temp Máx: {t.temp_max} °C\n" +
                                                $"Temp Min: {t.temp_min} °C\n" +
                                                $"Clima: {t.description} \n" +
                                                $"Velocidade do Vento: {t.speed} m/s\n" +
                                                $"Visibilidade: {t.visibility} metros";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        await DisplayAlert("Cidade não encontrada", "Não foi possível encontrar a sua cidade. Verifique novamente.", "OK");
                        lbl_res.Text = string.Empty;
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a sua cidade.";
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Sem conexão", "Verifique sua conexão com a internet e verifique novamente.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
            }
        }
    }
}
