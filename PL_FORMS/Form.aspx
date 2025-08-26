<%@ Page Title="Formulario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="PL_FORMS.Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mt-4">Formulario de Materia</h2>

    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success"></asp:Label>

    <asp:HiddenField ID="hfIdMateria" runat="server" />

    <div class="mb-3">
        <label for="txtNombre" class="form-label">Nombre</label>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
    </div>

    <div class="mb-3">
        <label for="txtCreditos" class="form-label">Creditos</label>
        <asp:TextBox ID="txtCreditos" runat="server" CssClass="form-control" />
    </div>

    <div class="mb-3">
        <label for="txtCosto" class="form-label">Costo</label>
        <asp:TextBox ID="txtCosto" runat="server" CssClass="form-control" />
    </div>

    <asp:FileUpload ID="fuImagen" runat="server" CssClass="form-control mb-3" />
<asp:Image ID="imgPreview" runat="server" Width="150" Height="150" CssClass="mb-3" />

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success"
        OnClick="btnGuardar_Click" />
    <a href="GetAll.aspx" class="btn btn-secondary ms-2">Cancelar</a>

    <script type="text/javascript">
        function mostrarVistaPrevia(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var preview = document.getElementById('<%= imgPreview.ClientID %>');
                    preview.src = e.target.result;
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

</asp:Content>

