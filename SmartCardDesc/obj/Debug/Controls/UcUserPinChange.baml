   M S B A M L   `   `   `  ����H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SmartCardDesc.ControlsSmartCardDesc.Controls  5      ,    &SmartCardDesc.Controls.UcUserPinChange   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:SmartCardDesc.Controls     5      
�������.��5
      �   � ColumnDefinitions  5   
   �� 5      $	��150 �6   �� 5      $	��89* �6    � RowDefinitions 5   
   �� 5      $	��30* �6   �� 5      $	��30* �6   �� 5      $	��30* �6   �� 5      $	��30* �6   �� 5      $	��30* �6   �� 5      $
��136* �6   .+�5   
   �� $��Старый PIN��6   $��5,0,0,0q�5      $��Center=�5      61   �� 5   
   $��1��6   $��0��5      $��2q�5      ��5      �� *OldPin+5      �� 5    
   $��Новый PIN��6   $��5,0,0,0q�5!      $��0��5"      $��1��5#      $��Center=�5$      61   �� 5%   
   $��1��6   $��1��5&      $��2q�5'      ��5(      �� *NewPin+6,   �� 5*   
   $ ��Подтвердить PIN��6   $��5,0,0,0q�5+      $��0��5,      $��2��5-      $��Center=�5.      61   �� 5/   
   $��1��6   $��2��50      $��2q�51      ��52      �� *ConfirmNewPin+63   �� 54   
   $��1��6   $��3��55      $��3q�56      $��Изменить PIN��57      ��58      �� *	ChangePin+61   �� 59   
   -   5        $��txbDefaultPin��5;      $��0��59      $��4��5:      5;   +   �� 5<   
   $��1��6   $��4��5=      $��3q�5>      $��Сбросить PIN��5?      ��5@      �� *
RestorePin+62   �� 5A   
   $��6��6   $��2��5B      
���   �$��2q�5D      $��Bottom=�5E      ._�5F      �� $
��Left=�6   $��Bottom=�5G       �� Orientation$ 
Horizontal=�5H      .+�5I      � -   5        $��prStatus��5J      $��20��5I      $	��100��6*    � IsIndeterminate 5K      �� *IsIntermadiate+�� 5M      -   5        $��	txbStatus��5N      $��10,0,0,0q�5O      ��5P      �� *
StatusText+63   5Q      5R      5T      5U      