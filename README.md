# Roast Hive

An e-commerce website that sells different kinds of coffee from around the world

This is an application built on .NET CORE MVC which provides a user-friendly and visually appealing interface, as well as integrates essential features for managing products, and customers accounts.

### Prerequisites 
* HTML
* CSS
* JavaScript
* C#
* SQL 
* .NET ( [Download Link](https://dotnet.microsoft.com/en-us/download) )
* IDE 
* Command Line

## Installation
1. Fork and clone the repository:
```
git clone https://github.com/FruitbatM/roast-hive
```
OR

Download the zipped file

2. Navigate to the project directory.

```
cd project-file-path 
```
3. Build and run the application:

```
dotnet build
dotnet run
```
3. Access the application in your web browser at using the URL provided in the terminal.

## Users and Permissions

Users can register online. However, permissions will need to be modified using SQL queries. Having an admintrative role will allow a user to manage the products online.

To assign a user an adminitrator role:

1. Right click on the database RoastHiveApiMVC.db in Visual Code and choose open. 

2. Locate the AspNetRoles table and right-click it.

3. Select the New Query [Insert] option and insert the following code in the editor:
```
INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES ("ID Value", "Administrator", "ADMINISTRATOR", "ConcurrencyStamp Value");
```
4. Run query.

5. To assign the admin role, the user account id is first needed. Run the following query:
```
SELECT Id FROM AspNetUsers
WEHERE UserName = '<EMAIL ADDRESS USED!>'
```

6. Copy the ID and then replace the <USER ID> with the correct value in the query below: 
```
INSERT INTO AspNetUserRoles(UserId, RoleId)
VALUES ("<USER ID>", "72c79f64-d8b1-4ed1-9d22-1e51acc01f36")
```


