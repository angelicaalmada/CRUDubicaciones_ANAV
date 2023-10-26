using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Capas
using BLL;
using DAL;

namespace CRUDubicaciones_ANAV
{
    public partial class frmUbicaciones : System.Web.UI.Page
    { 
        ubicaciones_BLL oUbicacionesBLL;
        ubicaciones_DAL oUbicacionesDAL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarUbicaciones();
            }

        }

        //Metodo encargado de listar los datos de la BD en un GRIDView
        public void ListarUbicaciones()
        {
            oUbicacionesDAL = new ubicaciones_DAL();
            gvUbicaciones.DataSource = oUbicacionesDAL.Listar();
            gvUbicaciones.DataBind();
        }

        //Metodo encargado de recolectar los datos de nuestra interfaz
        public ubicaciones_BLL datosUbicacion()
        {
            int ID = 0;
            int.TryParse(txtID.Value, out ID);
            oUbicacionesBLL = new ubicaciones_BLL();

            //Recolectar datos de la capa de presentacion
            oUbicacionesBLL.ID = ID;
            oUbicacionesBLL.Ubicacion = txtUbicacion.Text;
            oUbicacionesBLL.Latitud = txtLat.Text;
            oUbicacionesBLL.Longitud = txtLong.Text;

            return oUbicacionesBLL;
        }


        protected void AgregarRegistro(object sender, EventArgs e)
        {
            oUbicacionesDAL = new ubicaciones_DAL();
            oUbicacionesDAL.Agregar(datosUbicacion());
            ListarUbicaciones(); //Para mostrarlo en el GV
        }

        protected void SeleccionRegistros(object sender, GridViewCommandEventArgs e)
        {
            int FilaSeleccionada = int.Parse(e.CommandArgument.ToString());

            txtID.Value = gvUbicaciones.Rows[FilaSeleccionada].Cells[1].Text;
            txtUbicacion.Text = gvUbicaciones.Rows[FilaSeleccionada].Cells[2].Text;
            txtLat.Text = gvUbicaciones.Rows[FilaSeleccionada].Cells[3].Text;
            txtLong.Text = gvUbicaciones.Rows[FilaSeleccionada].Cells[4].Text;

            //Ahora al momento que demos clik en "Seleccionar" habilitaremos
            //los botones eliminar y modificar, y a la vez, inhabilitaremos
            //el boton de agregar

            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            btnAgregar.Enabled = false;
        }

        protected void EliminarResgistro(object sender, EventArgs e)
        {
            oUbicacionesDAL = new ubicaciones_DAL();
            oUbicacionesDAL.Eliminar(datosUbicacion());
            ListarUbicaciones();
        }

        protected void ModificarRegistos(object sender, EventArgs e)
        {
            oUbicacionesDAL = new ubicaciones_DAL();
            oUbicacionesDAL.Modificar(datosUbicacion());
            ListarUbicaciones(); //Para mostrarlo en el GV
        }

        protected void LimpiarRegistro(object sender, EventArgs e)
        {
            txtLat.Text = "27.365938954017043";
            txtLong.Text = "-109.93136356074504";
            txtID.Value = "";
            ListarUbicaciones();
        }
    }
}