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
            SubirDatosCSVbis();
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
            new DataColumn("account_funds", typeof(int))
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
            BaseDeDatos.UnirYSumar();
            CargarProductos();

        }


    }
}
