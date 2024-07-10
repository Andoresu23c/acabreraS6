using acabreraS6.Models;
using System;
using System.Collections.ObjectModel;
using System.Net;


namespace acabreraS6.Views;

public partial class vUpdateDelete : ContentPage
{
    private const string url = "http://192.168.17.38/semana6/post.php";
    private readonly HttpClient client = new HttpClient();
    private readonly Estudiante estudiante = new Estudiante();
    public vUpdateDelete(Estudiante est)
	{
		InitializeComponent();
        txtCodigo.Text = est.codigo.ToString();
        txtNombre.Text = est.nombre;
        txtApellido.Text = est.apellido;
        txtEdad.Text = est.edad.ToString();

    }
    
    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        WebClient cliente = new WebClient();
        var param = new System.Collections.Specialized.NameValueCollection();
        param.Add("codigo", txtCodigo.Text);
        param.Add("nombre", txtNombre.Text);
        param.Add("apellido", txtApellido.Text);
        param.Add("edad", txtEdad.Text);
        cliente.UploadValues("http://192.168.17.38/semana6/post.php", "PUT", param);
        Navigation.PushAsync(new EstudiantePrincipal());

    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {

    }
}