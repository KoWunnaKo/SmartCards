   M S B A M L   `   `   `  ����H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:SmartCardDesc.Controls.OperationLists%SmartCardDesc.Controls.OperationLists  5      ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  5      W0clr-namespace:SmartCardDesc.ViewModel.Converters"SmartCardDesc.ViewModel.Converters  5	      7    1SmartCardDesc.Controls.OperationLists.UcCardsList   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Elocal3clr-namespace:SmartCardDesc.Controls.OperationLists     5      KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     5      >l0clr-namespace:SmartCardDesc.ViewModel.Converters     5	      -   5        
�������  � 	Resources  5      8   2SmartCardDesc.ViewModel.Converters.UserIdConverter   UserConverter  cARD_INFOViewSource��%:   &	        &	        5   
   6   �� 5   
   6   5      .��5      �  � DataContext# [ 6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $	��300 �6   �� 5      $��50 �6   �� 5      $��20 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ cARD_INFODataGrid��5         RowDetailsVisibilityMode$ VisibleWhenSelected=�5      $��1��5         EnableRowVirtualization .    AutoGenerateColumns .  ��. $
��Auto=�5      $
��Auto=�5       ��5      ��    Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      -   5        cARD_NUMBERColumn��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 5"   @   	   Header$	 Номер карты��6M   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6b   �� *CARD_NUMBER+  5#      -   5        cARD_STATEColumn��$
 Auto 5#   ?   $	 Статус карты��6L   
 6b   �� *
CARD_STATE+4  .System.Windows.Controls.DataGridTemplateColumn  5%      -   5        cREATE_DATEColumn��$
 Auto 5%   D   $	 Дата создания��6Q      CellTemplate 5&      �� 5'      .R�5(      (  "System.Windows.Controls.DatePicker     SelectedDate 6)   �� *CREATE_DATE+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions . 5)      5+        5,      -   5        cREATE_USERColumn��$
 Auto 5,   @   $	 Создатель��6M   
 6`   �� *CREATE_USER+ �� 	Converter# [    5-      -   5        eXPIRE_DATEColumn��$
 Auto 5-   D   $	 Годен до��6Q    5.      �� 5/      .R�50         6)   �� *EXPIRE_DATE+$ TwoWay=� .  . 51      53      4	  .System.Windows.Controls.DataGridCheckBoxColumn	  54      -   5        iS_ACTIVEColumn��$
 Auto 54   B   $	 Активность��6O   
 6c   �� *	IS_ACTIVE+	  55      -	   5        iS_PRINTEDColumn��$
 Auto 55   C   $	 Напечатаноли��6P   
 6f   �� *
IS_PRINTED+  56      -
   5        iSSUE_DATEColumn��$
 Auto 56   C   $	 Дата выпуска��6P    57      �� 58      .R�59         6)   �� *
ISSUE_DATE+$ TwoWay=� .  . 5:      5<        5=      -   5        oWNER_USERColumn��$
 Auto 5=   ?   $	 Владелец��6L   
 6^   �� *
