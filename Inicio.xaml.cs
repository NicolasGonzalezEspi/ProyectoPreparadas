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

namespace trabajoFinalInterfaces
{
    public partial class Inicio : Page
    {
        private string archivoCSVPath;

        public Inicio()
        {
            InitializeComponent();
            CargarProductos();  //al iniciarse esta página, se muestran todos los productos y sus respectivas categorías
            //además, siempre que se haga una opción tipo crud, se llamará a este método para que se vea reflejado automáticamente.
        }

        private void CargarProductos()
        {
            DataTable productos = BaseDeDatos.MostrarBBDDGlobal(); // llamada a la función determinada de  baseDeDatos
            dgProductos.ItemsSource = productos.DefaultView; //volcamos los datos al datagrid (la tabla)
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Mostrar ventana emergente para ingresar el DNI
            string dniIngresado = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el DNI:",
                "Buscar por DNI",
                ""
            );

            // Verificar que el DNI no esté vacío
            if (!string.IsNullOrWhiteSpace(dniIngresado))
            {
                DataTable resultado = BaseDeDatos.MostrarBBDDGlobalFiltroDNI(dniIngresado);

                if (resultado.Rows.Count > 0)
                {
                    MessageBox.Show("Datos encontrados.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataTable productos = BaseDeDatos.MostrarBBDDGlobalFiltroDNI(dniIngresado); // llamada a la función determinada de  baseDeDatos
                    dgProductos.ItemsSource = productos.DefaultView; //volcamos los datos al datagrid (la tabla)

                    // Aquí puedes mostrar los datos en una tabla o en un ListView si lo deseas
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para el DNI ingresado.", "Sin resultados", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un DNI válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Mostrar ventana emergente para ingresar el email
            string emailIngresado = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el email:",
                "Buscar por email",
                ""
            );

            // Verificar que el email no esté vacío
            if (!string.IsNullOrWhiteSpace(emailIngresado))
            {
                DataTable resultado = BaseDeDatos.MostrarBBDDGlobalFiltroEmail(emailIngresado);

                if (resultado.Rows.Count > 0)
                {
                    MessageBox.Show("Datos encontr ados.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    // ✅ Corrección: Mostrar los datos obtenidos por email
                    dgProductos.ItemsSource = resultado.DefaultView;

                }
                else
                {
                    MessageBox.Show("No se encontraron datos para el email ingresado.", "Sin resultados", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un email válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem != null)
            {
                DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;

                // Usar exactamente los mismos nombres que en los Bindings del XAML
                string codigoConvocatoria = filaSeleccionada["codigoconvocatoriag"].ToString();
                string dni = filaSeleccionada["dnig"].ToString();

                string observacion = Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingrese la observación:",
                    "Añadir Observación",
                    filaSeleccionada["observaciones"]?.ToString() ?? ""
                );

                if (!string.IsNullOrWhiteSpace(observacion))
                {
                    // Pasar los nombres correctos a la función SQL
                    bool actualizado = BaseDeDatos.ActualizarObservacion(
                        codigoConvocatoria,
                        dni,
                        observacion,
                        "codigo_convocatoriag",  // Nombre real en BD
                        "dnig"                   // Nombre real en BD
                    );

                    if (actualizado)
                    {
                        MessageBox.Show("Observación añadida correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarProductos();
                    }
                }
            }
        }

        private void ExportarBackup(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable datos = BaseDeDatos.MostrarBBDDGlobal();
                if (datos == null || datos.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivos CSV (*.csv)|*.csv",
                    Title = "Guardar archivo CSV"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string rutaArchivo = saveFileDialog.FileName;
                    using (StreamWriter writer = new StreamWriter(rutaArchivo, false, Encoding.GetEncoding(1252)))
                    {
                        string[] columnas = datos.Columns.Cast<DataColumn>().Select(col => col.ColumnName).ToArray();
                        writer.WriteLine(string.Join(",", columnas));

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

        private void ImportarBackup(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == true)
            {
                archivoCSVPath = openFileDialog.FileName;
                MessageBox.Show($"Archivo seleccionado: {archivoCSVPath}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            SubirDatosCSV();
        }

        private void SubirDatosCSV()
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
                    new DataColumn("categoriag", typeof(string)),
                    new DataColumn("convocatoriag", typeof(string)),
                    new DataColumn("codigo_convocatoriag", typeof(string)),
                    new DataColumn("provinciag", typeof(string)),
                    new DataColumn("localidadg", typeof(string)),
                    new DataColumn("nombreg", typeof(string)),
                    new DataColumn("apellidosg", typeof(string)),
                    new DataColumn("fecha_solicitudg", typeof(string)),
                    new DataColumn("estado_inscripciong", typeof(string)),
                    new DataColumn("tipo_inscripciong", typeof(string)),
                    new DataColumn("estado_matriculaciong", typeof(string)),
                    new DataColumn("emailg", typeof(string)),
                    new DataColumn("telefonog", typeof(string)),
                    new DataColumn("nivel_estudiosg", typeof(string)),
                    new DataColumn("sexog", typeof(string)),
                    new DataColumn("fecha_nacimientog", typeof(string)),
                    new DataColumn("dnig", typeof(string)),
                    new DataColumn("situacion_laboralg", typeof(string)),
                    new DataColumn("asistencia_remotag", typeof(string)),
                    new DataColumn("tabletg", typeof(string)),
                    new DataColumn("puntosg", typeof(int)),
                    new DataColumn("fecha_importacion", typeof(string)),
                    new DataColumn("observaciones", typeof(string))
                });

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Encoding = Encoding.GetEncoding(1252), // Codificación Windows-1252 para ANSI
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

                    while (csv.Read())
                    {
                        var nuevaFila = tablaFinal.NewRow();
                        foreach (DataColumn col in tablaFinal.Columns)
                        {
                            string valor = csv.GetField(col.ColumnName);

                            // Verifica si la columna es "puntosg" y convierte el valor correctamente
                            if (col.ColumnName == "puntosg")
                            {
                                if (int.TryParse(valor, out int puntos))
                                    nuevaFila[col.ColumnName] = puntos;
                                else
                                    nuevaFila[col.ColumnName] = DBNull.Value; // O asigna un valor por defecto, por ejemplo 0
                            }
                            else
                            {
                                nuevaFila[col.ColumnName] = valor ?? "";
                            }
                        }

                    }
                }

                if (BaseDeDatos.InsertarDatosCSVBackup(tablaFinal))
                {
                    MessageBox.Show($"Datos insertados correctamente. Registros procesados: {tablaFinal.Rows.Count}",
                                    "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void FiltrarCadiz(object sender, RoutedEventArgs e)
        {

        }
    }
}
