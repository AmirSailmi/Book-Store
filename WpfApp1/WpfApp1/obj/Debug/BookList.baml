   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.BookList   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  BookList��5   	   $	��450��6   $	��800��6'    � WindowStyle$
 None=�63   ���	#FF1B1717.��5	      �  � RowDefinitions 5
   
   �� 5      .+�5   
   �� -   5        $
��show��5      $
��Left=�6    $	��Top=�6;   $��75��6S   $��707,412,0,0q�6^   6s   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DataGrid��5      $
��Left=�6%   $	��383��6@   $	��Top=�6M   $	��738��6e   $��	28,10,0,0q�6q   
���   ���6�   Z�    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        BookName��,  &System.Windows.Controls.DataGridColumn   Header$
 Name��5   7   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   �� *
BookName+  5      -   5        
AuthorName��$ Author��5   :    6J   �� *
AuthorName+  5      -   5        	Price��$ Price��5   4    6C   �� *Price+  5      -   5        VIP��$	 VIP��5   2    6?   �� *IsVIP+  5      -   5        Year��$
 Year��5   3    6A   �� *Year+  5      -   5        	Point��$ Point��5   4    6C   �� *Point+  5      -	   5        VIPSubscriptionMonthlyFee��$$ VIP subscription fee (monthly)��5   H    6p   �� *VIPsubscriptionmonthlyfee+  5      -
   5        Discount��$ Discount��5   7    6I   �� *
Discount+  5      -   5        TimeForDiscount��$ Time for Discount��5   >    6Y   �� *TimeforDiscount+5      �� 5   
   -   5        $
��back��5      $
��Left=�6    $	��Top=�6;   $��75��6S   $��632,412,0,0q�6^   6s   5      5      