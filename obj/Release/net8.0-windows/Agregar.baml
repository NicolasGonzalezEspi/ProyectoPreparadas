   M S B A M L   `   `   `  ����   trabajoFinalInterfaces$    trabajoFinalInterfaces.Agregar   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       N� Title$  Agregar�� � 	Resources  �� MergedDictionaries �� Source��  �� $ Dictionary1.xaml?�%    .��� ���#34485e.+��� $��Formato MKP��$��20(�$
��Bold"�
�������$
��Left=�$	��Top=�$��	20,10,0,0q�&   System.Windows.Controls.DataGrid  -     Name$ dgProductos��   CustomDataGridStyle#��[     AutoGenerateColumns$ False��   
IsReadOnly$
 True��$��20, 65, 20, 20q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
user_login��	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *	userlogin+  $ 
user_email��$	 * 
 �� *	useremail+  $ user_nicename��$	 * 
 �� *usernicename+  $ 
first_name��$	 * 
 �� *	firstname+  $ 	last_name��$	 * 
 �� *
lastname+  $ telefono��$	 * 
 �� *
telefono+  $ account_funds��$	 * 
 �� *accountfunds+�� -   $ ��Exportar a CSV (deprecado)��$
��Left=�$��
215,17,0,0q�$	��Top=� J� RenderTransformOrigin$ 0.205,-0.024&�$y�	Collapsed=�
����d�$	��158��$��20���� -    ��Name$ BtnImportarMKP��  PageButtonStyle2#��[ $#��1. Importar Saldos MKP (CSV) �� � ToolTip$: 4Importar el estado actual del MKP: Usuarias y puntos��$
��Left=�$��
241,21,0,0q�$	��Top=�$��30��$	��211���� -     PageButtonStyle1#��[ $��2. Totalizar Saldos��$� |A igual DNI, se obtiene el saldo actual sumándolo a las nuevas convocatorias. Los datos se volcarán a Importar Marketplace��$
��Left=�$��
474,21,0,0q�$	��Top=�$	��187��$��30���� -    ��Name$ txtTotalRegistros��
�������$��36(�$
��Left=�$��
906,10,0,0q�$
��Wrap=�$��0��$	��Top=�$��80�� _� $6��0El número de registros que quedan por preparar.���� $��Total registros:��$��18(�
�������$
��Left=�$��
786,29,0,0q�$	��Top=� _� $���|El número muestra el número de registros que quedan por preparar. Se irán volcando datos en bloques de 100-200 usuarias. ��