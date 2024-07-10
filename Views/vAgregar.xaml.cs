using System.Net;

namespace acabreraS6.Views;

public partial class vAgregar : ContentPage
{
	public vAgregar()
	{
		InitializeComponent();
	}

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
		try
		{
			WebClient cliente = new WebClient();
			var param = new System.Collections.Specialized.NameValueCollection();
			param.Add("nombre", txtNombre.Text);
			param.Add("apellido", txtApellido.Text);
			param.Add("edad", txtEdad.Text);
			cliente.UploadValues("http://192.168.17.38/semana6/post.php", "POST", param);
			Navigation.PushAsync(new EstudiantePrincipal());
		}
		catch (Exception ex)
		{

			DisplayAlert("Alerta", ex.Message, "Cerrar");
		}

    }
}