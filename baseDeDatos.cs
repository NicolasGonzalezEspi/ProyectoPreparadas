using System;
using System.Data;
using System.IO;
using System.Windows;
using MySql.Data.MySqlClient;
using Renci.SshNet;

namespace trabajoFinalInterfaces
{
    internal class BaseDeDatos
    {
        private static readonly string cadenaConexion =
     "server=localhost;port=3306;user=root;password=root;database=northwind;Charset=utf8mb4;";

        private static MySqlConnection conexion = null;
        private static SshClient sshClient;
        private static ForwardedPortLocal portForward;
        private const int puertoLocal = 3307;
        public static int correctos = 0;
        public static int repetidos = 0;
        public static bool situacionRepetidosYCorrectos = false;

        public static bool Conectar()
        {
            try
            {
                if (conexion == null)
                {
                    conexion = new MySqlConnection(cadenaConexion);
                }
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static bool VerificarConexion()
        {
            string consulta = "SELECT 1;";

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    if (!Conectar()) return false;
                }

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    var resultado = cmd.ExecuteScalar();
                    return resultado != null && Convert.ToInt32(resultado) == 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar conexión: {ex.Message}");
                return false;
            }
        }

        public static string ObtenerUltimaActividad()
        {
            string actividad = string.Empty;
            string consulta = "SELECT actividad FROM actividad_usuarios ORDER BY fecha_actividad_usuario DESC LIMIT 1;";

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return actividad;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                object resultado = cmd.ExecuteScalar();
                if (resultado != null && resultado != DBNull.Value)
                {
                    actividad = resultado.ToString();
                }
            }

            return actividad;
        }

        public static void Desconectar()
        {
            if (conexion != null)
            {
                conexion.Close();
                conexion = null;
            }
        }

        public static bool Login(string usuario, string contrasena)
        {
            bool existe = false;
            string consultaUser = "SELECT * FROM usuarios WHERE usuario = @usuario AND contraseña = @contrasena";

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            using (MySqlCommand cmd = new MySqlCommand(consultaUser, conexion))
            {
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@contrasena", contrasena);

                using (MySqlDataReader lector = cmd.ExecuteReader())
                {
                    if (lector.Read()) existe = true;
                }
            }

            return existe;
        }

