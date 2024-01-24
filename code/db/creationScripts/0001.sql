SET search_path TO capstone;

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
create table pdf (
	id integer not null GENERATED always as IDENTITY primary key,
	name varchar(225) not null,
	link varchar(500)
);
create table video_link (
	id integer not null GENERATED always as IDENTITY primary key,
	name varchar(225) not null,
	link varchar(500)
);
INSERT INTO app_user (username, password, firstname, lastname, email, phone, role)
VALUES ('example_user', 'example_password', 'John', 'Doe', 'john@example.com', '1234567890', 'user');

INSERT INTO pdf (name, link)
VALUES ('example_pdf', 'https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf');

INSERT INTO video_link (name, link)
VALUES ('example_video', 'https://www.youtube.com/watch?v=C0DPdy98e4c');
