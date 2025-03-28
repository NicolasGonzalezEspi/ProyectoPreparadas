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

        private void Button_Click_2bis(object sender, RoutedEventArgs e)
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItems.Count > 0) // Verifica que haya elementos seleccionados
            {
                string observacion = Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingrese la observación:",
                    "Añadir Observación",
                    ""
                );

                if (!string.IsNullOrWhiteSpace(observacion))
                {
                    bool exitoGeneral = true;

                    foreach (var item in dgProductos.SelectedItems) // Iterar sobre todos los seleccionados
                    {
                        DataRowView filaSeleccionada = (DataRowView)item;

                        string codigoConvocatoria = filaSeleccionada["codigoconvocatoriag"].ToString();
                        string dni = filaSeleccionada["dnig"].ToString();

                        bool actualizado = BaseDeDatos.ActualizarObservacion(
                            codigoConvocatoria,
                            dni,
                            observacion,
                            "codigo_convocatoriag",
                            "dnig"
                        );

                        if (!actualizado)
                        {
                            exitoGeneral = false;
                        }
                    }

                    if (exitoGeneral)
                    {
                        MessageBox.Show("Observaciones añadidas correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Algunas observaciones no pudieron ser añadidas.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    CargarProductos(); // Refrescar datos
                }
            }
            else
            {
                MessageBox.Show("Seleccione al menos un elemento.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                        writer.WriteLine(string.Join(";", columnas));

                        foreach (DataRow fila in datos.Rows)
                        {
                            string[] valores = fila.ItemArray.Select(val => val.ToString()).ToArray();
                            writer.WriteLine(string.Join(";", valores));
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
            new DataColumn("categoriag", typeof(string)),         // 0
            new DataColumn("convocatoriag", typeof(string)),      // 1
            new DataColumn("codigo_convocatoriag", typeof(string)), // 2
            new DataColumn("provinciag", typeof(string)),         // 3
            new DataColumn("localidadg", typeof(string)),         // 4
            new DataColumn("nombreg", typeof(string)),            // 5
            new DataColumn("apellidosg", typeof(string)),         // 6
            new DataColumn("fecha_solicitudg", typeof(string)),    // 7
            new DataColumn("estado_inscripciong", typeof(string)), // 8
            new DataColumn("tipo_inscripciong", typeof(string)),    // 9
            new DataColumn("estado_matriculaciong", typeof(string)),// 10
            new DataColumn("emailg", typeof(string)),             // 11
            new DataColumn("telefonog", typeof(string)),          // 12
            new DataColumn("nivel_estudiosg", typeof(string)),    // 13
            new DataColumn("sexog", typeof(string)),             // 14
            new DataColumn("fecha_nacimientog", typeof(string)),   // 15
            new DataColumn("dnig", typeof(string)),              // 16
            new DataColumn("situacion_laboralg", typeof(string)),  // 17
            new DataColumn("asistencia_remotag", typeof(string)),  // 18
            new DataColumn("tabletg", typeof(string)),            // 19
            new DataColumn("puntosg", typeof(int)),              // 20
            new DataColumn("fecha_importacion", typeof(string)),   // 21
            new DataColumn("observaciones", typeof(string))       // 22
                });

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Encoding = Encoding.GetEncoding(1252), // Codificación Windows-1252 para ANSI
                    Delimiter = ";",
                    TrimOptions = TrimOptions.Trim,
                    MissingFieldFound = null,
                    HeaderValidated = null,
                    IgnoreBlankLines = true,
                    HasHeaderRecord = false // Indicamos que el CSV no tiene encabezados reales
                };

                using (var reader = new StreamReader(archivoCSVPath, Encoding.GetEncoding(1252)))
                using (var csv = new CsvReader(reader, config))
                {
                    // Leer y descartar la primera línea
                    if (csv.Read())
                    {
                        // No necesitamos hacer nada con esta primera lectura, solo avanzar el cursor
                    }

                    while (csv.Read())
                    {
                        var nuevaFila = tablaFinal.NewRow();
                        try
                        {
                            nuevaFila["categoriag"] = csv.GetField(0) ?? "";
                            nuevaFila["convocatoriag"] = csv.GetField(1) ?? "";
                            nuevaFila["codigo_convocatoriag"] = csv.GetField(2) ?? "";
                            nuevaFila["provinciag"] = csv.GetField(3) ?? "";
                            nuevaFila["localidadg"] = csv.GetField(4) ?? "";
                            nuevaFila["nombreg"] = csv.GetField(5) ?? "";
                            nuevaFila["apellidosg"] = csv.GetField(6) ?? "";
                            nuevaFila["fecha_solicitudg"] = csv.GetField(7) ?? "";
                            nuevaFila["estado_inscripciong"] = csv.GetField(8) ?? "";
                            nuevaFila["tipo_inscripciong"] = csv.GetField(9) ?? "";
                            nuevaFila["estado_matriculaciong"] = csv.GetField(10) ?? "";
                            nuevaFila["emailg"] = csv.GetField(11) ?? "";
                            nuevaFila["telefonog"] = csv.GetField(12) ?? "";
                            nuevaFila["nivel_estudiosg"] = csv.GetField(13) ?? "";
                            nuevaFila["sexog"] = csv.GetField(14) ?? "";
                            nuevaFila["fecha_nacimientog"] = csv.GetField(15) ?? "";
                            nuevaFila["dnig"] = csv.GetField(16) ?? "";
                            nuevaFila["situacion_laboralg"] = csv.GetField(17) ?? "";
                            nuevaFila["asistencia_remotag"] = csv.GetField(18) ?? "";
                            nuevaFila["tabletg"] = csv.GetField(19) ?? "";
                            nuevaFila["puntosg"] = int.TryParse(csv.GetField(20), out int puntos) ? puntos : (object)DBNull.Value;
                            nuevaFila["fecha_importacion"] = csv.GetField(21) ?? "";
                            nuevaFila["observaciones"] = csv.GetField(22) ?? "";
                        }
                        catch (CsvHelperException ex)
                        {
                            MessageBox.Show($"Error al leer fila del CSV: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            continue; // Saltar a la siguiente fila en caso de error
                        }
                        tablaFinal.Rows.Add(nuevaFila);
                    }
                }

                if (BaseDeDatos.InsertarDatosCSVBackup(tablaFinal))
                {
                    CargarProductos();
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
            DataTable resultado = BaseDeDatos.MostrarBBDDGlobalFiltroCadiz();

            if (resultado.Rows.Count > 0)
            {
           

              
                dgProductos.ItemsSource = resultado.DefaultView;

            }
        }
        private void FiltrarMalaga(object sender, RoutedEventArgs e)
        {
            DataTable resultado = BaseDeDatos.MostrarBBDDGlobalFiltroMalaga();

            if (resultado.Rows.Count > 0)
            {



                dgProductos.ItemsSource = resultado.DefaultView;

            }
        }
        private void cmbEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtener el ComboBox seleccionado
            ComboBox comboBox = sender as ComboBox;

            if (comboBox != null && comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                // Obtener el texto del item seleccionado
                string estadoSeleccionado = selectedItem.Content.ToString();

                if (dgProductos.SelectedItems.Count > 0) // Verifica que haya elementos seleccionados
                {
                    bool exitoGeneral = true;

                    foreach (var item in dgProductos.SelectedItems) // Iterar sobre todos los seleccionados
                    {
                        DataRowView filaSeleccionada = (DataRowView)item;

                        string codigoConvocatoria = filaSeleccionada["codigoconvocatoriag"].ToString();
                        string dni = filaSeleccionada["dnig"].ToString();

                        bool actualizado = BaseDeDatos.ActualizarObservacion(
                            codigoConvocatoria,
                            dni,
                            estadoSeleccionado,
                            "codigo_convocatoriag",
                            "dnig"
                        );

                        if (!actualizado)
                        {
                            exitoGeneral = false;
                        }
                    }

                    if (exitoGeneral)
                    {
                        MessageBox.Show("Observaciones añadidas correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Algunas observaciones no pudieron ser añadidas.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    CargarProductos(); // Refrescar datos
                }

                // Imprimir el estado seleccionado en la consola
                //   Console.WriteLine($"Estado seleccionado: {estadoSeleccionado}");
                //    MessageBox.Show($"Estado seleccionado: {estadoSeleccionado}", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        /*  
         *        <ComboBox x:Name="cmbEstado"
             HorizontalAlignment="Left" 
             VerticalAlignment="Top"
             Margin="650,27,0,0"
             SelectionChanged="cmbEstado_SelectionChanged">
               <ComboBoxItem IsSelected="True">Procesando</ComboBoxItem>
               <ComboBoxItem>Gestionando</ComboBoxItem>
               <ComboBoxItem>Completado</ComboBoxItem>
           </ComboBox>
         *  private void cmbEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
           {
               // Verificar si hay un elemento seleccionado antes de acceder a su contenido
               if (cmbEstado.SelectedItem is ComboBoxItem selectedItem && selectedItem.Content != null)
               {
                   string observacion = selectedItem.Content.ToString(); // Obtener el estado seleccionado

                   if (dgProductos.SelectedItems.Count > 0) // Verifica que haya elementos seleccionados
                   {
                       bool exitoGeneral = true;

                       foreach (var item in dgProductos.SelectedItems) // Iterar sobre todos los seleccionados
                       {
                           DataRowView filaSeleccionada = (DataRowView)item;

                           string codigoConvocatoria = filaSeleccionada["codigoconvocatoriag"].ToString();
                           string dni = filaSeleccionada["dnig"].ToString();

                           bool actualizado = BaseDeDatos.ActualizarObservacion(
                               codigoConvocatoria,
                               dni,
                               observacion,
                               "codigo_convocatoriag",
                               "dnig"
                           );

                           if (!actualizado)
                           {
                               exitoGeneral = false;
                           }
                       }

                       if (exitoGeneral)
                       {
                           MessageBox.Show("Observaciones añadidas correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                       }
                       else
                       {
                           MessageBox.Show("Algunas observaciones no pudieron ser añadidas.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                       }

                       CargarProductos(); // Refrescar datos
                   }
                   else
                   {
                       MessageBox.Show("Seleccione al menos un elemento.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                   }
               }
               else
               {
                   Console.WriteLine("Ningún estado seleccionado en el ComboBox."); // Mensaje en consola si es null
               }
           }*/

    }


}
    

