   M S B A M L   `   `   `  ����P  LComputersInDomainInfo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ComputersInDomainInfoComputersInDomainInfo  5   	   &     ComputersInDomainInfo.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:ComputersInDomainInfo     5   	     � Title$-  'CIDI - Computers In Domain Informations��5   	   $	��450��69   $	��958��6F   .��5	      � .+�5
   
   �� -   5        $��Update��5
      $
��Left=�6"   $��
30,292,0,0q�6=   $	��Top=�6Q   $��94��6i   $��44��6�   �� 5   
   -   5         ��Name$ username��5      $
��Left=�6$   $��23��6?   $��
20,137,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��120��6�   G� 5   
   -   5         G�Name$ password��5      $
��Left=�6(    G� PasswordChar$ *��6C   $��23��6T   $��
20,186,0,0q�6`   $	��Top=�6t   $	��120��6�   �� 5   
   $
��Left=�6   $��
20,116,0,0q�6/   $
��Wrap=�6C   $��	Username:��6W   $	��Top=�6h   �� 5   
   $
��Left=�6   $��
24,165,0,0q�6/   $
��Wrap=�6C   $��	Password:��6W   $	��Top=�6h   �� 5   
   -   5        $��Get File��5      $
��Left=�6$   $��	35,27,0,0q�6?   $	��Top=�6R   $��75��6j   6u   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $	��419��6T   $	��Top=�6a   $	��155��6y   ._�5      �� -   5        $��TO CSV��5      $��43,368,45,28q�6&   6<   5      �� 5   
   -   5         ��Name$ filename��5      $
��Left=�6&   $��	35,68,0,0q�6A   $
��Wrap=�6T   $�� ��6h   $	��Top=�6p   $��23��6�   $	��105��6�   �� 5   
   $
��Left=�6   $��9,68,0,0q�6/   $
��Wrap=�6A   $��File:��6U   $	��Top=�6b   �� 5   
   -   5         ��Name$ listview��5      $
��Left=�6%   $	��353��6@   $��	155,0,0,0q�6M   $	��Top=�6`   $	��795��6x    �� View 5      �� 5      .P�5      ��  �� Width$	 100��6%   $��Server��61   	 �� DisplayMemberBinding	 6A   �� *Server+�� 5      $ 80��6%   $��Server Name��60   	 6E   �� *
ServerName+�� 5      $	 150��6%   $��	Processor��61   	 6D   �� *	Processor+�� 5      $ 45��6%   $	��RAM��60   	 6=   �� *RAM+�� 5      $ 70��6%   $��	Disk Name��60   	 6C   �� *
NameDisk+�� 5      $	 100��6%   $��
Disk Space��61   	 6E   �� *	DiskSpace+�� 5      $	 100��6%   $��Free Disk Space��61   	 6J   �� *FreeDiskSpace+�� 5      $	 145��6%   $��Last Reboot��61   	 6F   �� *
LastReboot+5       5"      � 5#   
   -   5        
 �Name$
 progressbar��5#      $
��Left=�6+    � Minimum$ 0Q�6F   $��30��6R   $��160,358,0,0q�6^   $	��Top=�6s   $	��780��6�   �� 5$   
   -	   5        $ percentblock��5$      $
��Left=�6*   $��160,388,0,0q�6E   $
��Wrap=�6Z   $	��0 %��6n   $	��Top=�6y   $	��152��6�   5&      5'      