   M S B A M L   `   `   `  ����  trabajoFinalInterfaces!    trabajoFinalInterfaces.Main   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$!  Gestión Marketplace - V2.1��5   	    � WindowState$ 	Maximized=�6,    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5	      $ Dictionary1.xaml?�6%   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      $��* �6   .+�5   
   � $��0��6   ���#2c3e50.+�5      �� $
��Auto=�6   .��5      �� $��Center=�6   $��Center=�6:   $��10q�6U   .+�5      ��    TitleTextStyle#��[  6     �� TextAlignment$ Center=�6I   $��Grupo��6a   $��0,-10q�6n   �� 5      #��[  6    $ Center=�6I   $��Solutia��6a   $
��0,00q�6p   �� 5      $��1��6    
���   �$	��102��6:   $y�Visible=�6F   $��0, 2, 0, 10q�6[   �� 5        TitleTextStyle2#��[ 6    $ Center=�6I   $��
Preparadas��6`   $��
0,-12,0,10q�6r   �� 5      $��Gestión Puntos��6    $��23(�66   $
��Bold"�6D   ���#ecf0f1$ Center=�6k   $��0,15,0,5q�6�   �� 5       -   5         ��Name$ 	BtnBorrar��5         HubPrincipalButtonStyle1#��[ 60   $
��21.5(�6b   $��1. Nuevas Convos��6r   $
��0,10q�6�   �� 5!      -   5        $ 
BtnAgregar��5!      #��[ 61   $
��21.5(�6c   $��2. Formato MKP��6s   $
��0,10q�6�   �� 5"      -   5        $ BtnModificar��5"      #��[ 63   $
��21.5(�6e   $��3. Importar MKP��6u   $
��0,10q�6�   �� 5#      -   5        $ 	BtnInicio��5#      #��[ 60   $
��21.5(�6b   $��4. BBDD Global��6r   $
��0,10q�6�   �� 5%      $��Gestión Pedidos��6    $��23(�67   $
��Bold"�6E   ���#ecf0f1$ Center=�6l   $��0,15,0,5q�6�   �� 5'      -   5        $ 
BtnPedidos��5'        HubPrincipalButtonStyle3#��[ 61   $
��21.5(�6c   $��Pedidos��6s   $
��0,10q�6�   6�   �� 5(      -   5        $ BtnIncidencias��5(      #��[ 65   $
��21.5(�6g   $��Incidencias��6w   $
��0,10q�6�   6�   �� 5)      -   5        $ BtnProductos��5)      #��[ 63   $
��21.5(�6e   $��	Productos��6u   $
��0,10q�6�   6�   �� 5+      $��Supervisión��6    $��23(�63   $
��Bold"�6A   ���#ecf0f1$ Center=�6h   $��0,15,0,5q�6   �� 5,      -   5        $ BtnActividad��5,      #��[ 63   $
��21.5(�6e   $��	Actividad��6u   $
��0,10q�6�    � ToolTip$ Hola  ��6�   �� 5-      -	   5        $ BtnReportes��5-      #��[ 62   $
��21.5(�6d   $��Reportes PDF��6t   $
��0,10q�6�   $y�	Collapsed=�6�   �� 5.      -
   5        $ BtnDashboard��5.      #��[ 63   $
��21.5(�6e   $��	Dashboard��6u   $
��0,10q�6�   6�   �� 5/      -   5        $��Cerrar Sesión��5/      $��0,30,0,0q�6M   $y�	Collapsed=�6_   �� 50      -   5        $
��21.5(�50      $y�	Collapsed=�6-   $�� ��6D   ���#2c3e54$��0,-15q�6~   $	��102��6�   $��22��6�   51      52      53       � 56   
   -   5        	  �Name$	 	MainFrame��56      
  � NavigationUIVisibility$
 Hidden=�6#   $��1��6C   57      58      