   M S B A M L   `   `   `  ����E  Abeatsloves, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:beatsloves.ViewModelsbeatsloves.ViewModels  5      $    beatsloves.Views.SoldBeatsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      :
viewmodels#clr-namespace:beatsloves.ViewModels     5        N� 
Background  �#272B2F N� Title$ 
Sold Beats��5
      .��5      �� $
��Auto=�6   .��5   
   � $��20q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��
Sold Beats��6   $��28(�6*   $
��Bold"�68   
�������$��Center=�5      $
��0,10q�65   �� 5      $��1��6   $	��500��6"   $��Center=�6.   $	��Top=�6K   ���#212529 �� Padding$ 20q�5   *    �� CornerRadius$ 15��67   ._�5      �� .+�5      �� $
��0,10q�6!   $��Center=�6/   .+�5      �� $	��150��6?   $	��150��6L   $��10q�6X   ��6    �� *ImageSource+6X   5      �� 5       $��New img��6   $	��200��5!      $��40��6)   $
��0,10q�65   $��16(�6C   ���#0ccafb
�������
������ $��Center=�5"   J   ��5    /   �� *UpdateUserImageCommand+5"   J   �� 5%      $
��0,10q�6!   .+�5&      �� $��Sold Beats List��6$   ���#FFFFFF$
��Bold"�6P   $��16(�6b   $
��0,10q�5'   $   �� 5(      $��Stretch=�6E   $��Stretch=�5)   #   $	��300��6?   ��5(   #   �� *	SoldBeats+ �� View 5*      �� 5+   "   .P�5,   &   �� $��ID��65    �� Width$ 50��6e    �� DisplayMemberBinding 6A   �� *Id+6e   �� 5-   &   $
��Name��65   $	 120��6j    6C   �� *Title+6j   �� 5.   &   $��Price��65   $ 50��6|    6D   �� *Price+ �� StringFormat$ N2��6|   �� 5/   &   $
��Date��65   $	 150��6�    6C   �� *PurchaseDate+$ 
dd.MM.yyyy��6�   50   #   52      53      �� 54      -   5        $	��200��54      $��40��6*   $
��0,10q�66   $��16(�6D   ���#0ccafb
�������
������ $��Center=�55   /   $
��Back��6e   57      58      59      5:      5;      