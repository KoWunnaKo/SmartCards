   M S B A M L   `   `   `  ���� E  AGID_Client, Version=2.9.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SmartCardDesc.SeciritySmartCardDesc.Secirity  (    "SmartCardDesc.Secirity.LoginWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6local$clr-namespace:SmartCardDesc.Secirity       � Title$  Avtorizasiya��$��208.834��$	��320�� � WindowStyle$ 
ToolWindow=� � WindowStartupLocation$ CenterScreen=� � Icon$0 */GID_Client;component/ResourcesPic/car.png��.���  � RowDefinitions �� $��50 ��� $��50 ��� $	��39* ��� $��20 � � ColumnDefinitions �� $
��121* ��� $
��171* �.+��� $��0��$��0��$��Login��$��20,0,0,0q�$
��Left=�$��Center=��� -   $��txbLogin��$��Stretch=� �� TextWrapping$
 Wrap=�$��15q�$��20��$��Center=�$��1������ *Login+ �� Mode$ TwoWay=� �� NotifyOnSourceUpdated . 	 �� UpdateSourceTrigger$	 PropertyChanged=��� $��0��$��1��$��Parol��$
��Left=�$��20,0,0,0q�$��Center=�G� -   $��txbPassword��$��Stretch=�$��Center=�$��20��$��15q�$��1��$��1���� $��2��$��2��
 �� Orientation$
 
Horizontal=�$��Right=�.+��� $��Center=�$
��Wrap=�$��Center=�$��22��$	��100��$��2��$��4��$��2��
���  ��$y�Hidden=����� *
StatusText+�� $��Kirish��$��Center=�$��25��$��Center=�$��84��$��5,0,5,0q�$��2�� �� 	IsDefault . ���� *LoginCommand+�� -   $��Bekor qilish��$��Center=�$��25��$��Center=�$��84��$��5,0,5,0q�$��2��$��1�� �� IsCancel . �� 
���   �$��2q�$��Bottom=�$��3��$��2��._��� $
��Left=�$��Bottom=�$
 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100�� � IsIndeterminate �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q�
���  ������ *
StatusText+