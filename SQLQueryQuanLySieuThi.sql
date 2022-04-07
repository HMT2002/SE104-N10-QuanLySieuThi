CREATE DATABASE QUANLYSIEUTHI

use QUANLYSIEUTHI

set dateformat dmy


CREATE TABLE KHACHHANG(
MAKH char(5),
HOTEN varchar(40),
SODT varchar(20),
NGSINH smalldatetime,
NGDK smalldatetime,
DOANHSO money,
primary key(MAKH)
)

CREATE TABLE NHANVIEN(
 MANV char(5),
 HOTEN varchar(40),
 SODT varchar(20),
 NGVL smalldatetime,
 LUONG money,
 primary key(MANV)
)

CREATE TABLE SANPHAM(
MASP char(5),
TENSP varchar(40),
DVT varchar(20),
NUOCSX varchar(40),
GIA money,
primary key(MASP)
)

CREATE TABLE HOADON(
SOHD int,
NGHD smalldatetime,
MAKH char(5),
MANV char(5),
TRIGIA money,
primary key(SOHD)
)

drop table HINHANH

CREATE TABLE HINHANH(
PICID int,
PICNAME varchar(50),
PICBI varbinary(MAX),
primary key(PICID)
)
INSERT INTO HINHANH (PICID, PICNAME, PICBI) VALUES (0, 'test',)


select * from HINHANH 

delete from HINHANH where PICID = 1