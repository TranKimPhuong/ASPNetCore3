using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using FormFile = System.Collections.Generic.KeyValuePair<string, byte[]>;
using FileDataCollection = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, byte[]>>;

namespace System.Net.Http
{
    public sealed class MultipartFormDataMemoryStreamProvider : MultipartMemoryStreamProvider
    {
        public NameValueCollection FormData { get; private set; }
        public FileDataCollection FileData { get; private set; }
        CancellationToken _cancellationToken;

        public MultipartFormDataMemoryStreamProvider()
        {
            this.FormData = new NameValueCollection();
            this.FileData = new FileDataCollection();
        }

        public override Task ExecutePostProcessingAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            return this.ExecutePostProcessingAsync();
        }

        public override Task ExecutePostProcessingAsync()
        {
            return ReadFormDataAsync(base.Contents, this.FormData, _cancellationToken);

        }

        private async Task ReadFormDataAsync(Collection<HttpContent> contents, NameValueCollection formData, CancellationToken cancellationToken)
        {
            foreach (HttpContent content in contents)
            {
                ContentDispositionHeaderValue contentDisposition = content.Headers.ContentDisposition;
                if (string.IsNullOrEmpty(contentDisposition.FileName))
                {
                    string name = UnquoteToken(contentDisposition.Name) ?? string.Empty;
                    cancellationToken.ThrowIfCancellationRequested();
                    string formFieldValue = await content.ReadAsStringAsync();
                    formData.Add(name, formFieldValue);
                }
                else
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var fileContent = await content.ReadAsByteArrayAsync();
                    if (fileContent != null)
                    {
                        var fileName = UnquoteToken(contentDisposition.FileName) ?? string.Empty;
                        FileData.Add(new FormFile(fileName, fileContent));
                    }
                }
            }
        }

        private string UnquoteToken(string token)
        {
            if (!string.IsNullOrWhiteSpace(token) && ((token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal)) && (token.Length > 1)))
            {
                return token.Substring(1, token.Length - 2);
            }
            return token;
        }

    }
}
