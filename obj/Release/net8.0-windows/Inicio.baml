   M S B A M L   `   `   `  ����   trabajoFinalInterfaces#    trabajoFinalInterfaces.Inicio   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Aav2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       N� Title$  Inicio�� � 	Resources  �� MergedDictionaries �� Source��  �� $ Dictionary1.xaml?�%    .��� ���#34485e.+��� $��BBDD Global��$��20(�$
��Bold"�
�������$
��Left=�$	��Top=�$��	20,10,0,0q�&   System.Windows.Controls.DataGrid  -     Name$ dgProductos��   CustomDataGridStyle#��[     AutoGenerateColumns$ False��   
IsReadOnly$
 True��$��Stretch=�$��Stretch=�$��20, 65, 20, 20q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
categoriag��	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *
categoriag+  $ convocatoriag��$	 * 
 �� *convocatoriag+  $ codigo_convocatoriag��$	 * 
 �� *codigoconvocatoriag+  $ 
provinciag��$	 * 
 �� *
provinciag+  $ 
localidadg��$	 * 
 �� *
localidadg+  $ nombreg��$	 * 
 �� *	nombreg+  $ 
apellidosg��$	 * 
 �� *
apellidosg+  $ fecha_solicitudg��$	 * 
 �� *fechasolicitudg+  $ estado_inscripciong��$	 * 
 �� *estadoinscripciong+  $ estado_matriculaciong��$	 * 
 �� *estadomatriculaciong+  $ emailg��$	 * 
 �� *emailg+  $ 	telefonog��$	 * 
 �� *	telefonog+  $ nivel_estudiosg��$	 * 
 �� *nivelestudiosg+  $ sexog��$	 * 
 �� *sexog+  $ fecha_nacimientog��$	 * 
 �� *fechanacimientog+  $
 dnig��$	 * 
 �� *dnig+  $ situacion_laboralg��$	 * 
 �� *situacionlaboralg+  $ asistencia_remotag��$	 * 
 �� *asistenciaremotag+  $ tabletg��$	 * 
 �� *	tabletg+  $ puntosg��$	 * 
 �� *	puntosg+  $ fecha_importacion��$	 * 
 �� *fechaimportacion+  $ observaciones��$	 * 
 �� *observaciones+�� $��Filtrar por:��$��16(�
�������$
��Left=�$��
155,30,0,0q�$	��Top=�$y�	Collapsed=��� -     PageButtonStyle1#��[ $��Añadir Observación��$
��Left=�$��
290,21,0,0q�$	��Top=�$	��150��$��29���� -    ��Name$ 	cmbEstado��$
��Left=�$y�	Collapsed=�$	��Top=�$��
673,28,0,0q�$��24��$��29��.@��� $��
Procesando���� $��Gestionando���� $��
Completado���� -   $ 
filtrarPor��$
��Left=�$	��Top=�$��
162,21,0,0q�$��27��$	��110��$��16(�.@��� $��Buscar por:�� �� 
IsSelected$
 True���� $	��DNI���� $��Email���� $��Cádiz���� $��Málaga���� $��Total registros:��$��18(�
�������$��Center=�$��
0,29,100,0q�$	��Top=��� -    ��Name$ txtTotalRegistros��
�������$��36(�$��Center=�$��0,10,-100,0q�$
��Wrap=�$��0��$	��Top=�$��80���� -     PageButtonStyle2#��[ $��Exportar Backup��$��Right=�$��0,21,335,110q�$	��Top=�$	��118��$��29�� � ToolTip$w qGuardar todos los registros que se muestran en pantalla en un archivo CSV. Almacenamiento a largo plazo de datos.���� -   #��[ $��Importar Backup��$��Right=�$��
0,21,210,0q�$	��Top=�$��29��$	��120��$~ xCarga todos los registros compatibles de un archivo CSV. Si la importación es correcta, muestra esos datos en pantalla.���� -   #��[ $��Borrar Tablas��$��Right=�$��	0,21,20,0q�$	��Top=����#A88C8C$	��175��$��29��