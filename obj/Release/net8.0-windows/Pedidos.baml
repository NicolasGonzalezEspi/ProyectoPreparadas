   M S B A M L   `   `   `  ���� Q  MtrabajoFinalInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:trabajoFinalInterfacestrabajoFinalInterfaces  $    trabajoFinalInterfaces.Pedidos   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:trabajoFinalInterfaces       N� Title$  Pedidos�� � 	Resources  �� MergedDictionaries �� Source��  �� $ Dictionary1.xaml?�%    .��� ���#34485e.+��� $��Pedidos��$��20(�$
��Bold"�
�������$
��Left=�$	��Top=�$��	20,10,0,0q�&   System.Windows.Controls.DataGrid  -     Name$ dgProductos��   CustomDataGridStyle#��[     AutoGenerateColumns$ False��   
IsReadOnly$
 True��$��20, 123, 20, 20q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID Pedido MK��	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *id_pedido_mk+  $ Fecha Pedido��$	 * 
 �� *fecha_pedido+  $ Nombre Completo��$	 * 
 �� *nombre_completo+  $ 
Dirección��$	 * 
 �� *	direccion+  $ Cod. Postal��$	 * 
 �� *codigo_postal+  $ Ciudad��$	 * 
 �� *ciudad+  $ 	Provincia��$	 * 
 �� *	provincia+  $ 
ID Usuario��   
Visibility$ 	Collapsed=�$	 * 
 �� *	idUsuario+  $ Observaciones��$	 * 
 �� *
nota_envio+  $ 	Teléfono��$	 * 
 �� *
telefono+  $ Email��$	 * 
 �� *email+  $	 SKU��$	 * 
 �� *SKU+  $ 	Proveedor��$	 * 
 �� *	proveedor+  $ Unidades��$	 * 
 �� *uds+  $ Nombre Artículo��$	 * 
 �� *nombre_articulo+  $ Puntos��$	 * 
 �� *	puntosg+  $ Modelo��$	 * 
 �� *modelo+  $ Estado Pedido��$	 * 
 �� *estado_pedido+  $ Fecha Gestión��$	 * 
 �� *fecha_gestion+4  .System.Windows.Controls.DataGridTemplateColumn  $ Tracking��$	 *    CellTemplate �� .R��� �� -   ���� *
tracking+.�.��� ���� *
tracking+  $ Fecha Inicio Tránsito��$	 * 
 �� *fecha_inicio_transito+  $ Fecha Entrega��$	 * 
 �� *fecha_entrega_alumna+  $ Fact/SN��$	 * 
 �� *facturadmi_snlosllanos+  $ Fecha Factura��$	 * 
 �� *fecha_factura+  $ Importe sin IVA��$	 * 
 �� *factura_sin_iva+  $ Abono��$	 * 
 �� *abono+  $ Fecha Abono��$	 * 
 �� *fecha_abono+  $ Abono sin IVA��$	 * 
 �� *abono_sin_iva+  $ Info. Incidencia��$	 * 
 �� *info_incidencia+�� -    ��Name$ 
filtrarPor��$
��Left=�$	��Top=�$��
214,74,0,0q�$��28��$	��110��$��16(�.@��� $��Buscar por:�� �� 
IsSelected$
 True���� $��IdPedido���� $��Nombre���� $��
Dirección���� $��Email���� $��Nº Factura���� $��SN���� -     PageButtonStyle2#��[ $��Importar nuevos Pedidos��$
��Left=�$��
120,21,0,0q�$	��Top=�$��30��$	��185���� -     PageButtonStyle1#��[ $��Editar Dirección��$��Right=�$��
0,21,332,0q�$	��Top=�$��30��$	��142���� -   #��[ $��Editar Incidencia��$��Right=�$��	0,21,20,0q�$	��Top=�$��30��$	��142���� -   #��[ $��Exportar DMI��$
��Left=�$��
318,21,0,0q�$	��Top=�$��30��$��92���� -   #��[ $��Exportar Solutia��$
��Left=�$��
424,21,0,0q�$	��Top=�$��30��$	��114���� -	   #��[ $��
Factura/SN��$y�	Collapsed=�$��Right=�$��
0,52,699,0q�$	��Top=�$��30��$	��142���� -
   $ filtrarPorV2��$
��Left=�$	��Top=�$��
350,74,0,0q�$��28��$	��110��$��16(�.@��� $��Filtrar por:��$
 True���� $	��DMI���� $��Solutia���� $��Con SN���� $��Con Nº Fact���� $��Sin SN/NºFact���� $��Mostrar Todos���� -   $ 	Vista_Por��$
��Left=�$	��Top=�$��	20,73,0,0q�$��28��$	��169��$��16(�.@��� $��Vista Optimizada��$
 True���� $��Vista Tracking���� $��Vista Factura/Abono���� $��Vista Dirección���� $��Vista Completa���� -   #��[ $��Añadir��$y�	Collapsed=�$��Right=�$��
0,21,331,0q�$	��Top=�$��30��$	��142���� -   #��[ $��Añadir Abono��$��Right=�$��	0,73,20,0q�$	��Top=�$��30��$	��142���� -   #��[ $��Añadir Tracking��$��Right=�$��
0,73,173,0q�$	��Top=�$��30��$	��142���� -   #��[ $��Añadir Factura��$��Right=�$��
0,73,332,0q�$	��Top=�$��30��$	��142���� -   $ Cambiar_Estado��$
��Left=�$	��Top=�$��
488,74,0,0q�$��28��$	��141��$��16(�.@��� $��Cambiar Estado��$
 True��
 � Tag$ NoNotify���� $��
Procesando���� $��
Gestionado���� $��En tránsito���� $��
Incidencia��$y�	Collapsed=��� $��	Entregado���� $��	Facturado���� $��	Cancelado���� $��Reembolsado��$y�	Collapsed=��� -   #��[ $��Duplicar Pedido (R)��$��Right=�$��
0,21,173,0q�$	��Top=�$��30��$	��142���� -   #��[ $��
Añadir SN��$��Right=�$��
0,73,487,0q�$	��Top=�$��30��$	��142���� -   #��[ $��Tracking y SN → Excel��$��Right=�$��
0,21,487,0q�$	��Top=�$��30��$	��151��