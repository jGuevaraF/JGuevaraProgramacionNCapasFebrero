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

function Ajax() {

    var json = ObtenerInfoFormulario();

    Guardar(json);
} 

function Update() {

}

function Guardar(json) {
    //validaciones

    if (json.IdMateria == 0) {
        Add();
    } else {
        Update();
    }
}

function ObtenerInfoFormulario() {
    //id
    //nombre
    var json= {
        "idMateria" : 234
    }

    var json = {
        "Nombre": 234
    }


    return json;
}
function Formulario() {

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