using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrontEndWPF.Models;
using System.Net.Http.Json;
namespace FrontEndWPF
{
    /// <summary>
    /// Lógica de interacción para Personas.xaml
    /// </summary>
    public partial class PersonasView : Page
    {
        public PersonasView()
        {
            this.Loaded += async (s, e) => { await Page_Loaded(s, e); };
            InitializeComponent();

            BtnConsultar.Click += async (s, e) => { await BtnConsultar_Click(s, e); };
            dataGrid1.MouseDoubleClick += DataGrid_MouseDoubleClick;
        }

        private async Task Page_Loaded(object s, RoutedEventArgs e)
        {
            await GetPersonas("");
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var element = (Persona)dataGrid1.SelectedItem;
            this.NavigationService.Navigate(new DetallePersonaView(element.identificacion), UriKind.Relative);
        }

        internal List<Persona> LstPersonas { get; set; }

        private async Task BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            await GetPersonas(txtIdentificacion.Text);
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DetallePersonaView(""), UriKind.Relative);
        }

        private async Task GetPersonas(string pFilter)
        {
            try
            {
                string apiUrl = "https://localhost:7164/api/Directorio/" + pFilter;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var personasCollection = await response.Content.ReadFromJsonAsync<IEnumerable<Persona>>();

                        if (personasCollection != null)
                        {
                            LstPersonas = personasCollection.ToList();
                            dataGrid1.ItemsSource = LstPersonas;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API no disponible, intente mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
