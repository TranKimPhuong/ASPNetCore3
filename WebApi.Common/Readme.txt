KO DùNG TRêN ASPNET CORE
- API Controller change to ControllerBase
- Delete System.Web.Http namespace
- IHttpActionResult change to IActionResult

1. MODELS
- class cần xóa : VendorAddress.cs
- install packages: Newtonsoft.Json
					Microsoft.AspNetCore.Http

2. CONFIG
- install packages: Microsoft.AspNetCore.Mvc
					log4net
					Microsoft.AspNetCore.Mvc.Core
					Microsoft.AspNetCore.Mvc.Core.Formatter.Json
					Microsoft.AspNetCore.Mvc.ViewFeatures
- AuthenticationFailureResult
	change IHttpActionResult to IActionResult

- CustomHandleErrorFilter (KO THẤy XÀI Ở ĐÂU)
	Change HandleErrorAttribute to ExceptionFilterAttribute

- MultipartFormDataMemoryStreamProvider (DON'T USE)
	Asp.Net Core as support for multipart file uploads built in. 
	The model binding component will make this available when you have a List<IFormFile> parameter

- NoCacheGlobalActionFilter (KO THẤy XÀI Ở ĐÂU)

- WebApiExceptionFilter
	Change ExceptionFilterAttribute to ActionFilterAttribute

- WebApiFilter  => EXCLUDE RỒI KHI NÀO XÀI THÌ TÍNH
	Change IAuthenticationFilter to ??? (VẪN XÀI KO PHẢI NET CORE)
	Microsoft.AspNet.WEBAPI.CORE
	Microsoft.AspNet.mvc

3. HELPER
- AESHelper : decrypt and encrypt
- AppHelper
- BaseDao
	install System.Data.SQLClient
- BlobHelper 
	install WindowAzure.Storage
- ConfigHelper
	install System.Configuration.ConfigurationManager
- MessageHelper
	install Microsoft.Azure.ServiceBus

4. EXTENSIONS
5. KEYVAULT
install Microsoft.Azure.KeyVault

6. EMAIL
install SendGrid

7.LOGGER
	install Microsoft.AspNetCore.Hosting
			Microsoft.AspNetCore.Hosting.Abstraction

	sau khi install vẫn ko tìm ra Sitename
	=> coi lại làm change System.Web.Hosting.HostingEnvironment