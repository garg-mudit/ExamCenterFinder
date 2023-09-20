# Exam Center Finder

## Project Notes


1. **Application TimeZone**: The application operates within the UTC TimeZone.

2. **Docker Setup**: Docker setup has not been completed due to a lack of experience. Consequently, individual setup of the SQL Server is required.

3. **Potential Improvements**:
   - Implementing Fluent Validation.
   - Adopting a Clean Architecture approach.
   - Implementation of global exception handling.

4. **User Type for Endpoint**: It's not entirely clear whether the endpoint's intended user is a corporate exam vendor like Microsoft or an individual user. This distinction is important because:
   - If it's a vendor **(as assumed in my application)**, they reserve the entire capacity for a given time slot.
   - However, if it's an individual user, they only reserve a single capacity.


## Running the .NET API

To run the .NET API, follow these steps:

1. **Prerequisites**: Ensure you have the following prerequisites installed on your system:
   - [.NET 7 SDK](https://dotnet.microsoft.com/download) for building and running .NET applications.

2. **Clone the Repository**: Clone the Git repository to your local machine using the `git clone` command.

3. **Configure SQL Server Connection String**: Before building the API, open the `appsettings.json` file located inside `ExamCenterFinder\ExamCenterFinder.API` and update the SQL Server connection string to match your local SQL Server setup.

4. **Navigate to the Project Directory**: Open a terminal or command prompt and navigate to the directory containing your .NET API project.

5. **Build the Project**: Run the following command to build the project:

```shell
dotnet build
```

6. **Run the API**: Once the build is successful, run the API with the following command:
```shell
dotnet run --project ExamCenterFinder.API --launch-profile "https"
```

7. **Access the API with Swagger**: Since you already have Swagger configured, you can utilize it to evaluate the API endpoints. Open your internet browser and navigate to this URL: [https://localhost:7210/swagger/index.html](https://localhost:7210/swagger/index.html)

These instructions should help you get your .NET API up and running for development and testing purposes.
