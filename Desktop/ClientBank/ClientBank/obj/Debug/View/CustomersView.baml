   M S B A M L   `   `   `  ����E  AClientBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ClientBank.ViewClientBank.View  5   	   #    ClientBank.View.CustomersView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ClientBank.View     5   	     � Title$  Клиенты��5   	   $	��450��6   $	��900��6&   $	��700��62    � Icon$ /Images/manager.png��6A   .��5	      �  � ColumnDefinitions 5
   
   �� 5   
   $��* �6   �� 5   
   $
��Auto �6   .+�5      � $��0��6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ listCustomers��5      $ �� IsSynchronizedWithCurrentItem$
 TrueY�6Q   $��0q�6v   $��0q�6�   $��Stretch=�6�      
IsReadOnly .    AutoGenerateColumns .     DataGridRow   RowStyle# [  5        Datagrid#��[ 5      ��5   *   �� *CustomersItems+	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5        DataGridHeader,  &System.Windows.Controls.DataGridColumn
   HeaderStyle#
 [ 6&      Header$ Статус��6T      MinWidth$ 75Q�6d      SortMemberPath$ Surname��6r   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   )   ��  �� Path$ 
StatusName�  5      #
 [ 6&   $ Клиент��6T   $ 75Q�6d   $
 Name��6r    5   )   �� $ 
ClientName�  5       #
 [ 6&   $ 
Город��6T   $ 75Q�6c   $ Surname��6q    5!   )   �� $ CityName�5%      �� 5&   
    �� Orientation$ 
Horizontal=�6   $��1��6.   .+�5'      ��   ButtonTemplate#��[ 6   $��35��6B   $��35��6M   ��6Y   �� *FirstCommand+��5(      ��  �� ElementName$ listCustomers��$ SelectedItem�{�5)      �� *EnabledPrevious+.��5*      �� $��/Images/ic_first.png��6   5,      �� 5-      #��[ 6   $��35��6@   $��35��6K   ��6W   �� *PreviousCommand+��5.      �� $ listCustomers��$ SelectedItem�{�5/      �� *EnabledPrevious+.��50      �� $��/Images/ic_left.png��6   51      �� 52        	TextBlock#��[ 6^   ��6   �� *EmploeePosition+ �� UpdateSourceTrigger$ PropertyChanged=�6^   �� 53      #��[ 6   $��35��6@   $��35��6K   ��6W   �� *NextCommand+{�54      �� *EnabledNext+��55      �� $ listCustomers��$ SelectedItem�.��56      �� $��/Images/ic_right.png��6   57      �� 58      #��[ 6   $��35��6@   $��35��6K   ��6W   �� *LastCommand+��59      �� $ listCustomers��$ SelectedItem�{�5:      �� *EnabledNext+.��5;      �� $��/Images/ic_last.png��6   5<      �� 5=       �� 	IsDefault . #��[ 6&   $��35��6Q   $��35��6\   ��6h   �� *	AddItem+��5>      �� $ listCustomers��$ SelectedItem�.��5?      �� $��/Images/ic_add.png��6   5@      �� 5A      #��[ 6   $��35��6@   $��35��6K   ��6W   �� *
EditItem+��5B      �� $ listCustomers��$ SelectedItem�.��5C      �� $��/Images/ic_edit.png��6   5D      �� 5E      #��[ 6   $��35��6@   $��35��6K   ��6X   �� *
RemoveItem+��5F      �� $ listCustomers��$ SelectedItem�.��5G      �� $��/Images/ic_delete.png��6   5H      5I      5J      � 5K      $��1��6   $��5q�6   y�6(   �� *GetVisibility+ 5L   
   �� 5M      $
��Auto �6   �� 5N      $
��Auto �6    5P   
   �� 5Q      $
��Auto �6   �� 5R      $
��Auto �6   �� 5S      $
��Auto �6   �� 5T      $
��Auto �6   �� 5U      $
��Auto �6   �� 5V      $
��Auto �6   �� 5W      $��* �6   .+�5Y      �� $��0��6   #��[ 6%   Статус:6Q   �� 5Z      -   5         ��Name$ status_select��5Z        ComboBox#��[ 6R   $��0��5[      $��1��6!    �� DisplayMemberPath$ Status��62    �� SelectedValuePath$ Id��5\      {�5Z   .   �� *StatusEnabled+��6t   �� $ 
StatusList� �� SelectedValue 5\   *   �� $ StatusId�$ PropertyChanged=� �� ValidatesOnDataErrors .  �� NotifyOnValidationError . �� 5]   
   $��1��69   #��[ 6F   y�6   �� *VisibleClient+	Имя:6o   �� 5^      -   5         ��Name$ txtName��5^   $   $��1��6     TextBox#��[ 65   $��1��6V   $	��200��6f   .��6r   ��5_      �� 5`      $
 Name�6    . $ PropertyChanged=�6I    �� ValidationRules 5a      5  /System.Windows.Controls.DataErrorValidationRule  5b      5d      .��5e       5f      �� 5g   
   $��1��6:   #��[ 6G   y�6   �� *VisibleBusines+Компания:6u   �� 5i      $��2��6:   #��[ 6G   y�6   �� *VisibleClient+Фамилия:6t   �� 5j      -   5        $ 
