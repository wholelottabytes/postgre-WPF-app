   M S B A M L   `   `   `  ����E  Abeatsloves, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:beatsloves.Servicesbeatsloves.Services  5      =#clr-namespace:beatsloves.ViewModelsbeatsloves.ViewModels  5      %    beatsloves.Views.BeatDetailPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2serv!clr-namespace:beatsloves.Services     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      :
viewmodels#clr-namespace:beatsloves.ViewModels     5        N� 
Background  �#272B2F N� Title$ Beat Details��5       � 	Resources 5      ;   5beatsloves.Services.PurchaseButtonVisibilityConverter   PurchaseVisibilityConverter��%   &	          5   
   61   5      .��5      �� $
��Auto=�6   .��5   
   � $��20q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �� $��Beat Details��6   $��28(�6,   $
��Bold"�6:   
�������$��Center=�5      $
��0,10q�65   �� 5      $��1��6   $	��400��6"   $��Center=�6.   $��Center=�6K   ���#212529 �� Padding$ 20q�5   *    �� CornerRadius$ 15��67   ._�5      �� .+�5       �� $
��0,10q�6!   $��Center=�6/   .+�5!      �� $	��150��6H   $	��150��6U   $��10q�6a   ��6    �� *SelectedBeat.Beatimg+6a   5"      �� 5%      $
��0,10q�6!   .+�5&      �� $��Title��6$   ���#FFFFFF$
��Bold"�6F   $��16(�6X   �� 5'      ���#FFFFFF$��16(�6]   ��6$   �� *SelectedBeat.Title+6]   5(      �� 5+      $
��0,10q�6!   .+�5,      �� $��Description��6$   ���#FFFFFF$
��Bold"�6L   $��16(�6^   �� 5-      $
��Wrap=�6N   ���#FFFFFF$��16(�6w   ��6$   �� *SelectedBeat.Description+6w   5.      �� 51      $
��0,10q�6!   .+�52      �� $��Average Rating��6$   ���#FFFFFF$
��Bold"�6O   $��16(�6a   �� 53      
���������54      �� 55   "    �� 
TargetType" ��6(   .�56   &   ��  �� Property � �� �� Value 6=   �� *AverageRating+	 �� Triggers	 57   &   �� 58   *   
 �� Binding
 66   �� *AverageRating+ �� Value 06X   .a�59   .   ��  � ��$ 
No Ratings��6E   5:   +   5<   #   5>      5?      �� 5@      $
��0,10q�6!   .+�5A      �� $��Type ID��6$   ���#FFFFFF$
��Bold"�6H   $��16(�6Z   �� 5B      ���#FFFFFF$��16(�6b   ��6$   �� *SelectedBeat.Typestring+6b   5C      �� 5F      $
��0,10q�6!   .+�5G      �� $
��Tags��6$   ���#FFFFFF$
��Bold"�6E   $��16(�6W   �� 5H      ��6'   �� *SelectedBeat.Tags+��5I      �� 5J   "   .R�5K   &   � 5L   #   ��5N      �� 5O   "   .R�5P   &   �� ���#0ccafb$ 5��6B   $��0q�6S   $ 5q�6^   ._�5Q   *   �� 
�������$��14(�6X   ��64   �� 6X   5R   '   5S   #   5U      5V      �� 5Y      $
��0,10q�6!   .+�5Z      �� $��Your Rating��6$   ���#FFFFFF$
��Bold"�6L   $��16(�6^   �� 5[       �� SelectedValuePath$ Content��6U   $	��100��6q    �� SelectedValue 6#   �� *
UserRating+ �� Mode$ TwoWay=�.@�5\      �� $��1��6+   �� 5]      $��2��6+   �� 5^      $��3��6+   �� 5_      $��4��6+   �� 5`      $��5��6+   5a      �� 5b      $
��Rate��6!   $	��120��6T   $��40��6`   $��
10,10,10,0q�5c   !   $��16(�65   ���#0ccafb
�������
������ $��Center=�5d   ;   ��5b   0   �� *RateBeatCommand+5d   ;   5e      �� 5h      $
��0,10q�6!   $��Center=�6/   .+�5j      r� -   5        $��MediaPlayer��5j   '   $	��300��5k      $��50��6    r� LoadedBehavior$ Manual=�6+    r� UnloadedBehavior$
 Stop=�6C    r� Source 5j   :   �� *SelectedBeat.Beatway+5l      �� 5o      $��10q�6%    �� Orientation$ Vertical=�61   .+�5p      �� -   5        $��ProgressSlider��5p   %   $	��300��6;    � Minimum$ 0Q�5q       � Maximum$ 1Q�6   5r      �� 5s      -   5        $��TimeText��5s   (   $��0:00 / 0:00��68   
�������$��Center=�5t   '   5u      �� 5x      $ 
Horizontal=�6%   $��Center=�6>   $��10q�6[   .+�5y      �� $
��Play��6%   $��80��64   $��30��6?   $��5q�6K   $��16(�5z   1   ���#0ccafb
�������
������ $��Center=�5{   7   ��5z      �� *PlayCommand+5{   7   �� 5|      $��Pause��6%   $��80��65   $��30��6@   $��5q�6L   $��16(�5}   2   ���#0ccafb
�������
������ $��Center=�5~   7   ��5}      �� *PauseCommand+5~   7   �� 5      $
��Stop��6%   $��80��64   $��30��6?   $��5q�6K   $��16(�5�   1   ���#0ccafb
�������
������ $��Center=�5�   7   ��5�      �� *StopCommand+5�   7   5�      5�      �� 5�      $��Purchase��6   $	��120��5�      $��40��6)   $
��0,10q�65   $��16(�6C   ���#0ccafb
�������
������ $��Center=�5�   7   ��5�   0   �� *PurchaseCommand+y�5�      �� *SelectedBeat.UserId+ �� 	Converter# [  5�   7   �� 5�      -   5        $	��200��5�      $��40��6*   $
��0,10q�66   $��16(�6D   ���#0ccafb
�������
������ $��Center=�5�   .   $
��Back��6d   5�      5�      5�      5�      5�      