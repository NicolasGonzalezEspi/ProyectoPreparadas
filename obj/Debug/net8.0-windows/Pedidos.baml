   M S B A M L   `   `   `  ����Q  MtrabajoFinalInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:trabajoFinalInterfacestrabajoFinalInterfaces  5      $    trabajoFinalInterfaces.Pedidos   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:trabajoFinalInterfaces     5        N� Title$  Pedidos��5	       � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Dictionary1.xaml?�6%   %    5      .��5      � ���#34485e.+�5   
   �� $��Pedidos��6   $��20(�5      $
��Bold"�6   
�������$
��Left=�5      $	��Top=�6(   $��	20,10,0,0q�5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgProductos��5         CustomDataGridStyle#��[  6(      AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5      $��20, 123, 20, 20q�5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Pedido MK��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6\   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6;   �� *id_pedido_mk+6\     5      $ Fecha Pedido��6%   $	 * 6\   
 6;   �� *fecha_pedido+6\     5      $ Nombre Completo��6%   $	 * 6b   
 6>   �� *nombre_completo+6b     5      $ 
Dirección��6%   $	 * 6W   
 69   �� *	direccion+6W     5       $ Cod. Postal��6%   $	 * 6\   
 6:   �� *codigo_postal+6\     5!      $ Ciudad��6%   $	 * 6P   
 65   �� *ciudad+6P     5"      $ 	Provincia��6%   $	 * 6V   
 68   �� *	provincia+6V     5#      $ 
ID Usuario��6%      
Visibility$ 	Collapsed=�69   $	 * 6n   
 6P   �� *	idUsuario+6n     5$      $ Observaciones��6%   $	 * 6[   
 6<   �� *
nota_envio+6[     5%      $ 	Teléfono��6%   $	 * 6T   
 67   �� *
telefono+6T     5&      $ Email��6%   $	 * 6N   
 64   �� *email+6N     5'      $	 SKU��6%   $	 * 6J   
 62   �� *SKU+6J     5(      $ 	Proveedor��6%   $	 * 6V   
 68   �� *	proveedor+6V     5)      $ Unidades��6%   $	 * 6O   
 67   �� *uds+6O     5*      $ Nombre Artículo��6%   $	 * 6b   
 6>   �� *nombre_articulo+6b     5+      $ Puntos��6%   $	 * 6Q   
 65   �� *	puntosg+6Q     5,      $ Modelo��6%   $	 * 6P   
 65   �� *modelo+6P     5-      $ Estado Pedido��6%   $	 * 6^   
 6<   �� *estado_pedido+6^     5.      $ Fecha Gestión��6%   $	 * 6^   
 6<   �� *fecha_gestion+6^   4  .System.Windows.Controls.DataGridTemplateColumn  5/      $ Tracking��6)   $	 * 6;      CellTemplate 50      �� 51      .R�52      �� �� 53      -   5        ��53      �� *
tracking+.�6i   .�54      �� ��6    �� *
tracking+55      56      57      59        5:      $ Fecha Inicio Tránsito��6%   $	 * 6n   
 6D   �� *fecha_inicio_transito+6n     5;      $ Fecha Entrega��6%   $	 * 6e   
 6<   �� *fecha_entrega_alumna+6e     5<      $ Fact/SN��6%   $	 * 6a   
 66   �� *facturadmi_snlosllanos+6a     5=      $ Fecha Factura��6%   $	 * 6^   
 6<   �� *fecha_factura+6^     5>      $ Importe sin IVA��6%   $	 * 6b   
 6>   �� *factura_sin_iva+6b     5?      $ Abono��6%   $	 * 6N   
 64   �� *abono+6N     5@      $ Fecha Abono��6%   $	 * 6Z   
 6:   �� *fecha_abono+6Z     5A      $ Abono sin IVA��6%   $	 * 6^   
 6<   �� *abono_sin_iva+6^     5B      $ Info. Incidencia��6%   $	 * 6c   
 6?   �� *info_incidencia+6c   5D      �� 5F   
   -   5         ��Name$ 
filtrarPor��5F      $
��Left=�5G      $	��Top=�5H      $��
214,74,0,0q�5I      $��28��5J      $	��110��5K      $��16(�5L   :   .@�5M      �� $��Buscar por:��6    �� 
IsSelected$
 True��61   �� 5N      $��IdPedido��6   �� 5O      $��Nombre��6   �� 5P      $��
Dirección��6   �� 5Q      $��Email��6   �� 5R      $��Nº Factura��6   �� 5S      $��SN��6   5T      �� 5X   
   -   5          PageButtonStyle2#��[ 5X      $��Importar nuevos Pedidos��6;   $
��Left=�6^   $��
120,21,0,0q�6z   $	��Top=�6�   $��30��6�   $	��185��6�   6�   �� 5Y   
   -   5          PageButtonStyle1#��[ 5Y      $��Editar Dirección��6;   $��Right=�6X   $��
0,21,332,0q�6u   $	��Top=�6�   $��30��6�   $	��142��6�   6�   �� 5Z   
   -   5        #��[ 5Z      $��Editar Incidencia��6;   $��Right=�6Y   $��	0,21,20,0q�6v   $	��Top=�6�   $��30��6�   $	��142��6�   6�   �� 5j   
   -   5        #��[ 5j      $��Exportar DMI��6;   $
��Left=�6S   $��
318,21,0,0q�6o   $	��Top=�6�   $��30��6�   $��92��6�   6�   �� 5k   
   -   5        #��[ 5k      $��Exportar Solutia��6;   $
��Left=�6W   $��
424,21,0,0q�6s   $	��Top=�6�   $��30��6�   $	��114��6�   6�   �� 5l   
   -	   5        #��[ 5l      $��
Factura/SN��6;   $y�	Collapsed=�6P   $��Right=�6h   $��
0,52,699,0q�6�   $	��Top=�6�   $��30��6�   $	��142��6�   6�   �� 5m   
   -
   5        $ filtrarPorV2��5m      $
��Left=�5n      $	��Top=�5o      $��
350,74,0,0q�5p      $��28��5q      $	��110��5r      $��16(�5s   >   .@�5t      �� $��Filtrar por:��6   $
 True��62   �� 5u      $	��DMI��6   �� 5v      $��Solutia��6   �� 5w      $��Con SN��6   �� 5x      $��Con Nº Fact��6   �� 5y      $��Sin SN/NºFact��6   �� 5z      $��Mostrar Todos��6   5}      �� 5~   
   -   5        $ 	Vista_Por��5~      $
��Left=�5      $	��Top=�5�      $��	20,73,0,0q�5�      $��28��5�      $	��169��5�      $��16(�5�   7   .@�5�      �� $��Vista Optimizada��6   $
 True��66   �� 5�      $��Vista Tracking��6   �� 5�      $��Vista Factura/Abono��6   �� 5�      $��Vista Dirección��6   �� 5�      $��Vista Completa��6   5�      �� 5�   
   -   5        #��[ 5�      $��Añadir��6;   $y�	Collapsed=�6L   $��Right=�6d   $��
0,21,331,0q�6�   $	��Top=�6�   $��30��6�   $	��142��6�   6�   �� 5�   
   -   5        #��[ 5�      $��Añadir Abono��6;   $��Right=�6T   $��	0,73,20,0q�6q   $	��Top=�6�   $��30��6�   $	��142��6�   6�   �� 5�   
   -   5        #��[ 5�      $��Añadir Tracking��6;   $��Right=�6W   $��
0,73,173,0q�6t   $	��Top=�6�   $��30��6�   $	��142��6�   6�   �� 5�   
   -   5        #��[ 5�      $��Añadir Factura��6;   $��Right=�6V   $��
0,73,332,0q�6s   $	��Top=�6�   $��30��6�   $	��142��6�   6�   �� 5�   
   -   5        $ Cambiar_Estado��5�      $
��Left=�5�      $	��Top=�5�      $��
488,74,0,0q�5�      $��28��5�      $	��141��5�      $��16(�5�   ?   .@�5�      �� $��Cambiar Estado��6   $
 True��64   
 � Tag$ NoNotify��6F   �� 5�      $��
Procesando��6   �� 5�      $��
Gestionado��6   �� 5�      $��En tránsito��6   �� 5�      $��
Incidencia��6   $y�	Collapsed=�60   �� 5�      $��	Entregado��6   �� 5�      $��	Facturado��6   �� 5�      $��	Cancelado��6   �� 5�      $��Reembolsado��6   $y�	Collapsed=�61   5�      �� 5�   
   -   5        #��[ 5�      $��Duplicar Pedido (R)��6;   $��Right=�6[   $��
0,21,173,0q�6x   $	��Top=�6�   $��30��6�   $	��142��6�   6�   �� 5�   
   -   5        #��[ 5�      $��
Añadir SN��6;   $��Right=�6Q   $��
0,73,487,0q�6n   $	��Top=�6�   $��30��6�   $	��142��6�   6�   �� 5�   
   -   5        #��[ 5�      $��Tracking y SN → Excel��6;   $��Right=�6]   $��
0,21,487,0q�6z   $	��Top=�6�   $��30��6�   $	��151��6�   6�   5�      5�      