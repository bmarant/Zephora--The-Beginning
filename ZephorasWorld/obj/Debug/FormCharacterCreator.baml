   M S B A M L   `   `   `  ����  ZephorasWorld(    "ZephorasWorld.FormCharacterCreator   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  FormCharacterCreator��5   	   $	��466��6&   $��608.785��63   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6    � Offset$ 0Q�6)   � 5      $ 	#FF804343��6   $ 1Q�6-   5	       � 	Resources 5         MyButton �� 
TargetType �� Property �� Value	 �� 
TargetType
 �� Triggers P� Property P� Value��%�  &	        �� 5   
   " ��6!   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      "	 ��6&   .R�5      �� $��border��6!   $��0q�6/   
���   �#��z��6W   ._�5      �� $��Center=�6/   $��Center=�6L   5      
 5      P� 5       �@J�IsMouseOver . . �5   "   ��  �@J�Opacity$	 0.8Q�6<   5      5      5      5      5      .��5$      � ��5'   
   �� 5(       �� ImageSource$ paper background.png��6    � RowDefinitions 5*   
   �� 5+      $	��31* �6   �� 5,      $
��114* �6   .+�5/   
   �� -   5         ��Name$ 	char_name��5/      $
��Left=�6%   $��28��6@   $��	15,49,0,0q�6L    �� TextWrapping$
 Wrap=�6_   $	��Top=�6t   $	��144��6�   $��0q�6�    �� TextAlignment$ Center=�6�   $��Segoe UI Semibold)�6�   .��6�   ��50      �� 51      $ textboxBackground.png��6    j� Stretch$ UniformToFill=�6A   .��52   "   53      �� 54   
   $��Character Name��6   $
��Left=�6)   $��	26,11,0,0q�6D   $	��Top=�6W   $	��169��6o   
���   �$��Sitka Small)�6�   $��18(�6�   �� 55   
   $��Character Make��6   $
��Left=�6+   $��	26,86,0,0q�6F   $	��Top=�6Y   $	��105��6q   $	��144��6~   
���   �$��0q�6�   $��2��6�   .��56      	� -   5         	�Name$ rdo_male��56      $$��Male                          ��6,   $
��Left=�57      $��
10,10,0,50q�67   $��73��6K   $��12(�6V   $��Normal"�6d   
���   �6x   58      	� 59   
   -   5        $ 
rdo_female��59      $��Female��6*   $
��Left=�6;   $��42,43.198,0,0q�6V   $	��Top=�6m   
���   �$��23��6�   $��69��6�   $��1��6�   �� 5:   
   $��Class��6   $
��Left=�6"   $��26,116,0,54q�6=   $	��199��6R   
���   �$��1��6q   $��0q�6~    J� Effect 5;      3  -System.Windows.Media.Effects.DropShadowEffect  5<      .��5>      �� -   5         ��Name$ cbo_charclass��5>       J� RenderTransformOrigin$ 0.503,-1.296&�6.   $��10,14,30,106q�6S   5@      5A      �� 5B   
   -   5         ��Name$ save_button��5B      $��Create Character��6&   #��[  6B   $
��Left=�6e   $��
26,301,0,0q�6�   $	��Top=�6�   $	��120��6�   $��34��6�   $��1��6�   
���������5C      �� 5D      $ 
button.png��6   5F      �� 5G   
   -   5         ��Name$ class_select��5G      $��Label��6&   $
��Left=�66   $��
288,41,0,0q�6Q   $	��Top=�6e   $	��112��6}   
���   �6�   �� 5H   
   -   5        $ 	count_add��5H      $
��Left=�6%   $��32��6@   $��
408,41,0,0q�6L   $
 Wrap=�6`   $	��Top=�6u   $��72��6�   $��15��6�   $ Center=�6�   $��16(�6�   $��0q�6�   .��6�   ��5I      �� 5J      $ textboxBackground.png��6   .��5K   "   5L      �� 5M   
   $��Statitistics��6   $
��Left=�6)   $��
288,86,0,0q�6D   $	��Top=�6X   $	��299��6p   $	��259��6|   
���   �$��2��6�   $��0q�6�   .��5N      �� -   5        $ str_pos1��5N      #��[  6'   $
��Left=�6J   $��137,2,0,180q�6e   $��Stretch=�6z   $��76��6�   $��0q�6�   ��5O      �� 5P      ��5R      �� 5S      $ addButton.png��6!   $ UniformToFill=�6=   5V      5W      �� 5X   
   $��Strength��6   $
