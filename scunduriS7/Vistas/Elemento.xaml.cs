using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace scunduriS7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Elemento : ContentPage
    {
        private SQLiteAsyncConnection con;
        public int IdSeleccionado;
        IEnumerable<Estudiante> rActualizar;
        IEnumerable<Estudiante> rEliminar;
        public Elemento(Estudiante datos)
        {
            InitializeComponent();
            txtNombre.Text = datos.Nombre;
            txtUsuario.Text = datos.Usuario;
            txtContrasena.Text = datos.Contrasena;
            IdSeleccionado = datos.Id;

            con = DependencyService.Get<DataBase>().GetConnection();
        }

        public static IEnumerable<Estudiante> Eliminar(SQLiteConnection db, int id) 
        {
            return db.Query<Estudiante>("delete from Estudiante where Id = ?", id);
        }

        public static IEnumerable<Estudiante> Actualizar(SQLiteConnection db, int id, string nombre, string usuario, string contrasena) 
        {
            return db.Query<Estudiante>("update Estudiante set Nombre=?,Usuario=?,Contrasena=? where Id = ?",nombre,usuario,contrasena,id);
        }
        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                rActualizar = Actualizar(db, IdSeleccionado, txtNombre.Text, txtUsuario.Text, txtContrasena.Text);
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                rEliminar = Eliminar(db, IdSeleccionado);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}