        public static DataTable MostrarProductos()
        {
            string consulta = @"SELECT p.ProductName AS 'Producto', c.CategoryName AS 'Categoría' 
                                FROM products p 
                                JOIN categories c ON p.CategoryID = c.CategoryID";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        public static DataTable MostrarMKP()
        {
            string consulta = @"select user_login as 'userlogin', user_email as 'useremail', user_nicename as 'usernicename', first_name as 'firstname', last_name as 'lastname', telefono as 'telefono', account_funds as 'accountfunds' from tablamkp;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
        public static DataTable MostrarMKPExportar()
        {
            string consulta = @"select user_login as 'user_login', user_email as 'user_email', user_nicename as 'user_nicename', first_name as 'first_name', last_name as 'last_name', telefono as 'Teléfono', account_funds as 'account_funds' from mkpexportar;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        public static Dictionary<string, string> ObtenerDiccionarioSKUModelo()
        {
            var diccionario = new Dictionary<string, string>();
            string consulta = "SELECT id_producto, modelo FROM productos";

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return diccionario;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string sku = reader.GetString("id_producto");
                    string modelo = reader.IsDBNull(reader.GetOrdinal("modelo")) ? "" : reader.GetString("modelo");
                    diccionario[sku] = modelo;
                }
            }

            return diccionario;
        }


        public static DataTable MostrarTodosPedidos()
        {
            string consulta = @"
SELECT 
    id_pedido_mk,
    DATE_FORMAT(fecha_Pedido, '%d/%m/%Y') AS fecha_pedido,
    nombre_completo,
    direccion,
    codigo_postal,
    ciudad,
    provincia,
    idUsuario,
    nota_envio,
    telefono,
    email,
    SKU,
    proveedor,
    uds,
    nombre_articulo,
    puntosg,
    modelo,
    estado_pedido,
    tracking,
    DATE_FORMAT(fecha_gestion, '%d/%m/%Y') AS fecha_gestion,
    DATE_FORMAT(fecha_inicio_transito, '%d/%m/%Y') AS fecha_inicio_transito,
    DATE_FORMAT(fecha_entrega_alumna, '%d/%m/%Y') AS fecha_entrega_alumna,
    facturadmi_snlosllanos,
    DATE_FORMAT(fecha_factura, '%d/%m/%Y') AS fecha_factura,
    CONCAT(FORMAT(`factura_sin_iva`, 2, 'es_ES'), '€') AS `factura_sin_iva`,
    abono,
    DATE_FORMAT(fecha_abono, '%d/%m/%Y') AS fecha_abono,
    CONCAT(FORMAT(`abono_sin_iva`, 2, 'es_ES'), '€') AS `abono_sin_iva`,
    info_incidencia
FROM pedidos;
    ";
            //    CONCAT(FORMAT(`importe_con_iva`, 2, 'es_ES'), '€') AS `importe_con_iva`,


            //    CONCAT(FORMAT(`factura_sin_iva`, 2, 'es_ES'), '€') AS `factura_sin_iva`,
            //    CONCAT(FORMAT(`abono_sin_iva`, 2, 'es_ES'), '€') AS `abono_sin_iva`,
            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
        public static DataTable MostrarTodosPedidosFactura()
        {

            string consulta = @"SELECT * 
            FROM pedidos 
            WHERE LEFT(facturadmi_snlosllanos, 3) = 'VFR';
            ";
            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
        public static DataTable MostrarTodosPedidosSN()
        {
            string consulta = @"SELECT * 
            FROM pedidos 
            WHERE LEFT(facturadmi_snlosllanos, 2) = 'SN';
            ";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
        public static DataTable MostrarTodosPedidosSinSNoFactura()
        {

            string consulta = @"
        SELECT 
        *
        FROM pedidos where length(facturadmi_snlosllanos)=0;
    ";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }


        public static DataTable MostrarTodosPedidosDMI()
        {
            string consulta = @"
        SELECT *
                FROM pedidos where proveedor='DMI';";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
        public static DataTable MostrarTodosPedidosDMIProcesando()
        {
            string consulta = @"
        SELECT *
                FROM pedidos where proveedor='DMI' && estado_pedido='Procesando';";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        public static DataTable MostrarTodosPedidosSolutia()
        {
            string consulta = @"
        SELECT *
                FROM pedidos where proveedor='Solutia';";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }       
        public static DataTable MostrarTodosPedidosSolutiaProcesando()
        {
            string consulta = @"
        SELECT *
                FROM pedidos where proveedor='Solutia' && estado_pedido='Procesando';";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        public static DataTable MostrarTodosProductos()
        {
            string consulta = @"
                 SELECT *
                FROM productos;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }
            return tabla;
        }

        public static DataTable MostrarPedidosPorId(string idPedido)
        {
            string consulta = @"
    SELECT *
    FROM pedidos
    WHERE id_pedido_mk like @idPedido;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.AddWithValue("@idPedido", "%" + idPedido + "%");


                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        public static DataTable MostrarPedidosPorNombre(string textoBusquedaDireccion)
        {
            string consulta = @"
        SELECT
*
        FROM pedidos
        WHERE nombre_completo LIKE @textoBusqueda;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.AddWithValue("@textoBusqueda", "%" + textoBusquedaDireccion + "%");

                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
        public static DataTable MostrarPedidosPorDireccion(string textoBusquedaDireccion)
        {
            string consulta = @"
        SELECT
*
        FROM pedidos
        WHERE direccion LIKE @textoBusqueda;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.AddWithValue("@textoBusqueda", "%" + textoBusquedaDireccion + "%");

                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        public static DataTable MostrarPedidosPorEmail(string textoBusquedaEmail)
        {
            string consulta = @"
        SELECT
* 
        FROM pedidos
        WHERE email LIKE @textoBusqueda;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.AddWithValue("@textoBusqueda", "%" + textoBusquedaEmail + "%");

                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        public static DataTable MostrarPedidosPorSN(string textoBusquedaEmail)
        {
            string consulta = @"
        SELECT
    *
        FROM pedidos
        WHERE facturadmi_snlosllanos LIKE @textoBusqueda;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.AddWithValue("@textoBusqueda", "%" + textoBusquedaEmail + "%");

                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        public static DataTable MostrarIncidencias()
        {
            string consulta = @"
SELECT
    id_pedido_mk_i AS 'id_pedido_mk_i',
    fecha_Pedido AS 'fecha_Pedido',
    direccion AS 'direccion',
    proveedor AS 'proveedor',
    nombre_articulo AS 'nombre_articulo',
    fecha_notificacion AS 'fecha_notificacion',
    fecha_gestion AS 'fecha_gestion',
    incidencia AS 'incidencia',
    estado AS 'estado',
    solucion AS 'solucion'
FROM
    incidencias;
";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
        public static DataTable MostrarActividadProductos()
        {
            string consulta = @"
SELECT * FROM northwind.actividad_usuarios order by fecha_actividad_usuario desc;
";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
        public static DataTable MostrarActividadProductosReciente()
        {
            string consulta = @"
    SELECT
    fecha_actividad_usuario,
    actividad,
    siguiente_paso
FROM
    northwind.actividad_usuarios
WHERE
    id >= (
        SELECT
            MAX(id)
        FROM
            northwind.actividad_usuarios
        WHERE
            actividad LIKE '1A.%'
    );";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }



        public static DataTable MostrarMKPExportarBis()
        {


            string consulta = @"SELECT user_login AS 'userlogin', user_email AS 'useremail', user_nicename AS 'usernicename', first_name AS 'firstname', last_name AS 'lastname', telefono AS 'telefono', account_funds AS 'accountfunds' FROM mkpexportar ORDER BY user_login LIMIT 100;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        public static void Eliminar100MKPExportar()
        {
            string consulta = "Delete FROM mkpexportar ORDER BY user_login LIMIT 100;";

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    if (!Conectar()) return;
                }

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Registros eliminados: {filasAfectadas}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar registros: {ex.Message}");
            }
            // MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public static void EliminarMKPExportar()
        {
            string consulta = "truncate table mkpexportar;";

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    if (!Conectar()) return;
                }

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Registros eliminados: {filasAfectadas}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar registros: {ex.Message}");
            }
            // MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static bool EliminarBBDDGlobal()
        {
            string consulta = "truncate table bbddglobal;";

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    if (!Conectar()) return false;
                }

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return true;
                    Console.WriteLine($"Registros eliminados: {filasAfectadas}");
                }
            }
            catch (Exception ex)
            {
                return false;
                Console.WriteLine($"Error al eliminar registros: {ex.Message}");
            }
            // MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static DataTable MostrarTablaTemporal()
        {
            string consulta = @"
    SELECT 
        categoria AS 'categoria', 
        convocatoria AS 'convocatoria', 
        codigo_convocatoria AS 'codigoconvocatoria', 
        nombre AS 'nombre', 
        apellidos AS 'apellidos', 
        fecha_solicitud AS 'fecha_solicitud', 
        estado_inscripcion AS 'estado_inscripcion', 
        tipo_inscripcion AS 'tipo_inscripcion', 
        estado_matriculacion AS 'estado_matriculacion', 
        email AS 'email', 
        telefono AS 'telefono', 
        nivel_estudios AS 'nivel_estudios', 
        sexo AS 'sexo', 
        provincia AS 'provincia', 
        localidad AS 'localidad', 
        fecha_nacimiento AS 'fecha_nacimiento', 
        dni AS 'dni', 
        situacion_laboral AS 'situacion_laboral', 
        asistencia_remota AS 'asistencia_remota', 
        tablet AS 'tablet', 
        puntos AS 'puntos' 
    FROM tablatemporal ORDER BY localidad;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
        public static DataTable MostrarTablaTemporalB()
        {
            string consulta = @"
    SELECT 
        codigo_convocatoria AS 'codigoconvocatoria', 
        nombre AS 'nombre', 
        apellidos AS 'apellidos', 
        email AS 'email', 
        telefono AS 'telefono', 
        localidad AS 'localidad', 
        fecha_nacimiento AS 'fecha_nacimiento', 
        dni AS 'dni', 
        tablet AS 'tablet', 
        puntos AS 'puntos' 
    FROM tablatemporal ORDER BY localidad;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }



        public static DataTable MostrarBBDDGlobal()
        {
            string consulta = @"
        SELECT 
            categoriag AS 'categoriag', 
            convocatoriag AS 'convocatoriag', 
            codigo_convocatoriag AS 'codigoconvocatoriag', 
            provinciag AS 'provinciag', 
            localidadg AS 'localidadg', 
            nombreg AS 'nombreg', 
            apellidosg AS 'apellidosg', 
            fecha_solicitudg AS 'fechasolicitudg', 
            estado_inscripciong AS 'estadoinscripciong', 
            tipo_inscripciong AS 'tipoinscripciong', 
            estado_matriculaciong AS 'estadomatriculaciong', 
            emailg AS 'emailg', 
            telefonog AS 'telefonog', 
            nivel_estudiosg AS 'nivelestudiosg', 
            sexog AS 'sexog', 
            fecha_nacimientog AS 'fechanacimientog', 
            dnig AS 'dnig', 
            situacion_laboralg AS 'situacionlaboralg', 
            asistencia_remotag AS 'asistenciaremotag', 
            tabletg AS 'tabletg',
            puntosg AS 'puntosg',
            fecha_importacion AS 'fechaimportacion', 
            observaciones AS 'observaciones'
            FROM bbddglobal;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        public static DataTable MostrarBBDDGlobalFiltroDNI(string DNI)
        {
            string consulta = @"
                SELECT 
                    categoriag AS 'categoriag', 
                    convocatoriag AS 'convocatoriag', 
                    codigo_convocatoriag AS 'codigoconvocatoriag', 
                    provinciag AS 'provinciag', 
                    localidadg AS 'localidadg', 
                    nombreg AS 'nombreg', 
                    apellidosg AS 'apellidosg', 
                    fecha_solicitudg AS 'fechasolicitudg', 
                    estado_inscripciong AS 'estadoinscripciong', 
                    tipo_inscripciong AS 'tipoinscripciong', 
                    estado_matriculaciong AS 'estadomatriculaciong', 
                    emailg AS 'emailg', 
                    telefonog AS 'telefonog', 
                    nivel_estudiosg AS 'nivelestudiosg', 
                    sexog AS 'sexog', 
                    fecha_nacimientog AS 'fechanacimientog', 
                    dnig AS 'dnig', 
                    situacion_laboralg AS 'situacionlaboralg', 
                    asistencia_remotag AS 'asistenciaremotag', 
                    tabletg AS 'tabletg',
                    puntosg AS 'puntosg',
                    fecha_importacion AS 'fechaimportacion', 
                    observaciones AS 'observaciones'
                FROM bbddglobal 
                WHERE dnig = @DNI;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.AddWithValue("@DNI", DNI);

                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }


     
        public static DataTable MostrarBBDDGlobalFiltroCadiz()
        {
            string consulta = @"
                SELECT 
                    categoriag AS 'categoriag', 
                    convocatoriag AS 'convocatoriag', 
                    codigo_convocatoriag AS 'codigoconvocatoriag', 
                    provinciag AS 'provinciag', 
                    localidadg AS 'localidadg', 
                    nombreg AS 'nombreg', 
                    apellidosg AS 'apellidosg', 
                    fecha_solicitudg AS 'fechasolicitudg', 
                    estado_inscripciong AS 'estadoinscripciong', 
                    tipo_inscripciong AS 'tipoinscripciong', 
                    estado_matriculaciong AS 'estadomatriculaciong', 
                    emailg AS 'emailg', 
                    telefonog AS 'telefonog', 
                    nivel_estudiosg AS 'nivelestudiosg', 
                    sexog AS 'sexog', 
                    fecha_nacimientog AS 'fechanacimientog', 
                    dnig AS 'dnig', 
                    situacion_laboralg AS 'situacionlaboralg', 
                    asistencia_remotag AS 'asistenciaremotag', 
                    tabletg AS 'tabletg',
                    puntosg AS 'puntosg',
                    fecha_importacion AS 'fechaimportacion', 
                    observaciones AS 'observaciones'
                FROM bbddglobal 
                WHERE provinciag = 'Cadiz';";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {

                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
        public static DataTable MostrarBBDDGlobalFiltroMalaga()
        {
            string consulta = @"
                SELECT 
                    categoriag AS 'categoriag', 
                    convocatoriag AS 'convocatoriag', 
                    codigo_convocatoriag AS 'codigoconvocatoriag', 
                    provinciag AS 'provinciag', 
                    localidadg AS 'localidadg', 
                    nombreg AS 'nombreg', 
                    apellidosg AS 'apellidosg', 
                    fecha_solicitudg AS 'fechasolicitudg', 
                    estado_inscripciong AS 'estadoinscripciong', 
                    tipo_inscripciong AS 'tipoinscripciong', 
                    estado_matriculaciong AS 'estadomatriculaciong', 
                    emailg AS 'emailg', 
                    telefonog AS 'telefonog', 
                    nivel_estudiosg AS 'nivelestudiosg', 
                    sexog AS 'sexog', 
                    fecha_nacimientog AS 'fechanacimientog', 
                    dnig AS 'dnig', 
                    situacion_laboralg AS 'situacionlaboralg', 
                    asistencia_remotag AS 'asistenciaremotag', 
                    tabletg AS 'tabletg',
                    puntosg AS 'puntosg',
                    fecha_importacion AS 'fechaimportacion', 
                    observaciones AS 'observaciones'
                FROM bbddglobal 
                WHERE provinciag = 'Málaga';";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {

                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }

        public static bool ActualizarIncidencia(string idPedido, string incidencia)
        {
            string consulta = "UPDATE incidencias SET incidencia = @incidencia WHERE id_pedido_mk_i = @idPedido";

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    if (!Conectar()) return false;
                }

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@incidencia", incidencia);
                    cmd.Parameters.AddWithValue("@idPedido", idPedido);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar incidencia: {ex.Message}");
                return false;
            }
        }


        public static DataTable MostrarBBDDGlobalFiltroEmail(string email)
        {
            string consulta = @"
        SELECT 
            categoriag AS 'categoriag', 
            convocatoriag AS 'convocatoriag', 
            codigo_convocatoriag AS 'codigoconvocatoriag', 
            provinciag AS 'provinciag', 
            localidadg AS 'localidadg', 
            nombreg AS 'nombreg', 
            apellidosg AS 'apellidosg', 
            fecha_solicitudg AS 'fechasolicitudg', 
            estado_inscripciong AS 'estadoinscripciong', 
            tipo_inscripciong AS 'tipoinscripciong', 
            estado_matriculaciong AS 'estadomatriculaciong', 
            emailg AS 'emailg', 
            telefonog AS 'telefonog', 
            nivel_estudiosg AS 'nivelestudiosg', 
            sexog AS 'sexog', 
            fecha_nacimientog AS 'fechanacimientog', 
            dnig AS 'dnig', 
            situacion_laboralg AS 'situacionlaboralg', 
            asistencia_remotag AS 'asistenciaremotag', 
            tabletg AS 'tabletg',
            puntosg AS 'puntosg',
            fecha_importacion AS 'fechaimportacion', 
            observaciones AS 'observaciones'
        FROM bbddglobal 
        WHERE emailg = @email;";

            DataTable tabla = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return tabla;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.AddWithValue("@email", email); // ✅ Corrección: ahora el parámetro se llama correctamente "email"

                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(tabla);
                }
            }

            return tabla;
        }
        public static bool ActualizarObservacion(
     string codigoConvocatoria,
     string dni,
     string observacion,
     string nombreColumnaCodigo = "codigo_convocatoriag",
     string nombreColumnaDni = "dnig")
        {
            string consulta = $"UPDATE bbddglobal SET observaciones = @observaciones " +
                             $"WHERE {nombreColumnaCodigo} = @codigo AND {nombreColumnaDni} = @dni";

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    if (!Conectar()) return false;
                }

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@observaciones", observacion);
                    cmd.Parameters.AddWithValue("@codigo", codigoConvocatoria);
                    cmd.Parameters.AddWithValue("@dni", dni);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar observación: {ex.Message}");
                return false;
            }
        }
        public static void eliminarVaciosTemporal()
        {
            string consulta = "DELETE FROM tablatemporal WHERE codigo_convocatoria = '' OR codigo_convocatoria IS NULL OR dni = '' OR dni IS NULL;";

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    if (!Conectar()) return;
                }

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Registros eliminados: {filasAfectadas}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar registros: {ex.Message}");
            }
        }

        public static void EliminarVaciosMKPExportar()
        {
            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    if (!Conectar()) return;
                }

                // Desactiva safe updates
                using (MySqlCommand cmdOff = new MySqlCommand("SET SQL_SAFE_UPDATES = 0;", conexion))
                {
                    cmdOff.ExecuteNonQuery();
                }

                // Ejecuta el DELETE seguro con PRIMARY KEY en WHERE
                string consultaDelete = "DELETE FROM mkpexportar WHERE (account_funds = 0 OR account_funds IS NULL) AND user_login IS NOT NULL;";
                int filasAfectadas = 0;

                using (MySqlCommand cmdDelete = new MySqlCommand(consultaDelete, conexion))
                {
                    filasAfectadas = cmdDelete.ExecuteNonQuery();
                }

                Console.WriteLine($"Registros eliminados: {filasAfectadas}");

                // Reactiva safe updates
                using (MySqlCommand cmdOn = new MySqlCommand("SET SQL_SAFE_UPDATES = 1;", conexion))
                {
                    cmdOn.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar registros: {ex.Message}");
            }
        }

        public static void eliminarVaciosPedidos()
        {
            string consulta = "DELETE FROM tablatemporal WHERE codigo_convocatoria = '' OR codigo_convocatoria IS NULL OR dni = '' OR dni IS NULL;";

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    if (!Conectar()) return;
                }

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Registros eliminados: {filasAfectadas}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar registros: {ex.Message}");
            }
        }


        public static DataTable ObtenerCategorias()
        {
            string consulta = "SELECT CategoryID AS 'ID', CategoryName AS 'Categoría' FROM categories";
            DataTable categorias = new DataTable();

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return categorias;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                using (MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd))
                {
                    adaptador.Fill(categorias);
                }
            }

            return categorias;
        }

        public static bool AgregarProducto(string nombreProducto, int idCategoria)
        {
            string consulta = "INSERT INTO products (ProductName, CategoryID) VALUES (@nombre, @categoria)";

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.AddWithValue("@nombre", nombreProducto);
                cmd.Parameters.AddWithValue("@categoria", idCategoria);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }


        public static bool EjecutarQueryIncidencia(string consulta, Dictionary<string, object> parametros)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                foreach (var parametro in parametros)
                {
                    cmd.Parameters.AddWithValue(parametro.Key, parametro.Value);
                }

                try
                {
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al ejecutar la consulta: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        public static bool ActualizarTracking(string numeroPedido, string trackingRecortado)
        {
            string consulta = "UPDATE pedidos SET tracking = @tracking, fecha_inicio_transito = @fechaTransito WHERE id_pedido_mk = @numeroPedido";
            var parametros = new Dictionary<string, object>
    {
        { "@tracking", trackingRecortado },
        { "@fechaTransito", DateTime.Now.ToString("yyyy-MM-dd") }, // Formato MySQL
        { "@numeroPedido", numeroPedido }
    };

            return EjecutarQueryIncidencia(consulta, parametros);
        }


        public static bool InsertarIncidenciaDuplicada(
    string idOriginal,
    string fechaPedido,
    string direccion,
    string proveedor,
    string nombreArticulo,
    string fechaNotificacion,
    string fechaGestion,
    string incidencia,
    string estado,
    string solucion)
        {
            string nuevoId = idOriginal + "R";
            string consulta = "INSERT INTO incidencias (id_pedido_mk_i, fecha_Pedido, direccion, proveedor, nombre_articulo, fecha_notificacion, fecha_gestion, incidencia, estado, solucion) " +
                              "VALUES (@id, @fechaPedido, @direccion, @proveedor, @nombreArticulo, @fechaNotificacion, @fechaGestion, @incidencia, @estado, @solucion)";

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    if (!Conectar()) return false;
                }

                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", nuevoId);
                    cmd.Parameters.AddWithValue("@fechaPedido", fechaPedido);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.Parameters.AddWithValue("@proveedor", proveedor);
                    cmd.Parameters.AddWithValue("@nombreArticulo", nombreArticulo);
                    cmd.Parameters.AddWithValue("@fechaNotificacion", fechaNotificacion);
                    cmd.Parameters.AddWithValue("@fechaGestion", fechaGestion);
                    cmd.Parameters.AddWithValue("@incidencia", incidencia);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@solucion", solucion);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar incidencia duplicada: {ex.Message}");
                return false;
            }
        }


        public static bool EjecutarUpdateIncidencia(string consulta)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
 

                try
                {
                   cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al ejecutar la consulta: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        public static bool CargarDatosGraficas()
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            string consulta = @"
        SELECT localidadg, COUNT(*) 
        FROM bbddglobal 
        GROUP BY localidadg 
        ORDER BY COUNT(*) DESC 
        LIMIT 5;
    ";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        string ciudad = reader.GetString(0);
                        int cantidad = reader.GetInt32(1);

                        DatosEstadisticos.DatosPorCiudad[ciudad] = cantidad;
                    }
                }





        
            }
            catch (Exception ex)
            {
                // Puedes loguear el error o mostrarlo
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
                return false;
            }


             consulta = @"
