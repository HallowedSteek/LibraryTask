using Library.Config;
using Library.FileProcessors;
using Library.Importing;
using Library.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<Configuration>();
        services.AddTransient<IBooksData, BooksData>();
        services.AddSingleton<ICSVProcessor, CSVProcessor>();
        services.AddSingleton<IJSONProcessor, JSONProcessor>();
        services.AddTransient<BookDataManipulation>();
    })
    .Build();


BookDataManipulation data = host.Services.GetRequiredService<BookDataManipulation>();

data.GroupByAuthor();