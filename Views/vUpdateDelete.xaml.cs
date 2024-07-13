using acabreraS6.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net;


namespace acabreraS6.Views;

public partial class vUpdateDelete : ContentPage
{
    private const string url = "http://192.168.17.38/semana6/post.php";
    private readonly HttpClient _client;
    private Estudiante _estudiante;
    public vUpdateDelete(Estudiante est)
	{
		InitializeComponent();
        _client = new HttpClient();
        _estudiante = est;
        BindingContext = _estudiante;
        txtCodigo.Text = _estudiante.codigo.ToString();
        txtNombre.Text = _estudiante.nombre;
        txtApellido.Text = _estudiante.apellido;
        txtEdad.Text = _estudiante.edad.ToString();

    }
    
    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var param = new System.Collections.Specialized.NameValueCollection();
            param.Add("codigo", txtCodigo.Text);
            param.Add("nombre", txtNombre.Text);
            param.Add("apellido", txtApellido.Text);
            param.Add("edad", txtEdad.Text);

            // Construimos la URL con los parámetros de consulta
            string baseUrl = "http://192.168.100.70/semana6/post.php";
            string queryString = string.Join("&", param.AllKeys.Select(key => $"{key}={param[key]}"));
            string url = $"{baseUrl}?{queryString}";

            cliente.UploadStringCompleted += (s, ev) =>
            {
                if (ev.Error == null)
                {
                    DisplayAlert("Alerta", "Estudiante actualizado correctamente", "Ok");
                    Navigation.PushAsync(new EstudiantePrincipal());
                }
                else
                {
                    DisplayAlert("Alerta", ev.Error.Message, "Cerrar");
                }
            };

            // Enviamos la solicitud PUT
            cliente.UploadStringAsync(new Uri(url), "PUT", string.Empty);
        }
        catch (Exception ex)
        {
            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool confirm = await DisplayAlert("Confirmar", "¿Está seguro que desea eliminar este estudiante?", "Sí", "No");
            if (confirm)
            {
                WebClient cliente = new WebClient();
                var param = new System.Collections.Specialized.NameValueCollection();
                param.Add("codigo", txtCodigo.Text);

                // Construimos la URL con los parámetros de consulta
                string baseUrl = "http://192.168.100.70/semana6/post.php";
                string queryString = string.Join("&", param.AllKeys.Select(key => $"{key}={param[key]}"));
                string url = $"{baseUrl}?{queryString}";

                cliente.UploadStringCompleted += (s, ev) =>
                {
                    if (ev.Error == null)
                    {
                        DisplayAlert("Alerta", "Estudiante eliminado correctamente", "Ok");
                        Navigation.PushAsync(new EstudiantePrincipal());
                    }
                    else
                    {
                        DisplayAlert("Alerta", ev.Error.Message, "Cerrar");
                    }
                };

                // Enviamos la solicitud DELETE
                cliente.UploadStringAsync(new Uri(url), "DELETE", string.Empty);
            }
        }
        catch (Exception ex)
        {
           await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }

    }
}