   M S B A M L   `   `   `  ���� H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:SmartCardDesc.Controls.Warehouse SmartCardDesc.Controls.Warehouse  ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  4    .SmartCardDesc.Controls.Warehouse.UcSpisaniyeOp   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     @local.clr-namespace:SmartCardDesc.Controls.Warehouse     KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     -     >�Name$  UcSpisaniye��
������� � 	Resources    wAREHOUSEViewSource��%   &	        �� .���  � DataContext# [   � RowDefinitions �� $	��157 ��� $	��23* ��� $
��165* ��� $	��42* ��� $	��31* �.+�� $��2�� �� $	��38* ��� $	��43* ��� $	��30* ��� $	��54* � � ColumnDefinitions �� $
��149* ��� $
��294* �.+��� $��2��$��4��
����  �$��2q��� $��0��$��0��$��	Дата:��$��	5,12,0,10q�$
��Left=�$��Center=�(  "System.Windows.Controls.DatePicker  -   $��dpSpisaniyeDt��$��0��$��1��$	��5,7q�$��Center=�   Text ��  �� Path$ UcSpisaniye� �� ElementName$ 	Inputdate���� $��1��$��0��$��Количество:��$��	5,14,0,13q�$
��Left=�$��Center=��� -   $��	txbAmount��$��1��$��1��$��	5,10,5,10q����� $ UcSpisaniye�$ Quantity���� -   $��btnWithdrawal��$��5��$��1��$��5q�$��Списать��&   System.Windows.Controls.DataGrid  -     Name$ wAREHOUSEDataGrid��	   SelectionMode$	 Single=�
   
IsReadOnly
 .    AutoGenerateColumns .     EnableRowVirtualization .    RowDetailsVisibilityMode$ VisibleWhenSelected=�$��0��$��5q�����    Columns 4  .System.Windows.Controls.DataGridTemplateColumn  -   iNPUT_DTColumn��,  &System.Windows.Controls.DataGridColumn   Header$ Дата ввода��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto    CellTemplate �� .R�     SelectedDate �� *
INPUT_DT+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions . 0  *System.Windows.Controls.DataGridTextColumn  -   iTEN_NAMEColumn��$ Наименование��$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	ITEN_NAME+  -   qUANTITYColumn��$ Количество��$
 Auto  �� *
QUANTITY+  -	   rEC_IDColumn��$
 Ид��$
 Auto  �� *REC_ID+  -
   tOTAL_AMOUNTColumn��$% Общее количество��$
 Auto  �� *TOTAL_AMOUNT+�� 
���   �$��2q�$��Bottom=�$��4��$��2��._��� $
��Left=�$��Bottom=� �� Orientation$ 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100�� � IsIndeterminate �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q����� *
StatusText+