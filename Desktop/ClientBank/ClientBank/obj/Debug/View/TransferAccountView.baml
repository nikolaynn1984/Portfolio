   M S B A M L   `   `   `  ����E  AClientBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ClientBank.ViewClientBank.View  5   	   F BAccountData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullT9clr-namespace:AccountData.Validation;assembly=AccountDataAccountData.Validation 5   	   )    #ClientBank.View.TransferAccountView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ClientBank.View     5   	   Kvalid9clr-namespace:AccountData.Validation;assembly=AccountData     5   	   -   5          �Name$  transWindow��5	   	    � Title$# Перевод средств��5
   	   $	��450��6!   $	��500��6.   $	��450��6:   $	��500��6J    � Icon$ /Images/ic_transfer.png��6Y    � 	Resources 5       �� 
TargetType �� Triggers P� Property P� Value �� Property	 �� Value
 �� RelativeSource �� Path��%�   '��       �� 5   
   " ��6    5      P� 5       �@=�HasError . . �5      ��  �@�ToolTip	 60   �� 
 �� *Self+$) #(Validation.Errors)[0].ErrorContent�5      5      5      .��5      � -   5         �Name$ gridTransfer��5      $��15q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $��* �6   .+�5"   
   �� $��Клиент:��6      TextBlockTransfer#��[  6#   �� 5#   
   $��1��6   $
��Left=�6Q     	TextBlock#��[ 6l   ��6$   �� $ GetAccounts.ClientName�6l   �� 5%   
   $��1��6   $��Остаток:��6"   #��[  62   �� 5&   
   $��1��6   $��1��6"   #��[ 6V   $
��Left=�6y   ��62   �� $ AccountMoney�6y   �� 5'   
   $��2��6   $ ��Счет списания:��6"   #��[  68   �� 5(   
   $��2��6   $��1��6"   $
��Left=�6�   #��[ 6�   ��62   �� $ GetAccounts.AccountNumber� �� StringFormat$ {0:0000-0000-0000-0000}��6�   �� 5)   
   $��3��6   $$��Счет получателя:��6"   #��[  6:   �� 5*   
   -   5         ��Name$ selectedAccountTo��5*        ComboBox#��[ 6.   $��3��5+      $��1��6!    �� SelectedValuePath$ Id��61    J� SnapsToDevicePixels . ��5*   P   �� $ GetListAccounts� �� SelectedValue 5,      �� $ SelectedAccount� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors .  �� NotifyOnValidationError . ��5-      �� 5.      .R�5/      �  50      �� 51      $��* �6/   �� 52      $��* �6/    54      �� 55      $
��Auto �6,   �� 56      $
��Auto �6,   .+�58      �� ��6$   �� *
ClientName+�� 59      $��1��6$   ��64   �� *ClientSurname+�� 5:      $��1��6$   $��2��61   ��6E   �� *AccountNumber+$ {0:0000-0000-0000-0000}��5;      5<      5>      �� 5?   
   $��4��6   $"��Сумма перевода:��6"   #��[  69   �� 5@   
   -   5         ��Name$ txtCashTransfer��5@      $��4��6+   $��1��68     TextBox#��[ 6H   .��6j   ��5A      �� 5B      $ Money�6    . $ PropertyChanged=�6F    �� ValidationRules 5C      &   AccountData.Validation.MoneyRule  5D      
   Min$ 1~�6*   5  /System.Windows.Controls.ExceptionValidationRule  5E      5G      .��5H      5I      �� 5J   
   $�� 5��6   $��2��6"   
���  ��  TextBlockDetail#��[ 6d   ��66   �� *MessageText+6d   �� 5K   
   $��Отмена��6   $��6��6"   $��Bottom=�6/     ButtonTemplate#��[ 6J   $��10q�6u   $��18(�6�   ��6�   �� *Exit+��6�   ��  �� ElementName$ transWindow���� 5L   
   $��Перевести��6   $��1��6%   $��Bottom=�65   $��6��6P   #��[ 6�   $��10q�6�   $��18(�6	  {�6]   �� $ txtCashTransfer��$A ;(Validation.Errors)[0].RuleInError.ValidatesOnTargetUpdated���6  �� *TransferMoney+��69  �� $ transWindow��5O      5P      