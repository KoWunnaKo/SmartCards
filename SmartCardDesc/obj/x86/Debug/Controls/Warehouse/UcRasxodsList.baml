   M S B A M L   `   `   `  ����H  DSmartCardDesc, Version=1.5.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:SmartCardDesc.Controls.Warehouse SmartCardDesc.Controls.Warehouse  5      W0clr-namespace:SmartCardDesc.ViewModel.Converters"SmartCardDesc.ViewModel.Converters  5      ]3clr-namespace:SmartCardDesc.EntityModel.EntityModel%SmartCardDesc.EntityModel.EntityModel  5      4    .SmartCardDesc.Controls.Warehouse.UcRasxodsList   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:SmartCardDesc.Controls.Warehouse     5      >l0clr-namespace:SmartCardDesc.ViewModel.Converters     5      KEntityModel3clr-namespace:SmartCardDesc.EntityModel.EntityModel     5      -   5        
�������$��477.66��5
   >     � 	Resources  5      8   2SmartCardDesc.ViewModel.Converters.UserIdConverter   UserConverter  wAREHOUSE_DTLViewSource��%:   &	        &	        5   
   6   �� 5   
   6   5      .��5      �  � DataContext# [ 6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��1* �6   �� 5      $��20 �6   .+�5   
   � -   5         �Name$ grid1��5      $	��Top=�6   $
��Left=�66   # [ 6Q    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5       �� 5!      $
��Auto �6#   �� 5"      $
��Auto �6#   .+�5$      �� $��Center=�6   $��0��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $��Создал:��6r   �� 5%      -   5         ��Name$ cREATE_USERTextBox��5%      $	��120��62   $��Center=�6>   $��0��6�   $��3q�6�   $��23��6�   $
��Left=�6  $��1��6&  ��6Y   �� *CREATE_USER+ �� 	Converter# [   �� Mode$ TwoWay=� �� NotifyOnValidationError . 	 �� ValidatesOnExceptions	 . 6&  �� 5&      $��Center=�6   $��1��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $��Тип операции:��6r   �� 5'      -   5        $ oP_TYPETextBox��5'      $	��120��6.   $��Center=�6:   $��1��6�   $��3q�6�   $��23��6�   $
��Left=�6�   $��1��6�   ��6U   �� *	OP_TYPE+$ TwoWay=� . 	 . 6�   �� 5(      $��Center=�6   $��2��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $(��"Родительский ключ:��6r   �� 5)      -   5        $ pARENT_IDTextBox��5)      $	��120��60   $��Center=�6<   $��2��6�   $��3q�6�   $��23��6�   $
��Left=�6�   $��1��6�   ��6W   �� *	PARENT_ID+$ TwoWay=� . 	 . 6�   �� 5*      $��Center=�6   $��3��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $��Количество:��6r   �� 5+      -   5        $ quantityTextBox��5+      $	��120��6/   $��Center=�6;   $��3��6�   $��3q�6�   $��23��6�   $
��Left=�6�   $��1��6�   ��6V   �� *
quantity+$ TwoWay=� . 	 . 6�   �� 5,      $��Center=�6   $��4��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $��Номер п/п:��6r   �� 5-      -   5        $ rEC_IDTextBox��5-      $	��120��6-   $��Center=�69   $��4��6�   $��3q�6�   $��23��6�   $
��Left=�6�   $��1��6�   ��6T   �� *REC_ID+$ TwoWay=� . 	 . 6�   �� 5.      $��Center=�6   $��5��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $��Статус:��6r   �� 5/      -   5        $ sTATETextBox��5/      $	��120��6,   $��Center=�68   $��5��6�   $��3q�6�   $��23��6�   $
��Left=�6�   $��1��6�   ��6S   �� *STATE+$ TwoWay=� . 	 . 6�   �� 50      $��Center=�6   $��6��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $��Сумма:��6r   �� 51      -	   5        $ tR_AMOUNTTextBox��51      $	��120��60   $��Center=�6<   $��6��6�   $��3q�6�   $��23��6�   $
��Left=�6�   $��1��6�   ��6W   �� *	TR_AMOUNT+$ TwoWay=� . 	 . 6�   �� 52      $��Center=�6   $��7��6/   $��3q�6<   $
��Left=�6G   $��0��6b   $ ��Дата операции:��6r   (  "System.Windows.Controls.DatePicker  53      -
   5        
  Name$
 tR_DATEDatePicker��53      $��Center=�64   $��7��6�   $��3q�6�   $
��Left=�6�   $��1��6�      SelectedDate 6O   �� *	TR_DATE+$ TwoWay=� . 	 . 6�   54      &   System.Windows.Controls.DataGrid  55   
   -   5          Name$ wAREHOUSE_DTLDataGrid��56         SelectionMode$ Single=�55         
IsReadOnly .    AutoGenerateColumns .     EnableRowVirtualization . $��1��56         RowDetailsVisibilityMode$ VisibleWhenSelected=�6�   ��6g   ��    Columns 57      0  *System.Windows.Controls.DataGridTextColumn  58      -   5        cREATE_USERColumn��,  &System.Windows.Controls.DataGridColumn   Header$ Создал��58   �      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *CREATE_USER+# [  6�     59      -   5        oP_TYPEColumn��$ Тип операции��59   X   $
 Auto 6n    6<   �� *	OP_TYPE+6n     5:      -   5        pARENT_IDColumn��$' !Родительский ключ��5:   \   $
 Auto 6w    6>   �� *	PARENT_ID+6w     5;      -   5        quantityColumn��$ Количество��5;   Z   $
 Auto 6n    6=   �� *
quantity+6n     5<      -   5        rEC_IDColumn��$ Номер п/п��5<   V   $
 Auto 6i    6;   �� *REC_ID+6i     5=      -   5        sTATEColumn��$ Статус��5=   T   $
 Auto 6d    6:   �� *STATE+6d     5>      -   5        tR_AMOUNTColumn��$ 
Сумма��5>   \   $
 Auto 6k    6>   �� *	TR_AMOUNT+6k   4  .System.Windows.Controls.DataGridTemplateColumn  5?      -   5        tR_DATEColumn��$ Дата операции��5?   @   $
 Auto 6W      CellTemplate 5@      �� 5A      .R�5B         6)   �� *	TR_DATE+$ TwoWay=� . 	 . 5C      5E      5G      �� 5H   
   
���   �$��2q�5I      $��Bottom=�5J      $��2��5K      $��2��5L      ._�5M      �� $
��Left=�6   $��Bottom=�5N       �� Orientation$ 
Horizontal=�5O      .+�5P      � -   5        $��prStatus��5Q      $��20��5P      $	��100��6*    � IsIndeterminate 5R      �� *IsIntermadiate+�� 5T      -   5        $��	txbStatus��5U      $��10,0,0,0q�5V      ��5W      �� *
StatusText+63   5X      5Y      5Z      5[      