CREATE DATABASE QUANLYSIEUTHI

use QUANLYSIEUTHI

set dateformat dmy

drop table KHACHHANG
CREATE TABLE KHACHHANG(
MAKH varchar(10),
HOTEN nvarchar(40),
SODT varchar(20),
NGSINH datetime,
NGDK datetime,
DOANHSO money,
PICID int,
constraint LK_HINHANH_KH foreign key(PICID) references HINHANH(PICID),
primary key(MAKH)
)
insert into KHACHHANG(MAKH,HOTEN,SODT,NGSINH,NGDK,DOANHSO)values('KH001','Nguyễn Văn A','079123456','2000-10-31','2014-9-8',30000)
insert into KHACHHANG(MAKH,HOTEN,SODT,NGSINH,NGDK,DOANHSO)values('KH002','Trần Văn B','079654321','2010-2-1','2012-6-17',450000)
insert into KHACHHANG(MAKH,HOTEN,SODT,NGSINH,NGDK,DOANHSO)values('KH003','Lê Văn C','079123654','1935-4-1','2020-8-3',7840000)
insert into KHACHHANG(MAKH,HOTEN,SODT,NGSINH,NGDK,DOANHSO)values('KH004','Lý Văn D','079321456','1999-7-3','2018-7-14',470000)

select * from KHACHHANG

CREATE TABLE NHANVIEN(
 MANV varchar(10),
 HOTEN nvarchar(40),
 SODT varchar(20),
 NGVL datetime,
 LUONG money,
 PICID int,
constraint LK_HINHANH_NV foreign key(PICID) references HINHANH(PICID),
 primary key(MANV)
)

select * from NHANVIEN

CREATE TABLE NHACUNGCAP(
MACC varchar(10),
TEN nvarchar(40),
SODT varchar(20),
XUATXU nvarchar(40),

primary key(MACC)
)

insert into NHACUNGCAP(MACC,TEN,SODT,XUATXU)values('CC001','Coca Corp','09999999','Mỹ')
insert into NHACUNGCAP(MACC,TEN,SODT,XUATXU)values('CC002','Nestle Corp','09999998','Anh')
insert into NHACUNGCAP(MACC,TEN,SODT,XUATXU)values('CC003','Amazon Corp','09999997','Canada')
insert into NHACUNGCAP(MACC,TEN,SODT,XUATXU)values('CC004','Tribeco Corp','09999996','Thái')
delete from NHACUNGCAP
select MACC,TEN,SODT,XUATXU from NHACUNGCAP where MACC='CC001'

CREATE TABLE SANPHAM(
MASP varchar(10),
TENSP nvarchar(40),
DVT nvarchar(20),
PICID int,
constraint LK_HINHANH_SP foreign key(PICID) references HINHANH(PICID),
MACC varchar(10),
constraint LK_CUNGCAP foreign key(MACC) references NHACUNGCAP(MACC),
GIA money,
SL int,
primary key(MASP)
)
alter table SANPHAM add SL int
insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('SP001','Coca cola','chai','CC001',10000,100)
insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('SP002','Lays','chai','CC002',15000,40)
insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('SP003','Singum','chai','CC003',8000,130)
insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('SP004','C2','chai','CC004',7000,50)
select * from SANPHAM

CREATE TABLE HOADON(
SOHD varchar(10),
NGHD datetime,
MAKH varchar(10),
constraint LK_KHACHHANG foreign key(MAKH) references KHACHHANG(MAKH),
MANV varchar(10),
constraint LK_NHANVIEN foreign key(MANV) references NHANVIEN(MANV),
TRIGIA money,
primary key(SOHD)
)

CREATE TABLE CTHD(
SOHD varchar(10),
constraint LK_HOADON foreign key(SOHD) references HOADON(SOHD),
MASP varchar(10),
constraint LK_SANPHAM foreign key(MASP) references SANPHAM(MASP),
SL int,
)

CREATE TABLE HINHANH(
PICID int,
PICNAME varchar(50),
PICBI varbinary(MAX),
primary key(PICID)
)

CREATE TABLE ACCOUNT(
ACC varchar(20),
PRI int,
PASS varchar(20),
primary key (ACC)
)

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

delete from HINHANH where PICID = 1
delete from ACCOUNT where ACC = 'test'