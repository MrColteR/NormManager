   M S B A M L   `   `   `  ����F  BNormManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:NormManager.ConvertersNormManager.Converters  5      0    *NormManager.Views.CreateMeasureValueWindow   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ;
converters$clr-namespace:NormManager.Converters     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$+  %Добавление величины��5      $	��260��5	      $	��254��5
       � WindowStartupLocation$ CenterScreen=�5       � DataContext 5      �� *CreatieMeasureValueViewModel+   ViewModelLocator �� Source# [  .��5      �  � 	Resources 5   
   6   0NormManager.Converters.MeasuredQuantityConverter  MeasuredQuantityConverter��%   &	         5      63   5       � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��40 �6   �� 5      $��20 �6   �� 5      $��30 �6   .+�5   
   �� $��0��5      $
��10,0q�5      $
��Left=�5      $��Center=�5      $(��"Название величины:��5       �� 5!   
   $��1��5"      $��24��5#      $
��10,0q�5$      ��5%      �� *	ValueName+ �� UpdateSourceTrigger$ PropertyChanged=��� 5&   
   $��2��5'      $
��10,0q�5(      $
��Left=�5)      $��Center=�5*      $��Тип величины:��5+      �� 5,   
   $��3��5-      $��24��5.      $
��10,0q�5/      ��50      �� *TypeOfValue+ �� SelectedItem 51      �� *SelectTypeValue+$ PropertyChanged=���52      �� 53      .R�54      �� ��6    ��  �� 	Converter# [ 55      57      �� 58   
   $��4��59      $��24��5:      $
��10,0q�5;      $4��.Изменить столбцы таблицы��5=      ��5<      �� *EditTableColumns+5=      � 5>   
   $��6��6   	 � ColumnDefinitions	 5?      �� 5@      �� 5A      .+�5C      �� $��0��5D      $	��100��5E      $��24��5F      $��10,0,0,0q�5G      $
��Left=�5H      $��Center=�5I      $��Создать��5K      ��5J      �� *CreateMeasureValue+5K      �� 5L      $��1��5M      $	��100��5N      $��24��5O      $��0,0,10,0q�5P      $��Right=�5Q      $��Center=�5R      $��Отмена��5T      ��5S      �� *Close+5T      5U      5V      5W      