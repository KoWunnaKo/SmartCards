   M S B A M L   `   `   `  ���� H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:SmartCardDesc.Controls.OperationLists%SmartCardDesc.Controls.OperationLists  ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  7    1SmartCardDesc.Controls.OperationLists.UcUsersList   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Elocal3clr-namespace:SmartCardDesc.Controls.OperationLists     KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     -   
�������  � 	Resources     uSERViewSource��%   &	        �� .���  � DataContext# [   � RowDefinitions �� $
��auto ��� $	��200 ��� $��50 ��� $��20 �.+�&   System.Windows.Controls.DataGrid  -     Name$ uSERDataGrid��   AutoGenerateColumns .     EnableRowVirtualization .    RowDetailsVisibilityMode$ VisibleWhenSelected=�$��1����. $
��Auto=�$
��Auto=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   rEC_IDColumn��,  &System.Windows.Controls.DataGridColumn   Header$ Номер п/п��	   Width5  /System.Windows.Controls.DataGridLengthConverter$
	 Auto 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *REC_ID+  -   sURNAME_NAMEColumn��$ Фамилия��$
	 Auto 
 �� *SURNAME_NAME+  -   fIRST_NAMEColumn��$ Имя��$
	 Auto 
 �� *
FIRST_NAME+  -   mIDDLE_NAMEColumn��$ Отчество��$
	 Auto 
 �� *MIDDLE_NAME+4  .System.Windows.Controls.DataGridTemplateColumn  -   bIRTH_DATEColumn��$ Дата рождения��$
	 Auto    CellTemplate �� .R�(  "System.Windows.Controls.DatePicker     SelectedDate �� *
BIRTH_DATE+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions .   -   aDDRESSColumn��$ 
Адрес��$
	 Auto 
 �� *	ADDRESS+4  .System.Windows.Controls.DataGridCheckBoxColumn  -	   cARD_FLGColumn��$% Выпущенали карта��$
	 Auto 
 �� *
CARD_FLG+  -
   cERT_CRT_FLGColumn��$- 'Полученли сертификат��$
	 Auto 
 �� *CERT_CRT_FLG+  -   cERT_WRT_FLGColumn��$- 'Записанли сертификат��$
	 Auto 
 �� *CERT_WRT_FLG+  -   gENDERColumn��$ Пол��$
	 Auto 
 �� *GENDER+  -   iS_ACTIVEColumn��$1 +Активенли пользователь��$
	 Auto 
 �� *	IS_ACTIVE+  -   kEY_FLGColumn��$+ %Сгенерированли ключ��$
	 Auto 
 �� *	KEY_FLG+  -   lOGINColumn��$ 
Логин��$
	 Auto 
 �� *LOGIN+  -   pASSPORTColumn��$ Пасспорт��$
	 Auto 
 �� *
PASSPORT+  -   	pINColumn��$ 
ПИНФЛ��$
	 Auto 
 �� *PIN+  -   rEG_DATEColumn��$% Дата регистрации��$
	 Auto  �� .R�   �� *
REG_DATE+$ TwoWay=� .  .   -   	tINColumn��$ ИНН��$
	 Auto 
 �� *TIN+� -    �Name$ grid1��$
��Left=�$��0��$	��Top=� � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
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
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Адрес:��$��0��$
��Left=�$��3q�$��14��$��Center=��� -    ��Name$ aDDRESSTextBox��$��1��$
��Left=�$��23��$��3q�$��14��$��Center=�$	��120������ *	ADDRESS+$ TwoWay=� .  . �� $ ��Дата рождения:��$��0��$
��Left=�$��3q�$��16��$��Center=�  -     Name$ bIRTH_DATEDatePicker��$��1��$
��Left=�$��3q�$��16��$��Center=� �� *
BIRTH_DATE+$ TwoWay=� .  . �� $&�� Выпущенали карта:��$��2��$
��Left=�$��3q�$��1��$��Center=��� -    ��Name$ cARD_FLGCheckBox��$�� ��$��3��$
��Left=�$��3q�$��1��$��Center=� d� 	IsChecked �� *
CARD_FLG+$ TwoWay=� .  . �� $.��(Полученли сертификат:��$��2��$
��Left=�$��3q�$��2��$��Center=��� -   $ cERT_CRT_FLGCheckBox��$�� ��$��3��$
��Left=�$��3q�$��2��$��Center=� �� *CERT_CRT_FLG+$ TwoWay=� .  . �� $.��(Записанли сертификат:��$��2��$
��Left=�$��3q�$��0��$��Center=��� -   $ cERT_WRT_FLGCheckBox��$�� ��$��3��$
��Left=�$��3q�$��0��$��Center=� �� *CERT_WRT_FLG+$ TwoWay=� .  . �� $��Имя:��$��0��$
��Left=�$��3q�$��2��$��Center=��� -   $ fIRST_NAMETextBox��$��1��$
��Left=�$��23��$��3q�$��2��$��Center=�$	��120������ *
FIRST_NAME+$ TwoWay=� .  . �� $��Пол:��$��0��$
��Left=�$��3q�$��9��$��Center=��� -   $ gENDERCheckBox��$�� ��$��1��$
��Left=�$��3q�$��9��$��Center=� �� *GENDER+$ TwoWay=� .  . �� $2��,Активенли пользователь:��$��2��$
��Left=�$��3q�$��3��$��Center=��� -   $ iS_ACTIVECheckBox��$�� ��$��3��$
��Left=�$��3q�$��3��$��Center=� �� *	IS_ACTIVE+$ TwoWay=� .  . �� $,��&Сгенерированли ключ:��$��2��$
��Left=�$��3q�$��9��$��Center=��� -   $ kEY_FLGCheckBox��$�� ��$��3��$
��Left=�$��3q�$��9��$��Center=� �� *	KEY_FLG+$ TwoWay=� .  . �� $��Логин:��$��0��$
��Left=�$��3q�$��10��$��Center=��� -   $ lOGINTextBox��$��1��$
��Left=�$��23��$��3q�$��10��$��Center=�$	��120������ *LOGIN+$ TwoWay=� .  . �� $��Отчество:��$��0��$
��Left=�$��3q�$��3��$��Center=��� -   $ mIDDLE_NAMETextBox��$��1��$
��Left=�$��23��$��3q�$��3��$��Center=�$	��120������ *MIDDLE_NAME+$ TwoWay=� .  . �� $��Пасспорт:��$��0��$
��Left=�$��3q�$��12��$��Center=��� -    $ pASSPORTTextBox��$��1��$
��Left=�$��23��$��3q�$��12��$��Center=�$	��120������ *
PASSPORT+$ TwoWay=� .  . �� $��ПИНФЛ:��$��0��$
��Left=�$��3q�$��13��$��Center=��� -!   $ 
pINTextBox��$��1��$
��Left=�$��23��$��3q�$��13��$��Center=�$	��120������ *PIN+$ TwoWay=� .  . �� $��Номер п/п:��$��0��$
��Left=�$��3q�$��0��$��Center=��� -"   $ rEC_IDTextBox��$��1��$
��Left=�$��23��$��3q�$��0��$��Center=�$	��120������ *REC_ID+$ TwoWay=� .  . �� $&�� Дата регистрации:��$��0��$
��Left=�$��3q�$��15��$��Center=�  -#   $ rEG_DATEDatePicker��$��1��$
��Left=�$��3q�$��15��$��Center=� �� *
REG_DATE+$ TwoWay=� .  . �� $��Фамилия:��$��0��$
��Left=�$��3q�$��1��$��Center=��� -$   $ sURNAME_NAMETextBox��$��1��$
��Left=�$��23��$��3q�$��1��$��Center=�$	��120������ *SURNAME_NAME+$ TwoWay=� .  . �� $��ИНН:��$��0��$
��Left=�$��3q�$��17��$��Center=��� -%   $ 
tINTextBox��$��1��$
��Left=�$��23��$��3q�$��17��$��Center=�$	��120������ *TIN+$ TwoWay=� .  . �� $��3��
���   �$��2q�$��Bottom=�._��� $
��Left=�$��Bottom=�$��20�� �� Orientation$ 
Horizontal=�.+�� -&   $��prStatus��$��20��$	��100�� � IsIndeterminate �� *IsIntermadiate+�� -'   $��	txbStatus��$��10,0,0,0q����� *
StatusText+�� $��2��$ 
Horizontal=�.+��� -(   $��btnSave��$��30��$��80��$��10q�$��Сохранить��