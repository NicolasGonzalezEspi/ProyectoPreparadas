   M S B A M L   `   `   `  ����Q  MtrabajoFinalInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:trabajoFinalInterfacestrabajoFinalInterfaces  5      &     trabajoFinalInterfaces.Productos   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:trabajoFinalInterfaces     5        N� Title$  	Productos��5	       � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Dictionary1.xaml?�6%   %    5      .��5      � ���#34485e.+�5   
   �� $��	Productos��6   $��20(�5      $
��Bold"�6   
�������$
��Left=�5      $	��Top=�6(   $��	20,10,0,0q�5      �� 5   
   -   5           PageButtonStyle1#��[  5      $��Dar de Alta��6;   $��Right=�6S   $��
0,21,485,0q�6p   $	��Top=�6�   $��30��6�   $	��142��6�   6�   �� 5   
   -   5        #��[  5      $��Dar de Baja��6;   $��Right=�6S   $��
0,21,320,0q�6p   $	��Top=�6�   $��30��6�   $	��142��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgProductos��5        CustomDataGridStyle#��[ 6(      AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5      $��20, 65, 20, 20q�5   
      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$	 SKU��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6R   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 62   �� *id_producto+6R     5!      $ Nombre Producto��6%   $	 * 6b   
 6>   �� *nombre_producto+6b     5"      $ Modelo��6%   $	 * 6P   
 65   �� *modelo+6P     5#      $ Puntos��6%   $	 * 6P   
 65   �� *puntos+6P     5$      $ Estado��6%   $	 * 6P   
 65   �� *estado+6P   5&      �� 5'   
   -   5        #��[  5'      $��Crear Producto��6;   $
��Left=�6V   $��
150,21,0,0q�6r   $	��Top=�6�   $��30��6�   $	��142��6�   6�   5)      5*      