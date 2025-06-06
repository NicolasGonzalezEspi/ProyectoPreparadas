using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Windows.Controls;

namespace trabajoFinalInterfaces
{
    public static class DatosEstadisticos
    {
        // Diccionario público para lectura/escritura desde cualquier clase
        public static Dictionary<string, int> DatosPorCiudad { get; set; } = new Dictionary<string, int>();
        public static Dictionary<string, int> PedidosPorProducto { get; set; } = new Dictionary<string, int>();
        public static Dictionary<string, int> ClientesPorEdad { get; set; } = new Dictionary<string, int>();

    }

    public partial class Dashboard : Page
    {
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection PieSeriesCollection { get; set; }
        public List<string> Labels { get; set; }

        public SeriesCollection LineSeriesCollection { get; set; }
        public List<string> LineLabels { get; set; }


        public Dashboard()
        {
            InitializeComponent();
            BaseDeDatos.CargarDatosGraficas();

                Labels = new List<string>();
                var valores = new ChartValues<int>();

                foreach (var entrada in DatosEstadisticos.DatosPorCiudad)
                {
                    Labels.Add(entrada.Key);         // Nombre de la ciudad
                    valores.Add(entrada.Value);      // Valor correspondiente
                }

                SeriesCollection = new SeriesCollection
{
    new ColumnSeries
    {
        Title = "Ciudades",
        Values = valores,
        DataLabels = true
    }
};
         


                // Series para gráfico circular (Pie Chart)
                PieSeriesCollection = new SeriesCollection();
                foreach (var entrada in DatosEstadisticos.PedidosPorProducto)
                {
                    PieSeriesCollection.Add(new PieSeries
                    {
                        Title = entrada.Key,
                        Values = new ChartValues<int> { entrada.Value },
                        DataLabels = true

                    });
                }

                pieChart.LegendLocation = LegendLocation.Bottom;



            LineLabels = new List<string>();
            var valoresLine = new ChartValues<int>();

            foreach (var entrada in DatosEstadisticos.ClientesPorEdad)
            {
                LineLabels.Add(entrada.Key);         // Año de nacimiento
                valoresLine.Add(entrada.Value);      // Número de clientes
            }

            LineSeriesCollection = new SeriesCollection
{
    new LineSeries
    {
        Title = "Clientes por Edad",
        Values = valoresLine,
        PointGeometry = DefaultGeometries.Circle,
        PointGeometrySize = 8,
        StrokeThickness = 2,
        LineSmoothness = 0.5, // Suaviza la línea
        DataLabels = true
    }
};



            DataContext = this;
            ActualizarTotalRegistros();
        }

        private void ActualizarTotalRegistros()
        {
            int totalRegistros = BaseDeDatos.ObtenerTotalRegistrosBbddGlobal();
            UsuariosTotales.Text = totalRegistros.ToString();

            int totalPuntosUtilizados = BaseDeDatos.ObtenerTotalPuntosUsados();
            PuntosTotalesUsados.Text = totalPuntosUtilizados.ToString();

            int convocatoriasTotales = BaseDeDatos.ObtenerTotalConvocatorias();
            ConvocatoriasTotales.Text= convocatoriasTotales.ToString();
            int incidenciasTotales = BaseDeDatos.ObtenerTotalIncidencias();
            IncidenciasTotales.Text= incidenciasTotales.ToString();

            String causaIncidenciaPrincipal = BaseDeDatos.ObtenerIncidenciaPrincipal();
            CausaIncidenciaPrincipal.Text = causaIncidenciaPrincipal.ToString();

            decimal mediaUsuarias_Convo = (decimal)totalRegistros / (decimal)convocatoriasTotales;
            MediaUsuarias_Convo.Text = mediaUsuarias_Convo.ToString("N2");
        }

    }
}