using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL_FORMS
{
    public partial class GetAll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            ML.Result result = BL.Materia.GetAll();
            gvUsuarios.DataSource = result.Objects;
            gvUsuarios.DataBind();
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                // Redirige a la vista de edición con el ID
                Response.Redirect($"Form.aspx?IdMateria={id}");
            }
            else if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ML.Result result = BL.Materia.DeleteLINQ(id);
                CargarDatos();
            }
        }
    }
}