   M S B A M L   `   `   `  ����Q  MtrabajoFinalInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:trabajoFinalInterfacestrabajoFinalInterfaces  5      &     trabajoFinalInterfaces.Actividad   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:trabajoFinalInterfaces     5        N� Title$  	Actividad��5	       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Dictionary1.xaml?�6%   %    5      .��5      � ���#34485e.+�5   
   �� $��Actividad Puntos��6   $��20(�5      $
��Bold"�6"   
�������$
��Left=�5      $	��Top=�6/   $��	20,10,0,0q�5      $��0��5      �� 5   
   $��Actividad Pedidos��6   $��20(�5      $
��Bold"�6!   
�������$
��Left=�5       $	��Top=�6.   $��	20,10,0,0q�5!      $y�	Collapsed=�6&   $��1��5"      &   System.Windows.Controls.DataGrid  5)   
   -   5          Name$ dgPuntos��5)         CustomDataGridStyle#��[  5*         AutoGenerateColumns$ False��5+         
IsReadOnly$ False��5,      $��10, 65, 20, 20q�5-      $��0��5.         Columns 5/      4  .System.Windows.Controls.DataGridTemplateColumn  50      ,  &System.Windows.Controls.DataGridColumn   Header$ Fecha Actividad��6)   	   Width5  /System.Windows.Controls.DataGridLengthConverter$
	 0.6* 6B   
   CellTemplate
 51      �� 52      .R�53      �� $
��Wrap=�6Q   ��6(   �� *fecha_actividad_usuario+6Q   54      56        57      $ 	Actividad��6)   $	 * 6<   
 58      �� 59      .R�5:      �� $
��Wrap=�6C   ��6(   �� *	actividad+6C   5;      5=        5>      $ Siguiente Paso��6)   $	 * 6A   
 5?      �� 5@      .R�5A      �� $
��Wrap=�6H   ��6(   �� *siguiente_paso+6H   5B      5D      5F      5K      5L      