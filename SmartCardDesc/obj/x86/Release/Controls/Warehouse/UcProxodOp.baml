   M S B A M L   `   `   `  ���� H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:SmartCardDesc.Controls.Warehouse SmartCardDesc.Controls.Warehouse  1    +SmartCardDesc.Controls.Warehouse.UcProxodOp   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     @local.clr-namespace:SmartCardDesc.Controls.Warehouse     
�������.���   � RowDefinitions  �� $	��39* ��� $	��44* ��� $	��47* ��� $	��45* ��� $	��23* ��� $	��46* ��� $
��176* ��� $��20 � � ColumnDefinitions �� $	��79* ��� $
��135* �.+��� $��2��$��6��
����  �$��2q��� $��0��$��0��$��	Дата:��$��	5,12,0,11q�$
��Left=�$��Center=�(  "System.Windows.Controls.DatePicker  $��0��$��1��$��5q�$��Center=�   Text �� *	Inputdate+�� $��1��$��0��$��Сумма:��$��	5,14,0,14q�$
��Left=�$��Center=��� $��1��$��1��$��	5,10,5,10q����� *Amount+�� $��2��$��0��$��Количество:��$��	5,16,0,15q�$
��Left=�$��Center=��� $��2��$��1��$��	5,10,5,10q����� *
Quantity+�� $��3��$��0��$��Наименование:��$��	5,15,0,14q�$
��Left=�$��Center=��� $��3��$��1��$��	5,10,5,10q����� *Name+�� $��5��$��1��$��5q�$��Оприходовать������ *	InputItem+�� 
���   �$��2q�$��Bottom=�$��7��$��2��._��� $
��Left=�$��Bottom=� �� Orientation$ 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100�� � IsIndeterminate �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q����� *
StatusText+