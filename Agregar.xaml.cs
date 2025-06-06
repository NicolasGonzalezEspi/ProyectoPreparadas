using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Mysqlx.Cursor;

namespace trabajoFinalInterfaces
{
    public partial class Agregar : Page
    {

        private string archivoCSVPath;

        public Agregar()
        {
            InitializeComponent();
            //CargarCategorias();
            CargarProductos();
            ActualizarTotalRegistros();
        }
        /*
         * 
         *      private void CargarCategorias()
        {
            DataTable categorias = BaseDeDatos.ObtenerCategorias();
            cbCategorias.ItemsSource = categorias.DefaultView;
            cbCategorias.DisplayMemberPath = "Categoría"; // Nombre a mostrar
            cbCategorias.SelectedValuePath = "ID"; // Valor real (CategoryID)
        }

         */

        private void CargarProductos()
        {
            DataTable productos = BaseDeDatos.MostrarMKP();
            dgProductos.ItemsSource = productos.DefaultView;
            ActualizarTotalRegistros();
        }

        private void BtnSeleccionarArchivoCSVClickBis(object sender, RoutedEventArgs e)
        {
            // Crear un cuadro de diálogo para seleccionar el archivo CSV
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == true)
            {
                archivoCSVPath = openFileDialog.FileName;
                MessageBox.Show($"Archivo seleccionado: {archivoCSVPath}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            SubirDatosCSVbisv2();
        }

        private void ActualizarTotalRegistros()
        {
            int totalRegistros = BaseDeDatos.ObtenerTotalRegistrosTablaMKP();
            txtTotalRegistros.Text = totalRegistros.ToString();
        }

        private void SubirDatosCSVbis()
        {
            if (string.IsNullOrEmpty(archivoCSVPath))
            {
                MessageBox.Show("Selecciona un archivo CSV válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {

                var tablaFinal = new DataTable();
                tablaFinal.Columns.AddRange(new DataColumn[]
                {
            new DataColumn("user_login", typeof(string)),
            new DataColumn("user_email", typeof(string)),
            new DataColumn("user_nicename", typeof(string)),
            new DataColumn("first_name", typeof(string)),
            new DataColumn("last_name", typeof(string)),
            new DataColumn("Telefono", typeof(string)),
            new DataColumn("account_funds", typeof(int)),
                new DataColumn("dni", typeof(string)),
    new DataColumn("codigo_convocatoria", typeof(string))
                });

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Encoding = Encoding.GetEncoding(1252),
                    Delimiter = ";",
                    TrimOptions = TrimOptions.Trim,
                    MissingFieldFound = null,
                    HeaderValidated = null,
                    IgnoreBlankLines = true
                };

                using (var reader = new StreamReader(archivoCSVPath, Encoding.GetEncoding(1252)))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    csv.ReadHeader();

                    int filaContador = 0;
                    while (csv.Read())
                    {
                        // Verificar si el primer campo (user_login) no está vacío o es nulo
                        string userLogin = csv.GetField(0);
                        if (!string.IsNullOrWhiteSpace(userLogin))
                        {
                            var nuevaFila = tablaFinal.NewRow();
                            nuevaFila["user_login"] = userLogin;
                            nuevaFila["user_email"] = csv.GetField(1) ?? "";
                            nuevaFila["user_nicename"] = csv.GetField(2) ?? "";
                            nuevaFila["first_name"] = csv.GetField(3) ?? "";
                            nuevaFila["last_name"] = csv.GetField(4) ?? "";
                            nuevaFila["Telefono"] = csv.GetField(5) ?? "";
                            nuevaFila["account_funds"] = int.TryParse(csv.GetField(6), out int funds) ? funds : 0;

                            // Generar valores únicos
                            string valorUnico = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + filaContador++;
                            nuevaFila["dni"] = "CSV_" + valorUnico;
                            nuevaFila["codigo_convocatoria"] = "CSV_" + valorUnico;

                            tablaFinal.Rows.Add(nuevaFila);
                        }
                    }
                }

                if (BaseDeDatos.InsertarDatosCSVbis(tablaFinal))
                {
                    MessageBox.Show($"Datos insertados correctamente. Registros procesados: {tablaFinal.Rows.Count}",
                                  "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarProductos();
                }
                else
                {
                    MessageBox.Show("Error al insertar datos en la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void SubirDatosCSVbisv2()
        {
            if (string.IsNullOrEmpty(archivoCSVPath))
            {
                MessageBox.Show("Selecciona un archivo CSV válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            try
            {

                var tablaFinal = new DataTable();
                tablaFinal.Columns.AddRange(new DataColumn[]
                {
            new DataColumn("user_login", typeof(string)),
            new DataColumn("user_email", typeof(string)),
            new DataColumn("user_nicename", typeof(string)),
                  new DataColumn("account_funds", typeof(int)),
            new DataColumn("first_name", typeof(string)),
            new DataColumn("last_name", typeof(string)),
            new DataColumn("Telefono", typeof(string)),
                new DataColumn("dni", typeof(string)),
    new DataColumn("codigo_convocatoria", typeof(string))
                });

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Encoding = Encoding.GetEncoding(1252),
                    Delimiter = ";",
                    TrimOptions = TrimOptions.Trim,
                    MissingFieldFound = null,
                    HeaderValidated = null,
                    IgnoreBlankLines = true
                };

                using (var reader = new StreamReader(archivoCSVPath, Encoding.GetEncoding(1252)))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    csv.ReadHeader();

                    int filaContador = 0;
                    while (csv.Read())
                    {
                        // Verificar si el primer campo (user_login) no está vacío o es nulo
                        string userLogin = csv.GetField(0);
                        if (!string.IsNullOrWhiteSpace(userLogin))
                        {
                            var nuevaFila = tablaFinal.NewRow();
                            nuevaFila["user_login"] = userLogin;
                            nuevaFila["user_email"] = csv.GetField(1) ?? "";
                            nuevaFila["user_nicename"] = csv.GetField(2) ?? "";
                            nuevaFila["first_name"] = csv.GetField(3) ?? "";
                            nuevaFila["last_name"] = csv.GetField(4) ?? "";
                            nuevaFila["Telefono"] = csv.GetField(5) ?? "";
                            nuevaFila["account_funds"] = int.TryParse(csv.GetField(6), out int funds) ? funds : 0;

                            // Generar valores únicos
                            string valorUnico = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + filaContador++;
                            nuevaFila["dni"] = "CSV_" + valorUnico;
                            nuevaFila["codigo_convocatoria"] = "CSV_" + valorUnico;

                            tablaFinal.Rows.Add(nuevaFila);
                        }
                    }
                }

                if (BaseDeDatos.InsertarDatosCSVbis(tablaFinal))
                {
                    MessageBox.Show($"Datos insertados correctamente. Registros procesados: {tablaFinal.Rows.Count}",
                                  "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    // ❌ Registro de error (inserción fallida)
                    var parametros = new Dictionary<string, object>
           {
               { "@fecha", DateTime.Now },
               { "@actividad", $"2A. Importación correcta estado actual Marketplace: {Path.GetFileName(archivoCSVPath)}" },
               { "@paso", "Considere revisar los datos. Al terminar, pulsa en Totalizar Saldos." }
           };

                    BaseDeDatos.EjecutarQueryIncidencia(
                        "INSERT INTO actividad_usuarios (fecha_actividad_usuario, actividad, siguiente_paso) VALUES (@fecha, @actividad, @paso)",
                        parametros
                    );
                    CargarProductos();
                    ActualizarTotalRegistros();
                    BtnImportarMKP.IsEnabled = false;
                    BtnImportarMKP.Content = "Datos previamente importados.";
                    Main.ActualizarUltimaActividad();

                }
                else
                {
                    MessageBox.Show("Error al insertar datos en la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    // ❌ Registro de error (inserción fallida)
                    var parametros = new Dictionary<string, object>
           {
               { "@fecha", DateTime.Now },
               { "@actividad", $"2A. Inserción fallida de Estado Actual MKP: {Path.GetFileName(archivoCSVPath)}" },
               { "@paso", "Revise la conexión. Reinicie la aplicación." }
           };

                    BaseDeDatos.EjecutarQueryIncidencia(
                        "INSERT INTO actividad_usuarios (fecha_actividad_usuario, actividad, siguiente_paso) VALUES (@fecha, @actividad, @paso)",
                        parametros
                    );
                    Main.ActualizarUltimaActividad();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // ❌ Registro de error (inserción fallida)
                var parametros = new Dictionary<string, object>
           {
               { "@fecha", DateTime.Now },
               { "@actividad", $"2A. Inserción fallida de Estado Actual MKP: {Path.GetFileName(archivoCSVPath)}" },
               { "@paso", "Revise formato, codificación, contenido o separadores del archivo de texto CSV." }
           };

                BaseDeDatos.EjecutarQueryIncidencia(
                    "INSERT INTO actividad_usuarios (fecha_actividad_usuario, actividad, siguiente_paso) VALUES (@fecha, @actividad, @paso)",
                    parametros
                );
                Main.ActualizarUltimaActividad();

            }
        }

        /*
private void BtnAgregar_Click(object sender, RoutedEventArgs e)
{
   string nombreProducto = txtProducto.Text.Trim();
   if (string.IsNullOrEmpty(nombreProducto) )
   {
       MessageBox.Show("Por favor, ingrese un nombre y seleccione una categoría.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
       return;
   }

   int idCategoria = Convert.ToInt32(cbCategorias.SelectedValue);

   bool agregado = BaseDeDatos.AgregarProducto(nombreProducto, idCategoria);
   if (agregado)
   {
       MessageBox.Show("Producto agregado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
       CargarProductos();
       txtProducto.Clear();
       cbCategorias.SelectedIndex = -1;
   }
   else
   {
       MessageBox.Show("Error al agregar el producto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
   }
}
*/

        private void ExportarCSV2(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable datos = BaseDeDatos.MostrarMKP();
                if (datos == null || datos.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Mostrar cuadro de diálogo para guardar el archivo
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivos CSV (*.csv)|*.csv",
                    Title = "Guardar archivo CSV"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string rutaArchivo = saveFileDialog.FileName;

                    // Escribir los datos en un archivo CSV
                    using (StreamWriter writer = new StreamWriter(rutaArchivo, false, Encoding.UTF8))
                    {
                        // Escribir encabezados
                        string[] columnas = datos.Columns.Cast<DataColumn>().Select(col => col.ColumnName).ToArray();
                        writer.WriteLine(string.Join(",", columnas));

                        // Escribir filas
                        foreach (DataRow fila in datos.Rows)
                        {
                            string[] valores = fila.ItemArray.Select(val => val.ToString()).ToArray();
                            writer.WriteLine(string.Join(",", valores));
                        }
                    }

                    MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportarCSV(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable datos = BaseDeDatos.MostrarMKP();
                if (datos == null || datos.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Mostrar cuadro de diálogo para guardar el archivo
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivos CSV (*.csv)|*.csv",
                    Title = "Guardar archivo CSV"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string rutaArchivo = saveFileDialog.FileName;

                    // Escribir los datos en un archivo CSV
                    using (StreamWriter writer = new StreamWriter(rutaArchivo, false, Encoding.UTF8))
                    {
                        // Escribir encabezados
                        string[] columnas = datos.Columns.Cast<DataColumn>().Select(col => col.ColumnName).ToArray();
                        writer.WriteLine(string.Join(",", columnas));

                        // Escribir filas
                        foreach (DataRow fila in datos.Rows)
                        {
                            string[] valores = fila.ItemArray.Select(val => val.ToString()).ToArray();
                            writer.WriteLine(string.Join(",", valores));
                        }
                    }

                    MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UnirYSumarDatos(object sender, RoutedEventArgs e)
        {
            int totalTablaTemporal = BaseDeDatos.ObtenerTotalRegistrosTablaTemporal();
            int totalTablaMKP = BaseDeDatos.ObtenerTotalRegistrosTablaMKP();

            if (totalTablaTemporal == totalTablaMKP)
            {
                MessageBox.Show("Aún no se han importado los saldos del Marketplace. \nRegistros en Nuevas Convos: "+totalTablaTemporal+ ". \nRegistros en Tabla MKP: "+totalTablaMKP+"." ,
                "Advertencia",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
                return;
            }



           /* MessageBoxResult confirmacion = MessageBox.Show("¿Está seguro de que desea sumar los puntos? \nRegistros en Nuevas Convos: " + totalTablaTemporal + ". \nRegistros en Tabla MKP: " + totalTablaMKP + ".",
                                         "Confirmar unión de datos.",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);*/
            MessageBoxResult confirmacion = MessageBox.Show("¿Está seguro de que desea totalizar los saldos?",
                                         "Confirmar unión de datos.",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);


            if (confirmacion == MessageBoxResult.Yes)
            {

                BaseDeDatos.UnirYSumar();
                CargarProductos();
                BaseDeDatos.EliminarVaciosMKPExportar();
                ActualizarTotalRegistros();
                // ❌ Registro de error (inserción fallida)
                var parametros = new Dictionary<string, object>
           {
               { "@fecha", DateTime.Now },
               { "@actividad", $"2B. Unión y suma de datos correcta." },
               { "@paso", "Pulse 3A hasta vaciar la tabla. Luego importe los archivos en el Marketplace. Entonces, pulse en Mover Nuevas Convos a BBDDGlobal." }
           };

                BaseDeDatos.EjecutarQueryIncidencia(
                    "INSERT INTO actividad_usuarios (fecha_actividad_usuario, actividad, siguiente_paso) VALUES (@fecha, @actividad, @paso)",
                    parametros
                );
                Main.ActualizarUltimaActividad();
                Main.IrAModificar();

            }
            else
            {
                MessageBox.Show("No se sumaron los puntos.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                // ❌ Registro de error (inserción fallida)
                var parametros = new Dictionary<string, object>
           {
               { "@fecha", DateTime.Now },
               { "@actividad", $"2B. Totalización de datos fallida." },
               { "@paso", "Revise la conexión. Reinicie la aplicación." }
           };

                BaseDeDatos.EjecutarQueryIncidencia(
                    "INSERT INTO actividad_usuarios (fecha_actividad_usuario, actividad, siguiente_paso) VALUES (@fecha, @actividad, @paso)",
                    parametros
                );

                Main.ActualizarUltimaActividad();


            }




        }


    }
}
