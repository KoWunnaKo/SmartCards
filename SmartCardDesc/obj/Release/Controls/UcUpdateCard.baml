   M S B A M L   `   `   `  ����   SmartCardDesc)    #SmartCardDesc.Controls.UcUpdateCard   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     
�������.���   � RowDefinitions  �� $��* ��� $��* �.+��� $��0�� �� Orientation$ Vertical=�.+��� $%��Ввести параметры���� 
����  �$��2q�._��   �� $��* ��� $��* ��� $��* ��� $��* ��� $��* � � ColumnDefinitions �� $��* ��� $��* �.+��� $#��Ид Пользователя��$��0��$��0��$��5q�� $��0��$��1�� �� $��2* ��� $��1* �.+��� -   $��	txbUserId��$��5q�$��0������  �� Path$ UserId� �� Mode$ TwoWay=��� $��1��$��5q�$��
Токен������ $ GetToken��� $��
Токен��$��1��$��0��$��5q��� $��1��$��1��$��5q� �� 
IsReadOnly . ���� $ Token��� $��Номер карты��$��2��$��0��$��5q��� $��2��$��1��$��5q��� $��Дата выпуска��$��3��$��0��$��5q�(  "System.Windows.Controls.DatePicker  $��3��$��1��$��5q��� $��Годен до��$��4��$��0��$��5q�  $��4��$��1��$��5q��� $ 
Horizontal=�$	��Top=�$��Right=�.+��� $��Очистить��$��80��$��25��$��10q��� $��Загрузить...��$��80��$��25��$��10q��� $��10q�.+��� $��Результаты:���� $��1��
��� � �$��2q�._��  �� $
��259* ��� $
��404* �  �� $	��37* ��� $
��193* �.+��� $��0��$��0��$��Center=�$
��Left=�$��Результат:��$��5q��� $��0��$��1��$��5q�.��