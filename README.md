# dotnet-6-registration-long-api using PostgreSQL

- Updated packages and framework to dotnet 6
- Implemented a data repository pattern for easier unit testing
- Extracted the data models from the business models for easier unit testing
- Implemented PostgreSQL entity framework instead of sqllite / sql server
- Got rid of the migrations classes, will include a psql statment below to create the db table instead. 
- Updated the URL to use HTTPS instead of HTTP

## Models Projects
2 Project dependencies you will want to clone:
- https://github.com/kjhoward/DevConsulting.Common.Models
- https://github.com/kjhoward/RegistrationApiModels

After you clone the projects, you will need to update the RegistrationLoginApi.csproj ProjectReference tags to point at the location you cloned the projects to.

There is are a couple "Models" projects referenced. This is required for items like the "QueryResult" and "UserResource" classes.

The idea of the Models assemblies is that they extracts your business models from the project allowing them to be used elsewhere and tested against external from the project. 

## Unit Test Project
The Unit test using Nunit can be found here:
- https://github.com/kjhoward/registration-login-api-test

Presently it covers the repository tests but I will be adding to it as time goes on. Feel free to grab it for your own needs.

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
