# Project Naming Rules and Task Assignments

## Naming Rules

- **Private Variables**: \_camelCase (e.g., `_userName`, `_vehicleCost`)
- **Public Variables**: camelCase (e.g., `userName`, `vehicleCost`)
- **Functions**: PascalCase (e.g., `GetCarById()`, `GetUserData()`)
- **Classes**: PascalCase (e.g., `UserData`, `VehicleData`)
- **Classes Properties**: PascalCase (e.g, `UserId`, `AccessRight`)
- **Files in Data Access Layer**: PascalCaseDAL (e.g., `VehicleDAL.cs`, `UserDAL.cs`)
- **Files in Business Layer**: PascalCaseBLL (e.g., `VehicleBLL.cs`, `UserBLL.cs`)
- **Files in Presentation Layer**: PascalCaseController (e.g., `VehicleController.cs`, `UserController.cs`)

## Branch Naming Convention

- `feature/<feature-name>` for new features.
- `bugfix/<bug-name>` for bug fixes.
- `hotfix/<hotfix-name>` for critical fixes.

## Task Assignments

- **Hamdy**:

  - **Level 1**: ApplicationTypes, Users.
  - **Level 2**: Applications.
  - **Level 3**: JobOrder.
  - **Level 4**: Maintenance, Missions.
  - **Level 5**: MissionsJobOrders, MissionsVehicles.

  ##

- **Abd-Elrahman**:

  - **Level 1**: Vehicles.
  - **Level 2**: Buses, Drivers.
  - **Level 3**: Employees, Patrols, Vacations, MaintenanceApplications, MissionsNotes.

  ##

- **Kamal**:
  - **Level 1**: VehicleConsumables, SpareParts.
  - **Level 3**: ConsumablesPurchaseOrders, ConsumablesWithdrawApplications, SparePartsPurchaseOrders, SparePartsWithdrawApplications.
  - **Level 5**: ConsumablesReplacements, SparePartsReplacements.

## General Guidelines

- Follow clean code practices.
- Write meaningful names.
- Write clear and descriptive commit messages.
- Don't merge your code with master unless it was reviewed.
