   M S B A M L   `   `   `  ����E  AGID_Client, Version=2.9.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GID_Client
GID_Client  5   	       GID_Client.VLDataChecker   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:GID_Client     5   	   X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Icon$  ResourcesPic/car.png��5	   	    � Title$ 	Tekshiruv��5
   	   $	��200��6   $	��450��6(   $	��200��5   	   $	��450��6   $	��200��6(   $	��450��68   .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $	��31* �6    � ColumnDefinitions 5   
   �� 5      $
��140* �6   �� 5      $
��191* �6   .+�5   
   �� $��0��6   $��0��5      $��Davlat raqam belgisi��5      $��Right=�5      $��5q�5      $
��Bold"�6   $��16(�61   6A   %  Xceed.Wpf.Toolkit.MaskedTextBox  5   
   -   5        $��	txbDocNum��5"      $��0��5      $��1��5      $��5q�5          Mask$ AAAAAAAA��5!       �� 	MaxLength$ 8��5#       �� CharacterCasing$ Upper=�5&      $��16(�5'   .   ��6   �� *
ftxbDocNum+5(      �� 5*   
   $��1��6   $��0��5+      $��Berilgan Sanasi��5,      $��Right=�5-      $��5q�5.      $
��Bold"�6   $��16(�61   6A     50   
   -   5        $��DpBirthDate��54      $��1��50      $��1��51      $��5q�52      $ 
00.00.0000��53      $��16(�58      ��57      �� *fDpIssueDate+.��59      5:      �� 5<   
   $��2��6   $��0��5=      $��Guvohnoma raqami��5>      $��Right=�5?      $��5q�5@      $
��Bold"�6   $��16(�61   6A     5B   
   -   5        $��txbGuvohnoma��5E      $��2��5B      $��1��5C      $��5q�5D      $ 
LLL0000000��5F      $ 9��5I      $ Upper=�5J      $��16(�5K   /   ��6   �� *ftxbDocNum2+5L      � 5M   
   $��3��6   $��2��6    5N      �� 5O      $��* �6#   �� 5P      $��* �6#   .+�5R      �� -   5        $��btnStart��5T      $��16��5R      $��0��6#   $��10q�5S       �� 	IsDefault . $��Boshlash��5W      {�5V      �� *
Ready2Read+ �� Mode$ TwoWay=���5X      �� *
ReadCard+5Y      �� 5[      -   5        $��	btnCancel��5]      $��16��5[      $��1��6#   $��10q�5\      	 �� IsCancel	 . $��Bekor qilish��5_      5`      5a      5c      5d      