   M S B A M L   `   `   `  ����E  AClientBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:ClientBank.CommandsClientBank.Commands  5      ;"clr-namespace:ClientBank.MyControlClientBank.MyControl  5      )    #ClientBank.MyControl.DepositControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      1mycom!clr-namespace:ClientBank.Commands    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:ClientBank.MyControl     5      .��5
      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5           Name$  listCreditTab��5   &      
IsReadOnly .    RowDetailsVisibilityMode$ VisibleWhenSelected=�5   E      DataGridRow   RowStyle# [  5        Datagrid#��[ 5        DataGrid_Centering   	CellStyle# [ 5      
�������   AutoGenerateColumns .  $ �� IsSynchronizedWithCurrentItem$
 TrueY�5   P   ��5      ��  �� Path$ AccountsList�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5        DataGridHeader,  &System.Windows.Controls.DataGridColumn	   HeaderStyle#	 [ 6%   
   Header$
 Состояние��6T      MinWidth$ 75Q�6g   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   )   �� *	StateName+  5      #	 [ 6&   $
 Клиент��6T   $ 75Q�6d      SortMemberPath$
 Name��6r    5   )   �� $ 
ClientName�  5      #	 [ 6%   $
 Номер счет��6S   $ 75Q�6g    5   )   �� $ AccountNumber� �� StringFormat$ {0:0000-0000-0000-0000}��  5      #	 [ 6%   $
 Остаток��6S   $ 75Q�6d    5   -   �� $
 Cash�$ {0:0.00}��  5      #	 [ 6%   $
 Вложено��6S   $ 75Q�6d    5   -   �� $ DepositAmount�$ {0:0.00}��  5      #	 [ 6%   $
 Ставка��6S   $ 75Q�6c    5   -   �� $ Ставка {0} %��$
 Rate�4  .System.Windows.Controls.DataGridCheckBoxColumn  5      #	 [ 6)   $ 
 Капитализация��6X   $ 75Q�6o     CheckBoxStyle1   ElementStyle# [ 5    S    6)   �� $ Capitalization�6S     5"      #	 [ 6%   $
 Заработано��6S   $ 75Q�6g    5#   -   �� $ MoneyEarned�$ {0:0.00}��  5$      #	 [ 6%   $
 Дата открытия��6S   $ 75Q�6j    5%   )   �� $ OpenDate�$ {0:dd/MM/yyyy}��  5&      #	 [ 6%   $
 Дата Закрытия��6S   $ 75Q�6j    5'   )   �� $ 	DateClose�$ {0:dd/MM/yyyy}�� � ContextMenu 5+      �� 5,      .@�5-      m� $)��#Перевести средства��6    m� Command$0 *mycom:AccountCommands.TramsferMoneyCommand��6h   y�6;   �� *TransferButtonVisible+ m� CommandParameter 5.   '   �� *PlacementTarget.SelectedItem+ �� RelativeSource ��  �� AncestorType" �� m� Icon 5/      �� 50      $��/Images/ic_transfer.png��6$   52      m� 53      $��Закрыть счет��6   $/ )mycom:AccountCommands.CloseAccountCommand��6_   y�65   �� *CloseButtonVisible+ 54   '   �� *PlacementTarget.SelectedItem+ �� " �� 55      �� 56      $��/Images/close.png��6$   58      59      5;      5<      5=      