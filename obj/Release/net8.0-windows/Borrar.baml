   M S B A M L   `   `   `  ���� Q  MtrabajoFinalInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:trabajoFinalInterfacestrabajoFinalInterfaces  #    trabajoFinalInterfaces.Borrar   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:trabajoFinalInterfaces       N� Title$  Borrar�� � 	Resources  �� MergedDictionaries �� Source��  �� $ Dictionary1.xaml?�%    .��� ���#34485e.+��� $��Fecha de Importación:��$y�	Collapsed=�$��16(�
�������$
��Left=�$��
546,44,0,0q�$	��Top=��� -    ��Name$ txtProductoBorrar��$	��114��$��24��$
��Left=�$��
732,41,0,0q�$	��Top=�$y�	Collapsed=��� -      PageButtonStyle1#��[  
����ح�$'��!Mover Nuevas Convos a BBDD Global��$y�	Collapsed=�$	��300��$��30��
���   �$
��Left=�$��
546,76,0,0q�$	��Top=�&   System.Windows.Controls.DataGrid  -     Name$ dgProductos��  CustomDataGridStyle#��[    AutoGenerateColumns$ False��   
IsReadOnly$
 True��$��20, 65, 20, 20q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 	categoria��
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	categoria+  $	 convocatoria��$
 *  �� *convocatoria+  $	 codigo_convocatoria��$
 *  �� *codigoconvocatoria+  $	 	provincia��$
 *  �� *	provincia+  $	 	localidad��$
 *  �� *	localidad+  $	 nombre��$
 *  �� *nombre+  $	 	apellidos��$
 *  �� *	apellidos+  $	 fecha_solicitud��$
 *  �� *fecha_solicitud+  $	 estado_inscripcion��$
 *  �� *estado_inscripcion+  $	 estado_matriculacion��$
 *  �� *estado_matriculacion+  $	 email��$
 *  �� *email+  $	 telefono��$
 *  �� *
telefono+  $	 nivel_estudios��$
 *  �� *nivel_estudios+  $
	 sexo��$
 *  �� *sexo+  $	 fecha_nacimiento��$
 *  �� *fecha_nacimiento+  $		 dni��$
 *  �� *dni+  $	 situacion_laboral��$
 *  �� *situacion_laboral+  $	 asistencia_remota��$
 *  �� *asistencia_remota+  $	 tablet��$
 *  �� *tablet+  $	 puntos��$
 *  �� *puntos+�� $#��Importar nuevas convocatorias��$y�	Collapsed=�$��16(�
�������$
��Left=�$��	17,40,0,0q�$	��Top=��� -   $	��↑��$��20(�$��28��$��31��
��� � �
�������$
��Left=�$��
279,30,0,0q�$	��Top=�$y�	Collapsed=��� -     PageButtonStyle2#��[ $%��1. Importar Nuevas Convos (CSV)�� � ToolTip$> 8Importar datos de las nuevas convocatorias certificadas.��$
��Left=�$��
187,21,0,0q�$	��Top=�$��30��$	��230���� -   #��[  
����ح�$"��2. Pasar Datos a Formato MKP��$) #Copiar datos a Formato Marketplace ��$	��214��$��30��$
��Left=�$��
422,21,0,0q�$	��Top=��� -   #��[  
����ح�$��Tablet: Sí Puntos: 0��$G ASelecciona un registro, y pulsa este botón para actualizar datos��$	��136��$��30��$��Right=�$��	0,21,20,0q�$	��Top=��� -   #��[  
����ح�$��Tablet: No Puntos: 40��$G ASelecciona un registro, y pulsa este botón para actualizar datos��$	��147��$��30��$��Right=�$��
0,21,161,0q�$	��Top=��� $��Nuevas Convos��$��20(�$
��Bold"�
�������$
��Left=�$	��Top=�$��	20,10,0,0q��� $��Fin del Proceso��$��20(�$
��Bold"�
�������$
��Left=�$	��Top=�$��
546,10,0,0q�$y�	Collapsed=��� $��Total registros:��$��18(�
�������$
��Left=�$��
647,27,0,0q�$	��Top=�$	��149�� _� $���|El número muestra el número de registros que quedan por preparar. Se irán volcando datos en bloques de 100-200 usuarias. ���� -	    ��Name$ txtTotalRegistros��
�������$��36(�$
��Left=�$��
771,10,0,0q�$
��Wrap=�$��0��$	��Top=�$��72�� _� $6��0El número de registros que quedan por preparar.��