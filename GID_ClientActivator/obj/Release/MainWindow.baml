   M S B A M L   `   `   `  ���� E  AGID_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GID_Client
GID_Client      GID_Client.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:GID_Client     -     � Title$"  АРМ Карт Мастер��$	��710��$
��1015�� � WindowStartupLocation$ CenterScreen=�$	��710��$
��1015��$	��710��$
��1015��.���  � RowDefinitions �� $
��Auto ��� �� $��20 �.+�+  %System.Windows.Controls.Ribbon.Ribbon  -     Name$ 	RibbonWin�� �� SelectedIndex$ 0��   HelpPaneContent 1  +System.Windows.Controls.Ribbon.RibbonButton     SmallImageSource$ ResourcesPic/Help.bmp��   ApplicationMenu :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5  /System.Windows.Controls.Ribbon.RibbonMenuButton   KeyTip$ F��.@�>  8System.Windows.Controls.Ribbon.RibbonApplicationMenuItem  $��Настройки��  $��
Выход��.@�.  (System.Windows.Controls.Ribbon.RibbonTab  -   $��Verification��$2��,Верификация и Активация��	   KeyTip$	 I��.@�0  *System.Windows.Controls.Ribbon.RibbonGroup  -   
  Name$
 ExchangeLists��$	��0,1q�$?��9Данные водителя и Тех. Паспорта��.@���  �� Orientation$ 
Horizontal=�.+�6  0System.Windows.Controls.Ribbon.RibbonRadioButton  -   $��btnUserInfolist��   Label$) #Водительские Права��   LargeImageSource$! ResourcesPic/driver1600.png��  -   $��btnCardInfolist��$+ %Технический Паспорт��$ ResourcesPic/car.png��  -   $
��Tsex��$�� Цех��$	 H��.@�  -   $
 ClipboardGroup��$	��0,1q�$;��5Операции производимые в Цеху��.@��� $ 
Horizontal=�.+�  -	   $��btnUserInfolist2��$) #Водительские Права��$! ResourcesPic/driver1600.png��  -
   $��btnCardInfolist2��$+ %Технический Паспорт��$ ResourcesPic/car.png���� $��1��.���� -   $��spPanel��$ Vertical=�]� $��2��$��Stretch=�
�������.+��� $��	10,0,10,0q�$
��Left=����� *
FullName+