   M S B A M L   `   `   `  ����H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:SmartCardDesc.Controls.OperationLists%SmartCardDesc.Controls.OperationLists  5      ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  5      6    0SmartCardDesc.Controls.OperationLists.UcLogsList   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Elocal3clr-namespace:SmartCardDesc.Controls.OperationLists     5      KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     5      -   5        
�������  � 	Resources  5         aUDITViewSource��%   &	        �� 5   
   6   5      .��5      �  � DataContext# [  6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $	��500 �6   �� 5      $��20 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ aUDITDataGrid��5         RowDetailsVisibilityMode$ VisibleWhenSelected=�5      $��1��5         EnableRowVirtualization .    AutoGenerateColumns .  ��. $
��Auto=�5      $
��Auto=�5      ��5      ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        cATEGORYColumn��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 5   =   	   Header$	 Категория��6J   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6]   �� *
CATEGORY+4  .System.Windows.Controls.DataGridTemplateColumn  5       -   5        cREATE_DATEColumn��$
 Auto 5    D   $	 Дата Создания��6Q      CellTemplate 5!      �� 5"      .R�5#      (  "System.Windows.Controls.DatePicker     SelectedDate 6)   �� *CREATE_DATE+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions . 5$      5&        5'      -   5        iDColumn��$
 Auto 5'   7   $
	 Ид��6D   
 6P   �� *ID+  5(      -   5        mESSAGEColumn��$
 Auto 5(   <   $	 Сообщение��6I   
 6\   �� *	MESSAGE+  5)      -   5        uSER_IDColumn��$
 Auto 5)   <   $	 Пользователь��6I   
 6_   �� *	USER_ID+5+      � 5,   
   -   5         �Name$ grid1��5,      $
��Left=�6   $��0��6:   $	��Top=�6G    � ColumnDefinitions 5-      �� 5.      $
��Auto �6#   �� 5/      $
��Auto �6#    51      �� 52      $
��Auto �6    �� 53      $
��Auto �6    �� 54      $
��Auto �6    �� 55      $
��Auto �6    �� 56      $
��Auto �6    .+�58      �� $��Категория:��6   $��0��6)   $
��Left=�69   $��3q�6T   $��0��6_   $��Center=�6l   �� 59      -	   5         ��Name$ cATEGORYTextBox��59      $��1��6/   $
��Left=�6?   $��23��6Z   $��3q�6f   $��0��6q   $��Center=�6�   $	��120��6�   ��6~   �� *
CATEGORY+$ TwoWay=� .  . 6�   �� 5:      $ ��Дата создания:��6   $��0��6-   $
��Left=�6=   $��3q�6X   $��1��6c   $��Center=�6p     5;      -
   5          Name$ cREATE_DATEDatePicker��5;      $��1��68   $
��Left=�6H   $��3q�6c   $��1��6n   $��Center=�6�    6{   �� *CREATE_DATE+$ TwoWay=� .  . 6�   �� 5<      $��Ид:��6   $��0��6"   $
��Left=�62   $��3q�6M   $��2��6X   $��Center=�6e   �� 5=      -   5        $ 	iDTextBox��5=      $��1��6)   $
��Left=�69   $��23��6T   $��3q�6`   $��2��6k   $��Center=�6�   $	��120��6�   ��6x   �� *ID+$ TwoWay=� .  . 6�   �� 5>      $��Сообщение:��6   $��0��6)   $
��Left=�69   $��3q�6T   $��3��6_   $��Center=�6l   �� 5?      -   5        $ mESSAGETextBox��5?      $��1��6.   $
��Left=�6>   $��23��6Y   $��3q�6e   $��3��6p   $��Center=�6�   $	��120��6�   ��6}   �� *	MESSAGE+$ TwoWay=� .  . 6�   �� 5@      $��Пользователь:��6   $��0��6,   $
��Left=�6<   $��3q�6W   $��4��6b   $��Center=�6o   �� 5A      -   5        $ uSER_IDTextBox��5A      $��1��6.   $
��Left=�6>   $��23��6Y   $��3q�6e   $��4��6p   $��Center=�6�   $	��120��6�   ��6}   �� *	USER_ID+$ TwoWay=� .  . 6�   5B      �� 5C   
   $��3��6   
���   �$��2q�62   $��Bottom=�5D      ._�5F      �� $
��Left=�6   $��Bottom=�5G      $��20��5H       �� Orientation$ 
Horizontal=�5I      .+�5J      � -   5        $��prStatus��5K      $��20��5J      $	��100��6*    � IsIndeterminate 5L      �� *IsIntermadiate+�� 5N      -   5        $��	txbStatus��5O      $��10,0,0,0q�5P      ��5Q      �� *
StatusText+63   5R      5S      5T      5U      