using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL_FORMS
{
    public partial class Form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IdMateria"] != null)
                {
                    int id = int.Parse(Request.QueryString["IdMateria"]);
                    CargarMateria(id);
                }
            }
        }

        private void CargarMateria(int id)
        {
            var result = BL.Materia.GetById(id);
            if (result.Correct)
            {
                var materia = (ML.Materia)result.Object;
                hfIdMateria.Value = materia.IdMateria.ToString();
                txtNombre.Text = materia.Nombre;
                txtCreditos.Text = materia.Creditos.ToString();
                txtCosto.Text = materia.Costo.ToString();
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ML.Materia materia = new ML.Materia();
            materia.Nombre = txtNombre.Text.Trim();
            materia.Creditos = Convert.ToDecimal(txtCreditos.Text.Trim());
            materia.Costo = Convert.ToDecimal(txtCosto.Text.Trim());

            if (!string.IsNullOrEmpty(hfIdMateria.Value))
            {
                // Update
                materia.IdMateria = int.Parse(hfIdMateria.Value);
                var result = BL.Materia.UpdateLINQ(materia);
                if (result.Correct)
                {
                    Response.Redirect("GetAll.aspx");
                }
                else
                {
                    lblMensaje.Text = "Error al actualizar.";
                }
            }
            else
            {
                // Add
                var result = BL.Materia.Add(materia);
                if (result.Correct)
                {
                    Response.Redirect("GetAll.aspx");
                }
                else
                {
                    lblMensaje.Text = "Error al agregar.";
                }
            }
        }
    }
}