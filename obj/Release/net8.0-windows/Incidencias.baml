   M S B A M L   `   `   `  ���� Q  MtrabajoFinalInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:trabajoFinalInterfacestrabajoFinalInterfaces  (    "trabajoFinalInterfaces.Incidencias   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:trabajoFinalInterfaces       N� Title$  Incidencias�� � 	Resources  �� MergedDictionaries �� Source��  �� $ Dictionary1.xaml?�%    .��� ���#34485e.+��� $��Incidencias��$��20(�$
��Bold"�
�������$
��Left=�$	��Top=�$��	20,10,0,0q�&   System.Windows.Controls.DataGrid  -     Name$ dgProductos��   CustomDataGridStyle#��[     AutoGenerateColumns$ False��   
IsReadOnly$ False��$��20, 65, 20, 20q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID Pedido MK��	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *id_pedido_mk_i+  $ Fecha Pedido��$	 * 
 �� *fecha_Pedido+  $ 
Dirección��$	 * 
 �� *	direccion+  $ 	Proveedor��$	 * 
 �� *	proveedor+  $ Nombre Artículo��$	 * 
 �� *nombre_articulo+  $ Fecha Notificación��$	 * 
 �� *fecha_notificacion+  $ Fecha Gestión��$	 * 
 �� *fecha_gestion+  $ 
Incidencia��$	 * 
 �� *
incidencia+  $ Estado��$	 * 
 �� *estado+  $ 	Solución��$	 * 
 �� *
solucion+�� -     PageButtonStyle1#��[ $��Actualizar Incidencia��$
��Left=�$��
533,21,0,0q�$	��Top=�$	��150��$��29���� -   #��[ $��Duplicar Incidencia (R)��$y�	Collapsed=�$
��Left=�$��
250,21,0,0q�$	��Top=�$	��167��$��29��