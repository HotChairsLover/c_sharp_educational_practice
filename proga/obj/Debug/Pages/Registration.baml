   M S B A M L   `   `   `  ����@  <proga, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:proga.Pagesproga.Pages  5          proga.Pages.Registration   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:proga.Pages     5        N� Title$  Registration��5	      .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF2168AE��6    � Offset$ 0Q�61   � 5      $ 	#FF16B5BD��6   $ 1Q�61   5       � 	Resources 5   
    �� MergedDictionaries �� Source�� 5       5      �� 5      $ /Resource/ButtonRes.xaml?�6)   %    5       � RowDefinitions 5   
   �� 5      $	��33* �6   �� 5      $	��92* �6   .+�5   
   � $��0,0,0,14q�6   $��0��6!   	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      
   
BlurRadius$
 25Q�6#      	Direction$	 268Q�63      ShadowDepth$ 9Q�6C   .+�5!      �� $��96��6   $	��Top=�6!   ��5"      �� 5#       �� Color$ 	#FF090909��6&    �� Opacity$	 0.8Q�68   5%      �� 5&      $��logo.png��6   $��	21,15,0,0q�6&   $
��Left=�69   $��59��6T   $��58��6_   $	��Top=�6k   �� 5'      $3��-Хочу Зарегистрироваться��6   $��	85,20,0,0q�66   
�������$��26(�6\   $��Yu Gothic UI Light)�6j   $
��Left=�6�   $	��328��6�   $��44��6�   $	��Top=�6�   �� 5(      -   5         ��Name$ RegIn��5(         MainBtnAnim#��[  60   $*��$Зарегистрироваться��6U   $��	0,50,10,0q�6r   $��Right=�6�   $	��179��6�   $��32��6�   $	��Top=�6�   �� 5)      -   5        $ AthIn��5)      #��[  60   $"��Авторизоваться��6U   $��	0,12,10,0q�6n   $��Right=�6�   $	��179��6�   $��32��6�   $	��Top=�6�   5*      � 5+   
   $��1��6   $	��780��6   $	��342��6(   $��Center=�65   $��Center=�6P   $��
45,8,45,18q�6m   	 5,        5-      $
 26Q�6#   $	 268Q�63   $ 14Q�6C   .+�5/      ��  �� CornerRadius$ 20��6   ��50      �� 51      $ 	#FF090909��6&   $	 0.8Q�68   ._�53      � $��	0,10,0,20q�6    54      �� 55      $	��66* �6(   �� 56      $	��63* �6(   �� 57      $	��64* �6(   �� 58      $	��64* �6(   �� 59      $	��65* �6(    � ColumnDefinitions 5;      �� 5<      $��* �6+   �� 5=      $��* �6+   .+�5?      �� $2��,Адрес электронной почты��6   $��0��6>   $��19,0,0,0q�6N   
�������$��16(�6s   $��Yu Gothic UI Light)�6�   $
��Left=�6�   $	��213��6�   $��32��6�   $	��Top=�6�   $��0��6�   �� 5@      -   5         ��Name$ tb_mail��5@      $��0��6/   $��30��6?    �� TextWrapping$
 Wrap=�6K   $�� ��6_   $��Center=�6g   $	��Top=�6�   $��
19,32,24,0q�6�   
�������$��2q�6�   $��1q�6�   ���	#FFFFC970���	#FF0A1117$��16(�6  $��0��6%  �� 5A      $��Фамилия��6   $��0��6.   $��19,0,0,0q�6>   
�������$��16(�6c   $��Yu Gothic UI Light)�6q   $
��Left=�6�   $	��213��6�   $��32��6�   $	��Top=�6�   $��1��6�   �� 5B      -   5        $ 
first_name��5B      $��0��62   $��30��6B   $
 Wrap=�6N   $�� ��6b   $��Center=�6j   $	��Top=�6�   $��
19,31,24,0q�6�   
�������$��2q�6�   $��1q�6�   ���	#FFFFC970���	#FF0A1117$��16(�6  $��1��6(  �� 5C      $��Имя��6   $��0��6*   $��19,0,0,0q�6:   
�������$��16(�6_   $��Yu Gothic UI Light)�6m   $
��Left=�6�   $	��213��6�   $��32��6�   $	��Top=�6�   $��2��6�   �� 5D      -   5        $
 name��5D      $��0��6,   $��30��6<   $
 Wrap=�6H   $�� ��6\   $��Center=�6d   $	��Top=�6�   $��
19,31,24,0q�6�   
�������$��2q�6�   $��1q�6�   ���	#FFFFC970���	#FF0A1117$��16(�6  $��2��6"  �� 5E      $��Отчество��6   $��0��6/   $��19,0,0,0q�6?   
�������$��16(�6d   $��Yu Gothic UI Light)�6r   $
��Left=�6�   $	��213��6�   $��32��6�   $	��Top=�6�   $��3��6�   �� 5F      -   5        $ 	last_name��5F      $��0��61   $��30��6A   $
 Wrap=�6M   $�� ��6a   $��Center=�6i   $	��Top=�6�   $��
19,31,24,0q�6�   
�������$��2q�6�   $��1q�6�   ���	#FFFFC970���	#FF0A1117$��16(�6  $��3��6'  �� 5G      $��Телефон��6   $��0��6.   $��19,0,0,0q�6>   
�������$��16(�6c   $��Yu Gothic UI Light)�6q   $
��Left=�6�   $	��213��6�   $��32��6�   $	��Top=�6�   $��4��6�   �� 5H      -   5        $ tb_phone��5H      $��0��60   $��30��6@   $
 Wrap=�6L   $�� ��6`   $��Center=�6h   $	��Top=�6�   $��
