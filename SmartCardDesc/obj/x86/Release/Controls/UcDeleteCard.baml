   M S B A M L   `   `   `  ���� H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SmartCardDesc.ControlsSmartCardDesc.Controls  )    #SmartCardDesc.Controls.UcDeleteCard   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:SmartCardDesc.Controls     
�������.���   � RowDefinitions  �� $��* ��� $��* ��� $��20 �.+��� $��0�� �� Orientation$ Vertical=�.+��� $%��Ввести параметры���� 
����  �$��2q�._�� $��-2,0,2,0q�  �� $��* � � ColumnDefinitions �� $��1* ��� $��2* �.+��� $#��Ид Пользователя��$��0��$��0��$��5q�� $��0��$��1�� �� $��2* �.+��� -   $��	cxbUserId��$��5q�$��0������  �� Path$ 	UsersList� �� SelectedValue �� $ SelectedUser� �� SelectedIndex �� $ SelectedIndex����� .R��� $��2q�
����  � �� CornerRadius$ 5��$��8q� �� Padding$ 4q�._��� $ 
Horizontal=�.+��� $��2q����� *LOGIN+�� $��2q����� *SURNAME_NAME+�� $��2q����� *
FIRST_NAME+�� $��1��$��5q�$��
Токен��$y�Hidden=����� $ GetToken��� $��
Токен��$��1��$��5q�$y�Hidden=��� $��1��$��1��$��5q� �� 
IsReadOnly . $y�Hidden=����� $ Token�.���� $ 
Horizontal=�$	��Top=�$��Right=�.+��� $��Очистить��$��80��$��25��$��10q����� *ClearResults+�� $��Загрузить...��$��80��$��25��$��10q����� *LoadResults+�� $��10q�.+��� $��Результаты:���� $��1��
��� � �$��2q�._��  �� $��84 ���   �� $��37 ��� $
��193* �.+��� $��0��$��0��$��Center=�$
��Left=�$��Результат:��$��	5,10,0,11q�$��16��$��55���� $��0��$��1��$��5q� . ���� $ Model.result�.���� 
���   �$��2q�$��Bottom=�$��2��._��� $
��Left=�$��Bottom=�$ 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100��	 � IsIndeterminate	 �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q����� *
StatusText+