using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Runtime.Remoting.Messaging;
using System.Configuration;
using System.Activities.Expressions;


public partial class Ajax_Default : System.Web.UI.Page
{
    public class Class_Clientes
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Identificacion { get; set; }
    }

    [WebMethod]
    public static string Mostrar_Clientes(string N)
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=sqlclientes;";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            SqlCommand command;
            if (N == "todos") // según el parámetro que recibo ejecuto la función correspondiente si es todos o según criterio de búsqueda por nombre
            {
                command = new SqlCommand("SELECT ID, Nombre, PrimerApellido, SegundoApellido, Identificacion FROM dbo.FTN_CLIENTES_PRUEBA_LISTA_CLIENTES()", con);
            }
            else
            {
                command = new SqlCommand("SELECT ID, Nombre, PrimerApellido, SegundoApellido, Identificacion FROM dbo.FTN_CLIENTES_PRUEBA_LISTA_CLIENTES_POR_NOMBRE(@Nombre)", con);
                command.Parameters.AddWithValue("@Nombre", N);
            }
            {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                    List<Class_Clientes> clientes = new List<Class_Clientes>(); // Lista de clientes que devuelvo para mostrar en la tabla
                    while (reader.Read())
                    {
                        Class_Clientes cliente = new Class_Clientes
                        {
                            Id = reader["ID"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            PrimerApellido = reader["PrimerApellido"].ToString(),
                            SegundoApellido = reader["SegundoApellido"].ToString(),
                            Identificacion = reader["Identificacion"].ToString()
                        };
                        clientes.Add(cliente);
                    }
                    return JsonConvert.SerializeObject(clientes);
                }
            }
        }
    }
    
    [WebMethod]
    public static string Eliminar_Cliente(string IdCliente)
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=sqlclientes;";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            if (!int.TryParse(IdCliente, out int idClienteInt))
            {
                return "Error: ID del cliente no es un número válido.";
            }

            SqlCommand co = new SqlCommand("SELECT * FROM dbo.FTN_CLIENTES_PRUEBA_LISTA_CLIENTES_POR_ID(@ID)", con);
            co.Parameters.AddWithValue("@ID", idClienteInt);
            SqlDataReader re = co.ExecuteReader();
            re.Read();

            using (SqlCommand command = new SqlCommand("STPR_CLIENTES_PRUEBA_MANTENIMIENTO", con))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@P_ID", re["ID"].ToString());
                command.Parameters.AddWithValue("@P_Nombre", re["Nombre"].ToString()); 
                command.Parameters.AddWithValue("@P_Apellido1", re["PrimerApellido"].ToString());
                command.Parameters.AddWithValue("@P_Apellido2", re["SegundoApellido"].ToString());
                command.Parameters.AddWithValue("@P_Identificacion", re["Identificacion"].ToString());
                command.Parameters.AddWithValue("@P_Direccion", re["Direccion"].ToString());
                command.Parameters.AddWithValue("@P_Telefono", re["Telefono"].ToString());
                command.Parameters.AddWithValue("@P_Accion", "B"); // B para Borrar
                SqlParameter outputParam = new SqlParameter("@P_Mensaje", SqlDbType.NVarChar, 50);
                outputParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(outputParam);

                re.Close();

                command.ExecuteNonQuery();
                string mensaje = outputParam.Value.ToString();
                return mensaje;
            }
        }
    }

    [WebMethod]
    public static string Editar_Cliente(Int64 IdCliente) 
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=sqlclientes;";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
             con.Open();
             SqlCommand command;
             command = new SqlCommand("SELECT ID, Nombre, PrimerApellido, SegundoApellido, Identificacion, Telefono, Direccion FROM dbo.FTN_CLIENTES_PRUEBA_LISTA_CLIENTES_POR_ID(@ID)", con);
             command.Parameters.AddWithValue("@ID", IdCliente);

             DataTable dt = new DataTable();
             SqlDataReader reader = command.ExecuteReader();
             dt.Load(reader);
             var response = JsonConvert.SerializeObject(dt);
            Console.WriteLine(response);
            return response;

        }
     }

    [WebMethod]
    public static string Guardar_Cliente(Int64 IdCliente, string nombre, string apellido1, string apellido2, string identificacion, string telefono, string direccion)
    {
        if (nombre == "" || apellido1 == "" || apellido2 == "" || identificacion == "")
        {
            return "¡Existen campos (Nombre - Apellidos - Identificación) que son obligatorios.!";
        }
        else if (identificacion.LongCount() > 10 || telefono.LongCount() > 10)
        {
            return "¡Los campos (Identificación - Teléfono) exceden su longitud (10).!";
        }
        else
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=sqlclientes;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("STPR_CLIENTES_PRUEBA_MANTENIMIENTO", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@P_ID", IdCliente);
                    command.Parameters.AddWithValue("@P_Nombre", nombre);
                    command.Parameters.AddWithValue("@P_Apellido1", apellido1);
                    command.Parameters.AddWithValue("@P_Apellido2", apellido2);
                    command.Parameters.AddWithValue("@P_Identificacion", identificacion);
                    command.Parameters.AddWithValue("@P_Telefono", telefono);
                    command.Parameters.AddWithValue("@P_Direccion", direccion);
                    if (IdCliente == 0)
                    {
                        command.Parameters.AddWithValue("@P_Accion", "I"); // I para Insertar
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@P_Accion", "A"); // A para Actualizar
                    }
                    SqlParameter outputParam = new SqlParameter("@P_Mensaje", SqlDbType.NVarChar, 50);
                    outputParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputParam);

                    command.ExecuteNonQuery();
                    string mensaje = outputParam.Value.ToString();
                    return mensaje;
                }
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
