   M S B A M L   `   `   `  ����   SmartCardDesc'    !SmartCardDesc.Controls.UcCardInfo   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     
�������.���   � RowDefinitions  �� $��1* ��� $��2* ��� $��20 �.+��� $��0�� �� Orientation$ Vertical=�.+��� $%��Ввести параметры���� 
����  �$��2q�._��   �� $��1* ��� $��2* � � ColumnDefinitions �� $��* ��� $��* �.+��� $#��Ид Пользователя��$��0��$��0��$��5q�� $��0��$��1�� �� $��2* �.+��� -   $��	cxbUserId��$��5q�$��0������  �� Path$ 	UsersList� �� SelectedValue �� $ SelectedUser� �� SelectedIndex �� $ SelectedIndex����� .R��� $��2q�
����  � �� CornerRadius$ 5��$��8q� �� Padding$ 4q�._��� $ 
Horizontal=�.+��� $��2q����� *LOGIN+�� $��2q����� *SURNAME_NAME+�� $��2q����� *
FIRST_NAME+�� $��1��$��5q�$��
Токен��$y�Hidden=����� $ GetToken��� $��
Токен��$��1��$��0��$��5q�$y�Hidden=��� -   $��tbxToken��$��1��$��1��$��5q� �� 
IsReadOnly . $y�Hidden=����� $ Token�	 �� Mode$	 TwoWay=��� $ 
Horizontal=�$	��Top=�$��Right=�.+��� $��Очистить��$��80��$��25��$��10q����� *ClearParams+�� $��Загрузить...��$��80��$��25��$��10q����� *GetCardInfo+�� $��10q�.+��� $��Результаты:���� $��1��
��� � �$��2q�._��   �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��* ��� $��* �.+��� $��0��$��0��$��Center=�$
��Left=�$��Результат:��$��5q��� $��0��$��1��$��10q� . ���� $ CardInfo.result�.���� $��1��$��0��$��Center=�$
��Left=�$��Статус карты:��$��5q��� $��1��$��1��$��10q� . ���� $ CardInfo.card_stat�.���� $��2��$��0��$��Center=�$
��Left=�$��Дата выпуска:��$��5q��� $��2��$��1��$��10q� . ���� $ CardInfo.issue_date�.���� $��3��$��0��$��Center=�$
��Left=�$��Номер карты:��$��5q��� $��3��$��1��$��10q� . ���� $ CardInfo.card_num�.���� $��4��$��0��$��Center=�$
��Left=�$��Юзер Ид:��$��5q��� $��4��$��1��$��10q� . ���� $ CardInfo.user_id�.���� $��5��$��0��$��Center=�$
��Left=�$��Годен до:��$��5q��� $��5��$��1��$��10q� . ���� $ CardInfo.expiry_date�.���� 
���   �$��2q�$��Bottom=�$��2��._��� $
��Left=�$��Bottom=�$ 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100��
 � IsIndeterminate
 �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q����� *
StatusText+