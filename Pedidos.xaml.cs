using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Win32;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.IO;
using ClosedXML.Excel;
using System.Diagnostics;

namespace trabajoFinalInterfaces
{
    /// <summary>
    /// Lógica de interacción para Pedidos.xaml
    /// </summary>
    public partial class Pedidos : Page
    {
        private static string archivoExcelPath;
        private string archivoCSVPath;
        public static string[] abonos = new string[4];
        public static string[] trackings = new string[5];
        public static string[] facturas = new string[5];
        private Dictionary<string, string> skuModeloDiccionario;


        public Pedidos()
        {
            InitializeComponent();
            CargarPedidos();

            //  chkVistaOptimizada.IsChecked = true;
            //  CheckBox_Checked(chkVistaOptimizada, null);
        }


        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
        private void CargarPedidos()
        {
            DataTable productos = BaseDeDatos.MostrarTodosPedidos();
            dgProductos.ItemsSource = productos.DefaultView;
        }

        //desactivable a voluntad
        private void dgProductos_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null)
                return;

            // Obtener la celda actual (seleccionada)
            var currentCell = dataGrid.CurrentCell;
            if (currentCell == null || currentCell.Column == null)
                return;

            // Filtrar las celdas para mantener solo la que está actualmente activa
            e.ClipboardRowContent.RemoveAll(cell =>
                cell.Column != currentCell.Column);
        }

        private void BtnSeleccionarArchivoCSVClick(object sender, RoutedEventArgs e)
        {
            skuModeloDiccionario = BaseDeDatos.ObtenerDiccionarioSKUModelo();

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

        private void SubirDatosCSV()
        {
            if (string.IsNullOrEmpty(archivoCSVPath))
            {
                MessageBox.Show("Selecciona un archivo CSV válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Se define el DataTable con los nombres de columna de la tabla de base de datos actualizada.
                var tablaFinal = new DataTable();
                tablaFinal.Columns.AddRange(new DataColumn[]
                {
            new DataColumn("id_pedido_mk", typeof(string)),
            new DataColumn("fecha_pedido", typeof(string)),    // Nota: se recomienda convertir este campo a DATE antes de la inserción
            new DataColumn("nombre_completo", typeof(string)),
            new DataColumn("direccion", typeof(string)),
            new DataColumn("codigo_postal", typeof(string)),
            new DataColumn("ciudad", typeof(string)),
            new DataColumn("provincia", typeof(string)),
            new DataColumn("idUsuario", typeof(string)),
            new DataColumn("nota_envio", typeof(string)),
            new DataColumn("telefono", typeof(string)),
            new DataColumn("email", typeof(string)),
            new DataColumn("SKU", typeof(string)),
            new DataColumn("proveedor", typeof(string)),
            new DataColumn("uds", typeof(string)),
            new DataColumn("nombre_articulo", typeof(string)),
            new DataColumn("puntosg", typeof(int)),
            new DataColumn("modelo", typeof(string)),
            new DataColumn("estado_pedido", typeof(string)),
            new DataColumn("tracking", typeof(string)),
            new DataColumn("fecha_gestion", typeof(string)),
            new DataColumn("fecha_inicio_transito", typeof(string)),
            new DataColumn("fecha_entrega_alumna", typeof(string)),
            new DataColumn("facturadmi_snlosllanos", typeof(string)),
            new DataColumn("fecha_factura", typeof(string)),
            new DataColumn("factura_sin_iva", typeof(string)),
            new DataColumn("abono", typeof(string)),
            new DataColumn("fecha_abono", typeof(string)),
            new DataColumn("abono_sin_iva", typeof(string)),
            new DataColumn("info_incidencia", typeof(string))
                });

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Encoding = new UTF8Encoding(false), // Codificación UTF-8 sin BOM
                    Delimiter = ";",
                    TrimOptions = TrimOptions.Trim,
                    MissingFieldFound = null,
                    HeaderValidated = null,
                    IgnoreBlankLines = true
                };

                using (var reader = new StreamReader(archivoCSVPath, new UTF8Encoding(false)))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        var nuevaFila = tablaFinal.NewRow();

                        // Asignación de campos que vienen del CSV (los índices se mantienen tal y como indicas)
                        nuevaFila["id_pedido_mk"] = csv.GetField(0) ?? "";
                        nuevaFila["fecha_Pedido"] = csv.GetField(1) ?? "";
                        nuevaFila["nombre_completo"] = csv.GetField(2) ?? "";
                        nuevaFila["direccion"] = csv.GetField(3) ?? "";
                        nuevaFila["codigo_postal"] = csv.GetField(4) ?? "";
                        nuevaFila["ciudad"] = csv.GetField(5) ?? "";
                        nuevaFila["provincia"] = csv.GetField(6) ?? "";
                        nuevaFila["idUsuario"] = csv.GetField(7) ?? "";
                        nuevaFila["nota_envio"] = csv.GetField(8) ?? "";
                        nuevaFila["telefono"] = csv.GetField(9) ?? "";
                        nuevaFila["email"] = csv.GetField(10) ?? "";

                        // SKU y proveedor derivado del SKU
                        string sku = csv.GetField(11);
                        nuevaFila["SKU"] = sku;
                        if (!string.IsNullOrEmpty(sku))
                        {
                            nuevaFila["proveedor"] = sku.StartsWith("D") ? "DMI" :
                                                     sku.StartsWith("S") ? "Solutia" : "";
                        }
                        else
                        {
                            nuevaFila["proveedor"] = "";
                        }

                        // Continuación de la asignación según índices
                        nuevaFila["uds"] = csv.GetField(13) ?? "";
                        nuevaFila["nombre_articulo"] = csv.GetField(14) ?? "";
                        // Se asume que en el CSV el campo puntosg es una cadena que se puede parsear a entero.
                        nuevaFila["puntosg"] = !string.IsNullOrEmpty(csv.GetField(15)) ? int.Parse(csv.GetField(15)) : 0;
                        // Buscar el modelo a partir del SKU en el diccionario
                        if (!string.IsNullOrEmpty(sku) && skuModeloDiccionario.ContainsKey(sku))
                        {
                            nuevaFila["modelo"] = skuModeloDiccionario[sku];
                        }
                        else
                        {
                            nuevaFila["modelo"] = ""; // o algún valor por defecto
                        }

                        nuevaFila["estado_pedido"] = csv.GetField(17) ?? "";

                        // Columnas nuevas: tracking, fecha_gestion, fecha_inicio_transito, fecha_entrega_alumna,
                        // facturadmi_snlosllanos, fecha_factura, factura_sin_iva, abono, fecha_abono, abono_sin_iva, info_incidencia.
                        // Dado que no vienen en el CSV se asignan como cadena vacía (puedes ajustar si las obtienes de otro campo o convertir formatos)
                        nuevaFila["tracking"] = "";
                        nuevaFila["fecha_gestion"]=null;
                        nuevaFila["fecha_inicio_transito"] = null;
                        nuevaFila["fecha_entrega_alumna"] = null;
                        nuevaFila["facturadmi_snlosllanos"] = "";
                        nuevaFila["fecha_factura"] = null;
                        nuevaFila["factura_sin_iva"] = null;
                        nuevaFila["abono"] = "";
                        nuevaFila["fecha_abono"] = null;
                        nuevaFila["abono_sin_iva"] = null;
                        nuevaFila["info_incidencia"] = "";  // O, si está en el CSV, ajusta el índice correspondiente

                        tablaFinal.Rows.Add(nuevaFila);
                    }
                }

                if (BaseDeDatos.InsertarDatosCSVPedidos2(tablaFinal))
                {
                    if (BaseDeDatos.situacionRepetidosYCorrectos == true)
                    {
                        MessageBox.Show("Hubo "+BaseDeDatos.correctos+" inserciones correctas.\nOtras "+BaseDeDatos.repetidos+" inserciones no completadas, por repetición.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        // BaseDeDatos.eliminarVaciosTemporal();
                        CargarPedidos();
                        BaseDeDatos.situacionRepetidosYCorrectos=false;
                    } else
                    {
                        MessageBox.Show("Datos insertados correctamente en la base de datos.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        // BaseDeDatos.eliminarVaciosTemporal();
                        CargarPedidos();
                    }

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

        private void CumplimentarAbono(object sender, RoutedEventArgs e)
        {

            if (dgProductos.SelectedItems.Count == 1)
            {
                DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;
                abonos[0]=  filaSeleccionada["id_pedido_mk"].ToString();
                abonos[1]=  filaSeleccionada["abono"].ToString();
                abonos[2]=  filaSeleccionada["fecha_abono"].ToString();
                abonos[3]=  filaSeleccionada["abono_sin_iva"].ToString();
                var ventana = new CrearAbono();
                bool? resultadoVentana = ventana.ShowDialog();

                if (resultadoVentana == true)
                {
                    CargarPedidos();
                }

            }
        }

        private void CumplimentarTracking(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItems.Count == 1)
            {
                DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;
                trackings[0] = filaSeleccionada["id_pedido_mk"].ToString();
                trackings[1] = filaSeleccionada["fecha_gestion"].ToString();
                trackings[2] = filaSeleccionada["tracking"].ToString();
                trackings[3] = filaSeleccionada["fecha_inicio_transito"].ToString();
                trackings[4] = filaSeleccionada["fecha_entrega_alumna"].ToString();
                var ventana = new CrearTracking();
                bool? resultadoVentana = ventana.ShowDialog();

                if (resultadoVentana == true)
                {
                    CargarPedidos();
                }

            }
        }





        private void EditarDireccion(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem != null)
            {
                DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;
                string direccionAnterior = filaSeleccionada["direccion"].ToString();

                var ventana = new CrearDireccion(direccionAnterior);
                bool? resultadoVentana = ventana.ShowDialog();

                if (resultadoVentana == true && !string.IsNullOrWhiteSpace(ventana.IncidenciaTexto)) // <- AQUÍ el cambio importante
                {
                    string nuevaDireccion = ventana.IncidenciaTexto; // <- AQUÍ también

                    string consulta = "UPDATE pedidos SET direccion = @direccion WHERE id_pedido_mk = @id_pedido_mk;";

                    Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@direccion", nuevaDireccion },
                { "@id_pedido_mk", filaSeleccionada["id_pedido_mk"].ToString() }
            };

                    bool resultado = BaseDeDatos.EjecutarQueryIncidencia(consulta, parametros);

                    if (resultado)
                    {
                        CargarPedidos();
                        MessageBox.Show("Dirección actualizada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar la dirección.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar una nueva dirección.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un pedido de la lista.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void CrearIncidencia(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem != null)
            {
                DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;

                string incidenciaAnterior = filaSeleccionada["info_incidencia"].ToString(); // o el campo que tengas

                // Mostrar la ventana personalizada con el texto ya existente
                var ventana = new CrearIncidencia(incidenciaAnterior);
                bool? resultadoVentana = ventana.ShowDialog();

                if (resultadoVentana == true && !string.IsNullOrWhiteSpace(ventana.IncidenciaTexto))
                {
                    string incidenciaIngresada = ventana.IncidenciaTexto;

                    string consulta = @"
            INSERT INTO incidencias (id_pedido_mk_i, fecha_Pedido, direccion, proveedor, nombre_articulo, 
                                     fecha_notificacion, fecha_gestion, incidencia, estado, solucion) 
            VALUES (@id_pedido_mk, @fecha_Pedido, @direccion, @proveedor, @nombre_articulo, 
                    '', '', @incidencia, '', '') 
            ON DUPLICATE KEY UPDATE 
            incidencia = VALUES(incidencia);";

                    Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                {"@id_pedido_mk", filaSeleccionada["id_pedido_mk"].ToString()},
                {"@fecha_Pedido", filaSeleccionada["fecha_Pedido"].ToString()},
                {"@direccion", filaSeleccionada["direccion"].ToString()},
                {"@proveedor", filaSeleccionada["proveedor"].ToString()},
                {"@nombre_articulo", filaSeleccionada["nombre_articulo"].ToString()},
                {"@incidencia", incidenciaIngresada}
            };

                    bool resultado = BaseDeDatos.EjecutarQueryIncidencia(consulta, parametros);

                    if (resultado && BaseDeDatos.EjecutarUpdateIncidencia("UPDATE pedidos p INNER JOIN incidencias i ON p.id_pedido_mk = i.id_pedido_mk_i SET p.info_incidencia = i.incidencia;"))
                    {
                        CargarPedidos();
                        MessageBox.Show("Incidencia registrada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar la incidencia.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar una incidencia.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un pedido de la lista.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Anadir_SN(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItems.Count == 0)
            {
                MessageBox.Show("No hay ningún registro seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (dgProductos.SelectedItems.Count > 1)
            {
                MessageBox.Show("Por favor, seleccione solo un registro.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;

            String id_pedido_mk = filaSeleccionada["id_pedido_mk"].ToString();

            string facturadmi_snlosllanos = "SN: " + Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el Serial Number:",
                "Añadir SN",
                ""
            );
            if (facturadmi_snlosllanos.Length <= 4)
            {
              //  MessageBox.Show("Debe ingresar número de serie.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string consulta = $"UPDATE pedidos SET facturadmi_snlosllanos = '{facturadmi_snlosllanos}' WHERE id_pedido_mk = '{id_pedido_mk}';";
            bool exito = BaseDeDatos.EjecutarUpdateIncidencia(consulta);
            if (exito)
            {
                MessageBox.Show("Número de serie añadido.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarPedidos();
            }

        }
        private void Factura_SN(object sender, RoutedEventArgs e)
        {
                
                
                if (dgProductos.SelectedItems.Count == 0)
            {
                MessageBox.Show("No hay ningún registro seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (dgProductos.SelectedItems.Count > 1)
            {
                MessageBox.Show("Por favor, seleccione solo un registro.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;



            MessageBoxResult resultado = MessageBox.Show(
                "¿Tiene este registro Factura?\n Si indica No, saldrá ventana para añadir SN correspondiente.",
                "Confirmar",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (resultado == MessageBoxResult.Yes)
            {
                // { "@id_pedido_mk", filaSeleccionada["id_pedido_mk"].ToString() }
                String id_pedido_mk = filaSeleccionada["id_pedido_mk"].ToString();
                string facturadmi_snlosllanos = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el número de factura:",
                "Añadir nº de Factura",
                ""
            );
                if (string.IsNullOrEmpty(facturadmi_snlosllanos))
                {
                    MessageBox.Show("Debe ingresar número de factura.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string consulta = $"UPDATE pedidos SET facturadmi_snlosllanos = '{facturadmi_snlosllanos}' WHERE id_pedido_mk = '{id_pedido_mk}';";

                bool exito =   BaseDeDatos.EjecutarUpdateIncidencia(consulta);
                if(exito) {
                    //MessageBox.Show("Factura añadida correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarPedidos();


                    string fechafactura = Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingrese la fecha de la factura (ejemplo 13-06-2025):",
                    "Añadir fecha de Factura",
                    ""
                    );
                    fechafactura= CrearTracking.FormatearFecha(fechafactura);
                    consulta = $"UPDATE pedidos SET fecha_factura = '{fechafactura}' WHERE id_pedido_mk = '{id_pedido_mk}';";

                    if(BaseDeDatos.EjecutarUpdateIncidencia(consulta)){

                        CargarPedidos(); 
                        string factura_sin_ivaS = Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingrese el precio de factura Sin IVA (ejemplo: 99,99):",
                    "Añadir Factura Sin IVA",
                    ""
                    );
                        factura_sin_ivaS = factura_sin_ivaS.Replace('.', ',');



                        if (!decimal.TryParse(factura_sin_ivaS, out decimal factura_sin_iva))
                        {
                            MessageBox.Show("El valor ingresado no es un número válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }


                        consulta = $"UPDATE pedidos SET factura_sin_iva = {factura_sin_iva.ToString(CultureInfo.InvariantCulture)} WHERE id_pedido_mk = '{id_pedido_mk}';";

                        if (BaseDeDatos.EjecutarUpdateIncidencia(consulta))
                        {
                            MessageBox.Show("Datos añadidos correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                            CargarPedidos();
                        }




                    }





                }
            }
            if (resultado == MessageBoxResult.No)
            {
                String id_pedido_mk = filaSeleccionada["id_pedido_mk"].ToString();

                string facturadmi_snlosllanos ="SN: "+ Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingrese el Serial Number:",
                    "Añadir SN",
                    ""
                );
                if (facturadmi_snlosllanos.Length <= 4)
                {
                    MessageBox.Show("Debe ingresar número de serie.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string consulta = $"UPDATE pedidos SET facturadmi_snlosllanos = '{facturadmi_snlosllanos}' WHERE id_pedido_mk = '{id_pedido_mk}';";
                bool exito = BaseDeDatos.EjecutarUpdateIncidencia(consulta);
                if (exito)
                {
                    MessageBox.Show("Número de serie añadido.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarPedidos();
                }
            }
        }
        /*
         * 
         */




        private void filtrarPor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filtrarPor.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedValue = selectedItem.Content.ToString();

                switch (selectedValue)
                {
                    case "Buscar por:":
                        //CargarPedidos();
                        break;
                    case "IdPedido":
                        BuscarIdPedido();
                        break;
                    case "Nombre":
                        BuscarNombre();
                        break;
                    case "Dirección":
                        BuscarDireccion();
                        break;
                    case "Email":
                        BuscarEmail();
                        break;
                    case "SN":
                        BuscarSN();
                        break;
                    case "Nº Factura":
                        BuscarNFactura();
                        break;
                }
            }
            filtrarPor.SelectedIndex = 0;

        }

        private void PedidosSN()
        {
            DataTable productos = BaseDeDatos.MostrarTodosPedidosSN();
            dgProductos.ItemsSource = productos.DefaultView;
        }
        private void PedidosFactura()
        {
            DataTable productos = BaseDeDatos.MostrarTodosPedidosFactura();
            dgProductos.ItemsSource = productos.DefaultView;
        }
        private void PedidosSinSNFactura()
        {
            DataTable productos = BaseDeDatos.MostrarTodosPedidosSinSNoFactura();
            dgProductos.ItemsSource = productos.DefaultView;
        }

        private void PedidosSolutia()
        {
            DataTable productos = BaseDeDatos.MostrarTodosPedidosSolutia();
            dgProductos.ItemsSource = productos.DefaultView;
        }
        private void PedidosDMI()
        {
            DataTable productos = BaseDeDatos.MostrarTodosPedidosDMI();
            dgProductos.ItemsSource = productos.DefaultView;
        }


        private void filtrarPor_SelectionChangedV2(object sender, SelectionChangedEventArgs e)
        {
            if (filtrarPorV2.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedValue = selectedItem.Content.ToString();

                switch (selectedValue)
                {
                    case "Filtrar por:":
                       // CargarPedidos();
                        break;
                    case "Con SN":
                        PedidosSN();
                        break;
                    case "Con Nº Fact":
                        PedidosFactura();
                        break;       
                    case "Sin SN/NºFact":
                        PedidosSinSNFactura();
                        break;     
                    case "Mostrar Todos":
                        CargarPedidos();
                        break;
                    case "Solutia":
                        PedidosSolutia();
                        break;
                    case "DMI":
                        PedidosDMI();
                        break;
                }
            }
            filtrarPorV2.SelectedIndex = 0;

        }



        private void BuscarIdPedido()
        {
            // Mostrar ventana emergente para ingresar el DNI
            string dniIngresado = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el IDPedido:",
                "Buscar por ID_Pedido",
                ""
            );



            // Verificar que el DNI no esté vacío
            if (!string.IsNullOrWhiteSpace(dniIngresado))
            {
                DataTable resultado = BaseDeDatos.MostrarPedidosPorId(dniIngresado);

                if (resultado.Rows.Count > 0)
                {
                    MessageBox.Show("Datos encontrados.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataTable productos = BaseDeDatos.MostrarPedidosPorId(dniIngresado); // llamada a la función determinada de  baseDeDatos
                    dgProductos.ItemsSource = productos.DefaultView; //volcamos los datos al datagrid (la tabla)

                    // Aquí puedes mostrar los datos en una tabla o en un ListView si lo deseas
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para el idpedido ingresado.", "Sin resultados", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un idpedido válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BuscarNombre()
        {
            // Mostrar ventana emergente para ingresar el DNI
            string dniIngresado = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el Nombre:",
                "Buscar por Nombre",
                ""
            );



            // Verificar que el DNI no esté vacío
            if (!string.IsNullOrWhiteSpace(dniIngresado))
            {
                DataTable resultado = BaseDeDatos.MostrarPedidosPorNombre(dniIngresado);

                if (resultado.Rows.Count > 0)
                {
                    MessageBox.Show("Datos encontrados.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataTable productos = BaseDeDatos.MostrarPedidosPorNombre(dniIngresado); // llamada a la función determinada de  baseDeDatos
                    dgProductos.ItemsSource = productos.DefaultView; //volcamos los datos al datagrid (la tabla)

                    // Aquí puedes mostrar los datos en una tabla o en un ListView si lo deseas
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para el nombre ingresado.", "Sin resultados", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un idpedido válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BuscarDireccion()
        {
            // Mostrar ventana emergente para ingresar el DNI
            string dniIngresado = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese la direccion:",
                "Buscar por Dirección",
                ""
            );



            // Verificar que el DNI no esté vacío
            if (!string.IsNullOrWhiteSpace(dniIngresado))
            {
                DataTable resultado = BaseDeDatos.MostrarPedidosPorDireccion(dniIngresado);

                if (resultado.Rows.Count > 0)
                {
                    MessageBox.Show("Datos encontrados.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                //    DataTable productos = BaseDeDatos.MostrarPedidosPorDireccion(dniIngresado); // llamada a la función determinada de  baseDeDatos
                    dgProductos.ItemsSource = resultado.DefaultView; //volcamos los datos al datagrid (la tabla)

                    // Aquí puedes mostrar los datos en una tabla o en un ListView si lo deseas
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para tal dirección..", "Sin resultados", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un registro válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BuscarEmail()
        {
            // Mostrar ventana emergente para ingresar el DNI
            string dniIngresado = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el email:",
                "Buscar por email",
                ""
            );



            // Verificar que el DNI no esté vacío
            if (!string.IsNullOrWhiteSpace(dniIngresado))
            {
                DataTable resultado = BaseDeDatos.MostrarPedidosPorEmail(dniIngresado);

                if (resultado.Rows.Count > 0)
                {
                    MessageBox.Show("Datos encontrados.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataTable productos = BaseDeDatos.MostrarPedidosPorEmail(dniIngresado); // llamada a la función determinada de  baseDeDatos
                    dgProductos.ItemsSource = productos.DefaultView; //volcamos los datos al datagrid (la tabla)

                    // Aquí puedes mostrar los datos en una tabla o en un ListView si lo deseas
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para tal email.", "Sin resultados", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un registro válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BuscarSN()
        {
            // Mostrar ventana emergente para ingresar el DNI
            string dniIngresado ="SN: "+ Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el número de serie:",
                "Buscar por SN",
                ""
            );



            // Verificar que el DNI no esté vacío
            if (!string.IsNullOrWhiteSpace(dniIngresado))
            {
                DataTable resultado = BaseDeDatos.MostrarPedidosPorSN(dniIngresado);

                if (resultado.Rows.Count > 0)
                {
                    MessageBox.Show("Datos encontrados.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataTable productos = BaseDeDatos.MostrarPedidosPorSN(dniIngresado); // llamada a la función determinada de  baseDeDatos
                    dgProductos.ItemsSource = productos.DefaultView; //volcamos los datos al datagrid (la tabla)

                    // Aquí puedes mostrar los datos en una tabla o en un ListView si lo deseas
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para tal SN.", "Sin resultados", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un registro válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BuscarNFactura()
        {
            // Mostrar ventana emergente para ingresar el DNI
            string dniIngresado = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el número de factura:",
                "Buscar por Nº Factura",
                ""
            );



            // Verificar que el DNI no esté vacío
            if (!string.IsNullOrWhiteSpace(dniIngresado))
            {
                DataTable resultado = BaseDeDatos.MostrarPedidosPorSN(dniIngresado);

                if (resultado.Rows.Count > 0)
                {
                    MessageBox.Show("Datos encontrados.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataTable productos = BaseDeDatos.MostrarPedidosPorSN(dniIngresado); // llamada a la función determinada de  baseDeDatos
                    dgProductos.ItemsSource = productos.DefaultView; //volcamos los datos al datagrid (la tabla)

                    // Aquí puedes mostrar los datos en una tabla o en un ListView si lo deseas
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para tal SN.", "Sin resultados", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un registro válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        private void Vista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Vista_Por.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedValue = selectedItem.Content.ToString();

                switch (selectedValue)
                {
                    case "Vista Optimizada":
                        VistaOptimizada();
                        break;
                    case "Vista Completa":
                        VistaCompleta();
                        break;
                    case "Vista Tracking":
                        VistaTracking();
                        break;
                    case "Vista Factura/Abono":
                        VistaPrecios();
                        break;   
                    case "Vista Dirección":
                        VistaDireccion();
                        break;
                }
            }
        }
        private void VistaDireccion()
        {
            //  MessageBox.Show("Vista Optimizada");
            VistaEstandar();

            MostrarSoloColumnas(new List<string>
    {
        "ID Pedido MK",
        "Fecha Pedido",
        "Nombre Completo",
        "Dirección",
        "Cod. Postal",
        "Ciudad",
        "Provincia",
        "Nota Envío",
        "Teléfono",
        "Email"
    });

            AjustarAncho("ID Pedido MK", 0.45);
            AjustarAncho("Fecha Pedido", 1.15);
            AjustarAncho("Cod. Postal", 0.43);
            AjustarAncho("Provincia", 0.35);
            AjustarAncho("Dirección", 2.35);
            AjustarAncho("Nombre Completo", 1.7);
        }


        private void VistaEstandar()
        {
            foreach (var column in dgProductos.Columns)
            {
                column.Visibility = Visibility.Visible;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star); // aplica a todos
            }
        }
        private void VistaCompleta()
        {
            VistaEstandar();
          //  MessageBox.Show("Vista Completa");
            // No se oculta nada, ya que muestra todo
        }

        private void VistaOptimizada()
        {
          //  MessageBox.Show("Vista Optimizada");
            VistaEstandar();

            OcultarColumnas(new List<string>
    {
        "Fecha Gestión",
        "Fecha Inicio Tránsito",
        "Fecha Entrega",
        "Fact/SN",
        "Fecha Factura",
        "Importe sin IVA",
        "Abono",
        "Fecha Abono",
        "Abono sin IVA",
        "ID Usuario"
    });

            AjustarAncho("Dirección", 2.35);
            AjustarAncho("Nombre Completo", 2.15);
        }

        private void VistaTracking()
        {
          //  MessageBox.Show("Vista Tracking");

            VistaEstandar();

            MostrarSoloColumnas(new List<string>
    {
        "ID Pedido MK",
        "Nombre Completo",
        "Tracking",
        "Fecha Gestión",
        "Fecha Inicio Tránsito",
        "Fecha Entrega"
    });
        }

        private void VistaPrecios()
        {
           // MessageBox.Show("Vista Precios");

            VistaEstandar();

            MostrarSoloColumnas(new List<string>
    {
        "ID Pedido MK",
        "Fact/SN",
        "Fecha Factura",
        "Importe sin IVA",
        "Abono",
        "Fecha Abono",
        "Abono sin IVA"
    });
        }

        private void OcultarColumnas(List<string> headers)
        {
            foreach (var column in dgProductos.Columns)
            {
                if (headers.Contains(column.Header?.ToString()))
                {
                    column.Visibility = Visibility.Hidden;
                }
            }
        }

        private void MostrarSoloColumnas(List<string> headersAMostrar)
        {
            foreach (var column in dgProductos.Columns)
            {
                column.Visibility = headersAMostrar.Contains(column.Header?.ToString()) ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void AjustarAncho(string header, double proportion)
        {
            foreach (var column in dgProductos.Columns)
            {
                if (column.Header?.ToString() == header)
                {
                    column.Width = new DataGridLength(proportion, DataGridLengthUnitType.Star);
                }
            }
        }


        /* vista optimizada
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var column in dgProductos.Columns)
            {
                if (column.Header != null)
                {
                    string header = column.Header.ToString();

                    if (header == "Tracking" ||
                        header == "Fecha Gestión" ||
                        header == "Fecha Inicio Tránsito" ||
                        header == "Fecha Entrega" ||
                        header == "Factura DMI" ||
                        header == "Importe sin IVA")
                    {
                        column.Visibility = Visibility.Hidden;
                    }

                    if (header == "Dirección")
                    {
                        column.Width = new DataGridLength(2.35, DataGridLengthUnitType.Star);
                    }
                }
            }
        }
        vista completa
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var column in dgProductos.Columns)
            {
                if (column.Header != null)
                {
                    string header = column.Header.ToString();

                    if (header == "Tracking" ||
                        header == "Fecha Gestión" ||
                        header == "Fecha Inicio Tránsito" ||
                        header == "Fecha Entrega" ||
                        header == "Factura DMI" ||
                        header == "Importe sin IVA")
                    {
                        column.Visibility = Visibility.Visible;
                    }

                    if (header == "Dirección")
                    {
                        column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    }
                }
            }
        }
        */

        /*
        private void VistaOptimizadaAntigua()
        {
            foreach (var column in dgProductos.Columns)
            {
                if (column.Header != null)
                {
                    string header = column.Header.ToString();

                    if (header == "Tracking" ||
                        header == "Fecha Gestión" ||
                        header == "Fecha Inicio Tránsito" ||
                        header == "Fecha Entrega" ||
                        header == "Factura DMI" ||
                        header == "Importe sin IVA")
                    {
                        column.Visibility = Visibility.Hidden;
                    }

                    if (header == "Dirección")
                    {
                        column.Width = new DataGridLength(2.35, DataGridLengthUnitType.Star);
                    }
                }
            }
        }

        private void VistaCompleta()
        {
            foreach (var column in dgProductos.Columns)
            {
                if (column.Header != null)
                {
                    string header = column.Header.ToString();

                    if (header == "Tracking" ||
                        header == "Fecha Gestión" ||
                        header == "Fecha Inicio Tránsito" ||
                        header == "Fecha Entrega" ||
                        header == "Factura DMI" ||
                        header == "Importe sin IVA")
                    {
                        column.Visibility = Visibility.Visible;
                    }

                    if (header == "Dirección")
                    {
                        column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    }
                }
            }
        }*/

        private void ExportarCSVaAPI(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable datos = BaseDeDatos.MostrarTodosPedidosDMIProcesando();
                if (datos == null || datos.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }


                string nombreArchivo = $"Pedidos_DMI_{DateTime.Now.ToString("dd.MM.yyyy")}.csv";

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
                        writer.WriteLine(string.Join(";", columnas));

                        // Escribir filas
                        foreach (DataRow fila in datos.Rows)
                        {
                            string[] valores = fila.ItemArray.Select(val => val.ToString()).ToArray();
                            writer.WriteLine(string.Join(";", valores));
                        }
                    }

                    DateTime fechaHoy = DateTime.Now.Date;
                    string fechaFormatoAmericano = fechaHoy.ToString("yyyy-MM-dd");
                    String consulta2 = $"UPDATE pedidos SET estado_pedido = 'Gestionado', fecha_gestion='{fechaFormatoAmericano}'  WHERE proveedor = 'DMI' AND estado_pedido = 'Procesando';";
                    BaseDeDatos.EjecutarUpdateIncidencia(consulta2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            CargarPedidos();
        }
        private void ExportarCSVaAPIv2(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable datos = BaseDeDatos.MostrarTodosPedidosSolutiaProcesando();
                if (datos == null || datos.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                string nombreArchivo = $"Pedidos_Solutia_{DateTime.Now.ToString("dd.MM.yyyy")}.csv";

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
                        writer.WriteLine(string.Join(";", columnas));

                        // Escribir filas
                        foreach (DataRow fila in datos.Rows)
                        {
                            string[] valores = fila.ItemArray.Select(val => val.ToString()).ToArray();
                            writer.WriteLine(string.Join(";", valores));
                        }
                    }
                    DateTime fechaHoy = DateTime.Now.Date;
                    string fechaFormatoAmericano = fechaHoy.ToString("yyyy-MM-dd");
                    String consulta2 = $"UPDATE pedidos SET estado_pedido = 'Gestionado', fecha_gestion='{fechaFormatoAmericano}'  WHERE proveedor = 'Solutia' AND estado_pedido = 'Procesando';";
                    BaseDeDatos.EjecutarUpdateIncidencia(consulta2);

                    MessageBox.Show("Datos exportados correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarPedidos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CambiarEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dgProductos.SelectedItems.Count == 0)
            {
              //  MessageBox.Show("No hay ningún registro seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (dgProductos.SelectedItems.Count > 1)
            {
             //   MessageBox.Show("Por favor, seleccione solo un registro.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;
            String id_pedido_mk = filaSeleccionada["id_pedido_mk"].ToString();


            if (Cambiar_Estado.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedValue = selectedItem.Content.ToString();

                switch (selectedValue)
                {
                    case "Cambiar Estado":
                        
                        break;
                    case "Procesando":
                        CambiarEstado(id_pedido_mk, "Procesando");
                        break;
                    case "Gestionado":
                        CambiarEstado(id_pedido_mk, "Gestionado");
                        break;
                    case "En tránsito":
                        CambiarEstado(id_pedido_mk, "En tránsito");
                        break;
                   // case "Incidencia":
                     //   CambiarEstado(id_pedido_mk, "Incidencia");
                       // break;
                    case "Entregado":
                        CambiarEstado(id_pedido_mk, "Entregado");
                        break;                  
                    case "Facturado":
                        CambiarEstado(id_pedido_mk, "Facturado");
                        break;
                    case "Reembolsado":
                        CambiarEstado(id_pedido_mk, "Reembolsado");
                        break; 
                    case "Cancelado":
                        CambiarEstado(id_pedido_mk, "Cancelado");
                        break;
                }
            }

            Cambiar_Estado.SelectedIndex = 0;

        }

        private void CambiarEstado(String id_pedido_mk, String estado)
        {
            string consulta = $"UPDATE pedidos SET estado_pedido = '{estado}' WHERE id_pedido_mk = '{id_pedido_mk}';";
            if (BaseDeDatos.EjecutarUpdateIncidencia(consulta))
            {
                CargarPedidos();
            //    MessageBox.Show("Estado actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DuplicarPedido(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un pedido para duplicar.");
                return;
            }

            if (dgProductos.SelectedItems.Count > 1)
            {
                MessageBox.Show("Selecciona solo un pedido para duplicar.");
                return;
            }

            DataRowView fila = (DataRowView)dgProductos.SelectedItem;

            // Construir el nuevo ID añadiendo una "R" al original
            string idOriginal = fila["id_pedido_mk"].ToString();
            string idDuplicado = idOriginal + "R";

            // Extraer los campos necesarios
            string fechaPedido = fila["fecha_Pedido"].ToString();
            fechaPedido=CrearTracking.FormatearFecha(fechaPedido);
            string nombreCompleto = fila["nombre_completo"].ToString();
            string direccion = fila["direccion"].ToString();
            string codigoPostal = fila["codigo_postal"].ToString();
            string ciudad = fila["ciudad"].ToString();
            string provincia = fila["provincia"].ToString();
            string idUsuario = fila["idUsuario"].ToString();
            string notaEnvio = fila["nota_envio"].ToString();
            string telefono = fila["telefono"].ToString();
            string email = fila["email"].ToString();
            string sku = fila["SKU"].ToString();
            string proveedor = fila["proveedor"].ToString();
            string uds = fila["uds"].ToString();
            string nombreArticulo = fila["nombre_articulo"].ToString();
            string puntosg = fila["puntosg"].ToString();
            string modelo = fila["modelo"].ToString();

            string consulta = $@"
        INSERT INTO pedidos (
            id_pedido_mk, fecha_Pedido, nombre_completo, direccion, codigo_postal, ciudad, provincia, 
            idUsuario, nota_envio, telefono, email, SKU, proveedor, uds, nombre_articulo, 
            puntosg, modelo, estado_pedido
        ) VALUES (
            '{idDuplicado}', '{fechaPedido}', '{nombreCompleto}', '{direccion}', '{codigoPostal}', 
            '{ciudad}', '{provincia}', '{idUsuario}', '{notaEnvio}', '{telefono}', '{email}', 
            '{sku}', '{proveedor}', '{uds}', '{nombreArticulo}', {puntosg}, '{modelo}', 'Procesando'
        );";

            bool insertado = BaseDeDatos.EjecutarUpdateIncidencia(consulta);

            if (insertado)
            {
                CargarPedidos(); // Asumiendo que tienes un método para recargar los pedidos en la tabla
                MessageBox.Show("Pedido duplicado correctamente.");
            }
            else
            {
                MessageBox.Show("No se pudo duplicar el pedido.");
            }
        }

        private void AnadirFactura(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItems.Count == 1)
            {
                DataRowView filaSeleccionada = (DataRowView)dgProductos.SelectedItem;
                facturas[0] = filaSeleccionada["id_pedido_mk"].ToString();
                facturas[1] = filaSeleccionada["facturadmi_snlosllanos"].ToString();
                facturas[2] = filaSeleccionada["fecha_factura"].ToString();
                facturas[3] = filaSeleccionada["factura_sin_iva"].ToString();
                var ventana = new CrearFactura();
                bool? resultadoVentana = ventana.ShowDialog();

                if (resultadoVentana == true)
                {
                    CargarPedidos();
                }

            }
        }

        private void AnadirTrackingsYSNExcel(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos Excel (*.xlsx)|*.xlsx";

            if (openFileDialog.ShowDialog() == true)
            {
                 archivoExcelPath = openFileDialog.FileName;

                try
                {
                    AnadirTrackingExcel();
                    AnadirSNExcel();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al leer el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        private string ExtraerSerialNumber(string input)
{
    if (input.Contains("S/N:"))
    {
        string numero = input.Split(new string[] { "S/N:" }, StringSplitOptions.None)[1].Trim();
        return $"SN: {numero}";
    }
    return "";
}

private string ExtraerNumeroPedido(string input)
{
    if (input.Contains("Pedido nº:"))
    {
        string numero = input.Split(new string[] { "Pedido nº:" }, StringSplitOptions.None)[1].Trim();
        return numero;
    }
    return "";
}


        private void AnadirTrackingExcel()
        {
                try
                {
                    // Contadores para éxitos y errores
                    int insercionesCorrectas = 0;
                    int insercionesFallidas = 0;

                    using (var workbook = new XLWorkbook(archivoExcelPath))
                    {
                        var worksheet = workbook.Worksheet(1); // Primer hoja
                        int fila = 2; // Comenzamos en la segunda fila (omitimos encabezado)

                        while (!worksheet.Cell(fila, 1).IsEmpty())
                        {
                            string numeroPedido = worksheet.Cell(fila, 1).GetValue<string>(); // Columna A
                            string tracking = worksheet.Cell(fila, 7).GetValue<string>();      // Columna G

                            if (tracking.Length >= 11)
                            {
                                tracking = tracking.Substring(10); // Desde el carácter 11
                            }

                            bool actualizado = BaseDeDatos.ActualizarTracking(numeroPedido, tracking);
                            if (actualizado)
                            {
                                insercionesCorrectas++; // Incrementamos el contador de éxitos
                            }
                            else
                            {
                                insercionesFallidas++; // Incrementamos el contador de fallos
                                /*  MessageBox.Show(
                                      $"No se pudo actualizar el tracking para el pedido {numeroPedido}.",
                                      "Error",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Warning
                                  ); */
                            }

                            fila++;
                        }
                    }

                    // Mensaje final con el resumen
                    string mensajeFinal = $"Trackings añadidos:\n\n" +
                                          $"- Inserciones correctas: {insercionesCorrectas}\n" +
                                          $"- Inserciones fallidas: {insercionesFallidas}";

                    MessageBox.Show(
                        mensajeFinal,
                        "Resumen de Inserciones",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    CargarPedidos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al leer el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


        private void AnadirSNExcel()
        {
            try
            {
                using (var workbook = new XLWorkbook(archivoExcelPath))
                {
                    var worksheet = workbook.Worksheet(2); // Hoja 2

                    // Paso 1: buscar todos los SNs
                    var listaSNs = new List<(int fila, string sn)>();
                    for (int fila = 1; fila <= worksheet.LastRowUsed().RowNumber(); fila++)
                    {
                        string valor = worksheet.Cell(fila, 3).GetValue<string>(); // Columna C
                        if (valor.Contains("S/N:"))
                        {
                            string sn = ExtraerSerialNumber(valor);
                            listaSNs.Add((fila, sn));
                        }
                    }

                    // Paso 2: buscar todos los pedidos
                    var listaPedidos = new List<(int fila, string pedido)>();
                    for (int fila = 1; fila <= worksheet.LastRowUsed().RowNumber(); fila++)
                    {
                        string valor = worksheet.Cell(fila, 10).GetValue<string>(); // Columna J
                        if (valor.Contains("Pedido nº:"))
                        {
                            string pedido = ExtraerNumeroPedido(valor);
                            listaPedidos.Add((fila, pedido));
                        }
                    }

                    // Contadores
                    int exitos = 0;
                    int fallos = 0;

                    // Paso 3: emparejar por cercanía de fila
                    foreach (var sn in listaSNs)
                    {
                        // Buscar el pedido más cercano en fila
                        var pedidoRelacionado = listaPedidos
                            .OrderBy(p => Math.Abs(p.fila - sn.fila))
                            .FirstOrDefault();

                        bool actualizado = BaseDeDatos.ActualizarSNenBD(pedidoRelacionado.pedido, sn.sn);

                        if (actualizado)
                            exitos++;
                        else
                            fallos++;
                    }

                    // Mensaje final con el resumen
                    string mensajeFinal = $"SNs actualizados:\n\n" +
                                          $"- Inserciones correctas: {exitos}\n" +
                                          $"- Inserciones fallidas: {fallos}";

                    MessageBox.Show(
                        mensajeFinal,
                        "Resumen de Inserciones de SN",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    if (exitos > 0)
                    {
                        CargarPedidos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}
