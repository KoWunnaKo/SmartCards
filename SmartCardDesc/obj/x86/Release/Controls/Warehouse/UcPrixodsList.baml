   M S B A M L   `   `   `  ���� H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:SmartCardDesc.Controls.Warehouse SmartCardDesc.Controls.Warehouse  ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  W0clr-namespace:SmartCardDesc.ViewModel.Converters"SmartCardDesc.ViewModel.Converters  4    .SmartCardDesc.Controls.Warehouse.UcPrixodsList   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     @local.clr-namespace:SmartCardDesc.Controls.Warehouse     KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     >l0clr-namespace:SmartCardDesc.ViewModel.Converters     -   
�������$��477.66��  � 	Resources  8   2SmartCardDesc.ViewModel.Converters.UserIdConverter   UserConverter  wAREHOUSE_DTLViewSource��%   &	        &	        �� .���  � DataContext# [  � RowDefinitions �� $
��auto ��� $��1* ��� $��20 �.+�� -    �Name$ grid1��$	��Top=�$
��Left=�# [  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+��� $��Center=�$��0��$��3q�$
��Left=�$��0��$��Создал:���� -    ��Name$ cREATE_USERTextBox��$	��120��$��Center=�$��0��$��3q�$��23��$
��Left=�$��1������ *CREATE_USER+ �� 	Converter# [   �� Mode$ TwoWay=� �� NotifyOnValidationError . 	 �� ValidatesOnExceptions	 . �� $��Center=�$��1��$��3q�$
��Left=�$��0��$��Тип операции:���� -   $ oP_TYPETextBox��$	��120��$��Center=�$��1��$��3q�$��23��$
��Left=�$��1������ *	OP_TYPE+$ TwoWay=� . 	 . �� $��Center=�$��2��$��3q�$
��Left=�$��0��$(��"Родительский ключ:���� -   $ pARENT_IDTextBox��$	��120��$��Center=�$��2��$��3q�$��23��$
��Left=�$��1������ *	PARENT_ID+$ TwoWay=� . 	 . �� $��Center=�$��3��$��3q�$
��Left=�$��0��$��Количество:���� -   $ quantityTextBox��$	��120��$��Center=�$��3��$��3q�$��23��$
��Left=�$��1������ *
quantity+$ TwoWay=� . 	 . �� $��Center=�$��4��$��3q�$
��Left=�$��0��$��Номер п/п:���� -   $ rEC_IDTextBox��$	��120��$��Center=�$��4��$��3q�$��23��$
��Left=�$��1������ *REC_ID+$ TwoWay=� . 	 . �� $��Center=�$��5��$��3q�$
��Left=�$��0��$��Статус:���� -   $ sTATETextBox��$	��120��$��Center=�$��5��$��3q�$��23��$
��Left=�$��1������ *STATE+$ TwoWay=� . 	 . �� $��Center=�$��6��$��3q�$
��Left=�$��0��$��Сумма:���� -	   $ tR_AMOUNTTextBox��$	��120��$��Center=�$��6��$��3q�$��23��$
��Left=�$��1������ *	TR_AMOUNT+$ TwoWay=� . 	 . �� $��Center=�$��7��$��3q�$
��Left=�$��0��$ ��Дата операции:��(  "System.Windows.Controls.DatePicker  -
   
  Name$
 tR_DATEDatePicker��$��Center=�$��7��$��3q�$
��Left=�$��1��   SelectedDate �� *	TR_DATE+$ TwoWay=� . 	 . &   System.Windows.Controls.DataGrid  -     Name$ wAREHOUSE_DTLDataGrid��   SelectionMode$ Single=�   
IsReadOnly .    AutoGenerateColumns .     EnableRowVirtualization . $��1��   RowDetailsVisibilityMode$ VisibleWhenSelected=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   cREATE_USERColumn��,  &System.Windows.Controls.DataGridColumn   Header$ Создал��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *CREATE_USER+# [    -   oP_TYPEColumn��$ Тип операции��$
 Auto  �� *	OP_TYPE+  -   pARENT_IDColumn��$' !Родительский ключ��$
 Auto  �� *	PARENT_ID+  -   quantityColumn��$ Количество��$
 Auto  �� *
quantity+  -   rEC_IDColumn��$ Номер п/п��$
 Auto  �� *REC_ID+  -   sTATEColumn��$ Статус��$
 Auto  �� *STATE+  -   tR_AMOUNTColumn��$ 
Сумма��$
 Auto  �� *	TR_AMOUNT+4  .System.Windows.Controls.DataGridTemplateColumn  -   tR_DATEColumn��$ Дата операции��$
 Auto    CellTemplate �� .R�   �� *	TR_DATE+$ TwoWay=� . 	 . �� 
���   �$��2q�$��Bottom=�$��2��$��2��._��� $
��Left=�$��Bottom=� �� Orientation$ 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100�� � IsIndeterminate �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q����� *
StatusText+