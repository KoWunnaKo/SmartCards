   M S B A M L   `   `   `  ���� E  AGID_Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GID_Client
GID_Client       GID_Client.MondatoryWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  $localclr-namespace:GID_Client  -     � Icon$  ResourcesPic/car.png�� � Title$ 	Tekshiruv��$	��200��$	��450��$	��200��$	��450��$	��200��$	��450��.���  � RowDefinitions �� $��40 ��� $��40 ��� $��40 ��� $	��31* � � ColumnDefinitions �� $
��130* ��� $
��191* �.+��� $��0��$��0��$��Guvohnoma raqami��$��Right=�$��5q�$
��Bold"�$��16(�%  Xceed.Wpf.Toolkit.MaskedTextBox  -   $��	txbDocNum��$��0��$��1��$��5q�   Mask$ 	LL0000000�� �� 	MaxLength$ 9�� �� CharacterCasing$ Upper=�-  'System.Windows.Input.KeyboardNavigation   TabIndex$ 0��$��16(����� *
ftxbDocNum+�� $��1��$��0��$��Tug'ilgan sanasi��$��Right=�$��5q�$
��Bold"�$��16(�  -   $��DpBirthDate��$��1��$��1��$��5q�$ 
00.00.0000��$ 1��$��16(����� *fDpBirthDate+.���� $��2��$��0��$��Yaroqlik muddati��$��Right=�$��5q�$
��Bold"�$��16(�  -   $��DpExpireDate��$��2��$��1��$��5q�$ 
00.00.0000��$ 2��$��16(����� *fDpExpireDate+.��� $��3��$��2�� �� $��* ��� $��* ��� $��* �.+��� -   $��btnStart��$��16��$��0��$��10q� �� 	IsDefault . $��Boshlash��{��� *
Ready2Read+	 �� Mode$	 TwoWay=����� *
ReadCard+�� -   $��	btnCancel��$��16��$��1��$��10q�
 �� IsCancel
 .  $��Tozalash���� -   $��btnClose��$��16��$��2��$��10q�
 . $��Yopmoq��