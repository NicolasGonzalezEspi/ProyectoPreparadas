using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    /// <summary>
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : Page
    {
        public Reportes()
        {
            InitializeComponent();
        }

        private void ExportarPDF_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los datos desde la base de datos
            DataTable data = BaseDeDatos.MostrarActividadProductos();

            if (data.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear el documento
            Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 20f);
            string rutaArchivo = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Reporte_Actividad.pdf");

            try
            {
                using (FileStream fs = new FileStream(rutaArchivo, FileMode.Create))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // Título
                    PdfParagraph titulo = new PdfParagraph("Reporte de Actividad de Usuarios", new PdfFont(PdfFont.FontFamily.HELVETICA, 18, PdfFont.BOLD));
                    titulo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(titulo);
                    doc.Add(new PdfParagraph("\n"));

                    // Tabla PDF
                    PdfPTable tabla = new PdfPTable(data.Columns.Count);
                    tabla.WidthPercentage = 100;

                    // Encabezados
                    foreach (DataColumn col in data.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(col.ColumnName, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tabla.AddCell(cell);
                    }

                    // Datos
                    foreach (DataRow row in data.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            tabla.AddCell(new Phrase(item?.ToString() ?? ""));
                        }
                    }

                    doc.Add(tabla);
                    doc.Close();
                }

                MessageBox.Show($"PDF generado correctamente:\n{rutaArchivo}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
