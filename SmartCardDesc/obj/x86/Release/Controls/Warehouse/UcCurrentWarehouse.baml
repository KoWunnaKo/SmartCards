   M S B A M L   `   `   `  ���� H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:SmartCardDesc.Controls.Warehouse SmartCardDesc.Controls.Warehouse  ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  9    3SmartCardDesc.Controls.Warehouse.UcCurrentWarehouse   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     @local.clr-namespace:SmartCardDesc.Controls.Warehouse     KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     -   
�������  � 	Resources     wAREHOUSEViewSource��%   &	        �� .���  � DataContext# [   � RowDefinitions �� $	��175 ��� $
��200* ��� $��20 �.+�� -    �Name$ grid1��$	��Top=�$��2��$
��Left=�# [  $��0�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+��� $��Center=�$��0��$��3q�$
��Left=�$��0��$��Дата ввода:��(  "System.Windows.Controls.DatePicker  -     Name$ iNPUT_DTDatePicker��$��Center=�$��0��$��3q�$
��Left=�$��1��   SelectedDate �� *
INPUT_DT+ �� Mode$ TwoWay=� �� NotifyOnValidationError . 	 �� ValidatesOnExceptions	 . �� $��Center=�$��1��$��3q�$
��Left=�$��0��$��Наименование:���� -   
 ��Name$
 iTEN_NAMETextBox��$	��120��$��Center=�$��1��$��3q�$��23��$
��Left=�$��1������ *	ITEN_NAME+$ TwoWay=� . 	 . �� $��Center=�$��2��$��3q�$
��Left=�$��0��$��Количество:���� -   $
 qUANTITYTextBox��$	��120��$��Center=�$��2��$��3q�$��23��$
��Left=�$��1������ *
QUANTITY+$ TwoWay=� . 	 . �� $��Center=�$��3��$��3q�$
��Left=�$��0��$��Номер п/п:���� -   $
 rEC_IDTextBox��$	��120��$��Center=�$��3��$��3q�$��23��$
��Left=�$��1������ *REC_ID+$ TwoWay=� . 	 . �� $��Center=�$��4��$��3q�$
��Left=�$��0��$��Общее сумма:���� -   $
 tOTAL_AMOUNTTextBox��$	��120��$��Center=�$��4��$��3q�$��23��$
��Left=�$��1������ *TOTAL_AMOUNT+$ TwoWay=� . 	 . &   System.Windows.Controls.DataGrid  -     Name$ wAREHOUSEDataGrid��   AutoGenerateColumns .     EnableRowVirtualization . $��1��   RowDetailsVisibilityMode$ VisibleWhenSelected=�����    Columns 4  .System.Windows.Controls.DataGridTemplateColumn  -	   iNPUT_DTColumn��,  &System.Windows.Controls.DataGridColumn   Header$ Дата ввода��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto    CellTemplate �� .R�   �� *
INPUT_DT+$ TwoWay=� . 	 . 0  *System.Windows.Controls.DataGridTextColumn  -
   iTEN_NAMEColumn��$ Наименование��$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	ITEN_NAME+  -   qUANTITYColumn��$ Количество��$
 Auto  �� *
QUANTITY+  -   rEC_IDColumn��$ Номер п/п��$
 Auto  �� *REC_ID+  -   tOTAL_AMOUNTColumn��$ Общая сумма��$
 Auto  �� *TOTAL_AMOUNT+�� 
���   �$��2q�$��Bottom=�$��2��$��2��._��� $
��Left=�$��Bottom=� �� Orientation$ 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100�� � IsIndeterminate �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q����� *
StatusText+