select nombre_articulo, count(*) from pedidos group by nombre_articulo limit 4;
    ";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        string pedidos = reader.GetString(0);
                        int cantidad = reader.GetInt32(1);

                        DatosEstadisticos.PedidosPorProducto[pedidos] = cantidad;
                    }
                }

         
            }
            catch (Exception ex)
            {
                // Puedes loguear el error o mostrarlo
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
                return false;
            }


            consulta = @"
SELECT CONCAT('Año: ', anio) AS year_fecha_nacimiento, COUNT(*) AS conteo
FROM (
    SELECT YEAR(fecha_nacimientog) AS anio
    FROM northwind.bbddglobal
    WHERE fecha_nacimientog IS NOT NULL
) AS sub
GROUP BY anio
ORDER BY anio ASC
LIMIT 1000;
    ";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        string pedidos = reader.GetString(0);
                        int cantidad = reader.GetInt32(1);

                        DatosEstadisticos.ClientesPorEdad[pedidos] = cantidad;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Puedes loguear el error o mostrarlo
                MessageBox.Show("Error al cargar los datosss: " + ex.Message);
                return false;
            }


        }


        public static void UnirYSumar()
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return;
            }

            string consulta = @"
        INSERT INTO mkpexportar (user_login, user_email, user_nicename, first_name, last_name, Telefono, account_funds)
        SELECT 
            user_login, 
            MAX(user_email) AS user_email, 
            MAX(user_nicename) AS user_nicename, 
            MAX(first_name) AS first_name, 
            MAX(last_name) AS last_name, 
            MAX(Telefono) AS Telefono, 
            SUM(account_funds) AS account_funds
        FROM tablamkp
        GROUP BY user_login
        ON DUPLICATE KEY UPDATE 
            user_email = VALUES(user_email),
            user_nicename = VALUES(user_nicename),
            first_name = VALUES(first_name),
            last_name = VALUES(last_name),
            Telefono = VALUES(Telefono),
            account_funds = VALUES(account_funds);
    ";

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.ExecuteNonQuery();
            }
          /*  string eliminarDetallesQuery = "TRUNCATE TABLE tablamkp";
            using (MySqlCommand cmd = new MySqlCommand(eliminarDetallesQuery, conexion))
            {
                cmd.ExecuteNonQuery();
            }*/
            MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

        }



        public static bool InsertarDatos(DataTable datos)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            string consulta = @"INSERT INTO tablatemporal 
    (categoria, convocatoria, codigo_convocatoria, provincia, localidad, nombre, apellidos, 
    fecha_solicitud, estado_inscripcion, tipo_inscripcion, estado_matriculacion, email, telefono, 
    nivel_estudios, sexo, fecha_nacimiento, dni, situacion_laboral, asistencia_remota, tablet, puntos) 
    VALUES (@categoria, @convocatoria, @codigo_convocatoria, @provincia, @localidad, @nombre, @apellidos, 
    @fecha_solicitud, @estado_inscripcion, @tipo_inscripcion, @estado_matriculacion, @email, @telefono, 
    @nivel_estudios, @sexo, @fecha_nacimiento, @dni, @situacion_laboral, @asistencia_remota, @tablet, @puntos)
    ON DUPLICATE KEY UPDATE 
    categoria = VALUES(categoria), convocatoria = VALUES(convocatoria), provincia = VALUES(provincia),
    localidad = VALUES(localidad), nombre = VALUES(nombre), apellidos = VALUES(apellidos), 
    fecha_solicitud = VALUES(fecha_solicitud), estado_inscripcion = VALUES(estado_inscripcion), 
    tipo_inscripcion = VALUES(tipo_inscripcion), estado_matriculacion = VALUES(estado_matriculacion), 
    email = VALUES(email), telefono = VALUES(telefono), nivel_estudios = VALUES(nivel_estudios), 
    sexo = VALUES(sexo), fecha_nacimiento = VALUES(fecha_nacimiento), dni = VALUES(dni), 
    situacion_laboral = VALUES(situacion_laboral), asistencia_remota = VALUES(asistencia_remota), 
    tablet = VALUES(tablet), puntos = VALUES(puntos);";


            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.Add("@categoria", MySqlDbType.VarChar);
                cmd.Parameters.Add("@convocatoria", MySqlDbType.VarChar);
                cmd.Parameters.Add("@codigo_convocatoria", MySqlDbType.VarChar);
                cmd.Parameters.Add("@provincia", MySqlDbType.VarChar);
                cmd.Parameters.Add("@localidad", MySqlDbType.VarChar);
                cmd.Parameters.Add("@nombre", MySqlDbType.VarChar);
                cmd.Parameters.Add("@apellidos", MySqlDbType.VarChar);
                cmd.Parameters.Add("@fecha_solicitud", MySqlDbType.VarChar);
                cmd.Parameters.Add("@estado_inscripcion", MySqlDbType.VarChar);
                cmd.Parameters.Add("@tipo_inscripcion", MySqlDbType.VarChar);
                cmd.Parameters.Add("@estado_matriculacion", MySqlDbType.VarChar);

                cmd.Parameters.Add("@email", MySqlDbType.VarChar);
                cmd.Parameters.Add("@telefono", MySqlDbType.VarChar);
                cmd.Parameters.Add("@nivel_estudios", MySqlDbType.VarChar);
                cmd.Parameters.Add("@sexo", MySqlDbType.VarChar);
                cmd.Parameters.Add("@fecha_nacimiento", MySqlDbType.VarChar);

                cmd.Parameters.Add("@dni", MySqlDbType.VarChar);
                cmd.Parameters.Add("@situacion_laboral", MySqlDbType.VarChar);
                cmd.Parameters.Add("@asistencia_remota", MySqlDbType.VarChar);
                cmd.Parameters.Add("@tablet", MySqlDbType.VarChar);
                cmd.Parameters.Add("@puntos", MySqlDbType.Int32);


                int filasInsertadas = 0;

                foreach (DataRow fila in datos.Rows)
                {
                    cmd.Parameters["@categoria"].Value = fila["categoria"];
                    cmd.Parameters["@convocatoria"].Value = fila["convocatoria"];
                    cmd.Parameters["@codigo_convocatoria"].Value = fila["codigo_convocatoria"];
                    cmd.Parameters["@provincia"].Value = fila["provincia"];
                    cmd.Parameters["@localidad"].Value = fila["localidad"];
                    cmd.Parameters["@nombre"].Value = fila["nombre"];
                    cmd.Parameters["@apellidos"].Value = fila["apellidos"];
                    cmd.Parameters["@fecha_solicitud"].Value = fila["fecha_solicitud"];
                    cmd.Parameters["@estado_inscripcion"].Value = fila["estado_inscripcion"];
                    cmd.Parameters["@tipo_inscripcion"].Value = fila["tipo_inscripcion"];
                    cmd.Parameters["@estado_matriculacion"].Value = fila["estado_matriculacion"];
                    cmd.Parameters["@email"].Value = fila["email"];
                    cmd.Parameters["@telefono"].Value = fila["telefono"];
                    cmd.Parameters["@nivel_estudios"].Value = fila["nivel_estudios"];
                    cmd.Parameters["@sexo"].Value = fila["sexo"];
                    cmd.Parameters["@fecha_nacimiento"].Value = fila["fecha_nacimiento"];
                    cmd.Parameters["@dni"].Value = fila["dni"];
                    cmd.Parameters["@situacion_laboral"].Value = fila["situacion_laboral"];
                    cmd.Parameters["@asistencia_remota"].Value = fila["asistencia_remota"];
                    cmd.Parameters["@tablet"].Value = fila["tablet"];
                    cmd.Parameters["@puntos"].Value = fila["puntos"];

                    filasInsertadas += cmd.ExecuteNonQuery();
                }

                return filasInsertadas > 0;
            }
        }

        public static bool InsertarDatosCSVbis(DataTable datos)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            using (MySqlTransaction transaccion = conexion.BeginTransaction())
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conexion;
                cmd.Transaction = transaccion;

                cmd.CommandText = @"
