   M S B A M L   `   `   `  ���� Q  MtrabajoFinalInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:trabajoFinalInterfacestrabajoFinalInterfaces  &     trabajoFinalInterfaces.Productos   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:trabajoFinalInterfaces       N� Title$  	Productos�� � 	Resources  �� MergedDictionaries �� Source��  �� $ Dictionary1.xaml?�%    .��� ���#34485e.+��� $��	Productos��$��20(�$
��Bold"�
�������$
��Left=�$	��Top=�$��	20,10,0,0q��� -      PageButtonStyle1#��[  $��Dar de Alta��$��Right=�$��
0,21,485,0q�$	��Top=�$��30��$	��142���� -   #��[  $��Dar de Baja��$��Right=�$��
0,21,320,0q�$	��Top=�$��30��$	��142��&   System.Windows.Controls.DataGrid  -     Name$ dgProductos��  CustomDataGridStyle#��[    AutoGenerateColumns$ False��   
IsReadOnly$
 True��$��20, 65, 20, 20q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$	 SKU��	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *id_producto+  $ Nombre Producto��$	 * 
 �� *nombre_producto+  $ Modelo��$	 * 
 �� *modelo+  $ Puntos��$	 * 
 �� *puntos+  $ Estado��$	 * 
 �� *estado+�� -   #��[  $��Crear Producto��$
��Left=�$��
150,21,0,0q�$	��Top=�$��30��$	��142��