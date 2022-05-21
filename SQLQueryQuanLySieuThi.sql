CREATE DATABASE QUANLYSIEUTHI

use QUANLYSIEUTHI

set dateformat dmy

drop table KHACHHANG
CREATE TABLE KHACHHANG(
MAKH varchar(10),
HOTEN nvarchar(40),
SODT varchar(20),
NGSINH smalldatetime,
NGDK smalldatetime,
DOANHSO money,
ACC varchar(20),
PICID int,
PICBI varbinary(MAX),
MAIL nvarchar(50),
GENDER nvarchar(20),
primary key(MAKH)
)
insert into KHACHHANG(MAKH,HOTEN,SODT,NGSINH,NGDK,DOANHSO,GENDER)values('KH001','Nguyễn Văn A','079123456','2000-10-31','2014-9-8',30000,'nam')
insert into KHACHHANG(MAKH,HOTEN,SODT,NGSINH,NGDK,DOANHSO)values('KH002','Trần Văn B','079654321','2010-2-1','2012-6-17',450000)
insert into KHACHHANG(MAKH,HOTEN,SODT,NGSINH,NGDK,DOANHSO)values('KH003','Lê Văn C','079123654','1935-4-1','2020-8-3',7840000)
insert into KHACHHANG(MAKH,HOTEN,SODT,NGSINH,NGDK,DOANHSO)values('KH004','Lý Văn D','079321456','1999-7-3','2018-7-14',470000)

select * from KHACHHANG

CREATE TABLE NHANVIEN
(
 MANV varchar(10),
 HOTEN nvarchar(40),
 SODT varchar(20),
 NGVL smalldatetime,
 LUONG money,
 ACC varchar(20),
 MAIL nvarchar(50),
 POSITION nvarchar(20),
 PICBI varbinary(MAX),
 CMND nvarchar(20),
 NGSINH smalldatetime,
 GENDER nvarchar(20),
 primary key(MANV)
)



alter table NHANVIEN add GHICHU nvarchar(250)
alter table KHACHHANG add GHICHU nvarchar(250)
alter table SANPHAM add GHICHU nvarchar(250)
alter table NHACUNGCAP add GHICHU nvarchar(250)

select * from NHANVIEN
delete from NHANVIEN where MANV='45'
update NHANVIEN where MANV='NV001' 

CREATE TABLE NHACUNGCAP(
MACC varchar(10),
TEN nvarchar(40),
SODT varchar(20),
XUATXU nvarchar(40),
primary key(MACC)
)
select * from NHACUNGCAP

CREATE TABLE SANPHAM(
MASP varchar(10),
TENSP nvarchar(40),
DVT nvarchar(20),
PICID int,
MACC varchar(10),
constraint LK_CUNGCAP foreign key(MACC) references NHACUNGCAP(MACC),
GIA money,
SL int,
PICBI varbinary(MAX),
primary key(MASP)
)
select * from SANPHAM

insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('SP001','Coca cola','chai','CC001',10000,100)
insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('SP002','Lays','chai','CC002',15000,40)
insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('SP003','Singum','chai','CC003',8000,130)
insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('SP004','C2','chai','CC004',7000,50)
insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('SP005','0 độ','chai','CC004',7000,50)

insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('SP006','7up','chai','CC004',7000,50)

insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('SP007','Sting','chai','CC004',7000,50)


select* from SANPHAM


delete from SANPHAM

CREATE TABLE HOADON(
SOHD varchar(10),
NGHD smalldatetime,
MAKH varchar(10),
constraint LK_KHACHHANG foreign key(MAKH) references KHACHHANG(MAKH),
MANV varchar(10),
constraint LK_NHANVIEN foreign key(MANV) references NHANVIEN(MANV),
TRIGIA money,
primary key(SOHD)
)
select * from HOADON

delete from HOADON


CREATE TABLE CTHD(
SOHD varchar(10),
constraint LK_HOADON foreign key(SOHD) references HOADON(SOHD),
MASP varchar(10),
constraint LK_SANPHAM foreign key(MASP) references SANPHAM(MASP),
SL int,
primary key(SOHD,MASP)
)

select * from CTHD 


delete from CTHD



drop table CTHD
CREATE TABLE ACCOUNT(
ACC varchar(20),
PRI int,
PASS nvarchar(MAX),
primary key (ACC)
)

CREATE TABLE NHAPHANG(
MANH varchar(10),
MANV varchar(10),
constraint LK_NHAPHANG_NHANVIEN foreign key(MANV) references NHANVIEN(MANV),

MASP varchar(10),
constraint LK_NHAPHANG_SANPHAM foreign key(MASP) references SANPHAM(MASP),

SLNHAPHANG int,
NGNH smalldatetime,
primary key (MANH)
)
select * from NHAPHANG

select MACC,TEN,SODT,XUATXU from NHACUNGCAP

alter table KHACHHANG add ACC varchar(20)
alter table KHACHHANG add constraint LK_TK_KHACHHANG foreign key(ACC) references ACCOUNT(ACC)
alter table NHANVIEN add ACC varchar(20)
alter table NHANVIEN add constraint LK_TK_NHANVIEN foreign key(ACC) references ACCOUNT(ACC)
alter table CTHD add constraint LK_HOADON foreign key(SOHD) references HOADON(SOHD)

insert into ACCOUNT(ACC,PRI,PASS) values('tue',2,'1')
insert into ACCOUNT(ACC,PRI,PASS) values('ngan',2,'1')
insert into ACCOUNT(ACC,PRI,PASS) values('huy',2,'1')

select * from ACCOUNT 
delete from ACCOUNT where ACC='tuee'
alter table ACCOUNT alter column PASS nvarchar(MAX)
select * from KHACHHANG