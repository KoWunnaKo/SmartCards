   M S B A M L   `   `   `  ����H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:SmartCardDesc.Controls.Warehouse SmartCardDesc.Controls.Warehouse  5      1    +SmartCardDesc.Controls.Warehouse.UcProxodOp   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:SmartCardDesc.Controls.Warehouse     5      
�������.��5
      �   � RowDefinitions  5   
   �� 5      $	��39* �6   �� 5      $	��44* �6   �� 5      $	��47* �6   �� 5      $	��45* �6   �� 5      $	��23* �6   �� 5      $	��46* �6   �� 5      $
��176* �6   �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      $	��79* �6   �� 5      $
��135* �6   .+�5   
   �� $��2��6   $��6��5      
����  �$��2q�5      6'   �� 5   
   $��0��5      $��0��5       $��	Дата:��5!      $��	5,12,0,11q�5"      $
��Left=�5#      $��Center=�5$      6*   (  "System.Windows.Controls.DatePicker  5%   
   $��0��5&      $��1��5'      $��5q�5(      $��Center=�5)         Text 5*      �� *	Inputdate+6*   �� 5+   
   $��1��5,      $��0��5-      $��Сумма:��5.      $��	5,14,0,14q�5/      $
��Left=�50      $��Center=�51      6*   �� 52   
   $��1��53      $��1��54      $��	5,10,5,10q�55      ��56      �� *Amount+57      �� 58   
   $��2��59      $��0��5:      $��Количество:��5;      $��	5,16,0,15q�5<      $
��Left=�5=      $��Center=�5>      6*   �� 5?   
   $��2��5@      $��1��5A      $��	5,10,5,10q�5B      ��5C      �� *
Quantity+5D      �� 5E   
   $��3��5F      $��0��5G      $��Наименование:��5H      $��	5,15,0,14q�5I      $
��Left=�5J      $��Center=�5K      6*   �� 5L   
   $��3��5M      $��1��5N      $��	5,10,5,10q�5O      ��5P      �� *Name+5Q      �� 5R   
   $��5��5S      $��1��5T      $��5q�5U      $��Оприходовать��5V      ��5W      �� *	InputItem+6-   �� 5X   
   
���   �$��2q�5Y      $��Bottom=�5Z      $��7��5[      $��2��5\      ._�5]      �� $
��Left=�6   $��Bottom=�5^       �� Orientation$ 
Horizontal=�5_      .+�5`      � -   5        $��prStatus��5a      $��20��5`      $	��100��6*    � IsIndeterminate 5b      �� *IsIntermadiate+�� 5d      -   5        $��	txbStatus��5e      $��10,0,0,0q�5f      ��5g      �� *
StatusText+63   5h      5i      5j      5k      