   M S B A M L   `   `   `  ����E  Abeatsloves, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:beatsloves.Modelsbeatsloves.Models  5      =#clr-namespace:beatsloves.ViewModelsbeatsloves.ViewModels  5      9!clr-namespace:beatsloves.Servicesbeatsloves.Services  5
          beatsloves.Views.MainPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      1localclr-namespace:beatsloves.Models     5      :
viewmodels#clr-namespace:beatsloves.ViewModels     5      2serv!clr-namespace:beatsloves.Services     5
      $	��600��5      $
��1080��6     N� 
FontFamily$  Segoe UI)�5       N� Title$ 
Beat Store��5       � 	Resources 5      7   1beatsloves.Services.UserIdEditVisibilityConverter !  UserIdEditVisibilityConverter9   3beatsloves.Services.UserIdDeleteVisibilityConverter # UserIdDeleteVisibilityConverter��%:   &	        &	        5   
   6-     5   
   6/   5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��170 �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��2��6   ���#272B2F 5      �� 5      $��* �6#   �� 5       $	��400 �6#   .+�5$      �� $��
Beat Store��6   $��28(�6*   $��Center=�68   $
��Left=�6S   $��10,0,0,0q�6n   
�������$
��Bold"�5%      �� 5(       �� Orientation$ 
Horizontal=�6   $��Center=�62   $��Center=�6O   .+�5)      �� $	��250��6   $��30��6&   $��Center=�5*      $��5q�6)   ��5)   2   �� *SearchQuery+ �� UpdateSourceTrigger$ PropertyChanged=�5*   )   �� 5,      $
��Bold"�6   $��Search��6+   $��75��6<   $��30��6G   $��Center=�5-   5   ��6   �� *LoadMySearchCommand+65   5.      �� 51      $ 
Horizontal=�6   $��1��62   $��Right=�6B   $��Center=�6^   $��10q�6y   .+�52      �� $
��Bold"�6   $	��Add��6+   $��40��69   $��30��6D   $��Center=�6{   ��6P   �� *OpenAddBeatPageCommand+6{   �� 53      $
��Bold"�6   $��+Balance��6,   $��70��6?   $��30��6J   $��Center=�6�   $��10,0,0,0q�6�   ��6V   �� *IncreaseBalanceCommand+6�   �� 55      $��30��6   $��30��6%   $��	10,0,10,0q�61   ��6D   �� *OpenSoldBeatsPageCommand+��56      �� 57       �� 
TargetType" ��6*   .R�58      �� $��30��6%   $��30��60    �� CornerRadius$ 15��6<   ��59   "   �� 5:   &   	 j� Stretch$	 UniformToFill=�6S   
 �� ImageSource
 61   �� *	UserImage+6S   5<      5=      5?      �� 5@      
�������$
��Bold"�6L   $��16(�6^   $��Center=�6l   $��0,0,10,0q�6�   ��6   �� *UserBalance+6�   �� 5C      -   5        $
��Bold"�5C      $��Logout��6+   $��80��6<   $��30��6G   $��Center=�6l   5D      5E      �� 5H   
   $��1��6   $��0��6#   ���#272B2F.+�5J      	�  	� 	GroupName$
 Menu��6    d� 	IsChecked$
 TrueY�6+   ��6<   �� *LoadMainBeatsCommand+.��5K      �� $ 
Horizontal=�6   .+�5L      �� $0��*pack://application:,,,/Images/img_home.png��6   $��26��6P   $��26��6[   $��10,0,5,0q�6g   �� 5M      $��20(�6    $��	Main page��5N      $��Center=�5O      
�������$
��Bold"�5Q      5R      5S      	� 5T      $
 Menu��6   ��6,   �� *LoadMyBeatsCommand+.��5U      �� $ 
Horizontal=�6   .+�5V      �� $4��.pack://application:,,,/Images/img_customer.png��6   $��26��6T   $��26��6_   $��10,0,5,0q�6k   �� 5W      $��20(�6    $��My beats��5X      $��Center=�5Y      
�������$
��Bold"�5[      5\      5]      	� 5^      $
 Menu��6   ��6,   �� *LoadPurchasesCommand+.��5_      �� $ 
Horizontal=�6   .+�5`      �� $7��1pack://application:,,,/Images/img_transaction.png��6   $��26��6W   $��26��6b   $��10,0,5,0q�6n   �� 5a      $��20(�6    $��	Purchases��5b      $��Center=�5c      
�������$
��Bold"�5e      5f      5g      5h      �� 5k   
   $��1��6   $��1��6$   ���#212529.��5l      �� .+�5m      �� $��
Your Beats��6   $��10q�6.   $��22(�6:   
�������$
��Bold"�5n      �� 5o      $
��20 0q�6   .+�5p      �� -   5         ��Name$ ListViewBeats��5p   #   ��6:   �� *Beats+��5q      �� 5r      .R�5s   "   � .+�5t   &   �� $��0,0,1,1q�6.     CustomButtonStyle2#��[ 6?   ��6k   �� *#!DataContext.OpenBeatDetailCommand+ �� RelativeSource ��  �� AncestorType Page��5u      �� .��5v   *   �� $��10q�61   $	��149��6=   $	��220��6I   ���#DDDDDD$��1q�6l   ._�5w   .   �� .+�5x   2   �� $	��140��69   $	��140��6E   $ 5��6R   $��5q�6c    J� Effect 5y   6   3  -System.Windows.Media.Effects.DropShadowEffect  5z   :      ShadowDepth$ 1Q�6K   ��5|   6   �� 5}   :   
 6E   �� *	Beatimg+5   3   �� 5�   2   $��5q�6<   $��17(�6t   $��Franklin Gothic Medium)�6�   ��6G   �� *Price+ �� StringFormat$ {0:C}��6�   �� 5�   2   $	��5 0q�6<   $��15(�6`   $
��Wrap=�6n   $��Center=�6�   ��6I   �� *Title+6�   5�   /   5�   +   5�   '   �� 5�   &   $ 
Horizontal=�61   $��Right=�6J   $	��Top=�6f   $��5q�6~   .+�5�   *   �� $
��Bold"�61   $
��Edit��6C   $��0,0,5,0q�6R     CustomButtonStyle#��[ 5�   e   ��5�      �� *%#DataContext.OpenEditBeatPageCommand+ ��  Page��5�      �� y�5�      �� *UserId+ �� 	Converter# [  6e   �� 5�   *   $
��Bold"�61   $��Delete��6C   #��[ 5�   1   ��5�      �� *DataContext.DeleteBeatCommand+ ��  Page��5�      �� y�5�      �� *UserId+# [ 5�   +   5�   '   5�   #   5�      ��5�      �� 5�      .R�5�   "   � 5�      5�      5�      �� 5�      $ 
Horizontal=�6   $��Center=�66   $
��0,10q�6S   $��2��6a   $��1��6n   .+�5�      �� $��16(�6=   $��Center=�6K   
�������$��10q�6y   ��6    �� *CurrentPage+6y   �� 5�      $
��Next��6   $	��100��6,   $��5q�6\   ��68   �� *NextPageCommand+6\   5�      5�      5�      5�      5�      