��Left=�6#   $��
314,27,0,0q�6>   $	��Top=�6R   
���   �$��1��6}   $��Sitka Small)�6�   �� 5Y   
   $��Wisdom��6   $
��Left=�6!   $��
314,90,0,0q�6<   $	��Top=�6P   
���   �$��1��6{   $��Sitka Small)�6�   �� 5Z   
   $��	Dexterity��6   $
��Left=�6$   $��314,141,0,0q�6?   $	��Top=�6T   
���   �$��1��6   $��Sitka Small)�6�   �� 5[   
   $��Health��6   $
��Left=�6!   $��314,210,0,0q�6<   $	��Top=�6Q   
���   �$��1��6|   $��Sitka Small)�6�   �� 5\   
   -	   5        $ str_stat��5\      $��0��6"   $
��Left=�6.   $��
386,27,0,0q�6I   $	��Top=�6]   
���   �$��1��6�   �� 5]   
   -
   5        $ wis_stat��5]      $��0��6"   $
��Left=�6.   $��
386,90,0,0q�6I   $	��Top=�6]   
���   �$��1��6�   �� 5^   
   -   5        $ dex_stat��5^      $��0��6"   $
��Left=�6.   $��386,141,0,0q�6I   $	��Top=�6^   
���   �$��1��6�   $ 
0.55,1.289&�6�   �� 5_   
   -   5        $ hlt_stat��5_      $��0��6"   $
��Left=�6.   $��386,210,0,0q�6I   $	��Top=�6^   
���   �$��1��6�   �� 5`   
   -   5        $ wis_pos2��5`      #��[  6#   $�� ��6G   $
��Left=�6R   $��
431,69,0,0q�6m   $	��Top=�6�   $��77��6�   $��1��6�   $��57��6�   $��0q�6�   ��5a      �� 5b      $ addButton.png��6   $ UniformToFill=�69   5d      �� 5e   
   -   5        $ dex_pos3��5e      #��[  6#   $�� ��6E   $
��Left=�6P   $��431,127,0,0q�6k   $	��Top=�6�   $��77��6�   $��1��6�   $��55��6�   $��0q�6�   ��5f      �� 5g      $ addButton.png��6   $ UniformToFill=�69   5k      �� 5l   
   -   5        $ hlt_pos4��5l      #��[  6#   $�� ��6E   $
��Left=�6P   $��430,194,0,0q�6k   $	��Top=�6�   $��78��6�   $��1��6�   $��51��6�   $��0q�6�   ��5m      �� 5n      $ addButton.png��6   $ UniformToFill=�69   5p      �� 5q   
   -   5        $ str_neg1��5q      #��[  6#   $�� ��6F   $
��Left=�6Q   $��
478,11,0,0q�6l   $	��Top=�6�   $��69��6�   $��1��6�   $��54��6�   $��0q�6�   ��5s      �� 5t      $ negButton.png��6   $ UniformToFill=�69   5w      �� 5x   
   -   5        $ wis_neg2��5x      #��[  6#   $�� ��6F   $
��Left=�6Q   $��
479,69,0,0q�6l   $	��Top=�6�   $��68��6�   $��1��6�   $��57��6�   $��0q�6�   ��5y      �� 5z      $ UniformToFill=�6   $ negButton.png��65   5|      �� 5}   
   -   5        $ dex_neg3��5}      #��[  6#   $�� ��6F   $
��Left=�6Q   $��478,127,0,0q�6l   $	��Top=�6�   $��69��6�   $��1��6�   $��55��6�   $��0q�6�   ��5~      �� 5      $ UniformToFill=�6   $ negButton.png��65   5�      �� 5�   
   -   5        $ hlt_neg4��5�      #��[  6#   $�� ��6F   $
��Left=�6Q   $��477,195,0,0q�6l   $	��Top=�6�   $��69��6�   $��1��6�   $��50��6�   $��0q�6�    �� 	ClickMode$ Press=�6�   ��5�      �� 5�      $ negButton.png��6   $ UniformToFill=�69   5�      �� 5�   
   -   5        $ 
levelstart��5�      $��Label��6$   $
��Left=�64   $��
508,41,0,0q�6O   $	��Top=�6c   �� 5�   
   $
��Left=�6   $��16��6/   $��0,77,0,0q�6;   $	��Top=�6M   $	��601��6e   
���   �6q   5�      5�      