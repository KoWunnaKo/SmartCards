   M S B A M L   `   `   `  ���� H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SmartCardDesc.ControlsSmartCardDesc.Controls  ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  W0clr-namespace:SmartCardDesc.ViewModel.Converters"SmartCardDesc.ViewModel.Converters  (    "SmartCardDesc.Controls.UcPrintCard   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:SmartCardDesc.Controls     KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     >l0clr-namespace:SmartCardDesc.ViewModel.Converters     -   
�������  � 	Resources  7   1SmartCardDesc.ViewModel.Converters.ImageConverter   ImageConverter   DecodeWidth  printerViewViewSource��%(   &	        &	        $	 100���� .���  � DataContext# [  � RowDefinitions �� $
��auto ��� $	��300 ��� $��20 � � ColumnDefinitions �� $��2* ��� $��1* �.+�&   System.Windows.Controls.DataGrid  -     Name$ printerViewDataGrid��   SelectionMode$ Single=�   
IsReadOnly . ��. $
��Auto=�$
��Auto=�   RowDetailsVisibilityMode$ VisibleWhenSelected=�	   EnableRowVirtualization	 . 
   AutoGenerateColumns
 .  $��1��$��2������    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   aDDRESSColumn��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto    Header$ 
Адрес��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	ADDRESS+4  .System.Windows.Controls.DataGridTemplateColumn  -   bIRTH_DATEColumn��$
 Auto $ Год рождения��   CellTemplate �� .R�(  "System.Windows.Controls.DatePicker     SelectedDate �� *
BIRTH_DATE+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions .   -   cARD_NUMBERColumn��$
 Auto $ Номер карты�� �� *CARD_NUMBER+  -   fIRST_NAMEColumn��$
 Auto $ Имя�� �� *
FIRST_NAME+  -   mIDDLE_NAMEColumn��$
 Auto $ Отчество�� �� *MIDDLE_NAME+  -   	pINColumn��$
 Auto $ ПНФЛ�� �� *PIN+4	  .System.Windows.Controls.DataGridCheckBoxColumn	  -	   pRINTEDColumn��$
 Auto $ Напечатано�� �� *	PRINTED+  -
   rEC_IDColumn��$
 Auto $% Порядковый номер�� �� *REC_ID+  -   sURNAME_NAMEColumn��$
 Auto $ Фамилия�� �� *SURNAME_NAME+  -   	tINColumn��$
 Auto $ ИНН�� �� *TIN+� -    �Name$ grid1��$��Stretch=�$	��Top=�$��0�� �� $
��Auto ��� $
��Auto � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Адресс:��$��0��$
��Left=�$��3q�$��7��$��Center=��� -    ��Name$ aDDRESSTextBox��$��1��$��Stretch=�$��23��$��3q�$��7��$��Center=����� *	ADDRESS+$ TwoWay=� .  . �� $��Год рождения:��$��0��$
��Left=�$��3q�$��4��$��Center=�  -     Name$ bIRTH_DATEDatePicker��$��1��$��Stretch=�$��3q�$��4��$��Center=� �� *
BIRTH_DATE+$ TwoWay=� .  . �� $��Номер карты:��$��0��$
��Left=�$��3q�$��8��$��Center=��� -   $ cARD_NUMBERTextBox��$��1��$��Stretch=�$��23��$��3q�$��8��$��Center=����� *CARD_NUMBER+$ TwoWay=� .  . �� $��Имя:��$��0��$
��Left=�$��3q�$��1��$��Center=��� -   $ fIRST_NAMETextBox��$��1��$��Stretch=�$��23��$��3q�$��1��$��Center=����� *
FIRST_NAME+$ TwoWay=� .  . �� $��Отчество:��$��0��$
��Left=�$��3q�$��3��$��Center=��� -   $ mIDDLE_NAMETextBox��$��1��$��Stretch=�$��23��$��3q�$��3��$��Center=����� *MIDDLE_NAME+$ TwoWay=� .  . �� $��	ПНФЛ:��$��0��$
��Left=�$��3q�$��5��$��Center=��� -   $ 
pINTextBox��$��1��$��Stretch=�$��23��$��3q�$��5��$��Center=����� *PIN+$ TwoWay=� .  . �� $��Напечатоно:��$��0��$
��Left=�$��3q�$��6��$��Center=��� -    ��Name$ pRINTEDCheckBox��$�� ��$��1��$
��Left=�$��3q�$��6��$��Center=� d� 	IsChecked �� *	PRINTED+$ TwoWay=� .  . �� $&�� Порядковый Номер:��$��0��$
��Left=�$��3q�$��0��$��Center=��� -   $ rEC_IDTextBox��$��1��$��Stretch=�$��23��$��3q�$��0��$��Center=����� *REC_ID+$ TwoWay=� .  . �� $��Фамилия:��$��0��$
��Left=�$��3q�$��2��$��Center=��� -   $ sURNAME_NAMETextBox��$��1��$��Stretch=�$��23��$��3q�$��2��$��Center=����� *SURNAME_NAME+$ TwoWay=� .  . �� $��Инн:��$��0��$
��Left=�$��3q�$��9��$��Center=��� -   $ 
tINTextBox��$��1��$��Stretch=�$��23��$��3q�$��9��$��Center=����� *TIN+$ TwoWay=� .  . �� $��0��$��1��
����  �$��2q�$��5q�._���  �� Orientation$ Vertical=�.+��� $	��200��$	��200������ *PICTURE_PATH+ �� 	Converter# [  �� -   $��btnSelectImage��$��5q�$��30��$��Выбрать Фото���� -   $��
btnPrinter��$��5q�$��30��$4��.Вызвать программу печати��$��Bottom=��� -   $��btnSave��$��30��$��5q�$��Сохранить���� $��2��$��2��
���   �$��2q�$��Bottom=�._��� $
��Left=�$��Bottom=�$��20��$ 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100�� � IsIndeterminate �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q����� *
StatusText+