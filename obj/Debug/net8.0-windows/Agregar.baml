   M S B A M L   `   `   `  ����  trabajoFinalInterfaces$    trabajoFinalInterfaces.Agregar   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Agregar��5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5
      $ Dictionary1.xaml?�6%   %    5      .��5      � ���#34485e.+�5   
   �� $��Formato MKP��6   $��20(�5      $
��Bold"�6"   
�������$
��Left=�5      $	��Top=�6/   $��	20,10,0,0q�5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgProductos��5         CustomDataGridStyle#��[  6)      AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5      $��20, 65, 20, 20q�5      $��2��6+      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
user_login��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6W   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 69   �� *	userlogin+6W     5       $ 
user_email��6%   $	 * 6W   
 69   �� *	useremail+6W     5!      $ user_nicename��6%   $	 * 6]   
 6<   �� *usernicename+6]     5"      $ 
first_name��6%   $	 * 6W   
 69   �� *	firstname+6W     5#      $ 	last_name��6%   $	 * 6U   
 68   �� *
lastname+6U     5$      $ telefono��6%   $	 * 6T   
 67   �� *
telefono+6T     5%      $ account_funds��6%   $	 * 6]   
 6<   �� *accountfunds+6]   5'      �� 5(   
   -   5        $ ��Exportar a CSV (deprecado)��5(      $
��Left=�66   $��4,17,0,0q�6Q   $	��Top=�6c    J� RenderTransformOrigin$ 0.205,-0.024&�6{   $y�	Collapsed=�6�   
����d�$	��158��6�   $��20��6�   $��1��6�   �� 5)      -   5         ��Name$ BtnImportarMKP��5)   3     PageButtonStyle2#��[ 6	   $#��2A. Importar Saldos MKP (CSV)��5*   	    � ToolTip$: 4Importar el estado actual del MKP: Usuarias y puntos��5+   	   $
��Left=�5,   	   $��
187,21,0,0q�6$   $	��Top=�68   $��30��5-   2   $	��220��6>   $��1��6J   �� 5/      -   5          PageButtonStyle1#��[ 5/   	   $��2B. Totalizar Saldos��50   	   $� |A igual DNI, se obtiene el saldo actual sumándolo a las nuevas convocatorias. Los datos se volcarán a Importar Marketplace��51   	   $
��Left=�52   	   $��
422,21,0,0q�6$   $	��Top=�68   $	��214��53   !   $��30��6-   $��1��69   �� 55   
   -   5         ��Name$ txtTotalRegistros��55      
�������$��36(�6C   $
��Left=�56      $��
771,10,0,0q�6(   $
��Wrap=�57      $��0��6!   $	��Top=�6*   $��72��6B    58      _� 59      $6��0El número de registros que quedan por preparar.��6   5;      �� 5<   
   $��Total registros:��6   $��18(�5=      
�������$
��Left=�5>      $��
647,27,0,0q�6(   $	��Top=�5?      $	��149��6%    5@      _� 5A      $���|El número muestra el número de registros que quedan por preparar. Se irán volcando datos en bloques de 100-200 usuarias. ��6   5C      5E      5F      