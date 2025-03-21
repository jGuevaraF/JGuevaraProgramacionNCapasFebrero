$(document).ready(function () {
    GetAll();

});

function GetAll() {
    $.ajax({
        url: pathGetAll,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            if (result.Correct) {

                let tbody = $('#tbodyMateria');
                //let tbody = document.getElementById('tbodyMateria')
                tbody.empty();

                $.each(result.Objects, function (i, valor) {
                    let etiqueta = `
                        <tr>
                            <td>${valor.IdMateria}</td>
                            <td>${valor.Nombre}</td>
                            <td>${valor.Creditos}</td>
                        </tr>
                    `;

                    tbody.append(etiqueta)
                })

            } else {
                alert('Error' + result.ErrorMessage)
            }
        },
        error: function () {
            console.log('ERROR')
        }

    })
}

function Add() {

}  

function Update() {

}

function Guardar() {
    //validaciones

    if (IdMateria == 0) {
        Add();
    } else {
        Update();
    }
}
function Formulario() {
    cleanModal();
    DDLROl();
    DLLEstado();
    showModal();
}
function showModal() {
    $('#staticBackdrop').modal("show");
}

function cleanModal() {

}

function DDL() {

}


function DDLRol() {

}

function DDLEstado() {

}