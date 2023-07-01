# Backend Service

This is a backend service for an application I am developing. The purpose of this service is to provide repositories and controllers for managing various entities such as categories, products, vendors, reviewers, addresses, countries, and reviewer addresses.

<img width="1129" alt="Screenshot 2023-07-01 at 5 57 34 PM" src="https://github.com/ChrisMKocabas/asp.net-web-api-backend/assets/75855099/4f1650e8-be36-4c8e-a3d0-aecf853cb3cb">

## Table of Contents

- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Available API Endpoints](#available-api-endpoints)
- [Running the Application](#running-the-application)
- [Using Visual Studio for Mac](#using-visual-studio-for-mac)

## Technologies Used

The backend service is built using the following technologies:

- C#
- ASP.NET Core
- Entity Framework Core
- AutoMapper

## Project Structure

The project is organized into the following folders:

- Controllers: Contains API controllers for each entity (Category, Product, Vendor, Reviewer, Address, Country, ReviewerAddress).
- Data: Contains the data context and entity configurations.
- Dto: Contains data transfer objects used for communication between the backend service and the client application.
- Interfaces: Contains interfaces for the repositories.
- Models: Contains entity models used by the backend service.
- Repository: Contains implementations of the repositories.
- Mappings: Contains AutoMapper profiles for mapping between models and DTOs.

## Available API Endpoints

The backend service exposes the following API endpoints:

- `api/category`: Endpoints for managing categories.
- `api/product`: Endpoints for managing products.
- `api/vendor`: Endpoints for managing vendors.
- `api/reviewer`: Endpoints for managing reviewers.
- `api/address`: Endpoints for managing addresses.
- `api/country`: Endpoints for managing countries.
- `api/revieweraddress`: Endpoints for managing reviewer addresses.

## Running the Application

To run the backend service, please follow these steps:

1. Make sure you have the .NET 5 SDK installed.
2. Clone the repository to your local machine.
3. Navigate to the project directory: `cd BackendService`.
4. Build the application: `dotnet build`.
5. Run the application: `dotnet run`.

The backend service will start running on `https://localhost:5001` (or `http://localhost:5000`).

**Note:** This README file provides a brief overview of the backend service. For more details about each endpoint and its functionality, please refer to the source code and the XML documentation comments provided in the project.

For any questions or issues, please contact me at chriskocabas@outlook.com

## Using Visual Studio for Mac

If you are using Visual Studio for Mac, please follow the additional steps below to set up Entity Framework (EF) Tools and run migrations:

### Installing Entity Framework Tools to Support Migrations and More

1. Install `dotnet-ef` by running the following command in a terminal:
dotnet tool install --global dotnet-ef


2. If you need to install a specific version of the tool, use the following command:
dotnet tool install --global dotnet-ef --version 3.1.4


3. Add the "dotnet-ef" tools directory to the PATH environment variable. Replace `'your user folder'` with your actual user folder:
export PATH="$PATH:/Users/'your user folder'/.dotnet/tools"


4. Open a command line terminal, navigate to the project folder, and run the following command to restore dependencies:
dotnet restore


5. If the restoration process completes successfully, you should be able to run the following command to verify the installation:
dotnet ef


### Running Migrations and Seeding Data

To create the initial migration and prepare the database, run the following commands:
dotnet ef migrations add initial
dotnet ef database update


After these steps are successful, you can populate your database by running the following command:
dotnet run seeddata


**Note:** If you encounter any issues, run the `dotnet build` command to check for detailed error messages.


For any questions or issues, please contact chriskocabas@outlook.com
