drop database if exists :"new_db_name";
drop role if EXISTS :"new_db_name";
create role :new_db_name with login encrypted password 'abc123' connection limit -1;
create database :new_db_name with owner :new_db_name encoding='UTF8';
alter database :new_db_name set timezone to 'Asia/Shanghai';
\c :new_db_name;
