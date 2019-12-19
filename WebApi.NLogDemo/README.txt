Follow to https://medium.com/@jocapc/configuring-nlog-in-asp-net-core-web-app-59d1ab3697de
	and https://www.codeproject.com/Questions/4386550/Why-my-logs-file-not-creating-on-azure-blob-storag

Install packages
	NLog.Web.AspNetCore
	NLog.Extensions.AzureStorage

=> Ra file + console => ok
=>	Emulator : ko support
	Container bt : ko có tableName vì dang xài NLog.Extensions.AzureStorage 
				   not NLog.Extensions.AzureTableStorage