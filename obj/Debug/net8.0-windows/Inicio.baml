   M S B A M L   `   `   `  ����  trabajoFinalInterfaces#    trabajoFinalInterfaces.Inicio   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Aav2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6E     N� Title$  Inicio��5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5	      �� 5      $ Dictionary1.xaml?�6%   %    5      .��5      � ���#34485e.+�5   
   �� $��BBDD Global��6   $��20(�5      $
��Bold"�6"   
�������$
��Left=�5      $	��Top=�6/   $��	20,10,0,0q�5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgProductos��5         CustomDataGridStyle#��[  5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5      $��Stretch=�5      $��Stretch=�6)   $��20, 65, 20, 20q�5         Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Header$ 
categoriag��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6X   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 69   �� *
categoriag+6X     5"      $ convocatoriag��6%   $	 * 6^   
 6<   �� *convocatoriag+6^     5#      $ codigo_convocatoriag��6%   $	 * 6k   
 6C   �� *codigoconvocatoriag+6k     5$      $ 
provinciag��6%   $	 * 6X   
 69   �� *
provinciag+6X     5%      $ 
localidadg��6%   $	 * 6X   
 69   �� *
localidadg+6X     5&      $ nombreg��6%   $	 * 6R   
 66   �� *	nombreg+6R     5'      $ 
apellidosg��6%   $	 * 6X   
 69   �� *
apellidosg+6X     5(      $ fecha_solicitudg��6%   $	 * 6c   
 6?   �� *fechasolicitudg+6c     5)      $ estado_inscripciong��6%   $	 * 6i   
 6B   �� *estadoinscripciong+6i     5,      $ estado_matriculaciong��6%   $	 * 6m   
 6D   �� *estadomatriculaciong+6m     5-      $ emailg��6%   $	 * 6P   
 65   �� *emailg+6P     5.      $ 	telefonog��6%   $	 * 6V   
 68   �� *	telefonog+6V     5/      $ nivel_estudiosg��6%   $	 * 6a   
 6>   �� *nivelestudiosg+6a     50      $ sexog��6%   $	 * 6N   
 64   �� *sexog+6N     51      $ fecha_nacimientog��6%   $	 * 6e   
 6@   �� *fechanacimientog+6e     52      $
 dnig��6%   $	 * 6L   
 63   �� *dnig+6L     53      $ situacion_laboralg��6%   $	 * 6g   
 6A   �� *situacionlaboralg+6g     54      $ asistencia_remotag��6%   $	 * 6g   
 6A   �� *asistenciaremotag+6g     55      $ tabletg��6%   $	 * 6R   
 66   �� *	tabletg+6R     56      $ puntosg��6%   $	 * 6R   
 66   �� *	puntosg+6R     57      $ fecha_importacion��6%   $	 * 6e   
 6@   �� *fechaimportacion+6e     58      $ observaciones��6%   $	 * 6^   
 6<   �� *observaciones+6^   5:      �� 5;   
   $��Filtrar por:��6   $��16(�5<      
�������$
��Left=�5=      $��
155,30,0,0q�6(   $	��Top=�5>      $y�	Collapsed=�6%   �� 5?   
   -   5          PageButtonStyle1#��[ 5?      $��Añadir Observación��6;   $
��Left=�6X   $��
290,21,0,0q�6s   $	��Top=�6�   $	��150��6�   $��29��6�   �� 5B   
   -   5         ��Name$ 	cmbEstado��5B      $
��Left=�5C      $y�	Collapsed=�5D      $	��Top=�5E      $��
673,28,0,0q�5F      $��24��5G   =   $��29��6I   .@�5H      �� $��
Procesando��6   �� 5I      $��Gestionando��6   �� 5J      $��
Completado��6   5K      �� 5L   
   -   5        $ 
filtrarPor��5L      $
��Left=�5M      $	��Top=�5N      $��
162,21,0,0q�5O      $��27��5P      $	��110��5Q      $��16(�5R   :   .@�5S      �� $��Buscar por:��6    �� 
IsSelected$
 True��61   �� 5T      $	��DNI��6   �� 5U      $��Email��6   �� 5V      $��Cádiz��6   �� 5W      $��Málaga��6   5X      �� 5\   
   $��Total registros:��6   $��18(�5]      
�������$��Center=�5^      $��
0,29,100,0q�6!   $	��Top=�5_      �� 5`   
   -   5         ��Name$ txtTotalRegistros��5`      
�������$��36(�6C   $��Center=�5a      $��0,10,-100,0q�6!   $
��Wrap=�5b      $��0��6   $	��Top=�6!   $��80��69   �� 5d   
   -   5          PageButtonStyle2#��[ 5d      $��Exportar Backup��5e   	   $��Right=�5f   	   $��0,21,335,110q�5g   	   $	��Top=�5h   	   $	��118��5j   	   $��29��5k   	    � ToolTip$w qGuardar todos los registros que se muestran en pantalla en un archivo CSV. Almacenamiento a largo plazo de datos.��5l   	   �� 5m   
   -   5        #��[ 5m      $��Importar Backup��6<   $��Right=�6V   $��
0,21,210,0q�6r   $	��Top=�6�   $��29��6�   $	��120��6�   $~ xCarga todos los registros compatibles de un archivo CSV. Si la importación es correcta, muestra esos datos en pantalla.��6�   �� 5n   
   -   5        #��[ 5n      $��Borrar Tablas��5o      $��Right=�5p      $��	0,21,20,0q�5q      $	��Top=�5r      ���#A88C8C$	��175��5u      $��29��5v      5x      5y      