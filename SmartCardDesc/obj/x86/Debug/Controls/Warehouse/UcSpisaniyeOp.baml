   M S B A M L   `   `   `  ����H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:SmartCardDesc.Controls.Warehouse SmartCardDesc.Controls.Warehouse  5      ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  5      4    .SmartCardDesc.Controls.Warehouse.UcSpisaniyeOp   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:SmartCardDesc.Controls.Warehouse     5      KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     5      -   5          >�Name$  UcSpisaniye��5      
������� � 	Resources 5         wAREHOUSEViewSource��%   &	        �� 5   
   6   5      .��5      �  � DataContext# [  6    � RowDefinitions 5   
   �� 5      $	��157 �6   �� 5      $	��23* �6   �� 5      $
��165* �6   �� 5      $	��42* �6   �� 5      $	��31* �6   .+�5   
   � $��2��6    5      �� 5      $	��38* �6    �� 5      $	��43* �6    �� 5      $	��30* �6    �� 5      $	��54* �6     � ColumnDefinitions 5      �� 5      $
��149* �6#   �� 5       $
��294* �6#   .+�5"      �� $��2��5#      $��4��5$      
����  �$��2q�5&      6'   �� 5'      $��0��5(      $��0��5)      $��	Дата:��5*      $��	5,12,0,10q�5+      $
��Left=�5,      $��Center=�5-      6*   (  "System.Windows.Controls.DatePicker  5.      -   5        $��dpSpisaniyeDt��53      $��0��5/      $��1��50      $	��5,7q�51      $��Center=�52         Text 54      ��  �� Path$ UcSpisaniye� �� ElementName$ 	Inputdate��6L   �� 55      $��1��56      $��0��57      $��Количество:��58      $��	5,14,0,13q�59      $
��Left=�5:      $��Center=�5;      6*   �� 5<      -   5        $��	txbAmount��5@      $��1��5=      $��1��5>      $��	5,10,5,10q�5?      ��5A      �� $ UcSpisaniye�$ Quantity��5B      �� 5C      -   5        $��btnWithdrawal��5H      $��5��5D      $��1��5E      $��5q�5F      $��Списать��5G      5I   +   5J      &   System.Windows.Controls.DataGrid  5K   
   -   5          Name$ wAREHOUSEDataGrid��5L      	   SelectionMode$	 Single=�5K      
   
IsReadOnly
 .    AutoGenerateColumns .     EnableRowVirtualization .    RowDetailsVisibilityMode$ VisibleWhenSelected=�5P      $��0��5Q      $��5q�5R      ��5O      ��    Columns 5S      4  .System.Windows.Controls.DataGridTemplateColumn  5T      -   5        iNPUT_DTColumn��,  &System.Windows.Controls.DataGridColumn   Header$ Дата ввода��5T   A      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6U      CellTemplate 5U      �� 5V      .R�5W           SelectedDate 6)   �� *
INPUT_DT+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions . 5X      5Z      0  *System.Windows.Controls.DataGridTextColumn  5[      -   5        iTEN_NAMEColumn��$ Наименование��5[   \   $
 Auto 6r   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *	ITEN_NAME+6r     5\      -   5        qUANTITYColumn��$ Количество��5\   Z   $
 Auto 6n    6=   �� *
QUANTITY+6n     5]      -	   5        rEC_IDColumn��$
 Ид��5]   V   $
 Auto 6b    6;   �� *REC_ID+6b     5^      -
   5        tOTAL_AMOUNTColumn��$% Общее количество��5^   b   $
 Auto 6|    6A   �� *TOTAL_AMOUNT+6|   5`      �� 5a   
   
���   �$��2q�5b      $��Bottom=�5c      $��4��5d      $��2��5e      ._�5f      �� $
��Left=�6   $��Bottom=�5g       �� Orientation$ 
Horizontal=�5h      .+�5i      � -   5        $��prStatus��5j      $��20��5i      $	��100��6*    � IsIndeterminate 5k      �� *IsIntermadiate+�� 5m      -   5        $��	txbStatus��5n      $��10,0,0,0q�5o      ��5p      �� *
StatusText+63   5q      5r      5s      5t      