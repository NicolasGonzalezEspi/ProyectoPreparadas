using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using ClosedXML.Excel;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using ClosedXML.Excel;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Text;



namespace trabajoFinalInterfaces
{
    public partial class Borrar : Page
    {
        private string archivoExcelPath;
        private string archivoCSVPath;

        public Borrar()
        {
            InitializeComponent();
            CargarProductos();
        }

        private void BtnSeleccionarArchivoClick(object sender, RoutedEventArgs e)
        {
            // Crear un cuadro de diálogo para seleccionar el archivo Excel
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos Excel (*.xlsx)|*.xlsx";

            if (openFileDialog.ShowDialog() == true)
            {
                archivoExcelPath = openFileDialog.FileName;
                MessageBox.Show($"Archivo seleccionado: {archivoExcelPath}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                subirDatos();

            }

        }

        private void BtnSeleccionarArchivoCSVClick(object sender, RoutedEventArgs e)
        {
            // Crear un cuadro de diálogo para seleccionar el archivo CSV
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == true)
            {
                archivoCSVPath = openFileDialog.FileName;
                MessageBox.Show($"Archivo seleccionado: {archivoCSVPath}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                SubirDatosCSV();
            }
       
        }


        private void subirDatos()
        {
            if (string.IsNullOrEmpty(archivoExcelPath) && string.IsNullOrEmpty(archivoCSVPath))
            {
                MessageBox.Show("Selecciona un archivo válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var workbook = new XLWorkbook(archivoExcelPath))
                {
                    var tablaFinal = new DataTable();
                    tablaFinal.Columns.AddRange(new DataColumn[]
                    {
                new DataColumn("categoria", typeof(string)),
                new DataColumn("convocatoria", typeof(string)),
                new DataColumn("codigo_convocatoria", typeof(string)),
                new DataColumn("provincia", typeof(string)),
                new DataColumn("localidad", typeof(string)),
                new DataColumn("nombre", typeof(string)),
                new DataColumn("apellidos", typeof(string)),
                new DataColumn("fecha_solicitud", typeof(string)),
                new DataColumn("estado_inscripcion", typeof(string)),
                new DataColumn("tipo_inscripcion", typeof(string)),
                new DataColumn("estado_matriculacion", typeof(string)),
                new DataColumn("email", typeof(string)),
                new DataColumn("telefono", typeof(string)),
                new DataColumn("nivel_estudios", typeof(string)),
                new DataColumn("sexo", typeof(string)),
                new DataColumn("fecha_nacimiento", typeof(string)),
                new DataColumn("dni", typeof(string)),
                new DataColumn("situacion_laboral", typeof(string)),
                new DataColumn("asistencia_remota", typeof(string)),
                new DataColumn("tablet", typeof(string)),
                new DataColumn("puntos", typeof(int))
                    });

                    string[] hojas = { "Cádiz", "Málaga" };

                    foreach (var hoja in hojas)
                    {
                        var ws = workbook.Worksheet(hoja);
                        var rows = ws.RangeUsed().RowsUsed().Skip(1); // Omitir encabezados

                        foreach (var row in rows)
                        {
                            var nuevaFila = tablaFinal.NewRow();
                            nuevaFila["categoria"] = row.Cell(1).GetString();
                            nuevaFila["convocatoria"] = row.Cell(2).GetString();
                            nuevaFila["codigo_convocatoria"] = row.Cell(3).GetString();
                            nuevaFila["provincia"] = row.Cell(4).GetString();
                            nuevaFila["localidad"] = row.Cell(5).GetString();
                            nuevaFila["nombre"] = row.Cell(6).GetString();
                            nuevaFila["apellidos"] = row.Cell(7).GetString();

                            // Manejar la fecha de solicitud con try-catch
                            try
                            {
                                nuevaFila["fecha_solicitud"] = ObtenerFecha(row.Cell(8));
                            }
                            catch
                            {
                                nuevaFila["fecha_solicitud"] = ""; // Valor predeterminado vacío si ocurre un error
                            }

                            nuevaFila["estado_inscripcion"] = row.Cell(9).GetString();
                            nuevaFila["tipo_inscripcion"] = row.Cell(10).GetString();
                            nuevaFila["estado_matriculacion"] = row.Cell(11).GetString();
                            nuevaFila["email"] = row.Cell(12).GetString();
                            nuevaFila["telefono"] = row.Cell(13).GetString();
                            nuevaFila["nivel_estudios"] = row.Cell(14).GetString();
                            nuevaFila["sexo"] = row.Cell(15).GetString();

                            // Manejar la fecha de nacimiento con try-catch
                            try
                            {
                                nuevaFila["fecha_nacimiento"] = ObtenerFecha(row.Cell(18));
                            }
                            catch
                            {
                                nuevaFila["fecha_nacimiento"] = ""; // Valor predeterminado vacío si ocurre un error
                            }

                            nuevaFila["dni"] = row.Cell(19).GetString();
                            nuevaFila["situacion_laboral"] = row.Cell(20).GetString();
                            nuevaFila["asistencia_remota"] = row.Cell(21).GetString();
                            nuevaFila["tablet"] = row.Cell(22).GetString();
                            nuevaFila["puntos"] = int.TryParse(row.Cell(23).GetString(), out int puntos) ? puntos : 0;

                            tablaFinal.Rows.Add(nuevaFila);
                        }
                    }

                    if (BaseDeDatos.InsertarDatos(tablaFinal))
                    {
                        MessageBox.Show("Datos insertados correctamente en la base de datos.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarProductos();
                    }
                    else
                    {
                        MessageBox.Show("Error al insertar datos en la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ObtenerFecha(IXLCell cell)
        {
            // Intenta obtener el valor de la celda como una fecha
            if (DateTime.TryParse(cell.GetString(), out DateTime fecha))
            {
                // Si es una fecha válida, devuélvela como string con el formato deseado
                return fecha.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                // Si no es una fecha válida, puedes devolver un valor por defecto
                return string.Empty;  // O "0000-00-00" o lo que prefieras
            }
        }


        private string FormatearFecha(IXLCell cell)
        {
            // Verificar si la celda contiene una fecha (DataType es DateTime)
            if (cell.DataType == XLDataType.DateTime)
            {
                // Si es una fecha, obtenerla como DateTime
                DateTime fecha = cell.GetDateTime();
                // Devolver la fecha formateada como string
                return fecha.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                // Si no es una fecha, devolver un valor vacío o un valor alternativo
                return string.Empty;
            }
        }



        private void CargarProductos()
        {
            DataTable productos = BaseDeDatos.MostrarTablaTemporal();
            dgProductos.ItemsSource = productos.DefaultView;
        }


        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            
              string nombreProducto = txtProductoBorrar.Text.Trim();



            // Si aún no hay producto, no hacer nada
            if (string.IsNullOrEmpty(nombreProducto))
            {
                MessageBox.Show("El campo 'Fecha de Importación' no puede estar vacío. ../../.. Dia, mes, año. ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Validar si tiene 8 caracteres
            if (nombreProducto.Length != 8)
            {
                MessageBox.Show("La fecha de importación debe tener 8 caracteres. El formato debe ser ../../.. Dia, mes, año. ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine("La fecha de importación debe tener 8 caracteres. El formato debe ser ../../..");
                return;
            }

            // Validar si cumple con el patrón (regex) ../../02

            string input = nombreProducto;
            string pattern = @"^\d{2}/\d{2}/\d{2}$";
            if (!Regex.IsMatch(input, pattern))
            {
                Console.WriteLine("Formato inválido: El formato debe ser NN/NN/NN, donde N son números.");
                MessageBox.Show("El campo 'Fecha de Importación' debe ser una fecha con el siguiente formato ../../.. Dia, mes, año. ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult confirmacion = MessageBox.Show($"¿Está seguro de que desea volcar los datos? \"{nombreProducto}\"?",
                                                            "Confirmar volcado",
                                                            MessageBoxButton.YesNo,
                                                            MessageBoxImage.Warning);

            if (confirmacion == MessageBoxResult.Yes)
            {
                bool eliminado = BaseDeDatos.VolcarDatos(nombreProducto);
                if (eliminado)
                {
                    MessageBox.Show("Datos volcados correctamente. ", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarProductos();
                    txtProductoBorrar.Clear();
                }
                else
                {
                    MessageBox.Show("No se pudo volcar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
             

        }

        private void dgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Si se selecciona un producto, mostrar su nombre en el TextBox
            if (dgProductos.SelectedItem != null)
            {
                DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;
                txtProductoBorrar.Text = filaSeleccionada["Producto"].ToString();
            }
        }

        private void MKP()
        {
            BaseDeDatos.VolcarDatosMKP();
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
            new DataColumn("categoria", typeof(string)),
            new DataColumn("convocatoria", typeof(string)),
            new DataColumn("codigo_convocatoria", typeof(string)),
            new DataColumn("provincia", typeof(string)),
            new DataColumn("localidad", typeof(string)),
            new DataColumn("nombre", typeof(string)),
            new DataColumn("apellidos", typeof(string)),
            new DataColumn("fecha_solicitud", typeof(string)),
            new DataColumn("estado_inscripcion", typeof(string)),
            new DataColumn("tipo_inscripcion", typeof(string)),
            new DataColumn("estado_matriculacion", typeof(string)),
            new DataColumn("email", typeof(string)),
            new DataColumn("telefono", typeof(string)),
            new DataColumn("nivel_estudios", typeof(string)),
            new DataColumn("sexo", typeof(string)),
            new DataColumn("fecha_nacimiento", typeof(string)),
            new DataColumn("dni", typeof(string)),
            new DataColumn("situacion_laboral", typeof(string)),
            new DataColumn("asistencia_remota", typeof(string)),
            new DataColumn("tablet", typeof(string)),
            new DataColumn("puntos", typeof(int))
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
                        nuevaFila["categoria"] = csv.GetField(0) ?? ""; // Usamos el índice 0 para la primera columna
                        nuevaFila["convocatoria"] = csv.GetField(1) ?? "";
                        nuevaFila["codigo_convocatoria"] = csv.GetField(2) ?? "";
                        nuevaFila["provincia"] = csv.GetField(3) ?? "";
                        nuevaFila["localidad"] = csv.GetField(4) ?? "";
                        nuevaFila["nombre"] = csv.GetField(5) ?? "";
                        nuevaFila["apellidos"] = csv.GetField(6) ?? "";

                        // Convertir fechas correctamente
                        nuevaFila["fecha_solicitud"] = (csv.GetField(7));
                        nuevaFila["estado_inscripcion"] = csv.GetField(8) ?? "";
                        nuevaFila["tipo_inscripcion"] = csv.GetField(9) ?? "";
                        nuevaFila["estado_matriculacion"] = csv.GetField(10) ?? "";
                        nuevaFila["email"] = csv.GetField(11) ?? "";
                        nuevaFila["telefono"] = csv.GetField(12) ?? "";
                        nuevaFila["nivel_estudios"] = csv.GetField(13) ?? "";
                        nuevaFila["sexo"] = csv.GetField(14) ?? "";

                        nuevaFila["fecha_nacimiento"] = (csv.GetField(17));
                        nuevaFila["dni"] = csv.GetField(18) ?? "";
                        nuevaFila["situacion_laboral"] = csv.GetField(19) ?? "";
                        nuevaFila["asistencia_remota"] = csv.GetField(20) ?? "";
                        nuevaFila["tablet"] = csv.GetField(21) ?? "";

                        nuevaFila["puntos"] = int.TryParse(csv.GetField(22), out int puntos) ? puntos : 0;

                        tablaFinal.Rows.Add(nuevaFila);
                    }
                }

                if (BaseDeDatos.InsertarDatosCSV(tablaFinal))
                {
                    MessageBox.Show("Datos insertados correctamente en la base de datos.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    BaseDeDatos.eliminarVaciosTemporal();
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
        private string ConvertirFecha(string fecha)
        {
            if (DateTime.TryParseExact(fecha, new[] { "dd/MM/yyyy", "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy" },
                                       CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaConvertida))
            {
                return fechaConvertida.ToString("dd/MM/yyyy HH:mm");
            }
            return "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Esto solo se debe hacer una vez por importación, si no, podría haber datos repetidos en la siguiente tabla",
                "Advertencia",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            MessageBoxResult result = MessageBox.Show("¿Está seguro de que quiere hacerlo?",
                                        "Confirmación",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Console.WriteLine("El usuario ha confirmado que quiere proceder");
                // Aquí iría el código para cuando el usuario dice Sí
                BaseDeDatos.VolcarDatosMKP();

            }
            else
            {
                Console.WriteLine("El usuario ha decidido no continuar con la operación");
                // Aquí iría el código para cuando el usuario dice No
            }

        }
    }
}