   M S B A M L   `   `   `  ����Q  MtrabajoFinalInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:trabajoFinalInterfacestrabajoFinalInterfaces  5      #    trabajoFinalInterfaces.Borrar   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:trabajoFinalInterfaces     5        N� Title$  Borrar��5	       � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Dictionary1.xaml?�6%   %    5      .��5      � ���#34485e.+�5   
   �� $��Fecha de Importación:��6   $y�	Collapsed=�61   $��16(�5      
�������$
��Left=�5      $��
546,44,0,0q�6/   $	��Top=�5      �� 5   
   -   5         ��Name$ txtProductoBorrar��5      $	��114��5      $��24��6   $
��Left=�5      $��
732,41,0,0q�6-   $	��Top=�5      $y�	Collapsed=�6*   �� 5    
   -   5           PageButtonStyle1#��[  5       
����ح�$'��!Mover Nuevas Convos a BBDD Global��6R   $y�	Collapsed=�6~   $	��300��5!      $��30��6   
���   �$
��Left=�5#      $��
546,76,0,0q�6,   $	��Top=�5$      6)   &   System.Windows.Controls.DataGrid  5)   
   -   5          Name$ dgProductos��5)        CustomDataGridStyle#��[ 6*      AutoGenerateColumns$ False��5*         
IsReadOnly$
 True��5+      $��20, 65, 20, 20q�5,         Columns 5.      0  *System.Windows.Controls.DataGridTextColumn  5/      ,  &System.Windows.Controls.DataGridColumn	   Header$	 	categoria��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *	categoria+6V     50      $	 convocatoria��6%   $
 * 6\    6;   �� *convocatoria+6\     51      $	 codigo_convocatoria��6%   $
 * 6i    6B   �� *codigoconvocatoria+6i     52      $	 	provincia��6%   $
 * 6V    68   �� *	provincia+6V     53      $	 	localidad��6%   $
 * 6V    68   �� *	localidad+6V     54      $	 nombre��6%   $
 * 6P    65   �� *nombre+6P     55      $	 	apellidos��6%   $
 * 6V    68   �� *	apellidos+6V     56      $	 fecha_solicitud��6%   $
 * 6b    6>   �� *fecha_solicitud+6b     57      $	 estado_inscripcion��6%   $
 * 6h    6A   �� *estado_inscripcion+6h     58      $	 estado_matriculacion��6%   $
 * 6l    6C   �� *estado_matriculacion+6l     59      $	 email��6%   $
 * 6N    64   �� *email+6N     5:      $	 telefono��6%   $
 * 6T    67   �� *
telefono+6T     5;      $	 nivel_estudios��6%   $
 * 6`    6=   �� *nivel_estudios+6`     5<      $
	 sexo��6%   $
 * 6L    63   �� *sexo+6L     5=      $	 fecha_nacimiento��6%   $
 * 6d    6?   �� *fecha_nacimiento+6d     5>      $		 dni��6%   $
 * 6J    62   �� *dni+6J     5?      $	 situacion_laboral��6%   $
 * 6f    6@   �� *situacion_laboral+6f     5@      $	 asistencia_remota��6%   $
 * 6f    6@   �� *asistencia_remota+6f     5A      $	 tablet��6%   $
 * 6P    65   �� *tablet+6P     5B      $	 puntos��6%   $
 * 6P    65   �� *puntos+6P   5E      �� 5F   
   $#��Importar nuevas convocatorias��6   $y�	Collapsed=�6:   $��16(�5G      
�������$
��Left=�5H      $��	17,40,0,0q�6(   $	��Top=�5I      �� 5J   
   -   5        $	��↑��5J      $��20(�6   $��28��5K      $��31��6   
��� � �
�������$
��Left=�5M      $��
279,30,0,0q�6(   $	��Top=�5N      $y�	Collapsed=�6%   5O      �� 5P   
   -   5          PageButtonStyle2#��[ 5P      $&�� 1A. Importar Nuevas Convos (CSV)��6;    � ToolTip$> 8Importar datos de las nuevas convocatorias certificadas.��6f   $
��Left=�6�   $��
181,21,0,0q�6�   $	��Top=�6�   $��30��6�   $	��236��6�   6
  �� 5Q   
   -   5        #��[  5Q      
����ح�$#��1B. Pasar Datos a Formato MKP��6R   $) #Copiar datos a Formato Marketplace ��6z   $	��214��6�   $��30��6�   $
��Left=�6�   $��
422,21,0,0q�6�   $	��Top=�6�   6  �� 5R   
   -   5        #��[  5R      
����ح�$��
Puntos: 40��6R   $G ASelecciona un registro, y pulsa este botón para actualizar datos��6g   $	��147��6�   $��30��6�   $��Right=�6�   $��
0,21,180,0q�6�   $	��Top=�6�   6  �� 5S   
   -   5        #��[  5S      
����ح�$��Tablet: Sí��6R   $G ASelecciona un registro, y pulsa este botón para actualizar datos��6g   $	��136��6�   $��30��6�   $��Right=�6�   $��	0,21,20,0q�6�   $	��Top=�6�   6  �� 5U   
   $��Nuevas Convos��6   $��20(�5V      $
��Bold"�6   
�������$
��Left=�5W      $	��Top=�6(   $��	20,10,0,0q�5X      �� 5Y   
   $��Fin del Proceso��6   $��20(�5Z      $
��Bold"�6   
�������$
��Left=�5[      $	��Top=�6(   $��
546,10,0,0q�5\      $y�	Collapsed=�6!   �� 5]   
   $��Total registros:��6   $��18(�5^      
�������$
��Left=�5_      $��
647,27,0,0q�6(   $	��Top=�5`      $	��149��6%    5a      _� 5b      $���|El número muestra el número de registros que quedan por preparar. Se irán volcando datos en bloques de 100-200 usuarias. ��6   5d      �� 5e   
   -	   5         ��Name$ txtTotalRegistros��5e      
�������$��36(�6C   $
��Left=�5f      $��
771,10,0,0q�6(   $
��Wrap=�5g      $��0��6!   $	��Top=�6*   $��72��6B    5h      _� 5i      $6��0El número de registros que quedan por preparar.��6   5k      5n      5o      