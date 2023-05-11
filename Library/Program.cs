using Library.Config;
using Library.FileProcessors;
using Library.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {

        services.AddSingleton<Configuration>();
        services.AddSingleton<IDataSource, DataSource>();
        services.AddTransient<IJSONProcessor, JSONProcessor>();
        services.AddTransient<ICSVProcessor, CSVProcessor>();
        services.AddTransient<ListOfBooksActions>();

    })
    .Build();

ListOfBooksActions data = host.Services.GetService<ListOfBooksActions>();

data.GroupByAuthor();

