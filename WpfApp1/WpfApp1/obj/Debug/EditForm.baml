   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.EditForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  EditForm��5   	   $	��520��6   $	��425��6'    � WindowStyle$
 None=�63    � WindowStartupLocation$ CenterScreen=�6F   .��5	      � ���	#FF1B1717 � RowDefinitions 5
   
   �� 5      .+�5   
   �� -   5         ��Name$ ChoosedField��5      $
��Left=�6)   $	��Top=�6D   $	��217��6\   $��
92,172,0,0q�6h   ���	#FF171414
���   �.@�5      �� -   5         ��Name$
 Name��5      $
��Name��6)   �� 5      -   5        $ Price��5      $��Price��6*   ��6:   Z� �� 5      -   5        $
 Year��5      $
��Year��6)   �� 5      -   5        $ Author��5      $��Author��6+   �� 5      -   5        $ AuthorProfile��5      $��AuthorProfile��62   �� 5      -   5        $ BookDescription��5      $��BookDescription��64   5      �� 5   
   $
��Left=�6   $
��Wrap=�6/   $%��name of book you want to change��6C   $	��Top=�6j   $��	44,28,0,0q�6�   $��30��6�   $	��326��6�   ���	#FFC4D334$��20(�6�   �� 5   
   -   5         ��Name$ NameOfSearchedBook��5      $
��Left=�6.   $��33��6I    �� TextWrapping$
 Wrap=�6U   $	��Top=�6i   $	��302��6�   $��	44,63,0,0q�6�   ���	#FFC4D334���	#FF242527��6�   Z� 6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $��Field you want to change��6C   $	��Top=�6c   $��
44,132,0,0q�6{   $��35��6�   $	��302��6�   ���	#FFC4D334$��20(�6�    �� Padding$ 45,0,0,0q�6�   �� 5   
   -	   5        $ EditText��5      $
��Left=�6$   $	��148��6?   $
 Wrap=�6L   $	��Top=�6`   $	��302��6x   $��
44,282,0,0q�6�   ���	#FF242527���	#FFC4D334��6�   Z� 6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $
��Edit��6C   $	��Top=�6O   $��
92,234,0,0q�6g   $	��217��6{   $��32��6�   ���	#FFC4D334$��20(�6�   $ 90,0,0,0q�6�   �� 5   
   -
   5        $��Submit Changes��5   &   $
��Left=�6?   $	��Top=�6Z   $	��118��6r   $��
92,482,0,0q�6~   ���	#FFC4D334���	#FF252222��6�   Z� 6�   �� 5   
   -   5        $
��Back��5      $
��Left=�6-   $	��Top=�6H   $��75��6`   $��
10,482,0,0q�6k   ���	#FFC4D334���	#FF292525��6   Z� 6�   5      5      