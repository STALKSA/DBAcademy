   M S B A M L   `   `   `  ����J  FStudentsManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:StudentsManagerStudentsManager  5   	        StudentsManager.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:StudentsManager     5   	   -   5          � Title$  
MainWindow��5   	   $	��920��6   $
��1326��6)   .��5
      �  J� RenderTransformOrigin$ 0.501,0.574&�6   $��10,28,46,169q�6/    � ColumnDefinitions 5   
   �� 5      $
��677* �6   �� 5      $
��204* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ visitsDataGrid��5      $��10,582,42,34q�6,   $��2��5   �     5   
   -   5        $ subjectsDataGrid��5      $��650,71,26,0q�6.   $��2��6C   $	��190��6W   $	��Top=�6d   5      �� 5   
   $��Предметы��6   $
��Left=�6#   $��
650,39,0,0q�6>   $	��Top=�6R   $	��121��6j   $��18(�6v   �� 5   
   $��Посещения��6   $
��Left=�6$   $��
10,533,0,0q�6?   $	��Top=�6S   $	��124��6k   $��18(�6w   $ 0.543,2.853&�6�     5   
   -   5        $ groupsDataGrid��5      $��645,346,0,228q�6+   $��2��6B   5      �� 5   
   $��Группы��6   $
��Left=�6!   $��645,304,0,0q�6<   $	��Top=�6Q   $	��100��6i   $��18(�6u   �� 5   
   $��Студенты��6   $��2,90,357,205q�6%   $��16(�6;   .��5      ]� -   5         ]�Name$ loadingStackPanel��5      $��Stretch=�5      $	��Top=�5      .+�5       �� .+�5!        -   5        $ studentsDataGrid��5!      $	��171��69   $	��NaN��6F      AutoGenerateColumns$ False��5$         CanUserAddRows$ False��5%         Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn   Header$ Имя��5(   %   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 70 5)   %   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 5'   1   �� *Name+5)   %     5*      $ День рождения��5+   %   $		 130 5,   %   
 5*   1   �� *
Birthdat+5,   %     5-      $ Мэил��5.   %   $		 150 5/   %   
 5-   1   �� *Email+5/   %     50      $  Группа��51   %   $		 100 52   %      
IsReadOnly$
 True��53   %   
 50   1   �� *Group+53   %     54      $$ Кол-во посещений��55   %   $		 200 56   %   $
 True��57   %   
 54   1   �� *
VisitCount+57   %   5:      � 5;       � Orientation$ 
Horizontal=�6    .+�5<      ��  �� Orientation$ 
Horizontal=�6%   .+�5=      �� $��16(�6$   .��62   Имя67   �� 5>      -   5         ��Name$ studentNameTextBox��5>   &   $	��7,3q�5?       �� TextWrapping$
 Wrap=�5@      $�� ��61   $	��120��69   5B      �� 5C      $ 
Horizontal=�6%   .+�5D      �� $��16(�6$   .��62   Дата рождения6A   (  "System.Windows.Controls.DatePicker  5E      -   5        $��studentBirthdayDatePicker��5E   )   $��16(�6J   $	��110��6X   6f   5F      �� 5G      $ 
Horizontal=�6%   .+�5H      �� $��16(�6$   .��62   Email69   �� 5I      -	   5        $ studentEmailTextBox��5I   &   $	��7,3q�5J      $
 Wrap=�5K      $�� ��61   $	��120��69   5M      �� 5N      $ 
Horizontal=�6%   .+�5O      �� $��16(�6$   $��7,3, 4,4q�62   .��6D   Группа6L   �� 5P      -
   5         ��Name$ studentGroupComboBox��5P   '   $��7,3,4,4q�5Q   '   $	��100��5R   '   $��16(�5S   '    �� 
IsEditable$ False��5T   '   5W      5X      5Y      � 5Z       5[      �� 5\      �� 5]      .+�5_      �� -   5         ��Name$ studentsSaveChangesButton��5_   !   $��0��6D   $+��%Сохранить изменения��5`      $��40��5a      $��0,0,4,0q�6)   6:   �� 5c      -   5        $��1��5c   !   $(��"Добавить как новый��5d      $��40��5e      $��Stretch=�5f      5g      5j      5k      5m      5n      �� 5o   
   -   5        $ searchTextBox��5o      $
��Left=�5p      $��	10,26,0,0q�5q      $
 Wrap=�5r      $��Поиск...��5s      
�������$	��Top=�5v      $	��593��5w      $��45��6   $��18(�5x      6    5z      5{      