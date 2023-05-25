using System;
using CommandLine;

namespace Generator.UseCases.Abstract
{
    internal abstract class BaseUseCase<TOptions, TResult> : IUseCase<TOptions>
    {
        public virtual void Execute(TOptions option)
        {
            Console.WriteLine($"Starting [{GetCommandName()}] ..");
            
            var result = ExecuteBusinessLogic(option);
            
            Console.WriteLine(".. DONE");

            HandleResult(result);
        }

        public abstract TResult ExecuteBusinessLogic(TOptions options);
        
        public abstract void HandleResult(TResult result);
        
        private string GetCommandName()
        {
            var attr = Attribute.GetCustomAttribute(typeof(TOptions), typeof(VerbAttribute));

            if (attr != null)
            {
                var cr = (VerbAttribute)attr;
                return cr.HelpText;
            }

            return string.Empty;

        }
    }
}