19,32,24,0q�6�   
�������$��2q�6�   $��1q�6�   ���	#FFFFC970���	#FF0A1117$��16(�6  $��4��6&  �� 5I      $!��Название улицы��6   $��1��65   $��12,0,0,0q�6E   
�������$��16(�6j   $��Yu Gothic UI Light)�6x   $
��Left=�6�   $	��213��6�   $��32��6�   $	��Top=�6�   �� 5J      -   5        $ 	tb_street��5J      $��30��61   $
 Wrap=�6=   $�� ��6Q   $��Center=�6Y   $	��Top=�6{   $��
12,32,20,0q�6�   
�������$��2q�6�   $��1q�6�   ���	#FFFFC970���	#FF0A1117$��16(�6	  $��1��6  �� 5K      $��Номер дома��6   $��12,0,0,0q�61   
�������$��16(�6V   $��Yu Gothic UI Light)�6d   $
��Left=�6�   $	��213��6�   $��31��6�   $	��Top=�6�   $��1��6�   $��1��6�   �� 5L      -	   5        $ tb_house��5L      $��30��60   $
 Wrap=�6<   $�� ��6P   $��Center=�6X   $	��Top=�6z   $��
12,31,20,0q�6�   
�������$��2q�6�   $��1q�6�   ���	#FFFFC970���	#FF0A1117$��16(�6  $��1��6  $��1��6&  �� 5M      $!��Номер квартиры��6   $��12,0,0,0q�65   
�������$��16(�6Z   $��Yu Gothic UI Light)�6h   $
��Left=�6�   $	��213��6�   $��32��6�   $	��Top=�6�   $��1��6�   $��2��6�   �� 5N      -
   5        $ tb_room��5N      $��30��6/   $
 Wrap=�6;   $�� ��6O   $��Center=�6W   $	��Top=�6y   $��
12,31,20,0q�6�   
�������$��2q�6�   $��1q�6�   ���	#FFFFC970���	#FF0A1117$��16(�6  $��1��6  $��2��6%  �� 5O      $!��Номер подъезда��6   $��12,0,0,0q�65   
�������$��16(�6Z   $��Yu Gothic UI Light)�6h   $
��Left=�6�   $	��213��6�   $��32��6�   $	��Top=�6�   $��1��6�   $��3��6�   �� 5P      -   5        $ tb_porch��5P      $��30��60   $
 Wrap=�6<   $�� ��6P   $��Center=�6X   $	��Top=�6z   $��
12,31,20,0q�6�   
�������$��2q�6�   $��1q�6�   ���	#FFFFC970���	#FF0A1117$��16(�6  $��1��6  $��3��6&  �� 5Q      $��Номер этажа��6   $��12,0,0,0q�62   
�������$��16(�6W   $��Yu Gothic UI Light)�6e   $
��Left=�6�   $	��213��6�   $��32��6�   $	��Top=�6�   $��1��6�   $��4��6�   �� 5R      -   5        $ tb_floor��5R      $��30��60   $
 Wrap=�6<   $�� ��6P   $��Center=�6X   $	��Top=�6z   $��
12,32,20,0q�6�   
�������$��2q�6�   $��1q�6�   ���	#FFFFC970���	#FF0A1117$��16(�6  $��1��6  $��4��6&  5S      5T      5U      5V      5W      