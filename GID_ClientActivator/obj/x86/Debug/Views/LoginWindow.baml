   M S B A M L   `   `   `  ����E  AGID_Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SmartCardDesc.SeciritySmartCardDesc.Secirity  5   	   (    "SmartCardDesc.Secirity.LoginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:SmartCardDesc.Secirity     5   	     � Title$  Avtorizasiya��5   	   $��208.834��6   $	��320��6/    � WindowStyle$ 
ToolWindow=�6;    � WindowStartupLocation$ CenterScreen=�5	   	    � Icon$0 */GID_Client;component/ResourcesPic/car.png��5
   	   .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $	��39* �6   �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      $
��121* �6   �� 5      $
��171* �6   .+�5   
   �� $��0��5      $��0��5      $��Login��5      $��20,0,0,0q�5      $
��Left=�5      $��Center=�5      5      �� 5   
   -   5        $��txbLogin��5%      $��Stretch=�5       �� TextWrapping$
 Wrap=�5       $��15q�5!      $��20��5"      $��Center=�5$      $��1��5&      ��5#      �� *Login+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated . 	 �� UpdateSourceTrigger$	 PropertyChanged=�5&      �� 5'   
   $��0��6   $��1��5(      $��Parol��5)      $
��Left=�5*      $��20,0,0,0q�5+      $��Center=�5,      62   G� 5-   
   -   5        $��txbPassword��54      $��Stretch=�5.      $��Center=�5/      $��20��50      $��15q�51      $��1��52      $��1��53      55      �� 58   
   $��2��6   $��2��6"   
 �� Orientation$
 
Horizontal=�59      $��Right=�5:      .+�5<      �� $��Center=�6   $
��Wrap=�5=      $��Center=�5?      $��22��5@      $	��100��5A      $��2��5B      $��4��5C      $��2��5D      
���  ��$y�Hidden=�5F      ��5>      �� *
StatusText+5F      �� 5I      $��Kirish��5J      $��Center=�5K      $��25��5L      $��Center=�5M      $��84��5N      $��5,0,5,0q�5O      $��2��5Q       �� 	IsDefault . ��5P      �� *LoginCommand+5Q      �� 5S      -   5        $��Bekor qilish��5T      $��Center=�5U      $��25��5V      $��Center=�5W      $��84��5X      $��5,0,5,0q�5Z      $��2��5[      $��1��5\       �� IsCancel . 6   5]      �� 5`   
   
���   �$��2q�6%   $��Bottom=�5a      $��3��5b      $��2��5c      ._�5d      �� $
��Left=�6   $��Bottom=�5e      $
 
Horizontal=�5f      .+�5g      � -   5        $��prStatus��5h      $��20��5g      $	��100��6*    � IsIndeterminate 5i      �� *IsIntermadiate+�� 5k      -   5        $��	txbStatus��5l      $��10,0,0,0q�5m      
���  ����5n      �� *
StatusText+5o   (   5p      5q      5u      5v      