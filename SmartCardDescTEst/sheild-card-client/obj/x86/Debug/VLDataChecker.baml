   M S B A M L   `   `   `  ����E  AGID_Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GID_Client
GID_Client  5   	       GID_Client.VLDataChecker   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:GID_Client     5   	   X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -   5          � Icon$  ResourcesPic/car.png��5
   	    � Title$ 	Tekshiruv��5   	   $	��200��6   $	��450��6(   $	��200��5   	   $	��450��6   $	��200��6(   $	��450��68   .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $	��31* �6    � ColumnDefinitions 5   
   �� 5      $
��140* �6   �� 5      $
��191* �6   .+�5   
   �� $��0��6   $��0��5      $��Davlat raqam belgisi��5      $��Right=�5      $��5q�5      $
��Bold"�6   $��16(�61   6A   %  Xceed.Wpf.Toolkit.MaskedTextBox  5   
   -   5        $��	txbDocNum��5#      $��0��5      $��1��5       $��5q�5!         Mask$ AAAAAAAA��5"       �� 	MaxLength$ 10��5$       �� CharacterCasing$ Upper=�5'      $��16(�5(   .   ��6   �� *
ftxbDocNum+5)      �� 5+   
   $��1��6   $��0��5,      $��Berilgan Sanasi��5-      $��Right=�5.      $��5q�5/      $
��Bold"�6   $��16(�61   6A     51   
   -   5        $��DpBirthDate��55      $��1��51      $��1��52      $��5q�53      $ 
00.00.0000��54      $��16(�59      ��58      �� *fDpIssueDate+.��5:      5;      �� 5=   
   $��2��6   $��0��5>      $��Guvohnoma raqami��5?      $��Right=�5@      $��5q�5A      $
��Bold"�6   $��16(�61   6A     5C   
   -   5        $��txbGuvohnoma��5F      $��2��5C      $��1��5D      $��5q�5E      $ 
LLL0000000��5G      $ 9��5J      $ Upper=�5K      $��16(�5L   /   ��6   �� *ftxbDocNum2+5M      � 5N   
   $��3��6   $��2��6    5O      �� 5P      $��* �6#   �� 5Q      $��* �6#   �� 5R      $��* �6#   .+�5T      �� -   5        $��btnStart��5V      $��16��5T      $��0��6#   $��10q�5U       �� 	IsDefault . $��Boshlash��5Y      {�5X      �� *
Ready2Read+ �� Mode$ TwoWay=���5Z      �� *
ReadCard+5[      �� 5]      -   5        $��	btnCancel��5_      $��16��5]      $��1��6#   $��10q�5^      	 �� IsCancel	 .  $��Tozalash��5a      5b      �� 5d      -   5        $��btnClose��5f      $��16��5d      $��2��6#   $��10q�5e      	 . $��Yopmoq��5h      5i      5j      5l      5m      