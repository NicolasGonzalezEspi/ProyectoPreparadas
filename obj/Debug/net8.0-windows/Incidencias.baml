   M S B A M L   `   `   `  ����Q  MtrabajoFinalInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:trabajoFinalInterfacestrabajoFinalInterfaces  5      (    "trabajoFinalInterfaces.Incidencias   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:trabajoFinalInterfaces     5        N� Title$  Incidencias��5	       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Dictionary1.xaml?�6%   %    5      .��5      � ���#34485e.+�5   
   �� $��Incidencias��6   $��20(�5   
   $
��Bold"�6   
�������$
��Left=�5   
   $	��Top=�6%   $��	20,10,0,0q�5   
   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgProductos��5         CustomDataGridStyle#��[  6(      AutoGenerateColumns$ False��5   
      
IsReadOnly$ False��5   
   $��20, 65, 20, 20q�5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Pedido MK��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6^   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6;   �� *id_pedido_mk_i+6^     5      $ Fecha Pedido��6%   $	 * 6\   
 6;   �� *fecha_Pedido+6\     5       $ 
Dirección��6%   $	 * 6V   
 68   �� *	direccion+6V     5!      $ 	Proveedor��6%   $	 * 6V   
 68   �� *	proveedor+6V     5"      $ Nombre Artículo��6%   $	 * 6b   
 6>   �� *nombre_articulo+6b     5#      $ Fecha Notificación��6%   $	 * 6h   
 6A   �� *fecha_notificacion+6h     5$      $ Fecha Gestión��6%   $	 * 6^   
 6<   �� *fecha_gestion+6^     5%      $ 
Incidencia��6%   $	 * 6X   
 69   �� *
incidencia+6X     5&      $ Estado��6%   $	 * 6P   
 65   �� *estado+6P     5'      $ 	Solución��6%   $	 * 6T   
 67   �� *
solucion+6T   5)      �� 5*   
   -   5          PageButtonStyle1#��[ 5*      $��Actualizar Incidencia��6;   $
��Left=�6[   $��
533,21,0,0q�6v   $	��Top=�6�   $	��150��6�   $��29��6�   �� 5+   
   -   5        #��[ 5+      $��Duplicar Incidencia (R)��6;   $y�	Collapsed=�6]   $
��Left=�6t   $��
250,21,0,0q�6�   $	��Top=�6�   $	��167��6�   $��29��6�   6�   5-      5.      