<%@ Page Title="GetAll" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GetAll.aspx.cs" Inherits="PL_FORMS.GetAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mt-4">Lista de Materias</h2>

    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered" GridLines="None"
        OnRowCommand="gvUsuarios_RowCommand">
        <Columns>
            <asp:BoundField DataField="IdMateria" HeaderText="IdMateria" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Creditos" HeaderText="Creditos" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-primary me-2"
                        CommandName="Editar" CommandArgument='<%# Eval("IdMateria") %>' ToolTip="Editar"
                        Text='<i class="bi bi-pencil"></i>' HtmlEncode="false">
                    </asp:LinkButton>


                    <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-danger"
                        CommandName="Eliminar" CommandArgument='<%# Eval("IdMateria") %>' ToolTip="Eliminar"
                        Text='<i class="bi bi-trash"></i>' HtmlEncode="false"
                        OnClientClick="return confirm('¿Estás seguro que deseas eliminar esta materia?');">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
