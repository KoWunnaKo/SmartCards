   M S B A M L   `   `   `  ���� H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:SmartCardDesc.Controls.OperationLists%SmartCardDesc.Controls.OperationLists  ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  6    0SmartCardDesc.Controls.OperationLists.UcLogsList   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Elocal3clr-namespace:SmartCardDesc.Controls.OperationLists     KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     -   
�������  � 	Resources     aUDITViewSource��%   &	        �� .���  � DataContext# [   � RowDefinitions �� $
��auto ��� $	��500 ��� $��20 �.+�&   System.Windows.Controls.DataGrid  -     Name$ aUDITDataGrid��   RowDetailsVisibilityMode$ VisibleWhenSelected=�$��1��   EnableRowVirtualization .    AutoGenerateColumns .  ��. $
��Auto=�$
��Auto=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   cATEGORYColumn��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 	   Header$	 Категория��1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *
CATEGORY+4  .System.Windows.Controls.DataGridTemplateColumn  -   cREATE_DATEColumn��$
 Auto $	 Дата Создания��   CellTemplate �� .R�(  "System.Windows.Controls.DatePicker     SelectedDate �� *CREATE_DATE+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions .   -   iDColumn��$
 Auto $
	 Ид��
 �� *ID+  -   mESSAGEColumn��$
 Auto $	 Сообщение��
 �� *	MESSAGE+  -   uSER_IDColumn��$
 Auto $	 Пользователь��
 �� *	USER_ID+� -    �Name$ grid1��$
��Left=�$��0��$	��Top=� � ColumnDefinitions �� $
��Auto ��� $
��Auto � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Категория:��$��0��$
��Left=�$��3q�$��0��$��Center=��� -	    ��Name$ cATEGORYTextBox��$��1��$
��Left=�$��23��$��3q�$��0��$��Center=�$	��120������ *
CATEGORY+$ TwoWay=� .  . �� $ ��Дата создания:��$��0��$
��Left=�$��3q�$��1��$��Center=�  -
     Name$ cREATE_DATEDatePicker��$��1��$
��Left=�$��3q�$��1��$��Center=� �� *CREATE_DATE+$ TwoWay=� .  . �� $��Ид:��$��0��$
��Left=�$��3q�$��2��$��Center=��� -   $ 	iDTextBox��$��1��$
��Left=�$��23��$��3q�$��2��$��Center=�$	��120������ *ID+$ TwoWay=� .  . �� $��Сообщение:��$��0��$
��Left=�$��3q�$��3��$��Center=��� -   $ mESSAGETextBox��$��1��$
��Left=�$��23��$��3q�$��3��$��Center=�$	��120������ *	MESSAGE+$ TwoWay=� .  . �� $��Пользователь:��$��0��$
��Left=�$��3q�$��4��$��Center=��� -   $ uSER_IDTextBox��$��1��$
��Left=�$��23��$��3q�$��4��$��Center=�$	��120������ *	USER_ID+$ TwoWay=� .  . �� $��3��
���   �$��2q�$��Bottom=�._��� $
��Left=�$��Bottom=�$��20�� �� Orientation$ 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100�� � IsIndeterminate �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q����� *
StatusText+