using McMaster.Extensions.CommandLineUtils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.CLI
{
    [Command(Name = "get", Description = "查找接口关键字")]
    [HelpOption("-h|--help")]
    class Program
    {
        static Task<int> Main(string[] args)
        {
            return CommandLineApplication.ExecuteAsync<Program>(args);
        }

        [Option(CommandOptionType.SingleValue, Description = "关键字", ShortName = "k", LongName = "keyword")]
        private string? Keyword { get; }

        private async Task<int> OnExecuteAsync(CommandLineApplication app, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(Keyword))
            {
                app.ShowHelp();
                return 0;
            }

            //TODO:制作接口

            Console.WriteLine("接口："+Keyword);
            return 1;
        }
    }
}
