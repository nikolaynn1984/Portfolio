   M S B A M L   `   `   `  ����E  AClientBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ClientBank.ViewClientBank.View  5   	   F BAccountData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullT9clr-namespace:AccountData.Validation;assembly=AccountDataAccountData.Validation 5   	   #    ClientBank.View.ReplenishView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ClientBank.View     5   	   Irul9clr-namespace:AccountData.Validation;assembly=AccountData     5   	   -   5          �Name$  	repWindow��5	   	    � Title$% Пополнение счета��5
   	   $	��250��6"   $	��400��6/   $	��250��6;   $	��400��6K    � Icon$ /Images/ic_add.png��6\   .��5      �� $��10q�6   .+�5   
   ��    	TextBlock#��[  6D   $
��Left=�6g   $��15(�6�   ��6   ��  �� Path$ AccountsItemId.ClientName�6�   �� 5   
   #��[  6G   $
��Left=�6j   $��15(�6�   ��6   �� $" AccountsItemId.ClientSurname�6�   �� 5   
   #��[  6o   $
��Left=�6�   $��15(�6�   ��6   �� $" AccountsItemId.AccountNumber� �� StringFormat$ {0:0000-0000-0000-0000}��6�   �� 5   
   $%��Сумма пополнения��6   #��[  6,   $��Center=�6O   $��5q�6l   $��Oblique$�6w   �� 5   
   -   5         ��Name$ txtCashReplenish��5        TextBox#��[ 6,   .��6N   ��5      �� 5      $ Money�6    �� NotifyOnValidationError .  �� UpdateSourceTrigger$ PropertyChanged=�6F    �� ValidationRules 5      &   AccountData.Validation.MoneyRule  5      
	   Min$	 1~�6(   5  /System.Windows.Controls.ExceptionValidationRule  5      5      .��5      5      �� 5   
   $��Пополнить��6�     ButtonTemplate#��[ 6�   $��5q�6�   {�6   �� 
 �� ElementName$
 txtCashReplenish��$A ;(Validation.Errors)[0].RuleInError.ValidatesOnTargetUpdated���6�   �� *ReplenishButton+��6�   �� $
 	repWindow��5      5      