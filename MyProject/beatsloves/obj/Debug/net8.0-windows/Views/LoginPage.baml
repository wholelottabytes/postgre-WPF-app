   M S B A M L   `   `   `  ����  
beatsloves     beatsloves.Views.LoginPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        N� 
Background  �#272B2F N� Title$ 	LoginPage��5      .��5
      � $��Center=�6   $��Center=�6&   .+�5   
   �� ���#212529 �� Padding$ 20q�6&    �� CornerRadius$ 15��63   ._�5      �� $��Center=�6   $	��300��66   .+�5      �� $��Login:��6   ���#FFFFFF$
��Bold"�6?   $��16(�6Q   $��0,0,0,5q�6_   �� 5      -   5         ��Name$ UsernameTextBox��5      ���#3A3F44
�������
������ $��10q�5      $��0,0,0,10q�6'   ��5      �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�5   9   �� 5      $��	Password:��6   ���#FFFFFF$
��Bold"�6B   $��16(�6T   $��0,10,0,5q�6b   G� 5      -   5         G�Name$ PasswordBox��5      ���#3A3F44
�������
������ $��10q�5      $��0,0,0,10q�6+   5      �� 5      $��Sign up��6   $	��140��6O   $��40��6[   ���#0ccafb
�������$
��Bold"�5   A   
������ $
��0,10q�6n   $��Center=�5      ��5   +   �� *RegisterCommand+5      �� 5!      -   5         ��Name$ LoginButton��5!      $��Sign in��6.   $	��140��6a   $��40��6m   ���#0ccafb
�������$
��Bold"�5"   A   
������ $
��0,10q�6m   $��Center=�5#      ��5!   @   �� *LoginCommand+5#      5$      5%      5&      5'      