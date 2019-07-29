### Steps
1. dotnet restore
2. dotnet build
3. dotnet run 
4. https://localhost:5001/swagger/index.html

### Known Issues 
- Configurations should transfer to config files
- Api key inside app.configs but it is not correct way to use api key and config; it should not pushed to the repository. For easy testing concerns, i kept key in config; it is already related with beta platform and has limits.If gateway fails to get data, you can use your own key to run example; just replace appsettings and appsettings.Development json files ApiKey value with yours if required.

