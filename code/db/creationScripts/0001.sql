CREATE SCHEMA IF NOT EXISTS capstone;

-- Use the capsone schema
SET search_path TO capstone;

-- Drop tables if they exist
DROP TABLE IF EXISTS login;
DROP TABLE IF EXISTS app_user;
DROP TABLE IF EXISTS source;
DROP TABLE IF EXISTS source_type;
DROP TABLE IF EXISTS note;
DROP TABLE IF EXISTS tag;
DROP TABLE IF EXISTS note_comment;


-- Create tables
CREATE TABLE login (
    username VARCHAR(225) primary key,
    password VARCHAR(225) NOT NULL
);

CREATE TABLE app_user (
    username VARCHAR(255) PRIMARY KEY REFERENCES capstone.login(username) not null,
    fname VARCHAR(255),
    lname VARCHAR(255),
    phone CHAR(10),
    email VARCHAR(255)
);

CREATE TABLE source_type (
    source_type_id SERIAL PRIMARY KEY,
    type_name VARCHAR(225) NOT NULL
);

CREATE TABLE source (
    source_id SERIAL PRIMARY KEY,
    description VARCHAR(600),
    name VARCHAR(225) NOT NULL,
    content JSON NOT NULL,
   	meta_data JSON NOT NULL,
    source_type_id INTEGER REFERENCES capstone.source_type(source_type_id) NOT NULL,
    tags JSON,
    created_by VARCHAR(225)
);

CREATE TABLE note (
    note_id SERIAL PRIMARY KEY,
    source_id INTEGER REFERENCES capstone.source(source_id) ON DELETE CASCADE NOT NULL,
    content JSON NOT NULL,
    username VARCHAR(225) NOT NULL
);

CREATE TABLE tag (
    tag_id SERIAL PRIMARY KEY,
    tag VARCHAR(225) NOT NULL,
    note INTEGER REFERENCES capstone.note(note_id) ON DELETE CASCADE NOT NULL
);

CREATE TABLE note_comment (
    comment_id SERIAL PRIMARY KEY,
    note_id INTEGER REFERENCES capstone.note(note_id) NOT NULL,
    source_id INTEGER REFERENCES capstone.source(source_id) NOT NULL,
    username VARCHAR(225) NOT NULL
);

INSERT INTO capstone.source_type (type_name) VALUES ('pdf'), ('video');
