   M S B A M L   `   `   `  ����@  <proga, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:proga.Elementsproga.Elements  5          proga.Elements.Main_tab   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:proga.Elements     5      $	��200��5   7   $��10,10,10,10q�6D   .��5	      �   J� Effect  5
   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 25Q�6      	Direction$	 200Q�6/      ShadowDepth$ 9Q�6?   6Q    � 	Resources 5   
    �� Source�� 5      $ /Resource/ButtonRes.xaml?�6!   %    6E   .+�5   
   ��  �� CornerRadius$ 20,20,20,20��6   ��5      �� 5       �� Color$ 	#FF090909��6"    �� Opacity$	 0.8Q�64   6D   5      �� 5   
   -   5        	 ��Name$		 pic��5   3      	ImageAnim#��[  6   $��../img/dish/img-1.png��6@   $��	10,31,0,0q�6_   $
��Left=�6r   $��93��6�   $��91��6�   $	��Top=�6�   6�   �� 5   
   -   5        
 ��Name$
 title��5      $��#ХочуПиццу��6   $��108,3,217,0q�64   
�������$��19(�6\   $��Yu Gothic UI Semibold)�6j   $��36��6�   $	��Top=�6�   6�   �� 5   
   -   5         ��Name$
 text��5      $��#ХочуПиццу��6"   $��115,39,217,0q�64   
�������$��16(�6]   $��Yu Gothic UI Light)�6k   $��56��6�   $	��Top=�6�   $
��Wrap=�6�   6�   �� 5   
   -   5        $ ingridients��5      $��#ХочуПиццу��6)   $��115,95,217,0q�6;   
�������$��16(�6d   $��Yu Gothic UI Light)�6r   $��56��6�   $	��Top=�6�   $
��Wrap=�6�   6�   �� 5   
   -   5        $
 price��5      $��	Цена:��6   $��
108,0,0,10q�6/   
�������$��16(�6V   $��Yu Gothic UI Light)�6d   $��Center=�6�   $
��Left=�5      $	��200��6+   $��39��67   $��Bottom=�6C   ��5      �� 5      $ 	#FF1A1A1A��6"   $
 0.76Q�64   6E   5      �� 5    
   -   5        $
 weight��5       $��Вес: ��6    $��
338,0,0,10q�60   
�������$��16(�6W   $��Yu Gothic UI Light)�6e   $��Center=�6�   $
��Left=�5!      $	��200��6+   $��39��67   $��Bottom=�6C   ��5"      �� 5#      $ 	#FF1A1A1A��6"   $
 0.76Q�64   6E   5%      � 5&   
   -   5         �Name$ 	group_btn��5&   9   $��0,24,0,0q�6   $��44��6!   $	��212��6-   $	��Top=�6L   $��Right=�6d   .+�5'      �� -   5          SelectBtnAnim#��[ 5'      
 � Tag$ 0��6<   $��23 см.��6V   $��	0,7,144,0q�6g   $��Right=�6z   $��65��6�   $��32��6�   $	��Top=�6�   ���	#FFFFC970
���   �5(   A   �� 5)      -	   5        #��[ 5)      $ 1��6<   $��30 см.��6V   $��0,7,77,0q�6g   $��Right=�6y   $��65��6�   $��32��6�   $	��Top=�6�   ���	#FFFFC970
���   �5*   A   �� 5+      -
   5        #��[ 5+      $ 2��6<   $��40 см.��6V   $��0,7,10,0q�6g   $��Right=�6y   $��65��6�   $��32��6�   $	��Top=�6�   ���	#FFFFC970
���   �5,   A   5-      � 5.   
   $��	0,84,10,0q�6   $��38��6"   $	��Top=�6.   $��Right=�6F   $	��199��6b   .+�5/      �� -   5          MainBtnAnim#��[ 5/   ,   $��-��6Q   $��Right=�6]   $��32��6y   $��29��6�   $	��Top=�6�   $��Center=�50      $��Center=�69   $��	0,3,167,0q�6[   6p   �� 51      -   5        $ col_vo��51      $��0��6(   
�������$��16(�6D   $��Yu Gothic UI Light)�6R   $��37,3,0,0q�6r    �� TextAlignment$ Center=�6�   $��29��6�   $	��Top=�52      $
��Left=�60   $	��125��6K   ��53      �� 54      $ 	#FF1A1A1A��6&   $
 0.76Q�68   6I   56      �� 57      -   5        #��[ 57   ,   $��+��6Q   $��Right=�6]   $��32��6y   $��29��6�   $	��Top=�6�   $��Center=�58      $��Center=�69   $��0,3,0,0q�6[   6n   59      �� 5:   
   -   5        #��[ 5:      $��Добавить��6G   $��	0,0,10,32q�6Z   $��Right=�6m   $	��199��6�   $��32��6�   $��Bottom=�6�   6�   5;      5<      