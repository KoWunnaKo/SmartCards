   M S B A M L   `   `   `  ����L  HKadastr.WinClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GID_Client
GID_Client  5   	        GID_Client.MondatoryWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   $localclr-namespace:GID_Client  5   	   -   5          � Icon$  ResourcesPic/car.png��5	   	    � Title$ 	Tekshiruv��5
   	   $	��200��6   $	��450��6(   $	��200��5   	   $	��450��6   $	��200��6(   $	��450��68   .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $	��31* �6    � ColumnDefinitions 5   
   �� 5      $
��130* �6   �� 5      $
��191* �6   .+�5   
   �� $��0��6   $��0��5      $��Guvohnoma raqami��5      $��Right=�5      $��5q�5      $
��Bold"�6   $��16(�61   6A   %  Xceed.Wpf.Toolkit.MaskedTextBox  5   
   -   5        $��	txbDocNum��5"      $��0��5      $��1��5      $��5q�5          Mask$ 	LL0000000��5!       �� 	MaxLength$ 9��5#       �� CharacterCasing$ Upper=�5$      -  'System.Windows.Input.KeyboardNavigation   TabIndex$ 0��5%      $��16(�5)   .   ��6   �� *
ftxbDocNum+5*      �� 5,   
   $��1��6   $��0��5-      $��Tug'ilgan sanasi��5.      $��Right=�5/      $��5q�50      $
��Bold"�6   $��16(�61   6A     52   
   -   5        $��DpBirthDate��56      $��1��52      $��1��53      $��5q�54      $ 
00.00.0000��55      $ 1��59      $��16(�5;      ��5:      �� *fDpBirthDate+.��5<      5>      �� 5@   
   $��2��6   $��0��5A      $��Yaroqlik muddati��5B      $��Right=�5C      $��5q�5D      $
��Bold"�6   $��16(�61   6A     5F   
   -   5        $��DpExpireDate��5J      $��2��5F      $��1��5G      $��5q�5H      $ 
00.00.0000��5I      $ 2��5M      $��16(�5N   4   ��6   �� *fDpExpireDate+.��5O      5P      � 5Q   
   $��3��6   $��2��6    5R      �� 5S      $��* �6#   �� 5T      $��* �6#   �� 5U      $��* �6#   .+�5W      �� -   5        $��btnStart��5Y      $��16��5W      $��0��6#   $��10q�5X       �� 	IsDefault . $��Boshlash��5\      {�5[      �� *
Ready2Read+	 �� Mode$	 TwoWay=���5]      �� *
ReadCard+5^      �� 5`      -   5        $��	btnCancel��5b      $��16��5`      $��1��6#   $��10q�5a      
 �� IsCancel
 .  $��Tozalash��5d      5e      �� 5g      -   5        $��btnClose��5i      $��16��5g      $��2��6#   $��10q�5h      
 . $��Yopmoq��5k      5l      5m      5o      5p      