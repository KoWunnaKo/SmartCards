   M S B A M L   `   `   `  ����H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:SmartCardDesc.Controls.Warehouse SmartCardDesc.Controls.Warehouse  5      ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  5      9    3SmartCardDesc.Controls.Warehouse.UcCurrentWarehouse   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:SmartCardDesc.Controls.Warehouse     5      KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     5      -   5        
�������  � 	Resources  5         wAREHOUSEViewSource��%   &	        �� 5   
   6   5      .��5      �  � DataContext# [  6    � RowDefinitions 5   
   �� 5      $	��175 �6   �� 5      $
��200* �6   �� 5      $��20 �6   .+�5   
   � -   5         �Name$ grid1��5      $	��Top=�5      $��2��5      $
��Left=�6   # [  5      $��0��5       5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       $
��Auto �6    �� 5!      $
��Auto �6     � ColumnDefinitions 5#      �� 5$      $
��Auto �6#   �� 5%      $
��Auto �6#   .+�5'      �� $��Center=�6   $��0��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $��Дата ввода:��6r   (  "System.Windows.Controls.DatePicker  5(      -   5          Name$ iNPUT_DTDatePicker��5(      $��Center=�65   $��0��6�   $��3q�6�   $
��Left=�6�   $��1��6�      SelectedDate 6P   �� *
INPUT_DT+ �� Mode$ TwoWay=� �� NotifyOnValidationError . 	 �� ValidatesOnExceptions	 . 6�   �� 5)      $��Center=�6   $��1��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $��Наименование:��6r   �� 5*      -   5        
 ��Name$
 iTEN_NAMETextBox��5*      $	��120��60   $��Center=�6<   $��1��6�   $��3q�6�   $��23��6�   $
��Left=�6�   $��1��6�   ��6W   �� *	ITEN_NAME+$ TwoWay=� . 	 . 6�   �� 5+      $��Center=�6   $��2��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $��Количество:��6r   �� 5,      -   5        $
 qUANTITYTextBox��5,      $	��120��6/   $��Center=�6;   $��2��6�   $��3q�6�   $��23��6�   $
��Left=�6�   $��1��6�   ��6V   �� *
QUANTITY+$ TwoWay=� . 	 . 6�   �� 5-      $��Center=�6   $��3��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $��Номер п/п:��6r   �� 5.      -   5        $
 rEC_IDTextBox��5.      $	��120��6-   $��Center=�69   $��3��6�   $��3q�6�   $��23��6�   $
��Left=�6�   $��1��6�   ��6T   �� *REC_ID+$ TwoWay=� . 	 . 6�   �� 5/      $��Center=�6   $��4��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $��Общее сумма:��6r   �� 50      -   5        $
 tOTAL_AMOUNTTextBox��50      $	��120��63   $��Center=�6?   $��4��6�   $��3q�6�   $��23��6�   $
��Left=�6�   $��1��6�   ��6Z   �� *TOTAL_AMOUNT+$ TwoWay=� . 	 . 6�   51      &   System.Windows.Controls.DataGrid  52   
   -   5          Name$ wAREHOUSEDataGrid��53         AutoGenerateColumns .     EnableRowVirtualization . $��1��57         RowDetailsVisibilityMode$ VisibleWhenSelected=�58      ��56      ��    Columns 59      4  .System.Windows.Controls.DataGridTemplateColumn  5:      -	   5        iNPUT_DTColumn��,  &System.Windows.Controls.DataGridColumn   Header$ Дата ввода��5:   A      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6U      CellTemplate 5;      �� 5<      .R�5=         6)   �� *
INPUT_DT+$ TwoWay=� . 	 . 5>      5@      0  *System.Windows.Controls.DataGridTextColumn  5A      -
   5        iTEN_NAMEColumn��$ Наименование��5A   \   $
 Auto 6r   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *	ITEN_NAME+6r     5B      -   5        qUANTITYColumn��$ Количество��5B   Z   $
 Auto 6n    6=   �� *
QUANTITY+6n     5C      -   5        rEC_IDColumn��$ Номер п/п��5C   V   $
 Auto 6i    6;   �� *REC_ID+6i     5D      -   5        tOTAL_AMOUNTColumn��$ Общая сумма��5D   b   $
 Auto 6w    6A   �� *TOTAL_AMOUNT+6w   5F      �� 5G   
   
���   �$��2q�5H      $��Bottom=�5I      $��2��5J      $��2��5K      ._�5L      �� $
��Left=�6   $��Bottom=�5M       �� Orientation$ 
Horizontal=�5N      .+�5O      � -   5        $��prStatus��5P      $��20��5O      $	��100��6*    � IsIndeterminate 5Q      �� *IsIntermadiate+�� 5S      -   5        $��	txbStatus��5T      $��10,0,0,0q�5U      ��5V      �� *
StatusText+63   5W      5X      5Y      5Z      