   M S B A M L   `   `   `  ����J  FSync-Async_Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Sync_Async_DemoSync_Async_Demo  5   	        Sync_Async_Demo.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Sync_Async_Demo     5   	     � Title$  
MainWindow��5   	   $	��700��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�65   .��5	      � $��20q�6    � RowDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� .+�5      �� $��1��6   $
��10 0q�6%   $��28(�63   Symple Async Demo App5      �� 5      -   5         ��Name$ rbParallelExecution��5      $��0��64   $��Right=�6A   $��Use parallel execution��6]    d� 	IsChecked 6~   �� *IsParallelActive+6�   5      �� 5   
   -   5         ��Name$ executeSync��5      $��1��6&   $��10q�63   $��10q�6?   $��Normal Execute��6L   $��15(�6e   6s   �� 5   
   -   5        $ executeParallelSync��5      $y�	Collapsed=�6.   $��2��6E   $��10q�6R   $��10q�6^   $��Normal Parallel Execute��6k   $��15(�6�   6�   �� 5   
   -   5        $ executeAsync��5      $��3��6'   $��10q�64   $��10q�6@   $��Async Execute��6M   $��15(�6e   6s   �� 5   
   -   5        $ executeParallelAsync��5      $y�	Collapsed=�6/   $��4��6F   $��10q�6S   $��10q�6_   $��Parallel Async Execute��6l   $��15(�6�   6�   �� 5   
   -   5        $ cancelOperation��5      $��5��6*   $��10q�67   $��10q�6C   $��Cancel Operation��6P   $��15(�6k   6y   � 5   
   -   5         �Name$ dashboardProgress��5      $��6��61    � Value$ 0Q�6>   $��10q�6H   $��30��6T   �� 5    
   -   5         ��Name$ resultsWindow��5       $��7��6+   $��
10,10,10,0q�68   6N   5!      5"      