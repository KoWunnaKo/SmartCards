   M S B A M L   `   `   `  ���� H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:SmartCardDesc.Controls.OperationLists%SmartCardDesc.Controls.OperationLists  ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  W0clr-namespace:SmartCardDesc.ViewModel.Converters"SmartCardDesc.ViewModel.Converters  7    1SmartCardDesc.Controls.OperationLists.UcCardsList   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Elocal3clr-namespace:SmartCardDesc.Controls.OperationLists     KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     >l0clr-namespace:SmartCardDesc.ViewModel.Converters     -   
�������  � 	Resources  8   2SmartCardDesc.ViewModel.Converters.UserIdConverter   UserConverter  cARD_INFOViewSource��%   &	        &	        �� .���  � DataContext# [  � RowDefinitions �� $
��auto ��� $	��300 ��� $��50 ��� $��20 �.+�&   System.Windows.Controls.DataGrid  -     Name$ cARD_INFODataGrid��   RowDetailsVisibilityMode$ VisibleWhenSelected=�$��1��   EnableRowVirtualization .    AutoGenerateColumns .  ��. $
��Auto=�$
��Auto=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   cARD_NUMBERColumn��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 	   Header$	 Номер карты��1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *CARD_NUMBER+  -   cARD_STATEColumn��$
 Auto $	 Статус карты��
 �� *
CARD_STATE+4  .System.Windows.Controls.DataGridTemplateColumn  -   cREATE_DATEColumn��$
 Auto $	 Дата создания��   CellTemplate �� .R�(  "System.Windows.Controls.DatePicker     SelectedDate �� *CREATE_DATE+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions .   -   cREATE_USERColumn��$
 Auto $	 Создатель��
 �� *CREATE_USER+ �� 	Converter# [    -   eXPIRE_DATEColumn��$
 Auto $	 Годен до�� �� .R�   �� *EXPIRE_DATE+$ TwoWay=� .  . 4	  .System.Windows.Controls.DataGridCheckBoxColumn	  -   iS_ACTIVEColumn��$
 Auto $	 Активность��
 �� *	IS_ACTIVE+	  -	   iS_PRINTEDColumn��$
 Auto $	 Напечатаноли��
 �� *
IS_PRINTED+  -
   iSSUE_DATEColumn��$
 Auto $	 Дата выпуска�� �� .R�   �� *
ISSUE_DATE+$ TwoWay=� .  .   -   oWNER_USERColumn��$
 Auto $	 Владелец��
 �� *
OWNER_USER+# [    -   pICTURE_PATHColumn��$
 Auto $"	 Путь к картинке��
 �� *PICTURE_PATH+  -   rEC_IDColumn��$
 Auto $	 Номер п/п��
 �� *REC_ID+  -   sERIAL_NUMBERColumn��$
 Auto $!	 Серийный номер��
 �� *SERIAL_NUMBER+� -    �Name$ grid1��$
��Left=�$��0��$	��Top=�$	��333�� � ColumnDefinitions �� $
��Auto ��� $	��400 � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Номер карты:��$��0��$
��Left=�$��3q�$��0��$��Center=��� -    ��Name$ cARD_NUMBERTextBox��$��1��$
��Left=�$��23��$��3q�$��0��$��Center=�$	��120������ *CARD_NUMBER+$ TwoWay=� .  . �� $��Статус карты:��$��0��$
��Left=�$��3q�$��1��$��Center=��� -   $ cARD_STATETextBox��$��1��$
��Left=�$��23��$��3q�$��1��$��Center=�$	��120������ *
CARD_STATE+$ TwoWay=� .  . �� $��Годен до:��$��0��$
��Left=�$��3q�$��5��$��Center=�  -     Name$ eXPIRE_DATEDatePicker��$��1��$
��Left=�$��3q�$��5��$��Center=� �� *EXPIRE_DATE+$ TwoWay=� .  . �� $$��Активнали карта:��$��0��$
��Left=�$��3q�$��6��$��Center=��� -    ��Name$ iS_ACTIVECheckBox��$�� ��$��1��$
��Left=�$��3q�$��6��$��Center=� d� 	IsChecked �� *	IS_ACTIVE+$ TwoWay=� .  . �� $*��$Напечатанали карта:��$��0��$
��Left=�$��3q�$��7��$��Center=��� -   $ iS_PRINTEDCheckBox��$�� ��$��1��$
��Left=�$��3q�$��7��$��Center=� �� *
IS_PRINTED+$ TwoWay=� .  . �� $��Дата выпуста:��$��0��$
��Left=�$��3q�$��8��$��Center=�  -   $ iSSUE_DATEDatePicker��$��1��$
��Left=�$��3q�$��8��$��Center=� �� *
ISSUE_DATE+$ TwoWay=� .  . �� $��Владелец:��$��0��$
��Left=�$��3q�$��9��$��Center=��� -   $ oWNER_USERTextBox��$��1��$
��Left=�$��23��$��3q�$��9��$��Center=�$	��300������ *
OWNER_USER+# [  $ TwoWay=� .  . �� $#��Путь к картинке:��$��0��$
��Left=�$��3q�$��10��$��Center=��� -   $ pICTURE_PATHTextBox��$��1��$
��Left=�$��23��$��3q�$��10��$��Center=�$	��300������ *PICTURE_PATH+$ TwoWay=� .  . �� $��Номер п/п:��$��0��$
��Left=�$��3q�$��11��$��Center=��� -   $ rEC_IDTextBox��$��1��$
��Left=�$��23��$��3q�$��11��$��Center=�$	��200������ *REC_ID+$ TwoWay=� .  . �� $"��Серийный номер:��$��0��$
��Left=�$��3q�$��12��$��Center=��� -   $ sERIAL_NUMBERTextBox��$��1��$
��Left=�$��23��$��3q�$��12��$��Center=�$	��200������ *SERIAL_NUMBER+$ TwoWay=� .  . �� $��3��
���   �$��2q�$��Bottom=�._��� $
��Left=�$��Bottom=�$��20�� �� Orientation$ 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100�� � IsIndeterminate �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q����� *
StatusText+�� $��2��$ 
Horizontal=�.+��� -   $��btnSave��$��30��$��80��$��10q�$��Сохранить��