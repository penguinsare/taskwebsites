# taskwebsites
1. Steps to create and initialize the database.
  - Check out the source code from the repository.  
  - Run the command to create and start a LocalDb instance: SqlLocalDb create MSSQLLocalDB -s
  - The database is in a file called  websites.mdf in the root folder. Please change the path to the database file in \TaskWebsites\appsettings.Development.json and \TaskWebsites\appsettings.json to match the local path. Alternatively a new database file can be created with the command: 
  sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "CREATE DATABASE websites ON PRIMARY ( NAME=[websites], FILENAME = 'C:\Users\shide\source\repos\TaskWebsites\websites.mdf') LOG ON (NAME=[websites_log], FILENAME = 'C:\Users\shide\source\repos\TaskWebsites\websites_log.ldf');"
  The local paths need to be adjusted. After the database file is created we can run "Update-Database" in the powershell inside Visual Studio to trigger Migrations to create the tables in the database.
2. Steps to prepare the source code to build/run properly
   - Import the solution in Visual Studio. Under \Properties\launchSettings.json you can find the required environment variables and another Visual Studio settings.
Security critical configuration is meant to be imported as environment variables.
   - Choose the Kestrel configuration and run the project. You might need to accept the developer certificate or disable https in the Startup.cs.
   - Example test URL:
  https://localhost:5001/api/websites?page=1&resultsPerPage=1&sortOrder=desc&sortBy=name

3. Additional Requirements

   - The image will be uploaded as binary but will be outputed as JSON containing URL, name and id instead of being downloaded.
   - It's better to save the image on the disk instead of the database.
   - Set a size limit and format for the image using validation attributes.
   - It's not very clear how the password for each website will be used. If the websites are treated like users of the REST API the the password should be hashed.
If the passwords will be used in some automatic login in another platforms
and have to be outputed in clear text to be used for login in these sites then the password should be encrypted with a symmetric encription so that it can be decrypted.
At the end the password should never be persisted in the database in plain text.
   - It's not very clear what (Vertical) categories are but maybe this terminology is in the context of vertical markets with categories like Automotive, Banking, Consumer etc.


