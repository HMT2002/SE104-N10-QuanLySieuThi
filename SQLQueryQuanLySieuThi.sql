CREATE DATABASE QUANLYSIEUTHI

use QUANLYSIEUTHI

set dateformat dmy

drop table KHACHHANG
CREATE TABLE KHACHHANG(
MAKH varchar(10),
HOTEN varchar(40),
SODT varchar(20),
NGSINH smalldatetime,
NGDK smalldatetime,
DOANHSO money,
PICID int,
constraint LK_HINHANH_KH foreign key(PICID) references HINHANH(PICID),
primary key(MAKH)
)

CREATE TABLE NHANVIEN(
 MANV varchar(10),
 HOTEN varchar(40),
 SODT varchar(20),
 NGVL smalldatetime,
 LUONG money,
 PICID int,
constraint LK_HINHANH_NV foreign key(PICID) references HINHANH(PICID),
 primary key(MANV)
)

CREATE TABLE NHACUNGCAP(
MACC varchar(10),
TEN varchar(40),
SODT varchar(20),
XUATXU varchar(40),
PICID int,
constraint LK_HINHANH_CC foreign key(PICID) references HINHANH(PICID),
primary key(MACC)
)

CREATE TABLE SANPHAM(
MASP varchar(10),
TENSP varchar(40),
DVT varchar(20),
PICID int,
constraint LK_HINHANH_SP foreign key(PICID) references HINHANH(PICID),
MACC varchar(10),
constraint LK_CUNGCAP foreign key(MACC) references NHACUNGCAP(MACC),
GIA money,
primary key(MASP)
)

CREATE TABLE HOADON(
SOHD int,
NGHD smalldatetime,
MAKH varchar(10),
constraint LK_KHACHHANG foreign key(MAKH) references KHACHHANG(MAKH),
MANV varchar(10),
constraint LK_NHANVIEN foreign key(MANV) references NHANVIEN(MANV),
TRIGIA money,
primary key(SOHD)
)

CREATE TABLE CTHD(
SOHD int,
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


select * from HINHANH 

delete from HINHANH where PICID = 1