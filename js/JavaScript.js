ft_MostrarClientes("todos");

function ft_MostrarClientes(nombre) { // Para recuperar todos los registros y mostrarlos en la tabla
    $(document).ready(function () {
        $.ajax({
            type: "POST",
            url: "Form_Consulta.aspx/Mostrar_Clientes", // WebMethod Mostrar_Clientes. Lo utilizo para mostrar todos los registros y el resultado de la búsqueda
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ N: nombre }),
            success: function (response) {
                var data = JSON.parse(response.d); 
                var table = $("#tbl_Clientes");
                table.empty(); 
                var headerRow = $("<tr></tr>");
                headerRow.append($("<th style='display: none;'>ID</th>")); // Encabezados de la tabla con Id oculto
                headerRow.append($("<th>Seleccionar</th>"));
                headerRow.append($("<th>Nombre</th>"));
                headerRow.append($("<th>Primer Apellido</th>"));
                headerRow.append($("<th>Segundo Apellido</th>"));
                headerRow.append($("<th>Identificación</th>"));
                headerRow.append($("<th>Acciones</th>"));
                table.append(headerRow);
                $.each(data, function (index, item) {
                    var row = $("<tr></tr>");
                    row.append($("<td style='display: none;'>" + item.Id + "</td>")); // Todos los registros con Id oculto
                    row.append($("<td><button type='button' class='btn btn-primary btn-sm' onclick='ft_EditarClientes(this)'>Sel</button></td>"));
                    row.append($("<td>" + item.Nombre + "</td>"));
                    row.append($("<td>" + item.PrimerApellido + "</td>"));
                    row.append($("<td>" + item.SegundoApellido + "</td>"));
                    row.append($("<td>" + item.Identificacion + "</td>"));
                    row.append($("<td><button class='btn btn-danger btn-sm' onclick='ft_EliminarClientes(this)'>Eliminar</button></td>"));
                    table.append(row);
                });
            },
            error: function (response) {
                alert("Error al recuperar los datos.");
            }
        });
    });
}

function ft_EliminarClientes(btn) { // Para eliminar el registro seleccionado con el botón eliminar de la fila deseada
    var row = $(btn).closest('tr');
    var IdC = row.find('td:eq(0)').text(); // Tomo el Id oculto de la primera columna
    console.log(IdC);

    $.ajax({
        type: "POST",
        url: "Form_Consulta.aspx/Eliminar_Cliente", // WebMethod Eliminar_Cliente que recibe el Id del cliente
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ IdCliente: IdC }),
        success: function (response) {
            var mensaje = response.d;
            alert(mensaje);
            ft_MostrarClientes('todos'); // Refresco la tabla después de la eliminación
        },
        error: function (response) {
            alert("Error al eliminar el cliente.");
        }
    });
}

function ft_EditarClientes(btn) {  // Para editar el cliente deseado según el botón Sel de la fila
    var row = $(btn).closest('tr');
    var IdC = row.find('td:eq(0)').text(); // Tomo el Id oculto de la primera columna

    $.ajax({
        type: "POST",
        url: "Form_Consulta.aspx/Editar_Cliente", // WebMethod Editar_Cliente que recibe el Id del cliente
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ IdCliente: IdC }),
        success: function (response) {
            var values = JSON.parse(response.d);
            $('#Form_Actualiza').modal('show'); // Muestro el formulario de Actualización con los valores recibidos
            $('#Id').val(values[0].ID);
            $('#nombre').val(values[0].Nombre);
            $('#primerApellido').val(values[0].PrimerApellido);
            $('#segundoApellido').val(values[0].SegundoApellido);
            $('#identificacion').val(values[0].Identificacion);
            $('#telefono').val(values[0].Telefono);
            $('#direccion').val(values[0].Direccion);
            console.log(values);
        },
        error: function (response) {
            alert("Error al recuperar el cliente.");
        }
    });
}

function ft_GuardarCliente() {  // Se usa para Guardar los datos de un nuevo cliente y para guardar las actualizaciones de los clientes
    var IdC = $('#Id').val();
    var n = $('#nombre').val();
    var pa = $('#primerApellido').val();
    var sa = $('#segundoApellido').val();
    var i = $('#identificacion').val();
    var t = $('#telefono').val();
    var d = $('#direccion').val();

    $.ajax({
        type: "POST",
        url: "Form_Consulta.aspx/Guardar_Cliente",  // WebMethod Guardar_Cliente recibe el Id del cliente 0 si es uno nuevo o el valor si es editado
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({
            IdCliente: IdC,
            nombre: n,
            apellido1: pa,
            apellido2: sa,
            identificacion: i,
            telefono: t,
            direccion: d,
        }),
        success: function (response) {
            var mensaje = response.d;
            alert(mensaje);
            ft_MostrarClientes('todos');
        },
        error: function (response) {
            alert("Error al insertar el cliente.");
        }
    });
    ft_Cerrar();
}

function ft_BuscarCliente() {
    ft_MostrarClientes($('#txt_Buscar').val()); // Mostrar los clientes en la tabla según el criterio de búsqueda
}

function ft_NuevoCliente() {  // Para insertar un nuevo cliente, inicializo los campos vacíos
    $('#Id').val(0);
    $('#nombre').val("");
    $('#primerApellido').val("");
    $('#segundoApellido').val("");
    $('#identificacion').val("");
    $('#telefono').val("");
    $('#direccion').val("");

    $('#Form_Actualiza').modal('show');
}

function ft_Cerrar() {
    $('#Form_Actualiza').modal('hide');
}





