   M S B A M L   `   `   `  ����?  ;pz_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:pz_2.View	pz_2.View  5          pz_2.View.CreateContact   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:pz_2.View     5      .��5	      � $	��300��6     � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��16(�5      $��Center=�5      $��10,20,10,20q�5      ��5      �� *
HeaderText+5      �� 5   
   $��1��6   $
��Wrap=�5      
���  ��$��
10,0,10,10q�5      ��5      �� *ErrorMessage+5      �� 5   
   $��2��6   .+�5      �� $��Тип контакту��6   $��	10,0,10,5q�5      �� 5      $��
10,0,10,10q�6    �� DisplayMemberPath$
 Name��5!      ��5       �� *Types+ �� SelectedValue 5"      �� *SelectedType+�� 5#      $��2��6   $'��!Значення контакту��5$      $��	10,0,10,5q�5%      �� 5&      $��3��6   $��
10,0,10,10q�5(      ��5'      �� *	InputText+ �� UpdateSourceTrigger$ PropertyChanged=�5(      5)      � 5+   
   $��3��6   $��Center=�6    � ColumnDefinitions 5,      �� 5-      $
��auto �6#   �� 5.      $
��auto �6#   .+�50      �� $��0��6   $��Додати��51      $��80��52      $��0,0,20,0q�53      $��Center=�54      ��55      �� *CreateContact+�� 56      $��1��6   $��
Назад��57      $��80��58      $��20,0,0,0q�59      $��Center=�5:      ��5;      �� *GoToBusiness+5<      5=      5>      