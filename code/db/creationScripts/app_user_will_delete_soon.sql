
create table app_user (
	user_id integer not null GENERATED always as IDENTITY primary key,
	username varchar(225) not null,
	password varchar(225) not null,
	firstname varchar(225),
	lastname varchar(225),
	email varchar(225),
	phone char(10),
	role varchar(100) not null,
	created_at timestamp default now() not null
);