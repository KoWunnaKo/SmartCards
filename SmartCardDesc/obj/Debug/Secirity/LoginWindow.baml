   M S B A M L   `   `   `  ����H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SmartCardDesc.SeciritySmartCardDesc.Secirity  5   	   (    "SmartCardDesc.Secirity.LoginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:SmartCardDesc.Secirity     5   	     � Title$  Авторизация��5   	   $��208.834��6   $	��320��6.    � WindowStyle$ 
ToolWindow=�6:    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $	��39* �6   �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      $
��121* �6   �� 5      $
��171* �6   .+�5   
   �� $��0��5      $��0��5      $��
Логин��5      $��20,0,0,0q�5      $
��Left=�5      $��Center=�5      5      �� 5   
   -   5        $��txbLogin��5$      $��Stretch=�5       �� TextWrapping$
 Wrap=�5      $��15q�5       $��20��5!      $��Center=�5#      $��1��5%      ��5"      �� *Login+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�5%      �� 5&   
   $��0��6   $��1��5'      $��Пароль��5(      $
��Left=�5)      $��20,0,0,0q�5*      $��Center=�5+      62   G� 5,   
   -   5        $��txbPassword��53      $��Stretch=�5-      $��Center=�5.      $��20��5/      $��15q�50      $��1��51      $��1��52      54      �� 57   
   $��2��6   $��2��6"   	 �� Orientation$	 
Horizontal=�58      $��Right=�59      .+�5;      �� $��Center=�6   $
��Wrap=�5<      $��Center=�5>      $��22��5?      $	��100��5@      $��2��5A      $��4��5B      $��2��5C      
���  ��$y�Hidden=�5E      ��5=      �� *
StatusText+5E      �� 5H      $
��ОК��5I      $��Center=�5J      $��25��5K      $��Center=�5L      $��84��5M      $��5,0,5,0q�5N      $��2��5P      
 �� 	IsDefault
 . ��5O      �� *LoginCommand+5P      �� 5R      -   5        $��Закрыть��5S      $��Center=�5T      $��25��5U      $��Center=�5V      $��84��5W      $��5,0,5,0q�5Y      $��2��5Z      $��1��5[       �� IsCancel . 6   5\      �� 5_   
   
���   �$��2q�6%   $��Bottom=�5`      $��3��5a      $��2��5b      ._�5c      �� $
��Left=�6   $��Bottom=�5d      $	 
Horizontal=�5e      .+�5f      � -   5        $��prStatus��5g      $��20��5f      $	��100��6*    � IsIndeterminate 5h      �� *IsIntermadiate+�� 5j      -   5        $��	txbStatus��5k      $��10,0,0,0q�5l      
���  ����5m      �� *
StatusText+5n   (   5o      5p      5t      5u      