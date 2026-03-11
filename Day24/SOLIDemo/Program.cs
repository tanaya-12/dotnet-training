﻿using System.Diagnostics.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddScoped<IMessageReader, TwitterMessageReader>();
            services.AddScoped<IMessageWriter, InstagramMessageWriter>();
            services.AddScoped<IMessageWriter, PdfMessageWriter>();
            services.AddScoped<IMyLogger, ConsoleLogger>();
            services.AddScoped<App>();

            var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetService<App>();

            app.Run();

            // Violation of DIP - new keyword in front of custom classes
            //MessageReader _reader = new MessageReader();
            //MessageWriter _writer = new MessageWriter();
            //App _app = new App(_reader, _writer);
            //_app.Run();

            // Console.WriteLine("Hello, World!");
        }
    }

    public class App
    {
        IMessageReader _messageReader;
        IMessageWriter _messageWriter;

        public App(IMessageReader reader, IMessageWriter writer)
        {
            _messageReader = reader;
            _messageWriter = writer;
        }

        public void Run()
        {
            _messageWriter.WriteMessage(_messageReader.ReadMessage());
        }
    }

    // Violation of Interface Segregation Principle
    //public interface IMessagesApp
    //{
    //    string ReadMessage();

    //    void WriteMessage(string message);
    //}
    public interface IMessageReader
    {
        string ReadMessage();
    }

    public class MessageReader : IMessageReader
    {
        public string ReadMessage() => "Hello, World!";
    }

    public class TwitterMessageReader : IMessageReader
    {
        // twitter integration
        public string ReadMessage() => "Hello, From Twitter!";
    }

    public interface IMessageWriter
    {
        void WriteMessage(string message);
    }

    public class MessageWriter : IMessageWriter
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }


    public interface IMyLogger
    {
        void Log();
    }

    public class ConsoleLogger : IMyLogger
    {
        public void Log()
        {
            Console.WriteLine("Entering Console");
        }
    }

    public class InstagramMessageWriter : IMessageWriter
    {
        IMyLogger _logger;
        public InstagramMessageWriter(IMyLogger logger)
        {
            _logger = logger;
        }
        public void WriteMessage(string message)
        {
            _logger.Log();
            Console.WriteLine($"{message} posted to instagram");
        }
    }

    public class PdfMessageWriter : IMessageWriter
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine($"PDF {message}");
        }
    }

    //public class MessagesApp : IMessagesApp
    //{
    //    public string ReadMessage() => "Hello World";
    //    public void WriteMessage(string message) => Console.WriteLine(message
    //}
}