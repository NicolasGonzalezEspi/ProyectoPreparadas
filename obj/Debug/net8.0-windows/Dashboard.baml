   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5      Q MtrabajoFinalInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:trabajoFinalInterfacestrabajoFinalInterfaces 5      &    trabajoFinalInterfaces.Dashboard   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      6local$clr-namespace:trabajoFinalInterfaces     5        N� Title$  	Dashboard��5
      .��5      � $��0q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
      LiveCharts.Wpf.PieChart  -   5          Name$ pieChart��5      $��0��5      $��2��6$      InnerRadius$ 50Q�5      &    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Bottom=�5      $��10q�5         Series 5      �� *PieSeriesCollection+5      #   LiveCharts.Wpf.CartesianChart  5"   
   $��0��5#      $��1��6   $��10q�5%       5$      �� *SeriesCollection+   AxisX 5'         LiveCharts.Wpf.Axis     Title$% Ciudades con Más Convocatorias��6   	   
Foreground
	 �   �
   Labels
 6B   �� *Labels+6\      AxisY 5*        $ Valores��6      MinValue$ 0Q�6+   5,        5/   
   -   5          Name$ 	lineChart��5/      $��0��50      $��0��6*   $��10q�52       51      �� *LineSeriesCollection+ 54        $ Año de Nacimiento��6   
	 �   �   LabelsRotation$ 45Q�6f   
 65   �� *
LineLabels+6f    57        $ Cantidad de Clientes��6   $ 0Q�68   59      �� 5;   
   $��1��6   $��0��6"   $��10q�62   $��Center=�6>   $��Center=�6Y   .+�5<      �� $
��Wrap=�6   $ ��Número total de usuarias:��6,   $��20(�6M   $
��Bold"�6[   $��Center=�6m   �� 5=      -   5         ��Name$ UsuariosTotales��5=      $
��Wrap=�61   $	��:::��6E   $��20(�6P   $
��Bold"�6^   $��Center=�6p   5>      �� 5@   
   $��1��6   $��1��6"   $��10q�62   $��Center=�6>   $��Center=�6Y   .+�5A      �� $
��Wrap=�6   $&�� Número total de Convocatorias: ��6,   $��20(�6S   $
��Bold"�6a   $��Center=�6s   �� 5B      -   5        $ ConvocatoriasTotales��5B      $
��Wrap=�66   $	��:::��6J   $��20(�6U   $
��Bold"�6c   $��Center=�6u   �� 5C      $
��Wrap=�6   $*��$Media de Usuarias por Convocatoria: ��6,   $��20(�6X   $
��Bold"�6f   $��Center=�6x   �� 5D      -   5        $ MediaUsuarias_Convo��5D      $
��Wrap=�65   $	��:::��6I   $��20(�6T   $
��Bold"�6b   $��Center=�6t   5E      �� 5F   
   $��1��6   $��2��6"   $��10q�62   $��Center=�6>   $��Center=�6Y   .+�5G      �� $
��Wrap=�6   $&�� Número total de Puntos Usados: ��6,   $��20(�6S   $
��Bold"�6a   $��Center=�6s   �� 5H      -   5        $ PuntosTotalesUsados��5H      $
��Wrap=�65   $	��:::��6I   $��20(�6T   $
��Bold"�6b   $��Center=�6t   �� 5I      $
��Wrap=�6   $$��Número total de Incidencias: ��6,   $��20(�6Q   $
��Bold"�6_   $��Center=�6q   �� 5J      -   5        $ IncidenciasTotales��5J      $
��Wrap=�64   $	��:::��6H   $��20(�6S   $
��Bold"�6a   $��Center=�6s   �� 5K      $
��Wrap=�6   $%��Causa Principal de Incidencias:��6,   $��20(�6S   $
��Bold"�6a   $��Center=�6s   �� 5L      -   5        $ CausaIncidenciaPrincipal��5L      $
��Wrap=�6:   $	��:::��6N   $��20(�6Y   $
��Bold"�6g   $��Center=�6y   5M      5Q      5R      