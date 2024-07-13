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

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vAgregar());
    }

    private void listaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objetoEstudiante = (Estudiante)e.SelectedItem;
        Navigation.PushAsync(new vUpdateDelete(objetoEstudiante));
    }
}