   M S B A M L   `   `   `  ����E  AClientBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ClientBank.ViewClientBank.View  5   	   F BAccountData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullT9clr-namespace:AccountData.Validation;assembly=AccountDataAccountData.Validation 5   	   $    ClientBank.View.AddDepositView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ClientBank.View     5   	   Irul9clr-namespace:AccountData.Validation;assembly=AccountData     5   	   -   5          �Name$  depositWindows��5   	    � Title$# Открытие вклада��5
   	   $	��450��6!   $	��400��6.   $	��500��6>   $	��600��6M    � Icon$ /Images/unnamed.png��6Y    � 	Resources 5       !  SliderThumb.Static.Foreground �� Color $  SliderThumb.MouseOver.Background   SliderThumb.MouseOver.Border " SliderThumb.Pressed.Background  SliderThumb.Pressed.Border # SliderThumb.Disabled.Background  SliderThumb.Disabled.Border ! SliderThumb.Static.Background  SliderThumb.Static.Border 	 SliderThumbHorizontalTop �� 
TargetType � UseLayoutRounding F�Name �� Stretch	 J� SnapsToDevicePixels
 �� Triggers P� Property P� Value �� 
TargetName �� Value �� Property 
 SliderThumbHorizontalBottom  SliderThumb.Track.Border   SliderThumb.Track.Background  RepeatButtonTransparent �� 
TargetType   SliderThumbHorizontalDefault  SliderHorizontal ��Name � RowDefinitions m�Name m� Fill m� 	Placement ��Name ]�Name ]� DecreaseRepeatButton �� DecreaseLarge ]� IncreaseRepeatButton �� IncreaseLarge ]� Thumb n�Name � OverridesDefaultStyle  SliderThumbVerticalLeft  SliderThumbVerticalRight  SliderThumbVerticalDefault  SliderVertical � ColumnDefinitions  SliderStyle1  �� Triggers��%:'  &	        &	 #     &	 F     &	 i     &	 �     &	 �     &	 �     &	 �     &	     &		 ;    7  7  7  7  7  7  7  7  &	
 S    7  7  7  7  7  7  7  7  &	 c    &	 �    &	 �    &	 �    7  7  7  7  7  7  7  7  &	 �    7  7  7  7  7  7 	 7 
 &	 �    7  7  7  7  7  7  7  7  &	 �    7  7  7  7  7  7  7  7  &	 �    7  7  7  7  7  7  7  7  &	 =    7  7  7  7  7  7  7  &	 $    7   7  7  �� 5   
   $ 	#FF3CB7E5��6@   �� 5   
   $ 	#FFA3D7E5��6C   �� 5   
   $ 	#FF7Eb4EA��6?   �� 5   
   $ 	#FF3CB7E5��6A   �� 5   
   $ 	#FF569DE5��6=   �� 5   
   $ 	#FF3CB7E5��6B   �� 5   
   $ 	#FFD9D9D9��6>   �� 5   
   $ 	#FF3CB7E5��6@   �� 5   
   $ 	#FFACACAC��6<   �� 5   
   " n�6;   .R�5      � $��Center=�6    . $��Center=�6I   .+�5      F� $
 grip��6   �����    ��[ �    ��[ `�S `�S �`�S     �ا ��[ �ا ��[ ��ا ��[ �ا ���ا ����ا �������    ����[ ��[ 	8��  $
 Fill=�6�   	 . 8�� $��1��6   . $��Center=�6D  5      
 5      P� 5       �@J�IsMouseOver . . �5      �� $
 grip��6-   8   � ��6?   �� 5      $
 grip��6/   8   � ��6A   5      P� 5       �@n�
IsDragging . . �5      �� $
 grip��6-   8   � ��6?   �� 5       $
 grip��6/   8   � ��6A   5!      P� 5"       � {� .  . �5#      �� $
 grip��6-   8   � ��6?   �� 5$      $
 grip��6/   8   � ��6A   5%      5'      �� 5(   
   " n�6>   .R�5)      � $��Center=�6    . $��Center=�6I   .+�5*      F� $
 grip��6   �����     � �     � `�S ��`�S ���`�S ���ا  � �ا  � ��ا  � �ا �ا ��ا     �         �  � 	8��  $
 Fill=�6�   	 . 8�� $��1��6   . $��Center=�6H  5+      
 5,      P� 5-       �@J�IsMouseOver . . �5.      �� $
 grip��6-   8   � ��6?   �� 5/      $
 grip��6/   8   � ��6A   50      P� 51       �@n�
IsDragging . . �52      �� $
 grip��6-   8   � ��6?   �� 53      $
 grip��6/   8   � ��6A   54      P� 55       � {� .  . �56      �� $
 grip��6-   8   � ��6?   �� 57      $
 grip��6/   8   � ��6A   58      5:      �� 5;   
   $ 	#FF3CB7E5��6;   �� 5<   
   $ 	#FF3CB7E5��6?   �� 5=   
   " ��60   .�5>      ��  �@�OverridesDefaultStyle . 66   �� 5?       � ��
 ���� 6+   �� 5@       � |� .  6*   �� 5A       � �� .  6*   �� 5B       � �� 5C      �� 5D      " ��6&   .R�5E      �� #��z��6$   #��z��6H   #��z��6j   5F      5H      5I      �� 5J   
   " n�6?   .R�5K      � $��Center=�6    . $��Center=�6I   .+�5L      F� $
 grip��6   d����        �        �ا �ا ��ا     �ا ���ا ����ا �������    ��	8��  $
 Fill=�6�   	 . 8�� $��1��6   . $��Center=�62  5M      
 5N      P� 5O       �@J�IsMouseOver . . �5P      �� $
 grip��6-   8   � ��6?   �� 5Q      $
 grip��6/   8   � ��6A   5R      P� 5S       �@n�
IsDragging . . �5T      �� $
 grip��6-   8   � ��6?   �� 5U      $
 grip��6/   8   � ��6A   5V      P� 5W       � {� .  . �5X      �� $
 grip��6-   8   � ��6?   �� 5Y      $
 grip��6/   8   � ��6A   5Z      5\      �� 5]   
   " ��63   .R�5^      �� $ border��6   #��z��6%   #��z��6Q   #��z��6�   	 . ._�5_      �  5`      �� 5a      $
��Auto �6(   �� 5b      $
��Auto �6(   #��z��66   �� 5c      $
��Auto �6(   .+�5e      m� $ TopTick��6   # z��6/   $��4��6S   $��0,0,0,2q�6^   $	 Top=�6o   $��0��6   $y�	Collapsed=�6�   m� 5f      $ 
BottomTick��6   # z��62   $��4��6V   $��0,2,0,0q�6a   $ Bottom=�6r   $��2��6�   $y�	Collapsed=�6�   �� 5g      $ TrackBackground��6   8��  $��1q�6n   8�� $	��4.0��6�   $	��5,0q�6�   $��1��6�   $��center=�6�   ._�5h      �� $��-6,-1q�6!   .+�5i      �� $ PART_SelectionRange��6(   #��� ��6E   $	��4.0��6�   $y�Hidden=�6�   5j      5k      ]� 5l      $ 
PART_Track��6   $��1��60    5m      �� 5n      #��Z 6+   8�� 6U    5p      �� 5q      #��Z 6+   8�� $��Right=�6�   $��396.97��6�    5s      n� 5t      $ Thumb��6$   |�.  $��18��6E    . 8�� $��Center=�6�   $��11��6�   5v      5w      5x      
 5y      P� 5z       �@��TickPlacement$ TopLeft=�63   . �5{      �� $ TopTick��63    � y�$ Visible=�6H   �� 5|      $ Thumb��61   8   � ��6D   �� 5}      $ TrackBackground��6/    � ��$ 5,2,5,0q�6L   5~      P� 5       �@��TickPlacement$ BottomRight=�63   . �5�      �� $ 
BottomTick��63    � y�$ Visible=�6K   �� 5�      $ Thumb��61   8   � ��6D   �� 5�      $ TrackBackground��6/    � ��$ 5,0,5,2q�6L   5�      P� 5�       �@��TickPlacement$
 Both=�63   . �5�      �� $ TopTick��63    � y�$ Visible=�6H   �� 5�      $ 
BottomTick��63    � y�$ Visible=�6K   5�      P� 5�       �@��IsSelectionRangeEnabled . . �5�      �� $ PART_SelectionRange��63    � y�$ Visible=�6T   5�      P� 5�       �@J�IsKeyboardFocused . . �5�      �� $ Thumb��63    � ��
 ��  �6F   5�      5�      �� 5�   
   " n�6:   .R�5�      � $��Center=�6    . $��Center=�6I   .+�5�      F� $
 grip��6   �������[ �ا Ӏ�[ �ا `�S `�S �    `�S ��[ ��[ Ӏ�[     ����Ӏ�    ���ا ���ا Ӏ��ا ��[ �ا ��[ �ا 	8��  $
 Fill=�6�   8�� 6�   5�      
 5�      P� 5�       �@J�IsMouseOver . . �5�      �� $
 grip��6-   8   � ��6?   �� 5�      $
 grip��6/   8   � ��6A   5�      P� 5�       �@n�
IsDragging . . �5�      �� $
 grip��6-   8   � ��6?   �� 5�      $
 grip��6/   8   � ��6A   5�      P� 5�       � {� .  . �5�      �� $
 grip��6-   8   � ��6?   �� 5�      $
 grip��6/   8   � ��6A   5�      5�      �� 5�   
   " n�6;   .R�5�      � $��Center=�6    . $��Center=�6I   .+�5�      F� $
 grip��6   ����� � �ا � � �ا ��`�S ��`�S Ӏ�`�S  �  � � �     �        �ا �ا �    �ا  � �ا  � �ا 	8��  $
 Fill=�6�   8�� 6�   5�      
 5�      P� 5�       �@J�IsMouseOver . . �5�      �� $
 grip��6-   8   � ��6?   �� 5�      $
 grip��6/   8   � ��6A   5�      P� 5�       �@n�
IsDragging . . �5�      �� $
 grip��6-   8   � ��6?   �� 5�      $
 grip��6/   8   � ��6A   5�      P� 5�       � {� .  . �5�      �� $
 grip��6-   8   � ��6?   �� 5�      $
 grip��6/   8   � ��6A   5�      5�      �� 5�   
   " n�6=   .R�5�      � $��Center=�6    . $��Center=�6I   .+�5�      F� $
 grip��6   +���� �  � ѠI � ѠI�y� � � �y� 	8��  $
 Fill=�6�   8�� 6�   5�      
 5�      P� 5�       �@J�IsMouseOver . . �5�      �� $
 grip��6-   8   � ��6?   �� 5�      $
 grip��6/   8   � ��6A   5�      P� 5�       �@n�
IsDragging . . �5�      �� $
 grip��6-   8   � ��6?   �� 5�      $
 grip��6/   8   � ��6A   5�      P� 5�       � {� .  . �5�      �� $
 grip��6-   8   � ��6?   �� 5�      $
 grip��6/   8   � ��6A   5�      5�      �� 5�   
   " ��61   .R�5�      �� $ border��6   #��z��6%   #��z��6Q   #��z��6�   	 . ._�5�      �  5�      �� 5�      $
��Auto �6+   �� 5�      #��z��6+   $
��Auto �6Q   �� 5�      $
��Auto �6+   .+�5�      m� $ TopTick��6   $��0��6/   # z��6?   $��0,0,2,0q�6c   $
 Left=�6t   $y�	Collapsed=�6�   $��4��6�   m� 5�      $ 
BottomTick��6   $��2��62   # z��6B   $��2,0,0,0q�6f   $ Right=�6w   $y�	Collapsed=�6�   $��4��6�   �� 5�      $ TrackBackground��6   8��  $��1q�6n   8�� $��1��6�   $��center=�6�   $	��0,5q�6�   $	��4.0��6�   ._�5�      �� $��-1,-6q�6!   .+�5�      �� $ PART_SelectionRange��6(   #��� ��6E   $y�Hidden=�6�   $	��4.0��6�   5�      5�      ]� 5�      $ 
PART_Track��6   $��1��60    5�      �� 5�      #��Z 6+   8�� 6U    5�      �� 5�      #��Z 6+   8�� 6U    5�      n� 5�      $ Thumb��6$   |�.  $��11��6E    . 8�� $	��Top=�6�   $��18��6�   5�      5�      5�      
 5�      P� 5�       �@��TickPlacement$ TopLeft=�63   . �5�      �� $ TopTick��63    � y�$ Visible=�6H   �� 5�      $ Thumb��61   8   � ��6D   �� 5�      $ TrackBackground��6/    � ��$ 2,5,0,5q�6L   5�      P� 5�       �@��TickPlacement$ BottomRight=�63   . �5�      �� $ 
BottomTick��63    � y�$ Visible=�6K   �� 5�      $ Thumb��61   8   � ��6D   �� 5�      $ TrackBackground��6/    � ��$ 0,5,2,5q�6L   5�      P� 5�       �@��TickPlacement$
 Both=�63   . �5�      �� $ TopTick��63    � y�$ Visible=�6H   �� 5�      $ 
BottomTick��63    � y�$ Visible=�6K   5�      P� 5�       �@��IsSelectionRangeEnabled . . �5�      �� $ PART_SelectionRange��63    � y�$ Visible=�6T   5�      P� 5�       �@J�IsKeyboardFocused . . �5�      �� $ Thumb��63    � ��
 ��  �6F   5�      5�      �� 5�   
   " ��6%   .�5�      ��  �@��IsPressAndHoldEnabled .  6=   �� 5�       � ��
 ���� 6+   �� 5�       � ��
 ���� 6,   �� 5       � ��8   6+   �� 5      � ��8  6)     5     P� 5      �@��Orientation$ Vertical=�61   . �5     ��  � ��8  61   5     5     5     .��5	     � -   5        ! �Name$! 
accounList��5	      5
  
   �� 5     $
��Auto �6   �� 5     $��* �6   .+�5  
   � ���	#FF00D1FF$��0��6&    5     �� 5     $	��40* �6#   �� 5     $	��60* �6#    5     �� 5     $
��Auto �6    �� 5     $
��Auto �6    �� 5     $
��Auto �6    �� 5     $
��Auto �6    .+�5     �� $��Right=�66     TextBlockDetail#��[ 6R   ��6   �� *StatusClient+6R   �� 5     $��1��64   #��[ 6D   ��6   �� *
ClientName+6D   �� 5     $��1��6   $��Дата открытия��6%   $��Right=�6:   #��[ 6V   �� 5     $��1��6   #��[ 6l   $��1��6�   ��6%   �� *DepositlItem.DateOpen+" �� StringFormat$" {0:dd/MM/yyyy}��6�   �� 5     $��2��6   $��Ставка��6%   $��Right=�63   #��[ 6O   �� 5     $��2��6   $��1��6&   # �� Orientation$# 
Horizontal=�66   $��Center=�6O   .+�5     �� #��[ 6   ��6F   �� *DepositlItem.Rate+�� 5      #��[ 6   $��% годовых��6E   5!     �� 5"     $��3��6   $��
Доход��6%   $��Right=�62   #��[ 6N   �� 5#     $��3��6   $��1��6%   #��[ 65   ��6^   �� *Incom+$" {0:0.00}��5$     � 5%  
   $��1��6    5&     �� 5'     $
��Auto �6    �� 5(     $
��Auto �6    �� 5)     $
��Auto �6    �� 5*     $��* �6     5,     �� 5-     $
��Auto �6#   �� 5.     $��* �6#   .+�50     �� $��0��6     	TextBlock#��[ 6%   $"Внесение наличных:6\   �� 51     -   5        $ ��Name$$ txtCash��51       TextBox#��[ 6)   $��1��6J   $��0��6Z   .��6h   ��52     �� 53     % �� Path$
% Cash�6   & �� NotifyOnValidationError& . ' �� UpdateSourceTrigger$' PropertyChanged=�6I   ( �� ValidationRules( 54     &   AccountData.Validation.MoneyRule  55     
)   Min$) 50000~�6,   
*   Max$* 5000000~�68   5  /System.Windows.Controls.ExceptionValidationRule  56     58     .��59      5:     �� 5;     $��1��6   #��[ 6%   $��	Срок:��6H   � 5<     $��1��6   $��1��6     5=     �� 5>     $��* �6'   �� 5?     $��* �6'    5A     �� 5B     $
��Auto �6$   �� 5C     $
��Auto �6$   .+�5E     �� #��[ 6   $��0��6?   $��0��6O   ��6]   �� $% 
MonthCount��� 5F     $��1��6   #��[ 6H   $
��Left=�6q   ��6,   �� *
MonthhMess+6q   �� 5G     -   5        + ��Name$+ 	txtPeriod��5G     , � SmallChange$, 3Q�5H     $��5q�6!   $��2��6,   $��1��6@   - � Maximum$- 12Q�5I     . � Minimum$. 3Q�6   / �� TickPlacement$/ TopLeft=�6*   0 �� TickFrequency$0 3Q�6B   1 �� IsSnapToTickEnabled1 . #���  6o   ���	#FF2382912 � Value2 5G  ,   �� *
MonthCount+$' PropertyChanged=�5I  �   5K     �� 5L     $��2��6A   #��[ 6N   y�6   �� *ElementVisibility+Капитализация:6�   �� 5N     -   5        3 ��Name$3 checkCapitalization��5N     $��25��65   $��2��6A   $��1��6N     AnimatedSwitch#��[ 6^   4 d� 	IsChecked4 6�   �� $% Capitalization��� 5P     $��3��6   $��1��6"   $��Сохранить��6�     MessagesButton#��[ 6�   $��Bottom=�6�   $��5q�6�   {�62   �� 5 �� ElementName$5 txtCash��$A% ;(Validation.Errors)[0].RuleInError.ValidatesOnTargetUpdated���6  �� *
AddAccount+��6"  �� $5 depositWindows���� 5Q     $��3��6   $��Отмена��6"   #��[ 63   $��Bottom=�6[   $��5q�6v   ��6�   �� *
ExitEdit+��6�   �� $5 depositWindows��5S     5U     5V     