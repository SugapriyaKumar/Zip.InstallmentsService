# Zip.InstallmentsService

Built on .NET 6
Language - C#
19-Dec-2022

**UI - Zip.Installments.App (MVC)**
Objective : User can input total order amuont and order date, click on calculate installment to calculate the installment amount and due date.
Integration : UI integrates to API via API Client
![Screenshot (7)](https://user-images.githubusercontent.com/91588590/208357106-a5e1f050-117e-4e98-971b-3f291d7993c6.png)

![Screenshot (8)](https://user-images.githubusercontent.com/91588590/208357079-c2b5c562-05a2-4ae3-a507-ddf5acda127d.png)

![Screenshot (9)](https://user-images.githubusercontent.com/91588590/208357059-974dec44-e663-418a-af98-94a9b92af764.png)



**API - Zip.InstallmentsService.API**
Objective : To send appropriate HTTP error code, response based on the request
Integration: API connects to Business Logic layer where internal complexity is hidden. (Applied Facade Design Pattern)
![Screenshot (11)](https://user-images.githubusercontent.com/91588590/208357179-4498fd9e-a8de-477a-b023-8696683bed03.png)


**Business Logic layer - Zip.InstallmentsService.BusinessLogic**
Objective : To determine the logic to compute the installment as per the requirement 
Integration : It can send the data to SQL DB via repository

**Repositary - Zip.installmentsService.Repo**
Followed Code first approach.
All the database communication enabling entities and DB Context are present in this layer. (Applied Respository pattern)
![Screenshot (12)](https://user-images.githubusercontent.com/91588590/208357200-f4d39216-a5dd-4404-a920-b16aa53a61ad.png)

**Unit Tests - Zip.InstallmentsService.Test**
Unit Tests with Shouldly (recommended from the boiler plate code) and Xunits

#Guidelines to run the app in local box

1. Fork/clone/Download the solution as zip
2. Open Zip.InstallmentsService.sln in Visual Studio (Recommended VS 2022, as the solution is built on .NET 6)
3. Make sure the **startup projects are** set as **Zip.Installments.App and Zip.InstallmentsService.API**
4. Ensure the connection strings (can be found in App settings of API) are setup appropriately, if DB entries are needed
5. DB Migrations can be executed, if needed
