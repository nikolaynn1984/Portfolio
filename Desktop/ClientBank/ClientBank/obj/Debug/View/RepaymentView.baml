   M S B A M L   `   `   `  ����E  AClientBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ClientBank.ViewClientBank.View  5   	   F BAccountData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullT9clr-namespace:AccountData.Validation;assembly=AccountDataAccountData.Validation 5   	   #    ClientBank.View.RepaymentView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ClientBank.View     5   	   Irul9clr-namespace:AccountData.Validation;assembly=AccountData     5   	   -   5          �Name$  repayWindow��5	   	    � Title$' !Погашение кредита��5
   	   $	��460��6#   $	��460��60   $	��900��6@   $	��900��6O    � Icon$ /Images/ic_add.png��6[   
�������.��5      �  � ColumnDefinitions 5   
   �� 5      $	��350 �6   �� 5      $��* �6   .+�5   
   �  5      �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $	��5,2q�6   
������� �� CornerRadius$ 5��65   $��0��6F   $��2��6S   ._�5      � .+�5      ��    TextBlockDetail#��[  6I   ��6    �� *GetAccounts.ClientName+6I   5       J� Effect 5       3  -System.Windows.Media.Effects.DropShadowEffect  5!         ShadowDepth$ 0Q�6'   69   5#      �� 5$      $	��5,2q�6   
�������$ 5��65   $��1��6F   $��2��6S   ._�5%      �  5&      �� 5'      $	��55* �6+   �� 5(      $	��45* �6+    5*      �� 5+      $
��Auto �6(   �� 5,      $
��Auto �6(   �� 5-      $
��Auto �6(   .+�5/      �� $��Остаток��6    $��0��6/     	TextBlock#��[ 6<   �� 50      $��Сумма кредита��6    $��1��65   #��[ 6B   �� 51      $��Ставка��6    $��2��6.   #��[ 6;   �� 54      $��1��6[   $��0��6k   #��[  6x   ��6    �� *CreditsItem.Cash+ �� StringFormat$ {0:0.00}��6x   �� 55      $��1��6b   $��1��6r   #��[  6   ��6    �� *CreditsItem.AmountIssue+$ {0:0.00}��6   �� 56      	 �� Orientation$	 
Horizontal=�6!   $��1��6:   $��2��6J   $��Center=�6W   .+�57      �� #��[  6G   ��6$   �� *CreditsItem.Rate+6G   �� 58      $��%��6$   #��[ 6-   59      5;       5<        5=      $ 0Q�6'   69   5?      �� 5@      $	��5,2q�6   $��2��6"   
�������$ 5��6B   $��2��6S   ._�5A      �  5B      �� 5C      $	��55* �6+   �� 5D      $	��45* �6+    5F      �� 5G      $
��Auto �6(   �� 5H      $
��Auto �6(   �� 5I      $
��Auto �6(   �� 5J      $
��Auto �6(   .+�5L      �� $'��!Осталось погасить��6    $��0��69   #��[ 6F   �� 5M      $)��#Ежемесячный платеж��6    $��1��6:   #��[ 6G   �� 5N      $��Выдано��6    $��2��6.   #��[ 6;   �� 5O      $
��До��6    $��3��6*   #��[ 67   �� 5Q      $��0��6a   $��1��6n   #��[  6~   ��6    �� *CreditsItem.RepayALoan+$ {0:0.00}��6~   �� 5R      $��1��6e   $��1��6r   #��[  6�   ��6    �� *CreditsItem.MonthlyPayment+$ {0:0.00}��6�   �� 5S      $��2��6e   $��1��6r   #��[  6�   ��6    �� *CreditsItem.OpenDate+$ {0:dd/MM/yyyy}��6�   �� 5T      $��3��6_   $��1��6l   #��[  6|   ��6    �� 
 �� Path$
 	DateClose�$ {0:dd/MM/yyyy}��6|   5U       5V        5W      $ 0Q�6'   69   5Y      � 5Z      $��3��6   $��2��6    $��5,10,5,5q�64    5[      �� 5\      $	��55* �6'   �� 5]      $	��45* �6'    5_      �� 5`      $
��Auto �6$   �� 5a      $
��Auto �6$   �� 5b      $
��Auto �6$   .+�5d      �� $)��#Погасить полностью��6   $��0��66   $��0��6D   #��[ 6T   �� 5e      $��0��6   $��1��6*   $��Stretch=�6:   .+�5f      �� $��25��6!     AnimatedSwitch#��[ 6.    d� 	IsChecked 6V   �� $
 	FullRepay�5g      �� 5h      -   5         ��Name$ txtCash��5h      $��1��6,   $��2��69   
�������$��Stretch=�6`   $��30��6~     TextBox#��[ 6�   .��6�   ��5i      �� 5j      $
 Money�6"    �� NotifyOnValidationError .  �� UpdateSourceTrigger$ PropertyChanged=�6N   $ {0:0.00}��6t    �� ValidationRules 5k      &   AccountData.Validation.MoneyRule  5l   "   
   Min$ 1~�60   5n      .��5o   $   5p      5q      �� 5r      $��Отмена��6   $��4��6&   $��Bottom=�63     ButtonTemplate#��[ 6N   $��5q�6y   $��18(�6�   ��6�   �� *ExitCommand+��6�   ��  �� ElementName$ repayWindow���� 5s      $��Перевести��6   $��1��6)   $��Bottom=�69   $��4��6T   #��[ 6�   $��5q�6�   $��18(�6  {�6a   �� $ txtCash��$A
 ;(Validation.Errors)[0].RuleInError.ValidatesOnTargetUpdated���6  �� *RepaymentMoneyCommand+��6<  �� $ repayWindow��5t      � 5u   
   $��1��6    5v      �� 5w      $
��Auto �6    �� 5x      $��* �6    .+�5z      �� $	��5,2q�6   
�������$��0��66   ._�5{      � .+�5}      �� $#��График платежей��6    #��[  68   5~      5      &   System.Windows.Controls.DataGrid  5�      -   5          Name$ listRepayment��5�   *      
IsReadOnly . $��1��6A   $��2q�6N   $��0q�6Y     DataGridRow   RowStyle# [ 5�         AlternatingRowBackground
 �����  Datagrid#��[ 5�      
�������   AutoGenerateColumns .  $ �� IsSynchronizedWithCurrentItem$
 TrueY�5�   P   ��5�      �� *GetRepayments+   Columns 5�      4  .System.Windows.Controls.DataGridCheckBoxColumn  5�        DataGridHeader,  &System.Windows.Controls.DataGridColumn   HeaderStyle# [ 6-      Header$ Состояние��6\      MinWidth$ 75Q�6o     CheckBoxStyle11  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 5�   I      Binding 6)   �� *	Performed+6I   0  *System.Windows.Controls.DataGridTextColumn  5�      $ Дата платежа��6)   $ 75Q�6?   # [ 6�    6N   �� *PlanningDate+$ {0:dd/MM/yyyy}��6�     5�      # [ 6*   $! Дата погашения��6X   $ 75Q�6p      SortMemberPath$
 Name��6~    5�   )   �� *RepaymentDate+$ {0:dd/MM/yyyy}��  5�      # [ 6)   $ Сумма платежа��6W   $ 75Q�6n    5�   )   �� *Amount+$ {0:f}��5�      5�      5�      5�      