using acabreraS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json.Serialization;

namespace acabreraS6.Views;

public partial class EstudiantePrincipal : ContentPage
{
    private const string url = "http://192.168.100.70/semana6/post.php";
    private readonly HttpClient client = new HttpClient();
    private ObservableCollection<Estudiante> estud;
    public EstudiantePrincipal()
    {
        InitializeComponent();
        Obtener();
    }

    public async void Obtener()
    {
        var content = await client.GetStringAsync(url);
        List<Estudiante> mostrarEst = JsonConvert.DeserializeObject<List<Estudiante>>(content);
        estud = new ObservableCollection<Estudiante>(mostrarEst);
        listaEstudiantes.ItemsSource = estud;
    }

    private async void OnAgregarActualizarClicked(object sender, EventArgs e)
    {
        Estudiante estudiante = new Estudiante
        {
            codigo = string.IsNullOrEmpty(codigoEntry.Text) ? 0 : int.Parse(codigoEntry.Text),
            nombre = nombreEntry.Text,
            apellido = apellidoEntry.Text,
            edad = int.Parse(edadEntry.Text)
        };
        var values = new Dictionary<string, string>
            {
                { "nombre", estudiante.nombre },
                { "apellido", estudiante.apellido },
                { "edad", estudiante.edad.ToString() }
            };
        if (estudiante.codigo != 0)
        {
            values.Add("codigo", estudiante.codigo.ToString());
        }

        var content = new FormUrlEncodedContent(values);

        HttpResponseMessage response;
        if (estudiante.codigo == 0)
        {
            // Agregar
            response = await client.PostAsync(url, content);
        }
        else
        {
            // Actualizar
            response = await client.PutAsync(url + "?codigo=" + estudiante.codigo, content);
        }

        if (response.IsSuccessStatusCode)
        {
            Obtener();
        }
        else
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            await DisplayAlert("Error", responseContent, "OK");
        }

    }
    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        
    }
    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            var selectedEstudiante = e.SelectedItem as Estudiante;
            codigoEntry.Text = selectedEstudiante.codigo.ToString();
            nombreEntry.Text = selectedEstudiante.nombre;
            apellidoEntry.Text = selectedEstudiante.apellido;
            edadEntry.Text = selectedEstudiante.edad.ToString();
            ((ListView)sender).SelectedItem = null;
        }
    }
}