   M S B A M L   `   `   `  ����E  AClientBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ClientBank.ViewClientBank.View  5   	   !    ClientBank.View.HistoryView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ClientBank.View     5   	     � Title$%  История операций��5   	   $	��800��6"   $	��600��6/   $	��800��6;   $	��600��6K    � Icon$ /Images/ic_history.png��6Z   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ listAccount��5
   &      
IsReadOnly .    DataGridRow   RowStyle# [  5        Datagrid#��[ 5      
�������   AutoGenerateColumns .  $��1��5   P   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6]   ��5      �� *OperationsList+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5        DataGridHeader,  &System.Windows.Controls.DataGridColumn   HeaderStyle# [ 6%   	   Header$	 Операция��6u   
   MinWidth$
 75Q�6�      
IsReadOnly 6S   ��  �� Path$ State�1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   )   �� *OperationName+  5      # [ 6%   $	 Номер счет��6S   $
 75Q�6g    5   )   �� $ AccountNumber� �� StringFormat$ {0:0000-0000-0000-0000}��  5      # [ 6%   $	 
Сумма��6S   $
 75Q�6b    5   -   �� $ Money�$ {0:0.00}��  5      # [ 6%   $	 Дата��6S   $
 75Q�6a    5   )   �� $ OperationDate�$ {0:dd/MM/yyyy}��5      5      5      