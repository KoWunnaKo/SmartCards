   M S B A M L   `   `   `  ����   SmartCardDesc)    #SmartCardDesc.Controls.UcInsertCard   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Hi:http://schemas.microsoft.com/expression/2010/interactivity     
�������.���   � RowDefinitions  �� $��2* ��� $��1* ��� $��20 �.+��� $��0�� �� Orientation$ Vertical=�.+��� $%��Ввести параметры���� 
����  �$��2q�._�� $��-2,0,2,0q�  �� $��* ��� $��0 ��� $��* ��� $��* ��� $��* ��� $��* � � ColumnDefinitions �� $��1* ��� $��2* �.+��� $#��Ид Пользователя��$��0��$��0��$��5q�� $��0��$��1�� �� $��2* �.+��� -   $��	cxbUserId��$��5q�$��0������  �� Path$ 	UsersList� �� SelectedValue �� $ SelectedUser� �� SelectedIndex �� $ SelectedIndex����� .R��� $��2q�
����  � �� CornerRadius$ 5��$��8q� �� Padding$ 4q�._��� $ 
Horizontal=�.+��� $��2q����� *LOGIN+�� $��2q����� *SURNAME_NAME+�� $��2q����� *
FIRST_NAME+�� $��1��$��5q�$��
Токен��$y�Hidden=����� $ GetToken��� $��
Токен��$��1��$��5q�$y�Hidden=��� $��1��$��1��$��5q� �� 
IsReadOnly . $y�Hidden=����� $ Token�.���� $��Номер карты��$��2��$��0��$��5q�� $��2��$��1�� �� $��2* ��� $��1* �.+��� $��5q����� $ Number�.���� $��1��$��5q�$��Получить������ *	GetNumber+�� $
��RFID��$��5��$��0��$��5q�� $��5��$��1�� �� $��2* ��� $��1* �.+��� $��5q� . ���� $
 Rfid�.���� $��1��$��5q�$��Получить������ *	GetRfId+�� $��Дата выпуска��$��3��$��0��$��5q�(  "System.Windows.Controls.DatePicker  $��3��$��1��$��5q�	   SelectedDate	 �� *	IssueDate+
 �� Mode$
 TwoWay=��� $��Годен до��$��4��$��0��$��5q�  $��4��$��1��$��5q�	 �� *
ExpireDate+$
 TwoWay=��� $ 
Horizontal=�$	��Top=�$��Right=�.+��� $��Очистить��$��80��$��25��$��10q����� *ClearResults+�� $��Загрузить...��$��80��$��25��$��10q����� *LoadResults+�� $��1��
��� � �$��2q�._��  �� $��84 ���   �� $��37 ��� $
��193* �.+��� $��0��$��0��$��Center=�$
��Left=�$��Результат:��$��	5,10,0,11q�$��16��$��55���� $��0��$��1��$��5q� . ���� $ Model.result�.���� 
���   �$��2q�$��Bottom=�$��2��._��� $
��Left=�$��Bottom=�$ 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100�� � IsIndeterminate �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q����� *
StatusText+