   M S B A M L   `   `   `  ����E  AVFMDesctop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:VFMDesctop.View.PagesVFMDesctop.View.Pages  5      G(clr-namespace:VFMDesctop.View.ComponentsVFMDesctop.View.Components  5      -    'VFMDesctop.View.Pages.AuthorizationPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      5local#clr-namespace:VFMDesctop.View.Pages     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      9
components(clr-namespace:VFMDesctop.View.Components  5      FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	        N� Title$  AuthorizationPage��5       � 	Resources 5      4   .VFMDesctop.View.Components.BindablePasswordBox �� 
TargetType   CustomPasswordBoxStyle �� Property �� Value��%w   &	        �� 5   
   "  6   .�5      ��  � ��$ 1q�60   �� 5       � ��
 �   �6,   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5    
   �� $��Авторизация��6   $��Center=�5!      $��Center=�5"      $��24(�5#        ForegroundColor#���  5$      �� 5&   
   $��2��6   $	��186��5'      $��28��5(      #���  5)      )  #MaterialDesignThemes.Wpf.HintAssist   
HelperText$- 'Ошибка: ошибка ошибок��5*         Hint$ 
Почта��5+       �� 	MaxLength$ 30��5,      $��15(�5-      $��
5, 0, 5, 0q�5.      $��Center=�5/      $��Center=�50        BorderBrushColor#���  51      $��1q�52      G� 54   
   $��3��6   $	��186��55      $��28��56       # MaterialDesignRevealPasswordBox#��[ 57      	 G� 	MaxLength$	 30��58      #���  59      $- 'Ошибка: ошибка ошибок��5:      $ Пароль��5;      $��15(�5<      $��
5, 0, 5, 0q�5=      $��Center=�5>      $��Center=�5?      #���  5@      $��1q�5A      �� 5C   
   $��4��6   #���  5D      $��Center=�5E      $��Center=�5F      
 � Cursor$

 Hand��5G        BackgroundColorСheckBox#���  5H      $	��186��5I      .��5K      �� $��20��6   .��6%   Запомнить?5M      5N      �� 5P   
   -   5        $��Login��5P      $��5��5Q      $��70��5R      #���  5S      #���  5T      $��34��5U      $��
Войти��5V      $

 Hand��5W      ���	#00000000$��13(�5Y      ��5Z      �� *LoginButton_Click+� 5\   
   $��6��6   $��Center=�5]      $��Center=�5^      $	��186��5_      $��20��6    � ColumnDefinitions 5a      �� 5b      $��1* �6#   �� 5c      $��1* �6#   .+�5f      �� -   5        $��ForgotPassword��5g      $��Забыл пароль?��5f      $��75��5h      $��Center=�5i      $
��Left=�5j      $

 Hand��5k      $��11(�5l      ��5n      �� 5o      " ��6   .�5p      ��  � ��# �  5q   !    �� Triggers 5r      P� 5s       P� Property �@J�IsMouseOver P� Value . . �5t   "   ��  � �� �#a3a3a36?   5u      5w      5z      �� 5|      -   5        $��Register��5�      $��Регистрация��5|      $��75��5}      $

 Hand��5~      $��1��5      $��Right=�5�      $��Center=�5�      $��11(�5�      ��5�      �� 5�      " ��6   .�5�      ��  � ��# �  67    5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � �� �#a3a3a36?   5�      5�      5�      5�      5�      5�      