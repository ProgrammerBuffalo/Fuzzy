   M S B A M L   `   `   `  ����E  AFuzzyLogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:FuzzyLogic
FuzzyLogic  5   	       FuzzyLogic.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:FuzzyLogic     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � 
������� � RowDefinitions 5
   
   �� 5      $��1* �6   �� 5      $��4* �6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��2* �6   .+�5   
   �� -   5         ��Name$ btn_make��5      $��20, 10q�6#   $��18(�5      $��	Calculate��6   5      �� 5   
   $	��525��6   $	��325��6    $��1��60   $��2��6=   $��Center=�6Q   $��Center=�6n    J� RenderTransformOrigin$ 0.5,0.5&�6�   ._�5      �� $��1��6   
����  �$ 0.5,0.5&�6/   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$	 180Q�6*   T� 5      5       5"      ��5$      �� 5%       �� ImageSource$ BackViewPlane.png��6   z�5'      V� 5(      .~�5)      �� �� 5*      �� 5+      -   5        
rotate��5+   &   T� 5,      5-      5/      �� 51   
   $��1��6   $��2��6   ._�52      � .+�53      �� $��30��6   $	��100��6(   $��18(�64   $	��Top=�54      $��Center=�64   ��55      ��  �� Path$ CurrentAngle� �� RelativeSource �� 	 �� Mode$	 FindAncestor=�
 �� AncestorType$
 WindowL��� 56      $��1��6   $��Center=�6&   
���  ��6C   �� 57      $��1��6   $��Center=�6'   
���  ��$ 0.5,0.5&�6M   z�58      V� 59      .~�5:      �� �� 5;      �� 5<      $	 180Q�6.   T� 5=      5>      5@      5A      5B      �� 5D   
   $��1��6   .+�5E      �� $��Angle offset��6   $��10, 0q�6+   �� 5F      -   5         ��Name$ textBox_angle��5F      $��10, 0q�6-   $��35��6<   $��16(�6H   5G      5H      5I      