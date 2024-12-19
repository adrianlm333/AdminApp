using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Net.Http;
using System.Net.Http.Json;
using FrontEndWPF.Models;
namespace FrontEndWPF
{
    /// <summary>
    /// Lógica de interacción para DetallePersona.xaml
    /// </summary>
    public partial class DetallePersona : Page
    {
        public DetallePersona(string paramPersonaID)
        {
            _paramPersonaId = paramPersonaID;
            this.Loaded += async (s, e) => { await Page_Loaded(s, e); };
            
            InitializeComponent();
        }

        private async Task Page_Loaded(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7164/api/Directorio/" + _paramPersonaId;
            if (!string.IsNullOrEmpty(_paramPersonaId))
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var strResponse = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(strResponse))
                        {
                            var itemPersonas = await response.Content.ReadFromJsonAsync<Persona>();

                            if (itemPersonas != null)
                            {
                                txtNombre.Text = itemPersonas.nombre;
                                txtApPaterno.Text = itemPersonas.apellido_paterno;
                                txtApMaterno.Text = itemPersonas.apellido_materno;
                                txtClaveIde.Text = itemPersonas.identificacion;
                                await GetFacturasPersona(itemPersonas.id);
                            }
                        }

                    }
                }
            }
        }

        private async Task GetFacturasPersona(int idPersona)
        {
            string apiUrl = "https://localhost:7164/api/Facturacion?idPersona=" + idPersona;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(strResponse))
                    {
                        var itemFacturas = await response.Content.ReadFromJsonAsync<IEnumerable<Factura>>();

                        if (itemFacturas != null)
                        {
                            grFactPersona.ItemsSource = itemFacturas;
                        }
                    }

                }
            }
            
        }

        private string _paramPersonaId {  get; set; }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtApPaterno.Text = string.Empty;
            txtApMaterno.Text = string.Empty;
            txtClaveIde.Text = string.Empty;
            _paramPersonaId = string.Empty;
            grFactPersona.ItemsSource = new List<Factura>();
        }
    }
}
