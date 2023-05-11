using Library.Config;
using Library.Types;
using Library.Utilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.FileProcessors;

internal class DataSource : IDataSource
{
    private string path = String.Empty;
    private readonly ILogger _logger;

    private readonly IJSONProcessor _jsonProcessor;
    private readonly ICSVProcessor _csvProcessor;
    public DataSource(Configuration config, ILogger<DataSource> logger, IJSONProcessor jsonProccessor, ICSVProcessor csvProcessor)
    {
        path = config.sourcePath;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _jsonProcessor = jsonProccessor;
        _csvProcessor = csvProcessor;
    }
    public List<Book> ReadListOfBooks()
    {
        List<Book> books = new List<Book>();

        try
        {
            var exists = File.Exists(path);
            if (!exists) { throw new FileNotFoundException(); }
            else
            {
                string[] importedFile = File.ReadAllLines(path);

                string extension = Path.GetExtension(path);


                //read csv
                if (extension.Equals(".csv"))
                {

                    books = _csvProcessor.ReadCSV();

                    _logger.LogInformation("CSV Read successfully...");


                    return books;
                }
                //read json
                else if (extension.Equals(".json"))
                {

                    books = _jsonProcessor.ReadJSON();

                    _logger.LogInformation("JSON Read successfully...");


                    return books;
                }
                else
                {
                    throw new FormatException();
                }
            }



        }
        catch (FileNotFoundException fnfe)
        {
            _logger.LogError("File not found");
            _logger.LogTrace(fnfe.StackTrace);
            return new List<Book>();

        }
        catch (FormatException fe)
        {
            _logger.LogError("Format not recognized");
            _logger.LogTrace(fe.StackTrace);
            return new List<Book>();
        }
        catch (Exception ex)
        {
            _logger.LogError("Unexpected error while reading data...");
            _logger.LogTrace(ex.StackTrace);
            return new List<Book>();
        }


    }
}
