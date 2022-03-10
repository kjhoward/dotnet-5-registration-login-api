# dotnet-6-registration-long-api using PostgreSQL

- Updated packages and framework to dotnet 6
- Implemented a data repository pattern for easier unit testing
- Extracted the data models from the business models for easier unit testing
- Implemented PostgreSQL entity framework instead of sqllite / sql server
- Got rid of the migrations classes, will include a psql statment below to create the db table instead. 
- Updated the URL to use HTTPS instead of HTTP

## Models Project
There is a "Models" project referenced, that can be found here https://github.com/kjhoward/RegistrationApiModels. This is required for the "QueryResult" and "UserResource" classes.

The idea of the Models assembly is that it extracts your business models from the project allowing them to be used elsewhere and tested against external from the project. 

## PSQL to create users table
create table app_users(
Id SERIAL PRIMARY KEY    NOT NULL,
FirstName CHAR(500)      NOT NULL,
LastName CHAR(500)       NOT NULL,
UserName CHAR(500)       NOT NULL,
PasswordHash CHAR(1000)  NOT NULL);

# Forked From dotnet-5-registration-login-api

.NET 5.0 - Simple API for User Management, Authentication and Registration

For documentation and instructions check out https://jasonwatmore.com/post/2021/05/25/net-5-simple-api-for-authentication-registration-and-user-management
