   M S B A M L   `   `   `  ����E  AGID_Client, Version=2.9.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GID_Client
GID_Client  5   	        GID_Client.MondatoryWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   $localclr-namespace:GID_Client  5   	     � Icon$  ResourcesPic/car.png��5	   	    � Title$ 	Tekshiruv��5
   	   $	��200��6   $	��450��6(   $	��200��5   	   $	��450��6   $	��200��6(   $	��450��68   .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $	��31* �6    � ColumnDefinitions 5   
   �� 5      $
��130* �6   �� 5      $
��191* �6   .+�5   
   �� $��0��6   $��0��5      $��Guvohnoma raqami��5      $��Right=�5      $��5q�5      $
��Bold"�6   $��16(�61   6A   %  Xceed.Wpf.Toolkit.MaskedTextBox  5   
   -   5        $��	txbDocNum��5!      $��0��5      $��1��5      $��5q�5         Mask$ 	LL0000000��5        �� 	MaxLength$ 9��5"       �� CharacterCasing$ Upper=�5#      -  'System.Windows.Input.KeyboardNavigation   TabIndex$ 0��5$      $��16(�5(   .   ��6   �� *
ftxbDocNum+5)      �� 5+   
   $��1��6   $��0��5,      $��Tug'ilgan sanasi��5-      $��Right=�5.      $��5q�5/      $
��Bold"�6   $��16(�61   6A     51   
   -   5        $��DpBirthDate��55      $��1��51      $��1��52      $��5q�53      $ 
00.00.0000��54      $ 1��58      $��16(�5:      ��59      �� *fDpBirthDate+.��5;      5=      �� 5?   
   $��2��6   $��0��5@      $��Yaroqlik muddati��5A      $��Right=�5B      $��5q�5C      $
��Bold"�6   $��16(�61   6A     5E   
   -   5        $��DpExpireDate��5I      $��2��5E      $��1��5F      $��5q�5G      $ 
00.00.0000��5H      $ 2��5L      $��16(�5M   4   ��6   �� *fDpExpireDate+.��5N      5O      � 5P   
   $��3��6   $��2��6    5Q      �� 5R      $��* �6#   �� 5S      $��* �6#   .+�5U      �� -   5        $��btnStart��5W      $��16��5U      $��0��6#   $��10q�5V       �� 	IsDefault . $��Boshlash��5Z      {�5Y      �� *
Ready2Read+	 �� Mode$	 TwoWay=���5[      �� *
ReadCard+5\      �� 5^      -   5        $��	btnCancel��5`      $��16��5^      $��1��6#   $��10q�5_      
 �� IsCancel
 . $��Bekor qilish��5b      5c      5d      5f      5g      