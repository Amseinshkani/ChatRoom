   M S B A M L   `   `   `  ????C  ?ServerUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ServerUIServerUI  5   	       ServerUI.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:ServerUI     5   	     ? Title$  Server??5   	   $	??470??6   $	??820??6%   .??5	   	   ? .+?5
      ??  ?? Orientation$ 
Horizontal=?6   $??Center=?6/   $	??Top=?6L   $??5q?6d   .+?5      ?? $ 
Horizontal=?6   .+?5      ?? .??6   Part :6!   ?? 5      -   5         ??Name$
 Port??5      $??20??6(   $	??100??64   5      ?? 5      $ 
Horizontal=?6   .+?5      ?? .??6   Ip :6   ?? 5      -   5        $ IP??5      $	??150??6'   $??20??63   {?.  $??	127.0.0.1??5      5      ?? 5      $ 
Horizontal=?6   $??10 0 0 0q?62   .+?5      ?? -   5         ??Name$ startbtn??5      $??50??6B   .??6M   Start6T   ?? 5      -   5        $ Stopbtn??5      $??50??6@   $??10 0 0 0q?6K   .??6]   Stop6c   5      5      ?? 5   
   $??Stretch=?6   $??0 35 0 0q?61   .+?5      ?? -   5        $??State??5      $??Center=?6!   
???  ??$??	ExtraBold"?5      .??6$   Server Stop61   ?? 5      -   5        $ 
RecieveBox??5       ?? 
IsReadOnly . $??14(?6<   $??10q?6J   $??2q?6W   $ ?? HorizontalScrollBarVisibility$
 Auto=?6b   " ?? VerticalScrollBarVisibility$
 Auto=?6?   $??300px??5      
??? ???6%   ?? 5!      $ 
Horizontal=?6   $??10 0 0 0q?62   $
??Left=?6D   .+?5"      ?? -   5        $ Sendbox??5"       ?? TextWrapping$
 Wrap=?6+   $??50??6?   $	??730??6K   ?? 5#      -   5        $ SendBtn??5#      $??50??6*   $??Center=?6K   {?.  $??5 0 0 0q?6x   .??6?   Send6?   5%      5&      5'   
   5(      