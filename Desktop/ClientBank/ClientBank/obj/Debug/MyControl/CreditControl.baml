   M S B A M L   `   `   `  ����E  AClientBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:ClientBank.CommandsClientBank.Commands  5      ;"clr-namespace:ClientBank.MyControlClientBank.MyControl  5      (    "ClientBank.MyControl.CreditControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      3mycom!clr-namespace:ClientBank.Commands     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:ClientBank.MyControl     5      .��5
      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5           Name$  listCredits��5   &      
IsReadOnly .    RowDetailsVisibilityMode$ VisibleWhenSelected=�5   E      DataGridRow   RowStyle# [  5        Datagrid#��[ 5        DataGrid_Centering   	CellStyle# [ 5      
�������   AutoGenerateColumns .  $ �� IsSynchronizedWithCurrentItem$
 TrueY�5   P   ��5      ��  �� Path$ AccountsList�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5        DataGridHeader,  &System.Windows.Controls.DataGridColumn	   HeaderStyle#	 [ 6%   
   Header$
 Состояние��6T      MinWidth$ 75Q�6g   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   )   �� *	StateName+  5      #	 [ 6&   $
 Клиент��6T   $ 75Q�6d      SortMemberPath$
 Name��6r    5   )   �� $ 
ClientName�  5      #	 [ 6%   $
 Номер счет��6S   $ 75Q�6g    5   )   �� $ AccountNumber� �� StringFormat$ {0:0000-0000-0000-0000}��  5      #	 [ 6%   $
 Остаток��6S   $ 75Q�6d    5   -   �� $
 Cash�$ {0:0.00}��  5      $
 К погашению��6%   $ 75Q�6:   #	 [ 5   %    5   I   �� $ 
RepayALoan�$ {0:0.00}��5   %     5      #	 [ 6%   $
 Ставка��6S   $ 75Q�6c    5   -   �� $ Ставка {0} %��$
 Rate�  5      $)
 #Ежемесячный платеж��6%   $ 75Q�6A   #	 [ 5    %    5   P   �� $ MonthlyPayment�$ {0:0.00}��5    %     5!      $
 Месяцев��6%   $ 75Q�66   #	 [ 5"   %    5!   E   �� $ MonthsPeriod�5"   %     5#      #	 [ 6%   $
 Дата открытия��6S   $ 75Q�6j    5$   )   �� $ OpenDate�$ {0:dd/MM/yyyy}��  5%      #	 [ 6%   $0
 *Следующая дата платежа��6S   $ 75Q�6s    5&   )   �� $ NextDate�$ {0:dd/MM/yyyy}�� � ContextMenu 5*      �� 5+      .@�5,      m� $)��#Перевести средства��6    m� Command$0 *mycom:AccountCommands.TramsferMoneyCommand��6h   y�6;   �� *TransferButtonVisible+ m� CommandParameter 5-   '   �� *PlacementTarget.SelectedItem+ �� RelativeSource ��  �� AncestorType" �� m� Icon 5.      �� 5/      $��/Images/ic_transfer.png��6$   51      m� 52      $#��Внести средства��6   $2 ,mycom:AccountCommands.RepaymentCommandButton��6d   y�68   �� *MonthlyButtonVisible+ 53   '   �� *PlacementTarget.SelectedItem+ �� " �� 54      �� 55      $��/Images/ic_transfer.png��6$   57      59      5;      5<      5=      