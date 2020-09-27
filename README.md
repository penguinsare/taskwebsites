# taskwebsites
# How to run the project
1. Check out the source code.
2. Run the command to create and start a LocalDb instance: SqlLocalDb create MSSQLLocalDB -s
3. The database is in a file called  websites.mdf in the root folder. Please change the path to the database file in \TaskWebsites\appsettings.Development.json and \TaskWebsites\appsettings.json
to match the local path.
4. Import the solution in Visual Studio. Under \Properties\launchSettings.json you can find the required environment variables and another Visual Studio settings.
Security critical configuration is meant to be importet as environment variables.
5. Choose the Kestrel configuration and run the project. You might need to accept the developer certificate or disable https.
6. Example test URL:
  https://localhost:5001/api/websites?page=1&resultsPerPage=1&sortOrder=desc&sortBy=name
