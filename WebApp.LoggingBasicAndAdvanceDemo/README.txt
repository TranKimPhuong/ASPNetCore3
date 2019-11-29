Follow to https://www.youtube.com/watch?v=oXNslgIXIbQ
USING MICROSFOT ILLOGER FOR .NET App WITHOUT NOTHER INSTALL PACKAGES

1. create Webapp and run to see log default

2. modify to read infor from appsetting.json
   and override log as my custom in Program.cs by ConfigureLogging

3. Show log from pages/controllers base on default ILogger in index.cshtml.cs
   
4. Add EventID: create a LoggingId class to define EvenID in index.cshtml.cs

5. work with all logging levels

6. Using ILoggerFactory to customize the category Name of log line(advance)

default: WebApp.LoggingBasicAndAdvanceDemo.Pages.IndexModel: Warning: WARNING LOG
change to : DemoCategoryName: Warning: WARNING LOG

or 
default: 
		warn: WebApp.LoggingBasicAndAdvanceDemo.Pages.IndexModel[0] 
			  WARNING LOG
change to : 
		warn: DemoCategoryName[0]
			  WARNING LOG

7. how to log in PROGRAM.cs