txtSurname��5j      #��[ 6+   $��1��6L   $��2��6\   $	��200��6i   .��6u   ��5k      �� 5l      $ Surname�6    . $ PropertyChanged=�6L    5m        5n      5p      .��5q       5r      �� 5s   	   $��2��69   #��[ 6F   y�6   �� *VisibleBusines+0.Должность представителя:6�   �� 5u      -   5        $ txtFIO��5u   >   #��[ 6N   $��1��6o   $��3��6   $	��200��6�   y�6   �� *VisibleBusines+.��6�   ��5v      �� 5w      $	 FIO�6    . $ PropertyChanged=�6H    5x        5y      5{      .��5|       5}      �� 5~      $��3��6>   #��[ 6K   y�6   �� *VisibleBusines+$"ФИО представителя:6�   �� 5�      $��3��6=   $��Дата рождения��6J   #��[ 6_   y�6   �� *VisibleClient+6�   �� 5�      -   5        $ dtDateOfBirthday��5�      #��[ 6W   $��1��6x   $��3��6�   $	��200��6�   y�60   �� *VisibleClient+.��6�   ��5�      �� 5�      $ DateBirthday�6    �� StringFormat$ {0:дд.мм.гггг}��62    . $ PropertyChanged=�6q    5�        5�      5�      .��5�       5�      �� 5�      -   5        $ txtINN��5�      #��[ 6N   $��1��6o   $��4��6   $	��200��6�   y�6'   �� *VisibleBusines+.��6�   ��5�      �� 5�      $	 INN�6    . $ PropertyChanged=�6H    5�        5�      5�      .��5�       5�      �� 5�      $��4��6>   #��[ 6K   y�6   �� *VisibleBusines+ИНН Компании:6}   �� 5�      $��5��6   #��[ 6%   Город:6P   �� 5�   
   -   5        $ cmdCity��5�      #��[ 6%   $��5��5�      $��1��6!   $
 City��62   $ Id��5�      ��5�   G   �� $ 
CitiesList� 5�   *   �� $ CityId�$ PropertyChanged=� .  . �� 5�      $��4��6@   $��VIP Статус��6M   #��[ 6_   y�6   �� *VIPClientVisible+6�   d� 5�      $��7,0,0,0q�6   $
��Left=�6,   $��4��6o   $��1��6|     AnimatedSwitch#��[ 6�   y�6G   �� *VIPClientVisible+��5�      �� *VIPChangedCommand+ d� 	IsChecked 6A   �� *	VipStatus+�� 5�      $ 
Horizontal=�6   $��9��62   $��2��6?   .+�5�      �� $��Сохранить��6   #��[ 6-   $��Bottom=�6y   $��5q�6�   {�6X   �� *
SaveButton+��6�   �� *
SaveItem+�� 5�      $��Отмена��6   #��[ 6*   $��Bottom=�6U   $��5q�6p   ��6|   �� *
ExitEdit+5�      5�      5�      5�      