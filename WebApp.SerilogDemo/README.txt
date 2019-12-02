Follow to https://www.youtube.com/watch?v=_iryZxv8Rxw

AI ĐÓ NÓI LÀ NẾU CÓ MICROSERVICE THÌ NÊN DÙNG SERILOG

KHỞI TẠO LOG.LOGGER TRONG PROGRAME.CS

Serilog Demo for WebApp
Read structure from appsetting.json: 
	Enrichers
	Write to Console
	Write to File: text/JSon
	Write to Seq: docker  ???
	Write to AzureTableStorage coi WebApi.Conversion4
		refer to https://stackoverflow.com/questions/49802904/log-structured-data-to-azure-storage-table-by-serilog-store-all-object-in-render
				https://stackoverflow.com/questions/43834082/how-can-i-read-logs-written-to-azure-table-storage-with-serilog


Package List
	Serilog.AspNetCore
	//Enrich Your Logs With More Context
	//https://dzone.com/articles/serilog-tutorial-for-net-logging-16-best-practices
	Serilog.Enrichers.Environment
	Serilog.Enrichers.Thread
	Serilog.Enrichers.Process

Changes on
	Program.cs
	Startup.cs
	appsetting.json

=> THE SAME FOR WEBAPI