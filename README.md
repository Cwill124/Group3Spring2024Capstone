Project Title: Group3Spring2024Capstone

Description: 
This project is a study tool that allows the user to create a pdf or link source and take notes to the source. The project has a desktop app and a website of the project allowing the user to choose where they study. The desktop application allows the user to view offline and resync at later date.

Project launch settings

web app:
	have angular installed on computer
	open a cmd 
	navigate to code/web-capstone/
	npm install
	ng serve --ssl true --ssl
	then Launch the ASP Backend
ASP Backend:
	naviagate to code/CapstoneAsp
	install any missing nuget packages (should be included with repo)
	launch 
Database:
	have PgAdmin4 and Postgresql installed on machine
	create a server in Psql shell if needed server info
							(Host=localhost;Port=5432;Database=postgres;Username='postgres';Password='root')
	select the query tool and run the db script 0001.sql. The script will create the schema, tables and insert the source_type values needed
	once completed it should be good to go for the desktop and web application
Desktop Application: 