INSERT INTO tablamkp 
(user_login, user_email, user_nicename, first_name, last_name, Telefono, account_funds, dni, codigo_convocatoria) 
VALUES 
(@user_login, @user_email, @user_nicename, @first_name, @last_name, @Telefono, @account_funds, @dni, @codigo_convocatoria) 
ON DUPLICATE KEY UPDATE 
user_email = VALUES(user_email), 
user_nicename = VALUES(user_nicename), 
first_name = VALUES(first_name), 
last_name = VALUES(last_name), 
Telefono = VALUES(Telefono), 
account_funds = VALUES(account_funds);";

                // Parámetros
                cmd.Parameters.Add("@user_login", MySqlDbType.VarChar);
                cmd.Parameters.Add("@user_email", MySqlDbType.VarChar);
                cmd.Parameters.Add("@user_nicename", MySqlDbType.VarChar);
                cmd.Parameters.Add("@first_name", MySqlDbType.VarChar);
                cmd.Parameters.Add("@last_name", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Telefono", MySqlDbType.VarChar);
                cmd.Parameters.Add("@account_funds", MySqlDbType.Int32);
                cmd.Parameters.Add("@dni", MySqlDbType.VarChar);
                cmd.Parameters.Add("@codigo_convocatoria", MySqlDbType.VarChar);

                try
                {
                    int filasInsertadas = 0;

                    foreach (DataRow fila in datos.Rows)
                    {
                        cmd.Parameters["@user_login"].Value = fila["user_login"];
                        cmd.Parameters["@user_email"].Value = fila["user_email"];
                        cmd.Parameters["@user_nicename"].Value = fila["user_nicename"];
                        cmd.Parameters["@first_name"].Value = fila["first_name"];
                        cmd.Parameters["@last_name"].Value = fila["last_name"];
                        cmd.Parameters["@Telefono"].Value = fila["Telefono"];
                        cmd.Parameters["@account_funds"].Value = fila["account_funds"];
                        cmd.Parameters["@dni"].Value = fila["dni"];
                        cmd.Parameters["@codigo_convocatoria"].Value = fila["codigo_convocatoria"];

                        filasInsertadas += cmd.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                    return filasInsertadas > 0;
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    MessageBox.Show($"Error al insertar datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        public static bool ActualizarSNenBD(string numeroPedido, string snFormateado)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            string consulta = @"UPDATE pedidos 
                        SET facturadmi_snlosllanos = @sn 
                        WHERE id_pedido_mk = @pedido";

            var parametros = new Dictionary<string, object>
    {
        { "@sn", snFormateado },
        { "@pedido", numeroPedido }
    };

            return EjecutarQueryIncidencia(consulta, parametros);
        }



        public static bool InsertarDatosCSVBackup(DataTable datos)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            string consulta = @"INSERT INTO bbddglobal
(categoriag, convocatoriag, codigo_convocatoriag, provinciag, localidadg, nombreg, apellidosg, fecha_solicitudg, estado_inscripciong, tipo_inscripciong, estado_matriculaciong, emailg, telefonog, nivel_estudiosg, sexog, fecha_nacimientog, dnig, situacion_laboralg, asistencia_remotag, tabletg, puntosg, fecha_importacion, observaciones)
VALUES (@categoriag, @convocatoriag, @codigo_convocatoriag, @provinciag, @localidadg, @nombreg, @apellidosg, @fecha_solicitudg, @estado_inscripciong, @tipo_inscripciong, @estado_matriculaciong, @emailg, @telefonog, @nivel_estudiosg, @sexog, @fecha_nacimientog, @dnig, @situacion_laboralg, @asistencia_remotag, @tabletg, @puntosg, @fecha_importacion, @observaciones)
ON DUPLICATE KEY UPDATE
categoriag = VALUES(categoriag),
convocatoriag = VALUES(convocatoriag),
provinciag = VALUES(provinciag),
localidadg = VALUES(localidadg),
nombreg = VALUES(nombreg),
apellidosg = VALUES(apellidosg),
fecha_solicitudg = VALUES(fecha_solicitudg),
estado_inscripciong = VALUES(estado_inscripciong),
tipo_inscripciong = VALUES(tipo_inscripciong),
estado_matriculaciong = VALUES(estado_matriculaciong),
emailg = VALUES(emailg),
telefonog = VALUES(telefonog),
nivel_estudiosg = VALUES(nivel_estudiosg),
sexog = VALUES(sexog),
fecha_nacimientog = VALUES(fecha_nacimientog),
situacion_laboralg = VALUES(situacion_laboralg),
asistencia_remotag = VALUES(asistencia_remotag),
tabletg = VALUES(tabletg),
puntosg = VALUES(puntosg),
fecha_importacion = VALUES(fecha_importacion),
observaciones = VALUES(observaciones);";

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                foreach (DataColumn col in datos.Columns)
                {
                    cmd.Parameters.Add($"@{col.ColumnName}", GetMySqlDbType(col.DataType));
                }

                int filasInsertadas = 0;
                foreach (DataRow fila in datos.Rows)
                {
                    foreach (DataColumn col in datos.Columns)
                    {
                        cmd.Parameters[$"@{col.ColumnName}"].Value = fila[col.ColumnName] ?? DBNull.Value;
                    }
                    filasInsertadas += cmd.ExecuteNonQuery();
                }
                return filasInsertadas > 0;
            }
        }
        private static MySqlDbType GetMySqlDbType(Type type)
        {
            if (type == typeof(string)) return MySqlDbType.VarChar;
            if (type == typeof(int)) return MySqlDbType.Int32;
            // Añade más tipos según sea necesario
            return MySqlDbType.VarChar; // Tipo por defecto
        }

        //todo bbdd insert pedidos
        public static bool InsertarDatosCSVPedidos(DataTable datos)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            using (MySqlTransaction transaccion = conexion.BeginTransaction())
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conexion;
                cmd.Transaction = transaccion;

                string consulta = @"INSERT INTO pedidos 
(id_pedido_mk, fecha_Pedido, nombre_completo, direccion, codigo_postal, ciudad, provincia, idUsuario, nota_envio, telefono, email, SKU, proveedor, uds, nombre_articulo, puntosg, modelo, estado_pedido, tracking, fecha_gestion, fecha_inicio_transito, fecha_entrega_alumna, facturadmi_snlosllanos, fecha_factura, factura_sin_iva, abono, fecha_abono, abono_sin_iva, info_incidencia) 
VALUES 
(@id_pedido_mk, @fecha_Pedido, @nombre_completo, @direccion, @codigo_postal, @ciudad, @provincia, @idUsuario, @nota_envio, @telefono, @email, @SKU, @proveedor, @uds, @nombre_articulo, @puntosg, @modelo, @estado_pedido, @tracking, @fecha_gestion, @fecha_inicio_transito, @fecha_entrega_alumna, @facturadmi_snlosllanos, @fecha_factura, @factura_sin_iva, @abono, @fecha_abono, @abono_sin_iva, @info_incidencia)";

                cmd.CommandText = consulta;

                // Agregar parámetros
                string[] columnas = {
            "id_pedido_mk", "fecha_Pedido", "nombre_completo", "direccion", "codigo_postal", "ciudad", "provincia",
            "idUsuario", "nota_envio", "telefono", "email", "SKU", "proveedor", "uds", "nombre_articulo",
            "puntosg", "modelo", "estado_pedido", "tracking", "fecha_gestion", "fecha_inicio_transito",
            "fecha_entrega_alumna", "facturadmi_snlosllanos", "fecha_factura", "factura_sin_iva", "abono",
            "fecha_abono", "abono_sin_iva", "info_incidencia"
        };

                foreach (var columna in columnas)
                {
                    cmd.Parameters.Add("@" + columna, columna == "puntosg" ? MySqlDbType.Int32 : MySqlDbType.VarChar);
                }

                try
                {
                    int filasInsertadas = 0;

                    foreach (DataRow fila in datos.Rows)
                    {
                        foreach (var columna in columnas)
                        {
                            cmd.Parameters["@" + columna].Value = fila[columna];
                        }

                        filasInsertadas += cmd.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                    return filasInsertadas > 0;
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    MessageBox.Show($"Error al insertar datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        public static bool InsertarDatosCSVPedidos2(DataTable datos)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            using (MySqlTransaction transaccion = conexion.BeginTransaction())
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conexion;
                cmd.Transaction = transaccion;

                string consulta = @"INSERT INTO pedidos 
(id_pedido_mk, fecha_Pedido, nombre_completo, direccion, codigo_postal, ciudad, provincia, idUsuario, nota_envio, telefono, email, SKU, proveedor, uds, nombre_articulo, puntosg, modelo, estado_pedido, tracking, fecha_gestion, fecha_inicio_transito, fecha_entrega_alumna, facturadmi_snlosllanos, fecha_factura, factura_sin_iva, abono, fecha_abono, abono_sin_iva, info_incidencia) 
VALUES 
(@id_pedido_mk, @fecha_Pedido, @nombre_completo, @direccion, @codigo_postal, @ciudad, @provincia, @idUsuario, @nota_envio, @telefono, @email, @SKU, @proveedor, @uds, @nombre_articulo, @puntosg, @modelo, @estado_pedido, @tracking, @fecha_gestion, @fecha_inicio_transito, @fecha_entrega_alumna, @facturadmi_snlosllanos, @fecha_factura, @factura_sin_iva, @abono, @fecha_abono, @abono_sin_iva, @info_incidencia)";

                cmd.CommandText = consulta;

                string[] columnas = {
            "id_pedido_mk", "fecha_Pedido", "nombre_completo", "direccion", "codigo_postal", "ciudad", "provincia",
            "idUsuario", "nota_envio", "telefono", "email", "SKU", "proveedor", "uds", "nombre_articulo",
            "puntosg", "modelo", "estado_pedido", "tracking", "fecha_gestion", "fecha_inicio_transito",
            "fecha_entrega_alumna", "facturadmi_snlosllanos", "fecha_factura", "factura_sin_iva", "abono",
            "fecha_abono", "abono_sin_iva", "info_incidencia"
        };

                foreach (var columna in columnas)
                {
                    cmd.Parameters.Add("@" + columna, columna == "puntosg" ? MySqlDbType.Int32 : MySqlDbType.VarChar);
                }

                correctos = 0;
                repetidos = 0;

                foreach (DataRow fila in datos.Rows)
                {
                    try
                    {
                        foreach (var columna in columnas)
                        {
                            cmd.Parameters["@" + columna].Value = fila[columna];
                        }

                        cmd.ExecuteNonQuery();
                        correctos++;
                    }
                    catch (MySqlException ex)
                    {
                        if (ex.Number == 1062) // Duplicado
                        {
                            repetidos++;
                            continue;
                        }
                        else
                        {
                            transaccion.Rollback();
                            MessageBox.Show($"Error al insertar fila: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                }

                transaccion.Commit();

                // Marcar si hubo tanto correctos como repetidos
                situacionRepetidosYCorrectos = (correctos > 0 && repetidos > 0);

                return correctos > 0;
            }
        }



        public static bool InsertarDatosCSV(DataTable datos)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            using (MySqlTransaction transaccion = conexion.BeginTransaction())  // Iniciar transacción
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = conexion;
                cmd.Transaction = transaccion;

                string consulta = @"INSERT INTO tablatemporal 
        (categoria, convocatoria, codigo_convocatoria, provincia, localidad, nombre, apellidos, 
        fecha_solicitud, estado_inscripcion, tipo_inscripcion, estado_matriculacion, email, telefono, 
        nivel_estudios, sexo, fecha_nacimiento, dni, situacion_laboral, asistencia_remota, tablet, puntos) 
        VALUES (@categoria, @convocatoria, @codigo_convocatoria, @provincia, @localidad, @nombre, @apellidos, 
        @fecha_solicitud, @estado_inscripcion, @tipo_inscripcion, @estado_matriculacion, @email, @telefono, 
        @nivel_estudios, @sexo, @fecha_nacimiento, @dni, @situacion_laboral, @asistencia_remota, @tablet, @puntos)
        ON DUPLICATE KEY UPDATE 
        categoria = VALUES(categoria), convocatoria = VALUES(convocatoria), provincia = VALUES(provincia),
        localidad = VALUES(localidad), nombre = VALUES(nombre), apellidos = VALUES(apellidos), 
        fecha_solicitud = VALUES(fecha_solicitud), estado_inscripcion = VALUES(estado_inscripcion), 
        tipo_inscripcion = VALUES(tipo_inscripcion), estado_matriculacion = VALUES(estado_matriculacion), 
        email = VALUES(email), telefono = VALUES(telefono), nivel_estudios = VALUES(nivel_estudios), 
        sexo = VALUES(sexo), fecha_nacimiento = VALUES(fecha_nacimiento), dni = VALUES(dni), 
        situacion_laboral = VALUES(situacion_laboral), asistencia_remota = VALUES(asistencia_remota), 
        tablet = VALUES(tablet), puntos = VALUES(puntos);";

                cmd.CommandText = consulta;

                // Agregar parámetros
                cmd.Parameters.Add("@categoria", MySqlDbType.VarChar);
                cmd.Parameters.Add("@convocatoria", MySqlDbType.VarChar);
                cmd.Parameters.Add("@codigo_convocatoria", MySqlDbType.VarChar);
                cmd.Parameters.Add("@provincia", MySqlDbType.VarChar);
                cmd.Parameters.Add("@localidad", MySqlDbType.VarChar);
                cmd.Parameters.Add("@nombre", MySqlDbType.VarChar);
                cmd.Parameters.Add("@apellidos", MySqlDbType.VarChar);
                cmd.Parameters.Add("@fecha_solicitud", MySqlDbType.VarChar);
                cmd.Parameters.Add("@estado_inscripcion", MySqlDbType.VarChar);
                cmd.Parameters.Add("@tipo_inscripcion", MySqlDbType.VarChar);
                cmd.Parameters.Add("@estado_matriculacion", MySqlDbType.VarChar);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar);
                cmd.Parameters.Add("@telefono", MySqlDbType.VarChar);
                cmd.Parameters.Add("@nivel_estudios", MySqlDbType.VarChar);
                cmd.Parameters.Add("@sexo", MySqlDbType.VarChar);
                cmd.Parameters.Add("@fecha_nacimiento", MySqlDbType.VarChar);
                cmd.Parameters.Add("@dni", MySqlDbType.VarChar);
                cmd.Parameters.Add("@situacion_laboral", MySqlDbType.VarChar);
                cmd.Parameters.Add("@asistencia_remota", MySqlDbType.VarChar);
                cmd.Parameters.Add("@tablet", MySqlDbType.VarChar);
                cmd.Parameters.Add("@puntos", MySqlDbType.Int32);

                try
                {
                    int filasInsertadas = 0;

                    foreach (DataRow fila in datos.Rows)
                    {
                        cmd.Parameters["@categoria"].Value = fila["categoria"];
                        cmd.Parameters["@convocatoria"].Value = fila["convocatoria"];
                        cmd.Parameters["@codigo_convocatoria"].Value = fila["codigo_convocatoria"];
                        cmd.Parameters["@provincia"].Value = fila["provincia"];
                        cmd.Parameters["@localidad"].Value = fila["localidad"];
                        cmd.Parameters["@nombre"].Value = fila["nombre"];
                        cmd.Parameters["@apellidos"].Value = fila["apellidos"];
                        cmd.Parameters["@fecha_solicitud"].Value = fila["fecha_solicitud"];
                        cmd.Parameters["@estado_inscripcion"].Value = fila["estado_inscripcion"];
                        cmd.Parameters["@tipo_inscripcion"].Value = fila["tipo_inscripcion"];
                        cmd.Parameters["@estado_matriculacion"].Value = fila["estado_matriculacion"];
                        cmd.Parameters["@email"].Value = fila["email"];
                        cmd.Parameters["@telefono"].Value = fila["telefono"];
                        cmd.Parameters["@nivel_estudios"].Value = fila["nivel_estudios"];
                        cmd.Parameters["@sexo"].Value = fila["sexo"];
                        cmd.Parameters["@fecha_nacimiento"].Value = fila["fecha_nacimiento"];
                        cmd.Parameters["@dni"].Value = fila["dni"];
                        cmd.Parameters["@situacion_laboral"].Value = fila["situacion_laboral"];
                        cmd.Parameters["@asistencia_remota"].Value = fila["asistencia_remota"];
                        cmd.Parameters["@tablet"].Value = fila["tablet"];
                        cmd.Parameters["@puntos"].Value = fila["puntos"];

                        filasInsertadas += cmd.ExecuteNonQuery();
                    }

                    // Si todo fue bien, confirmamos la transacción
                    transaccion.Commit();
                    return filasInsertadas > 0;
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();  // Si hay error, deshacer todo
                    MessageBox.Show($"Error al insertar datos, no se añadieron registros: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }


        public static bool InsertarDatosCSVbisbis(DataTable datos)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            string consulta = @"INSERT INTO tablatemporal 
    (categoria, convocatoria, codigo_convocatoria, provincia, localidad, nombre, apellidos, 
    fecha_solicitud, estado_inscripcion, tipo_inscripcion, estado_matriculacion, email, telefono, 
    nivel_estudios, sexo, fecha_nacimiento, dni, situacion_laboral, asistencia_remota, tablet, puntos) 
    VALUES (@categoria, @convocatoria, @codigo_convocatoria, @provincia, @localidad, @nombre, @apellidos, 
    @fecha_solicitud, @estado_inscripcion, @tipo_inscripcion, @estado_matriculacion, @email, @telefono, 
    @nivel_estudios, @sexo, @fecha_nacimiento, @dni, @situacion_laboral, @asistencia_remota, @tablet, @puntos)
    ON DUPLICATE KEY UPDATE 
    categoria = VALUES(categoria), convocatoria = VALUES(convocatoria), provincia = VALUES(provincia),
    localidad = VALUES(localidad), nombre = VALUES(nombre), apellidos = VALUES(apellidos), 
    fecha_solicitud = VALUES(fecha_solicitud), estado_inscripcion = VALUES(estado_inscripcion), 
    tipo_inscripcion = VALUES(tipo_inscripcion), estado_matriculacion = VALUES(estado_matriculacion), 
    email = VALUES(email), telefono = VALUES(telefono), nivel_estudios = VALUES(nivel_estudios), 
    sexo = VALUES(sexo), fecha_nacimiento = VALUES(fecha_nacimiento), dni = VALUES(dni), 
    situacion_laboral = VALUES(situacion_laboral), asistencia_remota = VALUES(asistencia_remota), 
    tablet = VALUES(tablet), puntos = VALUES(puntos);";


            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.Add("@categoria", MySqlDbType.VarChar);
                cmd.Parameters.Add("@convocatoria", MySqlDbType.VarChar);
                cmd.Parameters.Add("@codigo_convocatoria", MySqlDbType.VarChar);
                cmd.Parameters.Add("@provincia", MySqlDbType.VarChar);
                cmd.Parameters.Add("@localidad", MySqlDbType.VarChar);
                cmd.Parameters.Add("@nombre", MySqlDbType.VarChar);
                cmd.Parameters.Add("@apellidos", MySqlDbType.VarChar);
                cmd.Parameters.Add("@fecha_solicitud", MySqlDbType.VarChar);
                cmd.Parameters.Add("@estado_inscripcion", MySqlDbType.VarChar);
                cmd.Parameters.Add("@tipo_inscripcion", MySqlDbType.VarChar);
                cmd.Parameters.Add("@estado_matriculacion", MySqlDbType.VarChar);

                cmd.Parameters.Add("@email", MySqlDbType.VarChar);
                cmd.Parameters.Add("@telefono", MySqlDbType.VarChar);
                cmd.Parameters.Add("@nivel_estudios", MySqlDbType.VarChar);
                cmd.Parameters.Add("@sexo", MySqlDbType.VarChar);
                cmd.Parameters.Add("@fecha_nacimiento", MySqlDbType.VarChar);

                cmd.Parameters.Add("@dni", MySqlDbType.VarChar);
                cmd.Parameters.Add("@situacion_laboral", MySqlDbType.VarChar);
                cmd.Parameters.Add("@asistencia_remota", MySqlDbType.VarChar);
                cmd.Parameters.Add("@tablet", MySqlDbType.VarChar);
                cmd.Parameters.Add("@puntos", MySqlDbType.Int32);


                int filasInsertadas = 0;

                foreach (DataRow fila in datos.Rows)
                {
                    cmd.Parameters["@categoria"].Value = fila["categoria"];
                    cmd.Parameters["@convocatoria"].Value = fila["convocatoria"];
                    cmd.Parameters["@codigo_convocatoria"].Value = fila["codigo_convocatoria"];
                    cmd.Parameters["@provincia"].Value = fila["provincia"];
                    cmd.Parameters["@localidad"].Value = fila["localidad"];
                    cmd.Parameters["@nombre"].Value = fila["nombre"];
                    cmd.Parameters["@apellidos"].Value = fila["apellidos"];
                    cmd.Parameters["@fecha_solicitud"].Value = fila["fecha_solicitud"];
                    cmd.Parameters["@estado_inscripcion"].Value = fila["estado_inscripcion"];
                    cmd.Parameters["@tipo_inscripcion"].Value = fila["tipo_inscripcion"];
                    cmd.Parameters["@estado_matriculacion"].Value = fila["estado_matriculacion"];
                    cmd.Parameters["@email"].Value = fila["email"];
                    cmd.Parameters["@telefono"].Value = fila["telefono"];
                    cmd.Parameters["@nivel_estudios"].Value = fila["nivel_estudios"];
                    cmd.Parameters["@sexo"].Value = fila["sexo"];
                    cmd.Parameters["@fecha_nacimiento"].Value = fila["fecha_nacimiento"];
                    cmd.Parameters["@dni"].Value = fila["dni"];
                    cmd.Parameters["@situacion_laboral"].Value = fila["situacion_laboral"];
                    cmd.Parameters["@asistencia_remota"].Value = fila["asistencia_remota"];
                    cmd.Parameters["@tablet"].Value = fila["tablet"];
                    cmd.Parameters["@puntos"].Value = fila["puntos"];

                    filasInsertadas += cmd.ExecuteNonQuery();
                }

                return filasInsertadas > 0;
            }
        }
        public static bool ModificarProducto(string productoAntiguo, string productoNuevo, int idCategoria)
        {
            string consulta = "UPDATE products SET ProductName = @productoNuevo, CategoryID = @idCategoria WHERE ProductName = @productoAntiguo";

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.AddWithValue("@productoNuevo", productoNuevo);
                cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                cmd.Parameters.AddWithValue("@productoAntiguo", productoAntiguo);

                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        public static void TruncateMKP()
        {

        }
        public static bool BorrarProducto(string nombreProducto)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            // Obtener el ID del producto basado en el nombre
            string obtenerIdQuery = "SELECT ProductID FROM products WHERE ProductName = @nombreProducto";
            int productId;

            using (MySqlCommand cmd = new MySqlCommand(obtenerIdQuery, conexion))
            {
                cmd.Parameters.AddWithValue("@nombreProducto", nombreProducto);
                object result = cmd.ExecuteScalar();

                if (result == null) return false; // Producto no encontrado

                productId = Convert.ToInt32(result);
            }

            // Eliminar referencias en orderdetails
            string eliminarDetallesQuery = "DELETE FROM orderdetails WHERE ProductID = @productId";
            using (MySqlCommand cmd = new MySqlCommand(eliminarDetallesQuery, conexion))
            {
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.ExecuteNonQuery();
            }

            // Ahora eliminar el producto
            string eliminarProductoQuery = "DELETE FROM products WHERE ProductID = @productId";
            using (MySqlCommand cmd = new MySqlCommand(eliminarProductoQuery, conexion))
            {
                cmd.Parameters.AddWithValue("@productId", productId);
                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        public static bool VolcarDatos(string nombreProducto)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            try
            {
                // Volcar los datos de tablatemporal a bbddglobal
                if (!VolcarDatosABBDDGlobal(nombreProducto))
                {
                    return false;
                }

  

                return true;
            }
            catch (MySqlException ex)
            {
                // Manejo de la excepción, se captura toda la excepción para evitar fallos inesperados
                Console.WriteLine("Error al ejecutar la consulta MySQL: " + ex.Message);
                Console.WriteLine("Código de error: " + ex.ErrorCode);
                return false;
            }
        }

        private static bool VolcarDatosABBDDGlobal(string nombreProducto)
        {
            // Query para volcar todos los datos de tablaTemporal a bbddglobal
            string obtenerIdQuery = @"
       INSERT INTO bbddglobal (
    categoriag, convocatoriag, codigo_convocatoriag, provinciag, localidadg, 
    nombreg, apellidosg, fecha_solicitudg, estado_inscripciong, tipo_inscripciong, 
    estado_matriculaciong, emailg, telefonog, nivel_estudiosg, sexog, 
    fecha_nacimientog, dnig, situacion_laboralg, asistencia_remotag, 
    tabletg, puntosg, fecha_importacion, observaciones
) 
SELECT 
    categoria, convocatoria, codigo_convocatoria, provincia, localidad, 
    nombre, apellidos, fecha_solicitud, estado_inscripcion, tipo_inscripcion, 
    estado_matriculacion, email, telefono, nivel_estudios, sexo, 
    fecha_nacimiento, dni, situacion_laboral, asistencia_remota, 
    tablet, puntos, @nombreProducto, '' 
FROM tablatemporal
ON DUPLICATE KEY UPDATE
    categoriag = VALUES(categoriag), convocatoriag = VALUES(convocatoriag), 
    provinciag = VALUES(provinciag), localidadg = VALUES(localidadg), 
    nombreg = VALUES(nombreg), apellidosg = VALUES(apellidosg), 
    fecha_solicitudg = VALUES(fecha_solicitudg), estado_inscripciong = VALUES(estado_inscripciong), 
    tipo_inscripciong = VALUES(tipo_inscripciong), estado_matriculaciong = VALUES(estado_matriculaciong), 
    emailg = VALUES(emailg), telefonog = VALUES(telefonog), nivel_estudiosg = VALUES(nivel_estudiosg), 
    sexog = VALUES(sexog), fecha_nacimientog = VALUES(fecha_nacimientog), dnig = VALUES(dnig), 
    situacion_laboralg = VALUES(situacion_laboralg), asistencia_remotag = VALUES(asistencia_remotag), 
    tabletg = VALUES(tabletg), puntosg = VALUES(puntosg), 
    fecha_importacion = VALUES(fecha_importacion), observaciones = VALUES(observaciones);

    ";

            using (MySqlCommand cmd = new MySqlCommand(obtenerIdQuery, conexion))
            {
                cmd.Parameters.AddWithValue("@nombreProducto", nombreProducto);

                // Ejecutar la consulta
                cmd.ExecuteNonQuery();
            }

            return true;
        }

        private static bool LimpiarTablaTemporal()
        {
            // Query para limpiar la tabla temporal
            string eliminarDetallesQuery = "TRUNCATE TABLE tablatemporal";

            using (MySqlCommand cmd = new MySqlCommand(eliminarDetallesQuery, conexion))
            {
                // Ejecutar la consulta
                cmd.ExecuteNonQuery();
            }

            return true;
        }
        public static bool LimpiarExportarMKP()
        {
            // Query para limpiar la tabla temporal
            string eliminarDetallesQuery = "TRUNCATE TABLE mkpexportar";

            using (MySqlCommand cmd = new MySqlCommand(eliminarDetallesQuery, conexion))
            {
                // Ejecutar la consulta
                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public static bool VaciarTablasTemporales()
        {
            string[] tablas = { "tablatemporal", "tablamkp", "mkpexportar" };

            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    if (!Conectar()) return false;
                }

                foreach (var tabla in tablas)
                {
                    string consulta = $"TRUNCATE TABLE {tabla};";
                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                return true; // Éxito
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al vaciar tablas: {ex.Message}");
                return false; // Fallo
            }
        }


        public static int ObtenerTotalRegistrosBbddGlobal()
        {
            string consulta = "SELECT COUNT(*) FROM bbddglobal;";
            int totalRegistros = 0;

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return totalRegistros;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                totalRegistros = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return totalRegistros;
        }







        public static int ObtenerTotalRegistrosTablaTemporal()
        {
            string consulta = "SELECT COUNT(*) FROM tablatemporal;";
            int totalRegistros = 0;

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return totalRegistros;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                totalRegistros = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return totalRegistros;
        }
        public static int ObtenerTotalRegistrosTablaMKP()
        {
            string consulta = "SELECT COUNT(*) FROM tablamkp;";
            int totalRegistros = 0;

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return totalRegistros;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                totalRegistros = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return totalRegistros;
        }
        public static int ObtenerTotalRegistrosMKPExportar()
        {
            string consulta = "SELECT COUNT(*) FROM mkpexportar;";
            int totalRegistros = 0;

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return totalRegistros;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                totalRegistros = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return totalRegistros;
        }


        public static void EliminarMKPExportar(DataTable datosExportados)
{
    // Crear una lista con los 'user_login' usando el índice 0 en lugar del nombre de la columna
    List<string> userLogins = new List<string>();

    foreach (DataRow fila in datosExportados.Rows)
    {
        // Obtener el valor de la primera columna (índice 0)
        string userLogin = fila[0].ToString();
        userLogins.Add(userLogin);
    }

    // Convertir la lista de 'user_login' en un string adecuado para la cláusula IN
    string userLoginsStr = string.Join(",", userLogins.Select(u => $"'{u}'"));

    // Consulta DELETE usando 'user_login'
    string consulta = $@"
        DELETE FROM mkpexportar 
        WHERE user_login IN ({userLoginsStr});";

    if (conexion == null || conexion.State != ConnectionState.Open)
    {
        if (!Conectar()) return;
    }

    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
    {
        cmd.ExecuteNonQuery();
    }
}



        public static bool VolcarDatosMKP()
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            // Query para volcar todos los datos de tablaTemporal a mkp
            string obtenerIdQuery = @"INSERT INTO tablamkp (
    user_login, user_email, user_nicename, first_name, last_name, 
    Telefono, account_funds, dni, codigo_convocatoria
) 
SELECT 
    dni AS user_login, email AS user_email, dni AS user_nicename, 
    nombre AS first_name, apellidos AS last_name, telefono AS Telefono, 
    puntos AS account_funds, dni, codigo_convocatoria
FROM tablatemporal
ON DUPLICATE KEY UPDATE 
    user_email = VALUES(user_email), 
    user_nicename = VALUES(user_nicename), 
    first_name = VALUES(first_name), 
    last_name = VALUES(last_name), 
    Telefono = VALUES(Telefono), 
    account_funds = VALUES(account_funds)";



            using (MySqlCommand cmd = new MySqlCommand(obtenerIdQuery, conexion))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    // Manejo de la excepción, por ejemplo, registrar el error o mostrar un mensaje
                    Console.WriteLine("Se produjo un error al ejecutar el comando MySQL: " + ex.Message);
                    return false;
                    // Opcionalmente, puedes registrar más detalles de la excepción, como el código de error
                    Console.WriteLine("Código de error: " + ex.ErrorCode);
                    // Puedes realizar más acciones aquí si es necesario
                }
            }

            // Limpiar Tabla Temporal
          //  string eliminarDetallesQuery = "TRUNCATE TABLE tablaTemporal";
            //using (MySqlCommand cmd = new MySqlCommand(eliminarDetallesQuery, conexion))
            //{
              //  cmd.ExecuteNonQuery();
           // }

            return true;
        }

        internal static int ObtenerTotalPuntosUsados()
        {
            string consulta = "select sum(puntosg) from pedidos;";
            int totalRegistros = 0;

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return totalRegistros;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                totalRegistros = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return totalRegistros;
        }  
        internal static int ObtenerTotalConvocatorias()
        {
            string consulta = "SELECT COUNT(DISTINCT codigo_convocatoriag) AS total_convocatorias_distintas FROM bbddglobal;";
            int totalRegistros = 0;

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return totalRegistros;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                totalRegistros = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return totalRegistros;
        }  
        internal static int ObtenerTotalIncidencias()
        {
            string consulta = "SELECT count(*) FROM northwind.incidencias ;";
            int totalRegistros = 0;

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return totalRegistros;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                totalRegistros = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return totalRegistros;
        }

        internal static string ObtenerIncidenciaPrincipal()
        {
            string consulta = "SELECT incidencia FROM incidencias GROUP BY incidencia ORDER BY COUNT(*) DESC LIMIT 1;";
            string incidenciaPrincipal = string.Empty;

            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return incidenciaPrincipal;
            }

            using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
            {
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    incidenciaPrincipal = result.ToString();
                }
            }

            return incidenciaPrincipal;
        }
    }
}