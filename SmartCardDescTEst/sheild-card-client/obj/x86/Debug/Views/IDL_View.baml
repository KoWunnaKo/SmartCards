   M S B A M L   `   `   `  ����E  AGID_Client, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GID_Client.ViewsGID_Client.Views  5      ;"clr-namespace:GID_Client.ViewModelGID_Client.ViewModel  5
          GID_Client.Views.IDL_View   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:GID_Client.Views     5      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      *l"clr-namespace:GID_Client.ViewModel  5
      
�������$��11(�5        � 	Resources  5      /   )GID_Client.ViewModel.Base64ImageConverter   Base64ImageConverter��%   &	          5   
   6!   5      .��5      %  Xceed.Wpf.Toolkit.BusyIndicator  -   5          Name$ _busyIndicator��5         IsBusy 61   �� *IsIntermadiate+   BusyContent 6S   �� *
StatusText+.��5      �  � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��20 �6   .+�5   
   � $��0��6   $��0��5       5      �� 5      $	��250 �6    �� 5      $��30 �6    �� 5      $	��100 �6    �� 5       $��50 �6    .+�5"      �� -   5        $��PortraitPhoto��5#      $
��Auto��5$      $
��Auto��5%      $��0��5&      $��10q�5'      ��5(      �� *Base64ImageData+ �� 	Converter# [  6k   �� 5*      -   5        $��	SignPhoto��5/      $
��Auto��5+      $��2��5,      $
��Auto��5-      $��10q�5.      ��50      �� *Base64ImageData2+# [  51      �� 53      $��3��6   .+�54      �� $��50��55      $��5q�56      $&�� Прочитать с карты��57      $y�Hidden=�59      ��58      �� *
ReadCard+5:      �� 5;      $��5q�5<      $��50��5=      $��Сохранить��5>      $y�Hidden=�5@      ��5?      �� *
SaveCard+5A      5C      5D      �� 5E      $��1��6   $��0��5F      .+�5G      �  5H      �� 5I      $	��260 �6'   �� 5J      $	��250 �6'   �� 5K      $
��250* �6'   .+�5M      � $��0��6   $��0��5N       5O      �� 5P      $��50 �6    �� 5Q      $��50 �6    �� 5R      $��50 �6    �� 5S      $��50 �6    �� 5T      $��50 �6    �� 5U      $
��100* �6    .+�5W      � $��0��6   $��0��5X       5Y      �� 5Z      $��20 �6$   �� 5[      $��30 �6$   .+�5]      �� $��
Familiyasi��6   $��0��5^      $
��Left=�5_      $��Center=�5`      $��2,0,0,0q�5a      $��14(�6-   5b      �� 5c      $��2q�5d      $��1��5e       �� 
IsReadOnly . $��14(�5g   '   ��5f      �� *
LastName+5g   7   5h      � 5i      $��0��6   $��1��5j       5k      �� 5l      $��20 �6$   �� 5m      $��30 �6$   .+�5o      �� $
��Ismi��6   $��0��5p      $
��Left=�5q      $��Center=�5r      $��2,0,0,0q�5s      $��14(�6-   5t      �� 5u      $��2q�5v      $��1��5w       . $��14(�5y   '   ��5x      �� *	FirstName+5y   7   5z      � 5{      $��0��6   $��2��5|       5}      �� 5~      $��20 �6$   �� 5      $��30 �6$   .+�5�      �� $��Otasining ismi��6   $��0��5�      $
��Left=�5�      $��Center=�5�      $��2,0,0,0q�5�      $��14(�6-   5�      �� 5�      $��2q�5�      $��1��5�       . $��14(�5�   '   ��5�      �� *
MiddleName+5�   7   5�      � 5�      $��0��6   $��3��5�       5�      �� 5�      $��20 �6$   �� 5�      $��30 �6$   .+�5�      �� $��Tug'ilgan kuni��6   $��0��5�      $
��Left=�5�      $��Center=�5�      $��2,0,0,0q�5�      $��14(�6-   5�      �� 5�      $��2q�6   $��1��5�       . $��14(�5�   /   ��5�      �� *	BirthDate+.��5�      5�      5�      � 5�      $��0��6   $��4��5�       5�      �� 5�      $��20 �6$   �� 5�      $��30 �6$   .+�5�      �� $��Tug'ilgan joyi��6   $��0��5�      $
��Left=�5�      $��Center=�5�      $��2,0,0,0q�5�      $��14(�6-   5�      �� 5�      $��2q�5�      $��1��5�       . $��14(�5�   '   ��5�      �� *
BirthPlace+5�   7   5�      5�      � 5�      $��1��6   $��0��5�      $��10,0,0,0q�5�       5�      �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $��50 �6    �� 5�      $	��50* �6    .+�5�      � $��0��6   $��0��5�       5�      �� 5�      $��20 �6$   �� 5�      $��30 �6$   .+�5�      �� $��Berilgan sanasi��6   $��0��5�      $
��Left=�5�      $��Center=�5�      $��2,0,0,0q�5�      $��14(�6-   5�      �� 5�      $��2q�5�      $��1��5�       . $��14(�5�   '   ��5�      �� *	IssueDate+.��5�   5   5�      5�      � 5�      $��0��6   $��1��5�       5�      �� 5�      $��20 �6$   �� 5�      $��30 �6$   .+�5�      �� $��Berilgan joyi��6   $��0��5�      $
��Left=�5�      $��Center=�5�      $��2,0,0,0q�5�      $��14(�6-   5�      �� 5�      $��2q�5�      $��1��5�       . $��14(�5�   '   ��5�      �� *
GivenPlace+5�   7   5�      � 5�      $��0��6   $��2��5�       5�      �� 5�      $��20 �6$   �� 5�      $��30 �6$   .+�5�      �� $��Yaroqlilik sanasi��6   $��0��5�      $
��Left=�5�      $��Center=�5�      $��2,0,0,0q�5�      $��14(�6-   5�      �� 5�      $��2q�5�      $��1��5�       . $��14(�5�   1   ��6   �� *
ExpireDate+.��5�      5�      5�      � 5�      $��0��6   $��3��5�       5�      �� 5�      $��20 �6$   �� 5�      $��30 �6$   .+�5�      �� $��Guvohnoma raqami��6   $��0��5�      $
��Left=�5      $��Center=�5     $��2,0,0,0q�5     $��14(�6-   5     �� 5     $��2q�5     $��1��5      . $��14(�5  '   ��5     �� *LicenseNumber+5  7   5	     � 5
     $��0��6   $��4��5      5     �� 5     $��20 �6$   �� 5     $��30 �6$   .+�5     �� $��JSHIR��6   $��0��5     $
��Left=�5     $��Center=�5     $��2,0,0,0q�5     $��14(�6-   5     �� 5     $��2q�5     $��1��5      . $��14(�5  '   ��5     �� *PNFL+5  7   5     5     � 5     $��2��6   $��0��5      5     �� 5      $��20 �6    �� 5!     $	��200 �6    �� 5"     $
��100* �6    .+�5$     �� $��Manzili��6   $��0��5%     $
��Left=�5&     $��Center=�5'     $��2,0,0,0q�5(     $��14(�6-   5)     �� 5*     $��2q�5+     $��1��5,      �� TextWrapping$
 Wrap=�5-     	 �� AcceptsReturn	 .  . $��14(�50  #   ��5/     �� *
FullAdress+50  3   52     53     �� 54     $��5,5,5,3q�55     $"��Haydovchilik Kategoriyalari:��56     $
��Bold"�69   $��14(�6K   6[   �� 57     
���   �$��2q�6)   $��Bottom=�58     $
��Left=�59     $��1��5:     $��5,5,5,5q�5;     $��2��5<     $��0��5=     ._�5>     �  5?     �� 5@     $	��30* �6$   �� 5A     $��30 �6$   �� 5B     $��30 �6$   �� 5C     $��30 �6(   �� 5D     $��30 �6(   �� 5E     $��30 �6(   �� 5F     $��30 �6(   �� 5G     $��30 �6(    5I     �� 5J     $��0 �6'   �� 5K     $	��180 �6'   �� 5L     $	��110 �6'   �� 5M     $	��110 �6'   �� 5N     $��30 �6'   .+�5P     �� $��1��6   $��0��5Q     $��A��5R     $��Center=�5S  $   $��5q�5T     $��14(�6   6/   �� 5U     $��0��6   $��2��5V     $��Berilgan sanasi��5W     $��Center=�5X     $��14(�69   $
��Bold"�6G   5Y     �� 5[     $��0��6   $��3��5\     $��Tugash sanasi��5]     $��Center=�5^     $��14(�69   $
��Bold"�6G   5_     �� 5`     $��1��6   $��4��5a     $��Center=�5b     $��2q�5c  #   
 J� IsHitTestVisible
 .   d� 	IsChecked 5e  #   �� *
CheckedA+6D   �� 5f     $��2��6   $��4��5g     $��2q�5h     
 .  $��Center=�5k      5j     �� *
CheckedB+5k  :   �� 5l     $��3��6   $��4��5m     $��2q�6$   
 .  $��Center=�5o      5p  #   �� *
CheckedC+6D   �� 5q     $��4��6   $��Center=�5r     
 .  $��4��5t     $��2q�6$    5u  #   �� *
CheckedD+6D   �� 5v     $��5��6   $��Center=�5w     
 .  $��4��5y     $��2q�6$    5z  #   �� *	CheckedBE+6E   �� 5|     $��6��6   $��Center=�5}     
 .  $��4��5     $��2q�6$    5�  #   �� *	CheckedCE+6E   �� 5�     $��7��6   $��Center=�5�     
 .  $��4��5�     $��2q�6$    5�  #   �� *	CheckedDE+6E   �� 5�     $��1��6   $��3��5�      . $��14(�5�  >   ��6%   �� *	ExpireA+.��6L   5�     �� 5�     $��2��6   $��3��5�      . $��14(�5�  >   ��6%   �� *	ExpireB+.��6L   5�     �� 5�     $��3��6   $��3��5�      . $��14(�5�  >   ��6%   �� *	ExpireC+.��6L   5�     �� 5�     $��4��6   $��3��5�      . $��14(�5�  >   ��6%   �� *	ExpireD+.��6L   5�     �� 5�     $��5��6   $��3��5�      . $��14(�5�  ?   ��6%   �� *
ExpireBE+.��6M   5�     �� 5�     $��6��6   $��3��5�      . $��14(�5�  ?   ��6%   �� *
ExpireCE+.��6M   5�     �� 5�     $��7��6   $��3��5�      . $��14(�5�  ?   ��6%   �� *
ExpireDE+.��6M   5�     �� 5�     $��1��6   $��2��5�      . $��14(�5�  ,   ��5�     �� *IssueA+.��5�  :   5�     �� 5�     $��2��6   $��2��5�      . $��14(�5�  ,   ��5�  %   �� *IssueB+.��5�  :   5�     �� 5�     $��3��6   $��2��5�      . $��14(�5�  =   ��6%   �� *IssueC+.��6K   5�     �� 5�     $��4��6   $��2��5�      . $��14(�5�  =   ��6%   �� *IssueD+.��6K   5�     �� 5�     $��5��6   $��2��5�      . $��14(�5�  >   ��6%   �� *	IssueBE+.��6L   5�     �� 5�     $��6��6   $��2��5�      . $��14(�5�  >   ��6%   �� *	IssueCE+.��6L   5�     �� 5�     $��7��6   $��2��5�      . $��14(�5�  >   ��6%   �� *	IssueDE+.��6L   5�     �� 5�     $��1��6   $��1��5�     $��/Resources/A Category.png��5�     $��Center=�5�     $��5q�5�     $
��Fill=�6   61   �� 5�     $��2��6   $��0��5�     $��B��5�     $��Center=�5�     $��5q�5�     $��14(�6   6/   �� 5�     $��2��6   $��1��5�     $��/Resources/B Category.png��5�     $��Center=�5�     $��5q�5�     $
��Fill=�6   61   �� 5�     $��3��6   $��0��5�     $��C��5�     $��Center=�5�     $��5q�5�     $��14(�6   6/   �� 5�     $��3��6   $��1��5�     $��/Resources/C Category.png��5�     $��Center=�5�     $��	15, 0,0,0q�5�     $
��Fill=�6'   69   �� 5�     $��4��6   $��0��5�     $��D��5�     $��Center=�5�  $   $��5q�5�     $��14(�6   6/   �� 5�     $��4��6   $��1��5�     $��/Resources/Untitled.png��5�     $��Center=�5�     $��15,0,0,0q�5�     $
��Fill=�6&   68   �� 5�     $��5��6   $��0��5�     $��BE��5�     $��Center=�5�  $   $��5q�5�     $��14(�6   6/   �� 5�     $��5��6   $��1��5�     $��/Resources/BE.png��5�     $��Center=�5      $��5q�5     $
��Fill=�6   61   �� 5     $��6��6   $��1��5     $��/Resources/CE.png��5     $��Center=�5     $��5q�5     $
��Fill=�6   61   �� 5	     $��7��6   $��1��5
     $��/Resources/DE.png��5     $��Center=�5     $��5q�5     $
��Fill=�6   61   5     5     5     �� 5     
���   �$��2q�6)   $��Bottom=�5     $��2��5     $��3��6   ._�5     �� $
��Left=�6   $��Bottom=�5      �� Orientation$ 
Horizontal=�5     .+�5     � -   5        $��prStatus��5     $��20��5     $	��100��6*   5     �� 5     -   5        $��	txbStatus��5     $��10,0,0,0q�5     ��5     �� *
StatusText+63   �� 5     $��20��6   $��X��5      $y�Hidden=�5!     ��5"     �� *StopProcess+{�5#     �� *StopReadingProc+6A   5$     5%     5&     5'     5(     