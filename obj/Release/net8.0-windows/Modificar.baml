   M S B A M L   `   `   `  ����   trabajoFinalInterfaces&     trabajoFinalInterfaces.Modificar   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Aav2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       N� Title$  	Modificar�� � 	Resources  �� MergedDictionaries �� Source��  �� $ Dictionary1.xaml?�%    .��� ���#34485e.+��� $��Importar MKP��$��20(�$
��Bold"�
�������$
��Left=�$	��Top=�$��	20,10,0,0q�&   System.Windows.Controls.DataGrid  -     Name$ dgProductos��   CustomDataGridStyle#��[     AutoGenerateColumns$ False��   
IsReadOnly$
 True��$��20, 65, 20, 20q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
user_login��	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *
user_login+  $ 
user_email��$	 * 
 �� *
user_email+  $ user_nicename��$	 * 
 �� *user_nicename+  $ 
first_name��$	 * 
 �� *
first_name+  $ 	last_name��$	 * 
 �� *	last_name+  $ telefono��$	 * 
 �� *	Teléfono+  $ account_funds��$	 * 
 �� *account_funds+�� $��Organizar archivos:��$��16(�
�������$
��Left=�$��	98,39,0,0q�$	��Top=�$y�	Collapsed=��� -   $��Preparar archivos��  PageButtonStyle2#��[ $
��16.3(�$
��Left=�$��
246,21,0,0q�$	��Top=�$��30��$	��150�� � ToolTip$e _Volcar 100 registros de la tabla a un CSV. Si hay 199 o menos se volcarán todos los restantes.���� $��Total registros:��$��18(�
�������$��Center=�$��
0,29,100,0q�$	��Top=��� -    ��Name$ txtTotalRegistros��
�������$��36(�$��Center=�$��0,10,-100,0q�$
��Wrap=�$��0��$	��Top=�$��80���� -     PageButtonStyle1#��[ 
����ح�$'��!Mover Nuevas Convos a BBDD Global��$	��270��$��30��
���   �$��Right=�$��	0,21,20,0q�$	��Top=�$Z TTodos los datos de Nuevas Convos se volcarán a BBDD Global para posterior consulta.���� $��Fin del Proceso:��$��20(�$
��Bold"�
�������$��Right=�$	��Top=�$��
0,24,295,0q