   M S B A M L   `   `   `  ����  trabajoFinalInterfaces&     trabajoFinalInterfaces.Modificar   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Aav2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6E     N� Title$  	Modificar��5       � 	Resources 5       �� MergedDictionaries �� Source�� 5	   
    5
      �� 5      $ Dictionary1.xaml?�6%   %    5      .��5      � ���#34485e.+�5   
   �� $��Importar MKP��6   $��20(�5      $
��Bold"�6"   
�������$
��Left=�5      $	��Top=�6/   $��	20,10,0,0q�5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgProductos��5         CustomDataGridStyle#��[  6(      AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5      $��20, 65, 20, 20q�5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ 
user_login��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6X   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 69   �� *
user_login+6X     5!      $ 
user_email��6%   $	 * 6X   
 69   �� *
user_email+6X     5"      $ user_nicename��6%   $	 * 6^   
 6<   �� *user_nicename+6^     5#      $ 
first_name��6%   $	 * 6X   
 69   �� *
first_name+6X     5$      $ 	last_name��6%   $	 * 6V   
 68   �� *	last_name+6V     5%      $ telefono��6%   $	 * 6T   
 67   �� *	Teléfono+6T     5&      $ account_funds��6%   $	 * 6^   
 6<   �� *account_funds+6^   5(      �� 5*   
   $��Organizar archivos:��6   $��16(�5+      
�������$
��Left=�5,      $��	98,39,0,0q�6+   $	��Top=�5-      $y�	Collapsed=�6(   �� 5.   
   -   5        $��3A. Preparar archivos��5.        PageButtonStyle2#��[ 61   $
��16.3(�6[   $
��Left=�6k   $��
235,21,0,0q�6�   $	��Top=�6�   $��30��6�   $	��161��6�    � ToolTip$e _Volcar 100 registros de la tabla a un CSV. Si hay 199 o menos se volcarán todos los restantes.��6�   �� 5/   
   $��Total registros:��6   $��18(�50      
�������$
��Left=�51      $��
647,27,0,0q�6(   $	��Top=�52      $	��149��6%   �� 53   
   -   5         ��Name$ txtTotalRegistros��53      
�������$��36(�6C   $
��Left=�54      $��
771,10,0,0q�6    $
��Wrap=�55      $��0��6   $	��Top=�6!   $��80��69   �� 58   
   -   5          PageButtonStyle1#��[ 58      
����ح�$+��%3B. Mover Nuevas Convos a BBDD Global��6R   $	��285��59      $��30��6   
���   �$��Right=�5;      $��	0,21,20,0q�6)   $	��Top=�5<      $Z TTodos los datos de Nuevas Convos se volcarán a BBDD Global para posterior consulta.��6=   �� 5>   
   $��Fin del Proceso:��6   $��20(�5?      $
��Bold"�6   
�������$��Right=�5@      $	��Top=�6)   $��
0,24,308,0q�5A      5E      5F      