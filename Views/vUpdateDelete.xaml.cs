using acabreraS6.Models;

namespace acabreraS6.Views;

public partial class vUpdateDelete : ContentPage
{
	public vUpdateDelete(Estudiante est)
	{
		InitializeComponent();
        txtCodigo.Text= est.codigo.ToString();
        txtNombre.Text= est.nombre.ToString();
        txtApellido.Text= est.apellido.ToString();  
        txtEdad.Text= est.edad.ToString();
	}

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {

    }
}