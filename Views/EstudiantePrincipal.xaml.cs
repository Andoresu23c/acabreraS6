using acabreraS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace acabreraS6.Views;

public partial class EstudiantePrincipal : ContentPage
{
    private const string url = "http://192.168.17.38/semana6/post.php";
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
}