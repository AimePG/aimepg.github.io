<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_Consulta.aspx.cs" Inherits="Ajax_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../css/Estilos.css" rel="stylesheet" />
    <link href="../Componentes/Bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../Componentes/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Componentes/DataTables/datatables.css" rel="stylesheet" />
    <link href="../Componentes/DataTables/datatables.min.css" rel="stylesheet" />

</head>

<body>
            <h5>ATENCIÓN A CLIENTES</h5>
            <form id="Form_Consulta" runat="server">
                <div class="bottons">
                    <br/>
                     <button type="button" id="btn_Nuevo" class="btn btn-primary" onclick="ft_NuevoCliente()"> Nuevo </button>   
                     <button type="button" id="btn_Buscar" class="btn btn-primary" onclick="ft_BuscarCliente()"> Buscar </button>   
                </div>
                <div class="input-group">
                    <br/>
                    <label class="label">Buscar por nombre completo:</label> 
                    <input type="text" id="txt_Buscar" class="form-control" placeholder="val_Buscar"/>
                </div>
            <div>
                <br/>
                <table id="tbl_Clientes" class="table table-active" cellspacing="0" whith="100%">
                    <thead>
                        <tr>
                            <td></td>
                            <td></td>
                          </tr>
                    </thead>
                </table>
            </div>
            </form>

    <div class="modal fade" id="Form_Actualiza" tabindex="-1" role="dialog" aria-labelledby="Form_ActualizaLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="Form_ActualizaLabel">ACTUALIZAR DATOS DE LOS CLIENTES</h5>
                </div>
                <div class="modal-body">
                    <form id="nuevoForm">
                      <div class="form-group">
                            <label for="nombre">Nombre</label>
                            <input type="text" class="form-control" id="nombre" placeholder="Nombre"/>
                      </div>
                      <div class="form-group">
                            <label for="primerApellido">Primer Apellido</label>
                            <input type="text" class="form-control" id="primerApellido" placeholder="Primer Apellido"/>
                      </div>
                      <div class="form-group">
                            <label for="segundoApellido">Segundo Apellido</label>
                            <input type="text" class="form-control" id="segundoApellido" placeholder="Segundo Apellido"/>
                      </div>
                      <div class="form-group">
                        <label for="identificacion">Identificación</label>
                        <input type="text" class="form-control" id="identificacion" placeholder="Identificación"/>
                      </div>
                      <div class="form-group">
                            <label for="telefono">Teléfono</label>
                            <input type="text" class="form-control" id="telefono" placeholder="Teléfono"/>
                      </div>
                      <div class="form-group">
                        <label for="direccion">Dirección</label>
                        <input type="text" class="form-control" id="direccion" placeholder="Dirección"/>
                      </div>
                      <input type="hidden" id="Id" value="0"/>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="ft_Cerrar()">Cancelar</button>
                    <button type="button" class="btn btn-primary" onclick="ft_GuardarCliente()">Guardar</button>
                </div>
            </div>
        </div>
    </div>


    <script src="../Componentes/JQuery/jquery.min.js"></script>
    <script src="../Componentes/Bootstrap/js/bootstrap.js"></script>
    <script src="../Componentes/Bootstrap/js/bootstrap.min.js"></script>
    <script src="../Componentes/DataTables/datatables.js"></script>
    <script src="../Componentes/DataTables/datatables.min.js"></script>
    <script src="../Componentes/JQuery-confirm/js/jquery-confirm.js"></script>
    <script src="../js/JavaScript.js"></script>


</body>
</html>
