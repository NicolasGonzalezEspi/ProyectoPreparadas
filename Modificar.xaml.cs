using System;
using System.Data;
using System.IO;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;





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

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
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
                }
                else
                {
                    MessageBox.Show("No se pudo volcar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }



        }
    }
}
