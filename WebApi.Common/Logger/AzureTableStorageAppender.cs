using System;
using log4net.Appender;
using log4net.Core;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using WebApi.Common.Helper;
using System.Text;
using System.Globalization;

namespace WebApi.Common.Logger
{
    public class AzureTableStorageAppender : AppenderSkeleton
    {
        public string tblName
        {
            get
            {
                var now = DateTime.UtcNow;
                var TblLog = new StringBuilder(this.PrefixTable);
                TblLog.Append(now.ToString("'Log'yyyyMMdd"));

                return TblLog.ToString();
            }
        }

        private string PrefixTable
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(_PREFIX_LOG_TABLE))
                    {
                        //string prefixTable = AppHelper.ToString(System.Web.Hosting.HostingEnvironment.SiteName);
                        string prefixTable = AppHelper.ToString("SiteName_ko_bit_lay");
                        if (!string.IsNullOrEmpty(prefixTable))
                        {
                            if (prefixTable.StartsWith("WebApi.", StringComparison.OrdinalIgnoreCase)) prefixTable = prefixTable.Replace("WebApi.", "Local");
                            else prefixTable = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(prefixTable);                            
                        }

                        _PREFIX_LOG_TABLE = AppHelper.ToString(prefixTable);
                    }                    
                }
                catch (Exception e) { Console.WriteLine(e); }
                return _PREFIX_LOG_TABLE?.Replace("-", string.Empty);
            }
        }

        public string AzureStorageConnectionString { get; set; }
        private static String _PREFIX_LOG_TABLE = null;

        private static CloudTableClient _tblClient = null;

        private CloudTableClient tblClient
        {
            get
            {
                if (this.AzureStorageConnectionString == null) throw new ApplicationException("Missing the Azure Storage Connection String.");
                if (_tblClient == null)
                {
                    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(this.AzureStorageConnectionString);
                    _tblClient = cloudStorageAccount.CreateCloudTableClient();
                    if (_tblClient == null) throw new ApplicationException("Can't create the log table.");
                }
                return _tblClient;
            }
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            try
            {
                CloudTable tblLog = this.tblClient.GetTableReference(this.tblName);
                tblLog.CreateIfNotExistsAsync();

                var logEntry = new LogEntry
                {
                    Timestamp = loggingEvent.TimeStamp,
                    Message = base.RenderLoggingEvent(loggingEvent),
                    Level = loggingEvent.Level.Name
                };

                TableOperation insertOperation = TableOperation.Insert(logEntry);
                tblLog.ExecuteAsync(insertOperation);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }

    internal class LogEntry : TableEntity
    {
        public LogEntry()
        {
            var now = DateTime.UtcNow;
            PartitionKey = AppHelper.GetIpAddess();//host.HostName;
            RowKey = string.Format("P{0}", DateTime.Now.Ticks);//string.Format("{0:HH:mm:ss.ffffff}-{1}", now, Guid.NewGuid());
        }

        #region Table columns 
        public string Level { get; set; }
        public string Message { get; set; }
        #endregion
    }
}
