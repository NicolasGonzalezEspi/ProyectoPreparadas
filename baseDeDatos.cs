using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace trabajoFinalInterfaces
{
    internal class BaseDeDatos
    {
        private static readonly string cadenaConexion =
     "server=localhost;port=3306;user=root;password=root;database=northwind;Charset=utf8mb4;";
        private static MySqlConnection conexion = null;

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
            string consulta = @"select user_login as 'userlogin', user_email as 'useremail', user_nicename as 'usernicename', first_name as 'firstname', last_name as 'lastname', telefono as 'telefono', account_funds as 'accountfunds' from mkpexportar;";

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
    FROM tablatemporal;";

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

        public static void UnirYSumar()
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return;
            }

            string consulta = @"
        INSERT INTO MKPexportar (user_login, user_email, user_nicename, first_name, last_name, Telefono, account_funds)
        SELECT 
            user_login, 
            MAX(user_email) AS user_email, 
            MAX(user_nicename) AS user_nicename, 
            MAX(first_name) AS first_name, 
            MAX(last_name) AS last_name, 
            MAX(Telefono) AS Telefono, 
            SUM(account_funds) AS account_funds
        FROM TablaMKP
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
            string eliminarDetallesQuery = "TRUNCATE TABLE tablamkp";
            using (MySqlCommand cmd = new MySqlCommand(eliminarDetallesQuery, conexion))
            {
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

        }


        public static bool InsertarDatos(DataTable datos)
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
            {
                if (!Conectar()) return false;
            }

            string consulta = @"INSERT INTO tablaTemporal 
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

                cmd.CommandText = @"INSERT INTO tablamkp 
        (user_login, user_email, user_nicename, first_name, last_name, Telefono, account_funds) 
        VALUES (@user_login, @user_email, @user_nicename, @first_name, @last_name, @Telefono, @account_funds) 
        ON DUPLICATE KEY UPDATE 
        user_email = VALUES(user_email), 
        user_nicename = VALUES(user_nicename), 
        first_name = VALUES(first_name), 
        last_name = VALUES(last_name), 
        Telefono = VALUES(Telefono), 
        account_funds = VALUES(account_funds);";

                cmd.Parameters.Add("@user_login", MySqlDbType.VarChar);
                cmd.Parameters.Add("@user_email", MySqlDbType.VarChar);
                cmd.Parameters.Add("@user_nicename", MySqlDbType.VarChar);
                cmd.Parameters.Add("@first_name", MySqlDbType.VarChar);
                cmd.Parameters.Add("@last_name", MySqlDbType.VarChar);
                cmd.Parameters.Add("@Telefono", MySqlDbType.VarChar);
                cmd.Parameters.Add("@account_funds", MySqlDbType.Int32);

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

                        filasInsertadas += cmd.ExecuteNonQuery();
                    }

                    // Si todo se insertó correctamente, confirmamos la transacción
                    transaccion.Commit();
                    return filasInsertadas > 0;
                }
                catch (Exception ex)
                {
                    // Si hay un error, revertimos la transacción
                    transaccion.Rollback();
                    MessageBox.Show($"Error al insertar datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
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

                string consulta = @"INSERT INTO tablaTemporal 
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

            string consulta = @"INSERT INTO tablaTemporal 
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
        FROM tablaTemporal
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

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    // Manejo de la excepción, por ejemplo, registrar el error o mostrar un mensaje
                    Console.WriteLine("Se produjo un error al ejecutar el comando MySQL: " + ex.Message);
                    // Opcionalmente, puedes registrar más detalles de la excepción, como el código de error
                    Console.WriteLine("Código de error: " + ex.ErrorCode);
                    // Puedes realizar más acciones aquí si es necesario
                }
            }



            // Limpiar Tabla Temporal
            //  string eliminarDetallesQuery = "TRUNCATE TABLE tablaTemporal";
            // using (MySqlCommand cmd = new MySqlCommand(eliminarDetallesQuery, conexion))
           // {
             //   cmd.ExecuteNonQuery();
           // }

            return true;
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

            // Query para volcar todos los datos de tablaTemporal a bbddglobal
            string obtenerIdQuery = @"INSERT INTO TablaMKP (
    user_login, user_email, user_nicename, first_name, last_name, 
    Telefono, account_funds
) 
SELECT 
    dni AS user_login, email AS user_email, dni AS user_nicename, 
    nombre AS first_name, apellidos AS last_name, telefono AS Telefono, 
    puntos AS account_funds
FROM tablaTemporal
ON DUPLICATE KEY UPDATE 
    user_email = VALUES(user_email), 
    user_nicename = VALUES(user_nicename), 
    first_name = VALUES(first_name), 
    last_name = VALUES(last_name), 
    Telefono = VALUES(Telefono), 
    account_funds = VALUES(account_funds);
";


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


    }
}