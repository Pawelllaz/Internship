   M S B A M L   `   `   `  ���� A  =Backup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:BackupBackup      Backup.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:Backup     U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     �Name$  myWindow��   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � WindowStyle$ ThreeDBorderWindow=� � AllowsTransparency .   � 
ResizeMode$ 	CanResize=� � WindowStartupLocation$ CenterScreen=� � Title$ Kopia Zapasowa��$	��450��$	��800��$	��400��$	��500��$	��700��$
��1000�� � 	Resources   BackupNameWarningAnimation	 �� 
TargetName
 �� TargetProperty g� Duration	 W� To g� 	BeginTime	 �� To  ProgressAnimation W� From  ProgressAnimationReverse ! AmountCopyingWarningAnimation 
 myFont �� 
TargetType �� Property �� Value��%�  &	       &	 x    &	 �    &	 �    &	 *    �� .��W� $	 BackupNameWarning��$
 Opacity�$ 0:0:0.5D�$ 1X�W� $	 BackupNameWarning��$
 Opacity�$ 0:0:0.5D�$ 0:0:5X�$ 0X��� $	 BackupNameTextBox��$2
 ,(TextBox.Background).(SolidColorBrush.Color)�$ 0:0:0.4D�$	 RedX��� $	 BackupNameTextBox��$2
 ,(TextBox.Background).(SolidColorBrush.Color)�$ 0:0:0.4D�$ 0:0:0.8X�$ #F8FAF6X��� .��W� $	 Title��$
 Opacity�$ 0:0:0.2D�$ 1X�$ 0X�W� $		 Img��$
 Opacity�$ 0:0:0.2D�$ 1X�$ 0X�W� $	 CopyingStatus��$
 Opacity�$ 0:0:0.3D�$ 0:0:0.2X�$ 0X�$ 1X�W� $	 CopyingFile��$
 Opacity�$ 0:0:0.3D�$ 0:0:0.2X�$ 0X�$ 1X�W� $	 CopyPercent��$
 Opacity�$ 0:0:0.3D�$ 0:0:0.2X�$ 0X�$ 1X�W� $	 CopyingFileStaticText��$
 Opacity�$ 0:0:0.3D�$ 0:0:0.2X�$ 0X�$ 1X�W� $	 CopyingCapacity��$
 Opacity�$ 0:0:0.3D�$ 0:0:0.2X�$ 0X�$ 1X��� .��W� $	 CopyingStatus��$
 Opacity�$ 0:0:0.3D�$ 1X�$ 0X�W� $	 CopyingFile��$
 Opacity�$ 0:0:0.3D�$ 1X�$ 0X�W� $	 CopyPercent��$
 Opacity�$ 0:0:0.3D�$ 1X�$ 0X�W� $	 CopyingFileStaticText��$
 Opacity�$ 0:0:0.3D�$ 1X�$ 0X�W� $	 CopyingCapacity��$
 Opacity�$ 0:0:0.3D�$ 1X�$ 0X�W� $	 Title��$
 Opacity�$ 0:0:0.2D�$ 0:0:0.2X�$ 0X�$ 1X�W� $		 Img��$
 Opacity�$ 0:0:0.2D�$ 0:0:0.2X�$ 0X�$ 1X��� .��W� $	 Warning��$
 Opacity�$ 0:0:0.5D�$ 0X�$ 1X�W� $	 Warning��$
 Opacity�$ 0:0:0.5D�$ 0:0:2X�$ 1X�$ 0X��� " ��.���  � ��$ 15(���  � �� �#003776��  � ��$ Italic$���  � ��$ Comic Sans MS, Verdana)�.��� -    �Name$ MainGrid��.+��  � ColumnDefinitions �� $��* �$	��200���� $��2* �$	��200��.+��� -    ��Name$ Title��$��Center=�$��Center=�$��Comic Sans MS, Verdana)�$��25(�$��Black"�$��	0,0,0,250q�$��Italic$����#003776$��Kopia zapasowa�� J� Opacity$ 1Q��� -    ��Name$	 Img��$	��200��$	��200��$��Center=�$��Center=�$��backup1.png��$��0,0,0,0q�$ 1Q�� -   $ progresGrid��
������ $��0��$��	20,0,20,0q�.+��� -   $ CopyingStatus��$`��Zkopiowanie plikow                     z C:/test/test                     do C:/kopia/kopia��$��75��$��Center=�$
��Left=�$��	0,0,0,270q�$
��Wrap=�$��Comic Sans MS, Verdana)�$��16(�$��Italic$����#003776$ 0Q�� -    �Name$ CopyingProgressBar��$��30��$��Center=�$��Center=�$��	0,0,0,150q�$y�Hidden=�����  �� ElementName$ progresGrid�� �� Path$ ActualWidth��� -   $ CopyPercent��$	��50%��$��25��$��Center=�$��Center=�$��	0,0,0,150q�$
��Wrap=�$��Comic Sans MS, Verdana)�$��17(�$��Italic$����#FFFFFF$ 0Q��� -	   $ CopyingFileStaticText��$��Plik:��$��75��$��Center=�$
��Left=�$��	10,0,0,30q�$
��Wrap=�$��Comic Sans MS, Verdana)�$��16(�$��Italic$����#003776$ 0Q��� -
   $ CopyingFile��$��DCIM95012995105195.jpg��$��75��$��Center=�$
��Left=�$��	50,0,0,30q�$
��Wrap=�$��Comic Sans MS, Verdana)�$��16(�$��Italic$����#003776$ 0Q��� -   $ CopyingCapacity��$/��)pozostało:                      12345 MB��$��50��$��Center=�$
��Left=�$��0,70,0,0q�$
��Wrap=�$��Comic Sans MS, Verdana)�$��17(�$��Italic$����#003776$ 0Q��� -   $ Warning��#��[ $*��$Zaczekaj na zakończenie kopiowania!��$��50��$��Center=�$��Center=�$��	0,250,0,0q�$
��Wrap=�$��17(�
���  ��$ 0Q�� $��1�� � RowDefinitions �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��* ��� $��* �.+��� #��[ $��0��$��2��$��0��$��25��$��10q�$	��Top=�$��Center=�$��Nazwa kopii zapasowej���� -    ��Name$ BackupNameTextBox��$��0��$��2��$��0��$	��300��$��30��$��10q�$��Bottom=�$��Comic Sans MS, Verdana)�$��15(����#003776���� *
BackupName+�� $��0��$��2��$��1��$��2��$	��300��$��30��$��114,0,114,17q�._��� -   $ BackupNameWarning��#��[ 
���  ��$&�� Wprowadź nazwę kopii zapasowej��$��Center=�$��Bottom=�$ 0Q��� -   $	��300��$��40��$��1��$��0��$��2��$	��Top=�$��10q�$��Zrób nową kopię���� #��[ $��0��$��2��$��2��$��Bottom=�$��Center=�$��10q�$:��4Wcześniejsze kopie (kliknij, aby wykonać ponownie)���� -   $	��200��$��40��$��3��$��0������ *FastButton0+�� -   $	��200��$��40��$��3��$��1������ *FastButton1+�� -   $	��200��$��40��$��4��$��0������ *FastButton2+�� -   $	��200��$��40��$��4��$��1������ *FastButton3+� -   $ grid1��$��5��$��0��$��10q�.+��� -   $��40��$��Wyświetl wszystkie kopie������ $ grid1��$ ActualWidth�� $��5��$��1��$��0q�.+��� -   $	��150��$��40��$��Center=�$��Skasuj dane aplikacji��$��0q�
���  ��
���  ��)  #MaterialDesignThemes.Wpf.DialogHost  -     Name$ newBackupHost��   IsOpen .  $y�Visible=� $  MaterialDesignEmbeddedDialogHost#��[    DialogContent �� -     ��Name$  NewBackupBorder��$	��500�����#F0FAFF$	��200��$ 1Q�! �� CornerRadius$! 10��$��Center=�$��Center=�._��  �� $��* ��� $��* � �� $��* ��� $��* ��� $��3* �.+��� #��[ $��0��$��0��$��16(�$(��"ścieżka do folderu źródłowego��$��Bottom=�$��Center=��� #��[ $��1��$��0��$��16(�$%��ścieżka do folderu docelowego��$��Bottom=�$��Center=��� -   $ BackupSourceTextBox��$��0��$��1��$	��200��$��30��$	��Top=����� *NewBackupSourcePath+�� -   $ BackupDestinationTextBox��$��1��$��1��$	��200��$��30��$	��Top=����� *NewBackupDestinationPath+�� -   $��0��$��2��$��2��$	��280��$��	30,10,0,0q�$	��Top=�$
��Left=�$$��Utwórz nową kopię zaposową���� -   $��0��$��2��$��2��$	��140��$��	0,10,30,0q�$	��Top=�$��Right=�$��anuluj��
���  ��
���  ��$2��,materialDesign:DialogHost.CloseDialogCommand����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 3system&clr-namespace:System;assembly=mscorlib  �� #��[ $��0��$��2��$��2��$��
40,75,40,0q�$��13(�$G��APrzykładowa ścieżka do pliku:  C:\Program Files\Moje dokumenty��$
��Wrap=�  -   $ AllBackupsHost�� .  $y�Visible=�#��[  �� -   $  AllBackupsBorder�����#F0FAFF$ 1Q�$! 10��$��Center=�$��Center=����� $ MainGrid��$ ActualWidth����� $ MainGrid��$ ActualHeight�._�� .+��� -   $��40q�$	��Top=�$��Right=�$��25��$��45��
���  ��
���  ��$��X��$��0q�$��16(�$��Heavy"��� $	��Top=�$��Center=�$��50,45,50,50q�#��[ $G��ALista twoich kopii zaposowych (od lewej: nazwa, źródło i data)���� $��50,80,50,50q�.���� -    " ��Name$" AllBackupsStackPanel��# �� Orientation$# Vertical=�$��0,0,10,0q�  -!   $ DeleteDataHost�� .  $y�Visible=�#��[  �� -"   $  DeleteDataBorder��$	��400��$	��200�����#F0FAFF$ 1Q�$! 10��$��Center=�$��Center=�._�� .+��� $\��VSpowoduje to skasowanie tylko danych aplikacji (kopie zapasowe zostaną nie naruszone)��$	��Top=�$��Center=�#��[ $
��Wrap=�$��
15,25,15,0q��� -#   $��Center=�$
��Left=�$��50��$	��150��$��	35,70,0,0q�$��Skasuj��$��0q�$��20(��� -$   $��Center=�$��Right=�$��50��$	��150��$��	0,70,35,0q�$��Anuluj��$��0q�$��20(�
���  ��
���  ��