   M S B A M L   `   `   `  ����E  AClientBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ClientBank.ViewClientBank.View  5   	   F BAccountData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullT9clr-namespace:AccountData.Validation;assembly=AccountDataAccountData.Validation 5   	   $    ClientBank.View.AddAccountView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ClientBank.View     5   	   Irul9clr-namespace:AccountData.Validation;assembly=AccountData     5   	   -   5          �Name$  	accWindow��5   	    � Title$! Открытие счета��5
   	   $	��450��6    $	��400��6-   $	��500��6=   $	��600��6L    � Icon$ /Images/unnamed.png��6X   .��5      � -   5         �Name$ 
accounList��5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � ���	#FF00D1FF$��0��6&    � ColumnDefinitions 5      �� 5      $	��40* �6#   �� 5      $	��60* �6#    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��Статус��6   $��Right=�6&      TextBlockDetail#��[  6B   �� 5      $��1��66   #��[  6F   ��6   �� *StatusClient+6F   �� 5      $��1��6   $��Клиент��6%   $��Right=�63   #��[  6O   �� 5      $��1��64   $��1��6A   #��[  6Q   ��6   �� *
ClientName+6Q   �� 5      $��2��6   $��Дата открытия��6%   $��Right=�6:   #��[  6V   �� 5      $��2��6   #��[  6l   $��1��6�   ��6%   �� *PersonalItem.OpenDate+ �� StringFormat$ {0:dd/MM/yyyy}��6�   5       � 5!   
   $��1��6    5"      �� 5#      $
��Auto �6    �� 5$      $
��Auto �6    �� 5%      $
��Auto �6    �� 5&      $��* �6     5(      �� 5)      $
��Auto �6#   �� 5*      $��* �6#   .+�5,      �� $��0��6     	TextBlock#��[ 6%   $"Внесение наличных:6\   �� 5-      -   5         ��Name$ txtCash��5-        TextBox#��[ 6)   $��1��6J   $��0��6Z   .��6i   ��5.      �� 5/       �� Path$ PersonalItem.Cash�6   	 �� NotifyOnValidationError	 . 
 �� UpdateSourceTrigger$
 PropertyChanged=�6V    �� ValidationRules 50      &   AccountData.Validation.MoneyRule  51      
   Min$ 0~�6,   5  /System.Windows.Controls.ExceptionValidationRule  52      54      .��55       57      �� 59      $��3��6   $��1��6"   $��Сохранить��6�     MessagesButton#��[ 6�   $��Bottom=�6�   $��5q�6�   {�62   ��  �� ElementName$ txtCash��$A ;(Validation.Errors)[0].RuleInError.ValidatesOnTargetUpdated���6  �� *
AddAccount+��6"  �� $ 	accWindow���� 5:      $��3��6   $��Отмена��6"   #��[ 63   $��Bottom=�6[   $��5q�6v   ��6�   �� *
ExitEdit+��6�   �� $ 	accWindow��5<      5>      5?      