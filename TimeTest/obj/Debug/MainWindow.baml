   M S B A M L   `   `   `  ����C  ?TimeTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TimeTest.PropertiesTimeTest.Properties  5   	       TimeTest.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2prop!clr-namespace:TimeTest.Properties     5   	   -   5        #   TimeTest.Properties.Resources    AppTitle � Title# Z  5   	   $	��450��65   $	��800��6B   .��5      �  J� RenderTransformOrigin$ 0.518,0.253&�6   .+�5   
   (� $��200,0,200,300q�6   .��5      �� -   5        $��	textBlock��5      $
��Left=�6)   $��00:00:00��6D   $��18��6v   $ 0.457,1.205&�6�   5      �� 5   
   -   5        $	��IMG��5      $	��200��6    J� Opacity$
 0.50Q�6'   $��296,124,296,96q�66   �� 5   
   $
��List��6   $��34(�6   $��0,235,722,128q�6-   �� 5   
   ���#5e3288$��1q�6&   $
��Left=�6:   $
 0.50Q�6U   $	��141��6d   $	��Top=�6q   $	��170��6�   �� 5   
   -   5        $��button��5      $��20(�6   $
��Left=�6-   $��30��6H   $��	30,30,0,0q�6T   $	��Top=�6g   $	��120��6   $��背景画像��6�   �� 5   
   -   5        $��リスト追加��5      $��20(�6!   $
��Left=�6/   $��30��6J   $��	30,80,0,0q�6V   $	��Top=�6i   $	��120��6�   6�   �� 5    
   $��時刻��6   $��23(�6   $��-2,141,722,226q�6+   �� 5!   
   -   5         ��Name$ timeText��5!   -   $
��Left=�6   $��57��6?   $��
70,141,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $	��Top=�6s   $��98��6�   $ 0.447,1.162&�6�   �� 5"   
   -   5         ��Name$ listBox��5"   -   $
��Left=�6   $	��118��6>   $��
-2,291,0,0q�6K   $	��Top=�6_   $	��170��6w   $ 0.453,0.288&�6�   �� 5#   
   -   5        $ msgText��5#   9   $
��Left=�6   $��41��6-   $��	0,198,0,0q�6J   $
 Wrap=�6]   $��表示したい内容��6q   $	��Top=�6�   $	��168��6�   $ -5.778,-4.88&�6�   	� 5%   
   -	   5         	�Name$ alarmON��5%   )   $��Timer On��6   $
��Left=�6:   $��28��6U   $��
75,239,0,0q�6a   $	��Top=�6v   $��95��6�   6�   	� 5&   
   -
   5        $ alarmOff��5&   +   $��	Timer OFF��6   $
��Left=�6=   $��23��6X   $��
75,267,0,0q�6d   $	��Top=�6x   $��93��6�    d� 	IsChecked$
 TrueY�6�   6�   5)      5+      