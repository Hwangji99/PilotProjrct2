   M S B A M L   `   `   `  ����@  <Proj2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Proj2.ViewModelProj2.ViewModel  5   	       Proj2.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Proj2.ViewModel     5   	     � Title$   재고 관리 프로그램��5   	   $	��450��6   $	��800��6)    � DataContext 5	      #   Proj2.ViewModel.MainViewModel  5
   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��0.8* �6   �� 5      $
��0.3* �6    � RowDefinitions 5   
   �� 5      $
��0.8* �6   �� 5      $
��0.2* �6   .+�5   
   &   System.Windows.Controls.DataGrid     SelectionMode$ Single=�6Y      
IsReadOnly$ False��6p   $��	0,25,0,10q�6�      AutoGenerateColumns$ False��6�   $��Center=�5      ��5      �� *DV_1+ �� SelectedItem 60   �� *SelectedProduct+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 품명��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 160 6v   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ProductName+ �� UpdateSourceTrigger$ PropertyChanged=�6v     5      $	 물품 코드��6%   $
 80 6r    64   �� *Code+$ PropertyChanged=�6r     5      $	 수량��6%   $
 40 6s    61   �� *
Quantity+$ PropertyChanged=�6s     5      $	 설명��6%   $	
 250 6v    61   �� *Explanation+$ PropertyChanged=�6v     5      $	 	브랜드��6%   $
 80 6q    62   �� *Brand+$ PropertyChanged=�6q     5      $	 현재 사용자��6%   $
 80 6v    65   �� *	NowUser+$ PropertyChanged=�6v   5!      �� 5#   
   $��1��6   $��10q�6#   ��5$      �� 5%       �� Orientation$ 
Horizontal=�6   .+�5&      �� $N��HC:/Users/User/Documents/Dev/PilotProjrct2/Proj2/Proj2/Images/picture.png��6   $��16��6n   $��16��6y   �� 5'      $��제품사진��6    $��Center=�6,   $��5q�6G   5(      .��5+      �� $	��296��6@   $	��Top=�6M   $
��Left=�6e   $	��188��6�   ��6   �� *SelectedProductImagePath+6�   5,      �� 5.   
   $��1��6   $
��Left=�6"   $��	26,10,0,0q�6=   $��1��6P    �� TextWrapping$
 Wrap=�6]   $	��Top=�5/   3   $	��122��6K   $��28��6W   ��5.   q   �� *SearchProduct+$ PropertyChanged=�5/   W   �� 50   
   $��1��6   $
��Left=�6!   $��
148,10,0,0q�6<   $��1��6P   $	��Top=�6]   $��28��6u   $��60��51   3   ��6   �� *SearchCommand+��52      �� 53      $ 
Horizontal=�6   $��Center=�66   .+�54      �� $M��GC:/Users/User/Documents/Dev/PilotProjrct2/Proj2/Proj2/Images/search.png��6   $��16��6m   $��16��6x   �� 55      $��조회��6    $��Center=�6*   $��5,0,0,0q�6E   56      58      �� 5:   
   $��1��6   $
��Left=�6!   $��	26,53,0,0q�6<   $��1��6O   $	��Top=�6\   $��25��5;   0   $��57��6<   ��6   �� *
AddCommand+��5<      �� 5=      $ 
Horizontal=�6   $��Center=�66   .+�5>      �� $K��EC:/Users/User/Documents/Dev/PilotProjrct2/Proj2/Proj2/Images/plus.png��6   $��16��6k   $��16��6v   �� 5?      $��추가��6    $��Center=�6*   $��5,0,0,0q�6E   5@      5B      �� 5D   
   $��1��6   $
��Left=�6!   $��	88,53,0,0q�6<   $��1��6O   $	��Top=�6\   $��25��5E   3   $��55��6?   ��6   �� *UpdateCommand+��5F      �� 5G      $ 
Horizontal=�6   $��Center=�66   .+�5H      �� $M��GC:/Users/User/Documents/Dev/PilotProjrct2/Proj2/Proj2/Images/update.png��6   $��16��6m   $��16��6x   �� 5I      $��수정��6    $��Center=�6*   $��5,0,0,0q�6E   5J      5L      �� 5N   
   $��1��6   $
��Left=�6!   $��
148,53,0,0q�6<   $��1��6P   $	��Top=�6]   $��25��5O   0   $��54��6<   ��6   �� *
DelCommand+��5P      �� 5Q      $ 
Horizontal=�6   $��Center=�66   .+�5R      �� $M��GC:/Users/User/Documents/Dev/PilotProjrct2/Proj2/Proj2/Images/delete.png��6   $��16��6m   $��16��6x   �� 5S      $��삭제��6    $��Center=�6*   $��5,0,0,0q�6E   5T      5V      5W      5X      