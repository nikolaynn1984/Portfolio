   M S B A M L   `   `   `  ����E  AClientBank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ClientBank.ViewClientBank.View  5   	   !    ClientBank.View.MessageView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ClientBank.View     5   	   -   5          �Name$  	mesWindow��5	   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$' !Выберите действие��5
   	   $	��200��6#   $	��400��60    � AllowsTransparency .  � WindowStyle$
 None=�6V   ��6j   Z� .��5      � .+�5   
   � -   5         �Name$ gridBody��5      .+�5      �� 
������� �� CornerRadius$ 20��6(   $	��0.6q�6:   ���	#FF9393936P   � 5      $��5,35,5,0q�6    � RowDefinitions 5      �� 5      $��* �6$   �� 5      $
��Auto �6$   .+�5      �� $��0��6   $��5q�6*    �� Orientation$ 
Horizontal=�65   .+�5      �� $��Клиент: ��6    $��16(�60   �� 5      $��16(�6!   $
��Bold"�6/   ��6B   �� *
ClientName+�� 5      -   5        	 ��Name$	 txtSelected��5       $y�Hidden=�6O   ��65   �� *
Selected+6O   5      �� 5      $��1��6   $ Vertical=�6*   $��0,0,0,10q�6A   .+�5      �� -   5        
 ��Name$
 
btnAccount��5         MessagesButton#��[  61   $��Открыть счет��6�   ��6Y   �� *
AddAccount+��6x   ��  �� ElementName$ 	mesWindow��6�   �� 5      -   5        $
 
btnDeposit��5      #��[  6U   $��Открыть вклад��6�    �� 	ClickMode$ Press=�6�   {�61   �� *ButtonEnabled+��6}   �� *
AddDeposit+��6�   �� $ 	mesWindow��6�   �� 5      -   5        $
 	btnCredit��5      #��[  60   $��Выдать кредит��6�   ��6X   �� *	AddCredit+��6v   �� $ 	mesWindow��6�   5      5      5      � 5    
   -   5        $ gridBar��5        � Cursor$
 Hand��6   $��30��6.   $	��Top=�6:   .+�5!      �� ���	#FFE8E8E8$ 	20,20,0,0��6,   �� 5"      $'��!Выберите действие��6   $��10,5,5,5q�61   $��16(�6C   �� 5#      -   5        $
 btnClose��5#      $��Х��6'   $��Right=�64     MessagesCloseButton#��[ 6P   5$      5(      5)      