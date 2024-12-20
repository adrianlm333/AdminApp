using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Printing;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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

namespace FrontEndWPF
{
    /// <summary>
    /// Lógica de interacción para FacturaView.xaml
    /// </summary>
    public partial class FacturaView : Page
    {
        private string _paramPersonaId { get; set; }
        private readonly string ApiUrl = "https://localhost:7164/api/";
        private readonly string ApiFacturacion = "Facturacion/";
        private int _idPersona = 0;
        private string _identificacion = string.Empty;
        public FacturaView(int idPersona, string identificacion)
        {
            _idPersona = idPersona;
            _identificacion = identificacion;
            InitializeComponent();
            btnGuardar.Click += async (s, e) => { await btnGuardar_Click(s, e); };
        }

        private void inpMonto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextAllowed(e.Text))
            {
                e.Handled = true;
            }
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DetallePersonaView(_identificacion), UriKind.Relative);
        }

        private async Task btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var vDate = inpFecha.SelectedDate;
            var vMonto = inpMonto.Text;
            decimal decMonto = 0;
            if(decimal.TryParse(vMonto, out decMonto))
            {            
                bool continueSave = true;
                if (vDate == null)
                {
                    MessageBox.Show("Seleccione la fecha de la factura", "Factura", MessageBoxButton.OK, MessageBoxImage.Error);
                    continueSave = false;
                }
                if (decMonto <= 0)
                {
                    MessageBox.Show("Capture un monto valido", "Persona", MessageBoxButton.OK, MessageBoxImage.Error);
                    continueSave = false;
                }

                if (continueSave)
                {
                    string messageBoxText = "¿Quiere guardar la factura?";
                    string caption = "Guardar";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result;

                    result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

                    if (result == MessageBoxResult.Yes)
                    {
                        using (HttpClient client = new HttpClient())
                        {

                            using StringContent jsonContent = new(
                                JsonSerializer.Serialize(new
                                {
                                    id = 0,
                                    fecha = vDate,
                                    monto = vMonto,
                                    idpersona = _idPersona
                                }),
                                Encoding.UTF8,
                                "application/json");
                            HttpResponseMessage response;
                            response = await client.PutAsync(ApiUrl + ApiFacturacion, jsonContent);
                            

                            if (response.IsSuccessStatusCode)
                            {
                                var strResponse = await response.Content.ReadAsStringAsync();
                                if (!string.IsNullOrEmpty(strResponse))
                                {
                                    var personaResult = await response.Content.ReadFromJsonAsync<Factura>();

                                    if (personaResult != null)
                                    {
                                        MessageBox.Show("Se guardó correctamente", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
    }
}
