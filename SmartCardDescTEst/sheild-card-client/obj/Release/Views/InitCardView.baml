   M S B A M L   `   `   `  ���� E  AGID_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GID_Client.ViewsGID_Client.Views  #    GID_Client.Views.InitCardView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:GID_Client.Views     
�������.���   � RowDefinitions  �� $��1* ��� $��20 �.+��  � ColumnDefinitions �� $
��Auto ��� $	��300 ��� $��* �  �� $��35 ��� $��35 ��� $��35 ��� $��80 ��� $��40 ��� $	��100 ��� $	��100 ��� $��40 ��� $	��100 ��� $��40 ��� $
��243* �.+��� $��0��$��0��$��
Ридер��$��Right=�$��5q��� $��1��$��0��$��Статус Карты��$��Right=�$��5q��� $��2��$��0��$��Уникальный Ид��$��Right=�$��5q��� $��0��$��1��$��5q����� *MyReaderName+�� $��1��$��1��$��5q����� *MycardStatus+���� *BackgroundTb+���� *ForegroundTb+�� $��2��$��1��$��5q����� *MyUIDofCard+� $��3��$��1��  �� $��1* ��� $��1* �.+��� $��0��$��0��$8��2Открыть Водительские Права��$��5q����� *
OpenCardDR+�� $��1��$��0��$+��%Открыть Тех. Паспорт��$��5q����� *
OpenCardVR+� $��2��$��10��.+��� ���� *
LogsList+�� $��4��$��1��$,��&Сохранить на сервере��$��5q����� *SaveCardInfo+�� $��5��$��0��$��Right=�$��Закрытый ключ��$��5q��� $��6��$��0��$��Открытый ключ��$��Right=�$��5q��� $��5��$��1��$��5q� �� TextWrapping$
 Wrap=� �� AcceptsReturn .  �� 
IsReadOnly . ���� *
PrivateKey+�� $��6��$��1��$��5q�$
 Wrap=� .  . ���� *	PublicKey+�� $��7��$��1��$+��%Получить сертификат��$��5q�{��� *CertificateButton+���� *GetCertificate+�� $��8��$��0��$��Сертификат��$��Right=�$��5q��� $��8��$��1��$��5q�$
 Wrap=� .  . ���� *Certificate+�� $��9��$��1��$(��"Сохранить на Карту��$��5q����� *
Apply2Card+�� 
���   �$��2q�$��Bottom=�$��2��._��� $
��Left=�$��Bottom=� �� Orientation$ 
Horizontal=�.+�� -   $��prStatus��$��20��$	��100�� � IsIndeterminate �� *IsIntermadiate+�� -   $��	txbStatus��$��10,0,0,0q����� *
StatusText+