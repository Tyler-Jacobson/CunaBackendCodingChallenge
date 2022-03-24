# CUNA Mututal Backend Coding Challenge

## .NET 6 and SQL Server are needed to run this project.
Download links:
https://dotnet.microsoft.com/en-us/download/dotnet/6.0
https://www.microsoft.com/en-us/sql-server/sql-server-downloads

### If you have any issues running the project, please contact me TylerJacobsonSE@gmail.com

## To run the app locally. Follow the below steps:

Open your command line interface, navigate to a suitable directory and run the following commands

### `git clone https://github.com/Tyler-Jacobson/CunaBackendCodingChallenge`

In order to run the project:
### `cd CunaBackendCodingChallenge`

Once in the project directory, you can run this command to create the database:

### `dotnet ef database update`

You can run the below command to start the API. This can probably also be done by opening the project in your IDE and pressing the start button.
### `dotnet run`

In order to run tests, you will need to cd from the main directory in to the tests directory. This may require you to 'cd -' if you're already in CunaBackendCodingChallenge
### `cd -`
### `cd CunaBackendCodingChallenge.Test`

Once in the test directory, you can use the following command to run all tests
### `dotnet test`
Although this will look much better in the 'Test Explorer' window of your IDE (This has only been tried with Visual Studio 2022)

## Notes:
I've chosen to implement this project as a stateless REST API with a relational database. The database is in 3rd normal form, and the design details can be found in the accompanying design doc.

I've also left some comments in the code to explain my thinking in certain situations, and in areas where more code would be required in a production level application.

## General Info:
Boostrapped using Visual Studio 2022's ASP.NET Core Web API template

Written in C#

SQL Server database

Database migrations set up with Entity Framework

Tested with MSTest
