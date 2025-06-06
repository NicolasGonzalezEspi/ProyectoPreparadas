using System;
using System.Data;
using System.IO;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text;

using PdfDocument = iTextSharp.text.Document;
using PdfParagraph = iTextSharp.text.Paragraph;
using PdfFont = iTextSharp.text.Font;
using PdfFontFactory = iTextSharp.text.FontFactory;
using PdfTable = iTextSharp.text.pdf.PdfPTable;
using PdfCell = iTextSharp.text.pdf.PdfPCell;
using PdfPhrase = iTextSharp.text.Phrase;
using PdfBaseColor = iTextSharp.text.BaseColor;




namespace trabajoFinalInterfaces
{
    public partial class Modificar : Page
    {
        public Modificar()
        {
            InitializeComponent();
            CargarProductos();
            ActualizarTotalRegistros();
        }

       

        private void CargarProductos()
        {
            DataTable productos = BaseDeDatos.MostrarMKPExportar();
            dgProductos.ItemsSource = productos.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int cont = 1;
            int totalActual = BaseDeDatos.ObtenerTotalRegistrosMKPExportar();
            if (totalActual < 200)
            {
                try
                {
                    DataTable datos = BaseDeDatos.MostrarMKPExportar();
                    if (datos == null || datos.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    string nombreArchivo = $"{cont}. Impotar_Al_MKP_{DateTime.Now.ToString("dd.MM.yyyy")}.csv";

                    // Mostrar cuadro de diálogo para guardar el archivo
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Archivos CSV (*.csv)|*.csv",
                        Title = "Guardar archivo CSV",
                        FileName = nombreArchivo
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string rutaArchivo = saveFileDialog.FileName;

                        // Escribir los datos en un archivo CSV
                        using (StreamWriter writer = new StreamWriter(rutaArchivo, false, new UTF8Encoding(false)))
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

                       // BaseDeDatos.EliminarMKPExportar();
                        MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        BaseDeDatos.LimpiarExportarMKP();
                        CargarProductos();
                        ActualizarTotalRegistros();
                  

                        var parametros = new Dictionary<string, object>
           {
               { "@fecha", DateTime.Now },
               { "@actividad", $"3A. Exportación de usuarios y puntos nº{cont} correcta." },
               { "@paso", "Pulse 3A hasta vaciar la tabla. Luego importe los archivos en el Marketplace. Entonces, pulse en Mover Nuevas Convos a BBDDGlobal." }
           };

                        BaseDeDatos.EjecutarQueryIncidencia(
                            "INSERT INTO actividad_usuarios (fecha_actividad_usuario, actividad, siguiente_paso) VALUES (@fecha, @actividad, @paso)",
                            parametros
                        );
                        cont++;
                        Main.ActualizarUltimaActividad();



                       // BaseDeDatos.VaciarTablasTemporales();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    // ❌ Registro de error (inserción fallida)
                    var parametros = new Dictionary<string, object>
           {
               { "@fecha", DateTime.Now },
               { "@actividad", $"3A. Exportación de usuarios y puntos incorrecta." },
               { "@paso", "Revise la conexión. Reinicie la aplicación." }
           };

                    BaseDeDatos.EjecutarQueryIncidencia(
                        "INSERT INTO actividad_usuarios (fecha_actividad_usuario, actividad, siguiente_paso) VALUES (@fecha, @actividad, @paso)",
                        parametros
                    );
                    Main.ActualizarUltimaActividad();

                }
            } else
            {
                try
                {
                    DataTable datos = BaseDeDatos.MostrarMKPExportarBis();
                    if (datos == null || datos.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    string nombreArchivo = $"{cont}. Impotar_Al_MKP_{DateTime.Now.ToString("dd.MM.yyyy")}.csv";

                    // Mostrar cuadro de diálogo para guardar el archivo
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Archivos CSV (*.csv)|*.csv",
                        Title = "Guardar archivo CSV",
                        FileName = nombreArchivo
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string rutaArchivo = saveFileDialog.FileName;

                        // Escribir los datos en un archivo CSV
                        using (StreamWriter writer = new StreamWriter(rutaArchivo, false, new UTF8Encoding(false)))
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
                        BaseDeDatos.Eliminar100MKPExportar();
                        MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarProductos();
                        ActualizarTotalRegistros();
                        var parametros = new Dictionary<string, object>
           {
               { "@fecha", DateTime.Now },
               { "@actividad", $"3A. Exportación de usuarios y puntos nº{cont} correcta." },
               { "@paso", "Pulse 3A hasta vaciar la tabla. Luego importe los archivos en el Marketplace. Entonces, pulse en Mover Nuevas Convos a BBDDGlobal." }
           };

                        BaseDeDatos.EjecutarQueryIncidencia(
                            "INSERT INTO actividad_usuarios (fecha_actividad_usuario, actividad, siguiente_paso) VALUES (@fecha, @actividad, @paso)",
                            parametros
                        );
                        cont++;
                        Main.ActualizarUltimaActividad();


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    var parametros = new Dictionary<string, object>
           {
               { "@fecha", DateTime.Now },
               { "@actividad", $"3A. Exportación de usuarios y puntos incorrecta." },
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


        private void ExportarPDF()
        {
            string fechaHora = DateTime.Now.ToString("HH-mm_dd-MM-yyyy");
            string nombreArchivo = $"Reporte_Completo_Usuarios_{fechaHora}.pdf";
            string rutaArchivo = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                nombreArchivo
            );

            Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 20f);

            try
            {
                using (FileStream fs = new FileStream(rutaArchivo, FileMode.Create))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    var tablas = new List<(string titulo, DataTable tabla)>
            {
                ("Actividad de Usuarios", BaseDeDatos.MostrarActividadProductosReciente()),
                ("Usuarios - Convocatoria", BaseDeDatos.MostrarTablaTemporalB()),
                ("Estado Marketplace", BaseDeDatos.MostrarMKP())
            };

                    for (int i = 0; i < tablas.Count; i++)
                    {
                        var (titulo, tabla) = tablas[i];
                        if (tabla.Rows.Count == 0) continue;

                        PdfParagraph parrafoTitulo = new PdfParagraph(titulo, new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                        parrafoTitulo.Alignment = Element.ALIGN_CENTER;
                        parrafoTitulo.SpacingAfter = 10f;
                        doc.Add(parrafoTitulo);

                        PdfPTable tablaPDF = new PdfPTable(tabla.Columns.Count);
                        tablaPDF.WidthPercentage = 100;

                        foreach (DataColumn col in tabla.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(col.ColumnName, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                            tablaPDF.AddCell(cell);
                        }

                        foreach (DataRow row in tabla.Rows)
                        {
                            foreach (var item in row.ItemArray)
                            {
                                tablaPDF.AddCell(new Phrase(item?.ToString() ?? ""));
                            }
                        }

                        doc.Add(tablaPDF);

                        if (i == 0)
                            doc.NewPage();
                        else
                            doc.Add(new Paragraph("\n\n")); // Espacio entre otras tablas
                    }

                    doc.Close();
                }

                MessageBox.Show($"PDF generado correctamente:\n{rutaArchivo}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void ActualizarTotalRegistros()
        {
            int totalRegistros = BaseDeDatos.ObtenerTotalRegistrosMKPExportar();
            txtTotalRegistros.Text = totalRegistros.ToString();
        }

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (BaseDeDatos.ObtenerTotalRegistrosMKPExportar() > 0)
            {
                MessageBox.Show("No puedes volcar los datos a BBDDGlobal. Antes debes Preparar Archivos (3A), e importarlos al Marketplace.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Mostrar ventana emergente para ingresar el DNI
            string fecha = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese la fecha con formato ../../.. Ejemplo: 12/12/25 :",
                "Ingrese la fecha",
                ""
            );

            // Si aún no hay producto, no hacer nada
            if (string.IsNullOrEmpty(fecha))
            {
                MessageBox.Show("El campo 'Fecha de Importación' no puede estar vacío. ../../.. Dia, mes, año. ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Validar si tiene 8 caracteres
            if (fecha.Length != 8)
            {
                MessageBox.Show("La fecha de importación debe tener 8 caracteres. El formato debe ser ../../.. Dia, mes, año. ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine("La fecha de importación debe tener 8 caracteres. El formato debe ser ../../..");
                return;
            }

            string input = fecha;
            string pattern = @"^\d{2}/\d{2}/\d{2}$";
            if (!Regex.IsMatch(input, pattern))
            {
                Console.WriteLine("Formato inválido: El formato debe ser NN/NN/NN, donde N son números.");
                MessageBox.Show("El campo 'Fecha de Importación' debe ser una fecha con el siguiente formato ../../.. Dia, mes, año. ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult confirmacion = MessageBox.Show($"¿Está seguro de que desea volcar los datos? \"{fecha}\"?",
                                                     "Confirmar volcado",
                                                     MessageBoxButton.YesNo,
                                                     MessageBoxImage.Warning);


            if (confirmacion == MessageBoxResult.Yes)
            {
                bool eliminado = BaseDeDatos.VolcarDatos(fecha);
                if (eliminado)
                {
                    MessageBox.Show("Datos volcados correctamente. ", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarProductos();


                    var parametros = new Dictionary<string, object>
           {
               { "@fecha", DateTime.Now },
               { "@actividad", $"3B. Datos almacenados en BBDDGlobal para su posterior consulta." },
               { "@paso", "Proceso completado. Hora de importar nuevas convocatorias." }
           };

                    BaseDeDatos.EjecutarQueryIncidencia(
                        "INSERT INTO actividad_usuarios (fecha_actividad_usuario, actividad, siguiente_paso) VALUES (@fecha, @actividad, @paso)",
                        parametros
                    );
                    Main.ActualizarUltimaActividad();
                    ExportarPDF();
                    BaseDeDatos.VaciarTablasTemporales();
                }
                else
                {
                    MessageBox.Show("No se pudo volcar...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    var parametros = new Dictionary<string, object>
           {
               { "@fecha", DateTime.Now },
               { "@actividad", $"3B. Error en el volcado de datos a BBDDGlobal." },
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
}
