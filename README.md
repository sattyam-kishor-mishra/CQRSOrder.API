"# CQRSOrder.API" 

Execute the EF command

dotnet ef migrations add CreatePaymentTables --project OrderAPI --startup-project OrderAPI
dotnet ef database update --project OrderAPI

