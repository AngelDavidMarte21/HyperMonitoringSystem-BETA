   M S B A M L   `   `   `  ����J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5   	       WpfApplication1.Cliente   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication1     5   	     � Title$  Cliente��5   	   $	��450��6   $	��800��6&   .��5	      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��
dg_eventos��5         AutoGenerateColumns .     
IsReadOnly .    GridLinesVisibility$ 
Horizontal=�6T      HorizontalGridLinesBrush
 �����$��0,40,0,0q�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Panel��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 3* 6O   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *panel+6O     5      $
 Area��6%   $ 3* 6M    63   �� *area+6M     5      $
 Zona��6%   $ 1* 6M    63   �� *zone+6M     5      $ Evento��6%   $ 3* 6Q    65   �� *evento+6Q     5      $
 Tipo��6%   $ 2* 6M    63   �� *type+6M     5      $ Llegada��6%   $ 3* 6P    66   �� *time+6P   5      �� 5   
   -   5        $��	puertoTxt��5   �   $
��Left=�6   $��23��6-   $��	10,10,0,0q�69   	 �� TextWrapping$
	 Wrap=�6L   $
��1921��6`   $	��Top=�6l   $	��225��6�   6�   �� 5   
   -   5        $��Listen��5      $
��Left=�6"   $��	240,9,0,0q�6=   $	��Top=�6P   $��75��6h   $��26��6s   6   5      5      