OWNER_USER+# [    5>      -   5        pICTURE_PATHColumn��$
 Auto 5>   A   $"	 Путь к картинке��6N   
 6g   �� *PICTURE_PATH+  5?      -   5        rEC_IDColumn��$
 Auto 5?   ;   $	 Номер п/п��6H   
 6[   �� *REC_ID+  5@      -   5        sERIAL_NUMBERColumn��$
 Auto 5@   B   $!	 Серийный номер��6O   
 6g   �� *SERIAL_NUMBER+5B      � 5C   
   -   5         �Name$ grid1��5D      $
��Left=�5E      $��0��5F      $	��Top=�5G      $	��333��6%    � ColumnDefinitions 5H      �� 5I      $
��Auto �6#   �� 5J      $	��400 �6#    5L      �� 5M      $
��Auto �6    �� 5N      $
��Auto �6    �� 5O      $
��Auto �6    �� 5P      $
��Auto �6    �� 5Q      $
��Auto �6    �� 5R      $
��Auto �6    �� 5S      $
��Auto �6    �� 5T      $
��Auto �6    �� 5U      $
��Auto �6    �� 5V      $
��Auto �6    �� 5W      $
��Auto �6    �� 5X      $
��Auto �6    �� 5Y      $
��Auto �6    .+�5[      �� $��Номер карты:��6   $��0��6+   $
��Left=�6;   $��3q�6V   $��0��6a   $��Center=�6n   �� 5\      -   5         ��Name$ cARD_NUMBERTextBox��5\      $��1��62   $
��Left=�6B   $��23��6]   $��3q�6i   $��0��6t   $��Center=�6�   $	��120��6   ��6�   �� *CARD_NUMBER+$ TwoWay=� .  . 6   �� 5]      $��Статус карты:��6   $��0��6,   $
��Left=�6<   $��3q�6W   $��1��6b   $��Center=�6o   �� 5^      -   5        $ cARD_STATETextBox��5^      $��1��61   $
��Left=�6A   $��23��6\   $��3q�6h   $��1��6s   $��Center=�6�   $	��120��6�   ��6�   �� *
CARD_STATE+$ TwoWay=� .  . 6�   �� 5e      $��Годен до:��6   $��0��6(   $
��Left=�68   $��3q�6S   $��5��6^   $��Center=�6k     5f      -   5          Name$ eXPIRE_DATEDatePicker��5f      $��1��68   $
��Left=�6H   $��3q�6c   $��5��6n   $��Center=�6�    6{   �� *EXPIRE_DATE+$ TwoWay=� .  . 6�   �� 5g      $$��Активнали карта:��6   $��0��6/   $
��Left=�6?   $��3q�6Z   $��6��6e   $��Center=�6r   �� 5h      -   5         ��Name$ iS_ACTIVECheckBox��5h      $�� ��62   $��1��6=   $
��Left=�6M   $��3q�6�   $��6��6�   $��Center=�6�    d� 	IsChecked 6h   �� *	IS_ACTIVE+$ TwoWay=� .  . 6�   �� 5i      $*��$Напечатанали карта:��6   $��0��62   $
��Left=�6B   $��3q�6]   $��7��6h   $��Center=�6u   �� 5j      -   5        $ iS_PRINTEDCheckBox��5j      $�� ��63   $��1��6>   $
��Left=�6N   $��3q�6�   $��7��6�   $��Center=�6�    6i   �� *
IS_PRINTED+$ TwoWay=� .  . 6�   �� 5k      $��Дата выпуста:��6   $��0��6,   $
��Left=�6<   $��3q�6W   $��8��6b   $��Center=�6o     5l      -   5        $ iSSUE_DATEDatePicker��5l      $��1��67   $
��Left=�6G   $��3q�6b   $��8��6m   $��Center=�6�    6z   �� *
ISSUE_DATE+$ TwoWay=� .  . 6�   �� 5m      $��Владелец:��6   $��0��6(   $
��Left=�68   $��3q�6S   $��9��6^   $��Center=�6k   �� 5n      -   5        $ oWNER_USERTextBox��5n      $��1��61   $
��Left=�6A   $��23��6\   $��3q�6h   $��9��6s   $��Center=�6  $	��300��6)  ��6�   �� *
OWNER_USER+# [  $ TwoWay=� .  . 6)  �� 5o      $#��Путь к картинке:��6   $��0��6/   $
��Left=�6?   $��3q�6Z   $��10��6e   $��Center=�6s   �� 5p      -   5        $ pICTURE_PATHTextBox��5p      $��1��63   $
��Left=�6C   $��23��6^   $��3q�6j   $��10��6u   $��Center=�6�   $	��300��6  ��6�   �� *PICTURE_PATH+$ TwoWay=� .  . 6  �� 5q      $��Номер п/п:��6   $��0��6)   $
��Left=�69   $��3q�6T   $��11��6_   $��Center=�6m   �� 5r      -   5        $ rEC_IDTextBox��5r      $��1��6-   $
��Left=�6=   $��23��6X   $��3q�6d   $��11��6o   $��Center=�6�   $	��200��6�   ��6}   �� *REC_ID+$ TwoWay=� .  . 6�   �� 5s      $"��Серийный номер:��6   $��0��6.   $
��Left=�6>   $��3q�6Y   $��12��6d   $��Center=�6r   �� 5t      -   5        $ sERIAL_NUMBERTextBox��5t      $��1��64   $
��Left=�6D   $��23��6_   $��3q�6k   $��12��6v   $��Center=�6�   $	��200��6  ��6�   �� *SERIAL_NUMBER+$ TwoWay=� .  . 6  5u      �� 5v   
   $��3��6   
���   �$��2q�62   $��Bottom=�5w      ._�5y      �� $
��Left=�6   $��Bottom=�5z      $��20��5{       �� Orientation$ 
Horizontal=�5|      .+�5}      � -   5        $��prStatus��5~      $��20��5}      $	��100��6*    � IsIndeterminate 5      �� *IsIntermadiate+�� 5�      -   5        $��	txbStatus��5�      $��10,0,0,0q�5�      ��5�      �� *
StatusText+63   5�      5�      �� 5�   
   $��2��5�      $ 
Horizontal=�5�      .+�5�      �� -   5        $��btnSave��5�      $��30��5�      $��80��5�      $��10q�5�      $��Сохранить��5�      5�   )   5�      5�      5�      