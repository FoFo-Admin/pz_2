   M S B A M L   `   `   `  ����?  ;pz_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:pz_2.View	pz_2.View  5          pz_2.View.CreateProduct   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:pz_2.View     5      .��5	      � $	��300��6     � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��16(�5      $��Center=�5      $��10q�5      ��5      �� *
HeaderText+5      �� 5   
   $��1��6   $
��Wrap=�5      
���  ��$��
10,0,10,10q�5      ��5      �� *ErrorMessage+5      �� 5   
   $��2��6   .+�5      �� $#��Назва продукту *��6   $��	10,0,10,5q�5      �� 5      $��
10,0,10,10q�5       ��5      �� *Name+ �� UpdateSourceTrigger$ PropertyChanged=�5       �� 5!      $��Опис продукту��6   $��	10,0,10,5q�5"      �� 5#      $��
10,0,10,10q�5$      ��5#      �� *Description+$ PropertyChanged=�5$      �� 5%      $��Кількість��6   $��	10,0,10,5q�5&      �� 5'      $��
10,0,10,10q�5(      ��5'      �� *Amount+$ PropertyChanged=�5(      �� 5)      $��Ціна��6   $��	10,0,10,5q�5*      �� 5+      $��
10,0,10,10q�5,      ��5+      �� *Price+$ PropertyChanged=�5,      �� 5-      $��Знижка, %��6   $��	10,0,10,5q�5.      �� 5/      $��
10,0,10,10q�50      ��5/      �� *
Discount+$ PropertyChanged=�50      �� 51      $1��+Завантажити зображення��6   $��
0, 0, 0, 5q�52      $	��200��53      $��30��54      ��55      �� *SelectImage+�� 56      $��0, 0, 0, 10q�57      ��56      �� *
ImageUrl+57      58      � 5:   
   $��3��6   $��Center=�6    � ColumnDefinitions 5;      �� 5<      $
��auto �6#   �� 5=      $
��auto �6#   .+�5?      �� $��0��6   $��Додати��5@      $��80��5A      $��0,0,20,0q�5B      $��Center=�5C      ��5D      �� *CreateProduct+�� 5E      $��1��6   $��
Назад��5F      $��80��5G      $��20,0,0,0q�5H      $��Center=�5I      ��5J      �� *GoToBusiness+5K      5L      5M      