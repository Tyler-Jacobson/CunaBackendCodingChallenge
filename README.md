# CUNA Mutual Backend Coding Challenge

## .NET 6 and SQL Server are needed to run this project.
Download links:

https://dotnet.microsoft.com/en-us/download/dotnet/6.0

https://www.microsoft.com/en-us/sql-server/sql-server-downloads

### If you have any issues running the project, please contact me TylerJacobsonSE@gmail.com

## To run the app locally - Follow the below steps:

Open your command line interface, navigate to a suitable directory and run the following commands

### `git clone https://github.com/Tyler-Jacobson/CunaBackendCodingChallenge`

Navigate to the project directory:
### `cd CunaBackendCodingChallenge` x2

Once in the project directory, you can run this command to create the database:

### `dotnet ef database update`

After this, you will be able to run the below command to start the API. This can also be done by opening the project in your IDE and pressing the start button.
### `dotnet run`
With the app running, you will now be able to make requests to the available endpoints

## Endpoints:

1 - Creates an initial ClientRequest object in the database, and makes a mocked request to a third party service, asking them to update the entry

POST - `https://localhost:7001/api/ClientRequest`
With a request body of 
{
  "body": "anyString"
}

2 - Using the callback provided by the previous call, this request allows the 'service' to create a new object in the database, which is associated with the previous POST by a 1:1 relation

POST - `https://localhost:7001/api/ServiceReport/callback/{id}`
With a request body of 
{
  "body": "STARTED"
}

3 - Using the exact same callback URL with a PUT request allows the 'service' to update their post at any time with a finalized status

PUT - `https://localhost:7001/api/ServiceReport/callback/{id}`

With a request body of 
{
  "status": "COMPLETED",
  "detail": "anyString"
}

4 - The current status, details, and progress of a request can be accessed at any time after the original POST's creation by making a GET request to the associated id

GET - `https://localhost:7001/api/ClientRequest/{id}`

## Testing:

In order to run tests, you will need to cd from the main directory to the tests directory. This may require you to 'cd -' if you're already in *CunaBackendCodingChallenge*. Note: The database needs to be created before running tests, see above.
### `cd -`
### `cd CunaBackendCodingChallenge.Test`

Once in the test directory, you can use the following command to run all tests:
### `dotnet test`
Although this will probably look much better if you are able to run the tests through in the 'Test Explorer' window of your IDE (built with VS2022 in mind)

## Notes:
I've chosen to implement this project as a stateless REST API with a relational database. The database is in 3rd normal form, and the design details can be found in the accompanying design doc.

I've also left some comments in the code to explain my thinking in certain situations, and in areas where more code would be required in a production level application.

## General Info:
Bootstrapped using Visual Studio 2022's ASP.NET Core Web API template

Written in C#

SQL Server database

Database migrations set up with Entity Framework

Debugged with SwaggerUI

Tested with MSTest
