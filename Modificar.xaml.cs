using System;
using System.Data;
using System.IO;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MySql.Data.MySqlClient;




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

                        BaseDeDatos.EliminarMKPExportar();
                        MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarProductos();
                        ActualizarTotalRegistros();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
 
        }
        private void ActualizarTotalRegistros()
        {
            int totalRegistros = BaseDeDatos.ObtenerTotalRegistrosMKPExportar();
            txtTotalRegistros.Text = totalRegistros.ToString();
        }


    }
}
