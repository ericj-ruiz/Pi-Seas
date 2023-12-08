drop table GameWinners cascade constraints;
create table GameWinners
 (  
   initials         varchar2(10),
   score            integer,
   time             float,
   numberOfKills    integer,
   dateOfWin        date
 );
