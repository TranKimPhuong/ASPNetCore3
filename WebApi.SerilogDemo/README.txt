Follow to https://devblogs.microsoft.com/aspnet/asp-net-core-logging/
and https://stackoverflow.com/questions/57272654/inject-serilogs-ilogger-interface-in-asp-net-core-web-api-controller

AI ĐÓ NÓI LÀ NẾU CÓ MICROSERVICE THÌ NÊN DÙNG SERILOG

KHỞI TẠO LOG.LOGGER TRONG STARTUP.CS
AND SỬ DỤNG SERIAL LOG IN ANOTHER CLASS

Serilog Demo for WebApI
Read structure from appsetting.json: 
	Enrichers
	Write to Console



Package List
	Serilog.AspNetCore
	Serilog.Enrichers.Environment
	Serilog.Enrichers.Thread
	Serilog.Enrichers.Process

Changes on
	Program.cs
	Startup.cs
	appsetting.json

TODO: WriteTo.AzureTableStorage coi sau bên WebApi.conversion4