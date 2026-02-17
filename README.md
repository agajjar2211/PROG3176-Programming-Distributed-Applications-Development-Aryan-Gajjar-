OVERVIEW

The given project is an implementation of the Inventory Context of a car rental platform on the basis of the principles of Clean Architecture and Domain-Driven Design (DDD).

The microservice presents a RESTful API on how to handle vehicles, their lifecycle positions and enforce business regulations within the domain model.

Technologies used:

•	ASP.NET Core Web API (.NET 10)

•	Entity Framework Core

•	SQL Server LocalDB

•	Swagger (OpenAPI)

•	Clean Architecture

ARCHITECTURE OVERVIEW

The solution is based on Clean Architecture and has strict dependency direction:

Domain <= Application <= Infrastructure <= WebAPI

Every layer possesses a distinct responsibility and there are no circular dependencies.

DOMAIN LAYER

Purpose: Business rules and invariants.

Contains:

•	Vehicle aggregate root

•	VehicleStatus enum

•	DomainException

Business Rules Implemented within Vehicle Entity:

1\.	A vehicle cannot be rented if:

•	It is already rented

•	It is reserved

•	It is under service

2\.	One cannot declare a reserved vehicle to be Available without specific release.

3\.	Status transitions can be regulated by means of domain methods only:

•	MarkAvailable()

•	MarkReserved()

•	MarkRented()

•	MarkServiced()

4\.	DomainException is thrown by way of invalid state changes.

This layer does not have any EF core and ASP.NET dependencies.



APPLICATION LAYER

Implementation: Implements apply applications and facilitate processes.

Contains:

1\.	IVehicleRepository abstraction

2\.	DTOs:

•	CreateVehicleRequest

•	UpdateVehicleStatusRequest

•	VehicleResponse

3\.	VehicleService

4\.	Application-level exceptions

Domain behavior methods of all status changes are invoked.

Controllers and repositories do not apply any business rule.



INFRASTRUCTURE LAYER

Purpose: manages perseverance and the external issues.

Contains:

•	InventoryDbContext

•	EF Core configuration

•	Repository implementation

•	Database migrations

Key Configuration:

•	Id is a number identity (generated automatically).

•	There is a distinctive index on VehicleCode.

•	Persistence with SQL Server LocalDB.



WEB API LAYER

Purpose: Uncovers REST endpoints.

Contains:

•	VehiclesController

•	Swagger configuration

•	Global exception handling

•	Dependency injection setup

All logic is delegated into the Application layer.

API ENDPOINTS

Base URL:

https://localhost:7026/



GET /api/vehicles

Returns all vehicles.



GET /api/vehicles/{id}

Returns a vehicle by ID.



POST /api/vehicles

Creates a new vehicle.



Request example:

{

"vehicleCode": "CAR001",

"locationId": "LOC1",

"vehicleType": "Sedan"

}



PUT /api/vehicles/{id}/status

Updates vehicle status.



Request example:

{

"status": 2,

"explicitRelease": true

}



Status values:

1 = Available

2 = Reserved

3 = Rented

4 = Serviced



DELETE /api/vehicles/{id}

Deletes a vehicle.



VALIDATION AND TESTING

Validation will be done at two levels:

•	DTO validation on the basis of Data Annotations.

•	DomainException Domain validation.

Swagger: This testing was carried out on a business rule:

•	Bad Request is returned twice by renting.

•	Hiring a reserved car brings 400 back.

•	Available ReservesNeed explicit Release = true.

•	Vehicles that have been serviced cannot be hired.



DATABASE

Database: SQL Server LocalDB

Connection string:

Server=(localdb)\\MSSQLLocalDB;Database=VehicleInventoryDb;Trusted\_Connection=True;TrustServerCertificate=True

The schema of the database is created and updated using entity Framework core migrations.



HOW TO RUN



1\.	Clone the repository.

2\.	Run:

•	dotnet restore

•	dotnet build

•	dotnet ef database update, VehicleInventory.Infrastructure project.

•	dotnet run project VehicleInventory.WebAPI.

3\.	The browser is automatically launched with Swagger.



CONTRACT-FIRST DESIGN

The persistence implementation was preceded by request and response models.

The domain layer had status transition rules that were recorded and implemented.

Versioning strategy:

Current version: v1 (implicit).

The vehicles can be supported in future by using /api/v1/vehicles.



KNOWN LIMITATIONS

•	None of the authentication and authorization.

•	No pagination implemented.

•	None of the automated unit tests.

•	Swagger applied to manual testing.



GIT USAGE

The repository applies the significant incremental commits which indicate:

•	Architecture setup

•	Domain modeling

•	Application use cases

•	Infrastructure implementation

•	API configuration

•	Database migrations

•	Documentation

CONCLUSION

This microservice shows:

•	Correct Clean Architecture stratification.

•	Right Domain-Driven Design implementation.

•	Business rules that are found in the domain.

•	RESTful API best practices

•	EF core migration To migrations.



