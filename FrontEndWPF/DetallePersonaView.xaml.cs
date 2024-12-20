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
using System.Text.Json;
using FrontEndWPF.Tools;
namespace FrontEndWPF
{
    /// <summary>
    /// Lógica de interacción para DetallePersona.xaml
    /// </summary>
    public partial class DetallePersonaView : Page
    {
        private string _paramPersonaId { get; set; }
        private readonly string ApiUrl = "https://localhost:7164/api/";
        private readonly string ApiDirectorio = "Directorio/";
        private readonly string ApiFacturacion = "Facturacion/";
        private Persona? objPersona { get; set; }

        public DetallePersonaView(string paramPersonaID)
        {
            _paramPersonaId = paramPersonaID;
            this.Loaded += async (s, e) => { await Page_Loaded(s, e); };            
            InitializeComponent();
            btnUpdatePersona.Click += async (s, e) => { await btnUpdatePersona_Click(s, e); };
            btnEliminarPersona.Click += async (s, e) => { await btnEliminarPersona_Click(s, e); };
        }

        private async Task Page_Loaded(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_paramPersonaId))
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(ApiUrl + ApiDirectorio + _paramPersonaId);
                        if (response.IsSuccessStatusCode)
                        {
                            var strResponse = await response.Content.ReadAsStringAsync();
                            if (!string.IsNullOrEmpty(strResponse))
                            {
                                var itemPersonas = await response.Content.ReadFromJsonAsync<IEnumerable<Persona>>();

                                if (itemPersonas != null)
                                {
                                    txtNombre.Text = itemPersonas.First().nombre;
                                    txtApPaterno.Text = itemPersonas.First().apellido_paterno;
                                    txtApMaterno.Text = itemPersonas.First().apellido_materno;
                                    txtClaveIde.Text = itemPersonas.First().identificacion;
                                    txtClaveIde.IsReadOnly = true;
                                    btnAddFact.Visibility = Visibility.Visible;
                                    btnEliminarPersona.Visibility = Visibility.Visible;
                                    grFactPersona.Visibility = Visibility.Visible;
                                    objPersona = itemPersonas.First();
                                    await GetFacturasPersona(itemPersonas.First().id);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No existen datos para mostrar, intente mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
            else
            {
                txtNombre.Text = "";
                txtApPaterno.Text = "";
                txtApMaterno.Text = "";
                txtClaveIde.Text = "";
                txtClaveIde.IsReadOnly = false;
                grFactPersona.ItemsSource = new List<Factura>();
                btnAddFact.Visibility = Visibility.Hidden;
                btnEliminarPersona.Visibility = Visibility.Hidden;
                grFactPersona.Visibility = Visibility.Hidden;
            }
        }

        

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtApPaterno.Text = string.Empty;
            txtApMaterno.Text = string.Empty;
            txtClaveIde.Text = string.Empty;
            _paramPersonaId = string.Empty;
            grFactPersona.ItemsSource = new List<Factura>();
            objPersona = null;
        }

        private async Task btnUpdatePersona_Click(object sender, RoutedEventArgs e)
        {
            var vNombre = txtNombre.Text;
            var vApPaterno = txtApPaterno.Text;
            var vClaveId = txtClaveIde.Text;
            var continueSave = true;

            if (string.IsNullOrEmpty(vNombre))
            {
                MessageBox.Show("Capture el nombre", "Persona", MessageBoxButton.OK, MessageBoxImage.Error);
                continueSave = false;
            }
            if (string.IsNullOrEmpty(vApPaterno))
            {
                MessageBox.Show("Capture el apellido paterno", "Persona", MessageBoxButton.OK, MessageBoxImage.Error);
                continueSave = false;
            }
            if (string.IsNullOrEmpty(vClaveId))
            {
                MessageBox.Show("Capture el apellido materno", "Persona", MessageBoxButton.OK, MessageBoxImage.Error);
                continueSave = false;
            }

            if (continueSave)
            {
                string messageBoxText = "¿Quiere guardar los cambios?";
                string caption = "Guardar";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {                    
                        using (HttpClient client = new HttpClient())
                        {
                            int intId = 0;
                            if(objPersona != null)
                                intId = objPersona.id;

                            using StringContent jsonContent = new(
                                JsonSerializer.Serialize(new
                                {
                                    id = intId,
                                    nombre = txtNombre.Text,
                                    apellido_paterno = txtApPaterno.Text,
                                    apellido_materno = txtApMaterno.Text,
                                    identificacion = txtClaveIde.Text,
                                }),
                                Encoding.UTF8,
                                "application/json");
                            HttpResponseMessage response;
                            if (string.IsNullOrEmpty(_paramPersonaId)){
                                response = await client.PutAsync(ApiUrl + ApiDirectorio, jsonContent);
                            }
                            else
                            {
                                response = await client.PostAsync(ApiUrl + ApiDirectorio, jsonContent);
                            }

                            if (response.IsSuccessStatusCode)
                            {
                                var strResponse = await response.Content.ReadAsStringAsync();
                                if (!string.IsNullOrEmpty(strResponse))
                                {
                                    var personaResult = await response.Content.ReadFromJsonAsync<Persona>();

                                    if (personaResult != null)
                                    {
                                        MessageBox.Show("Se guardó correctamente", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No existen datos para mostrar, intente mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private async Task GetFacturasPersona(int idPersona)
        {
            try
            {


                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(ApiUrl + ApiFacturacion + "?idPersona=" + idPersona);
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
            catch (Exception ex)
            {
                MessageBox.Show("No existen datos para mostrar, intente mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task btnEliminarPersona_Click(object sender, RoutedEventArgs e)
        {
            string messageEliminarBoxText = "Se eliminara la persona y todas sus facturas, ¿Desea continuar?";
            string captionEliminar = "Eliminar";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageEliminarBoxText, captionEliminar, button, icon, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {
                string messageBoxText = "La persona y sus facturas fueron eliminadas";
                string caption = "Eliminar";

                try
                {


                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.DeleteAsync(ApiUrl + ApiDirectorio + "?identificacion=" + objPersona.identificacion);
                        if (response.IsSuccessStatusCode)
                        {
                            var strResponse = await response.Content.ReadAsStringAsync();
                            if (!string.IsNullOrEmpty(strResponse))
                            {
                                bool itemFacturas = await response.Content.ReadFromJsonAsync<bool>();

                                if (itemFacturas)
                                {
                                    MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                    this.NavigationService.Navigate(new PersonasView(), UriKind.Relative);
                                }
                                else
                                {
                                    messageBoxText = "No se pudo borrar, valide que los datos esten correctos";
                                    caption = "Eliminar";
                                    MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No existen datos para mostrar, intente mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnAddFact_Click(object sender, RoutedEventArgs e)
        {
            if(objPersona != null)
            {
                this.NavigationService.Navigate(new FacturaView(objPersona.id, _paramPersonaId), UriKind.Relative);
            }            
        }

        private void btnRegresarPrincipal_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PersonasView(), UriKind.Relative);
        }
    }
}
