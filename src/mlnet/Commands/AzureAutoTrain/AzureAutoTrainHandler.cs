// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.IO;

namespace Microsoft.ML.CLI.Commands
{
    internal static class AzureAutoTrainHandler
    {
        public const string AzureAutoTrainCommandName = "azure-auto-train";

        internal static System.CommandLine.Command AzureAutoTrain(ICommandHandler handler)
        {
            var newCommand = new System.CommandLine.Command(AzureAutoTrainCommandName, "Create a new .NET project using Azure AutoML to train and run a model", handler: handler)
            {
                MlTask(),
                TrainFile(),
                LabelName(),
                MaxExplorationTime(),
                //IgnoreColumns(),
                Verbosity(),
                Name(),
                OutputPath(),
                Workspace(),
                ComputeTarget(),
                SubscriptionId(),
                ResourceGroup(),
                Experiment(),
            };

            newCommand.Argument.AddValidator((sym) =>
            {
                if (!sym.Children.Contains("--trainfile"))
                {
                    return "Option required : --trainfile";
                }
                if (!sym.Children.Contains("--ml-task"))
                {
                    return "Option required : --ml-task";
                }
                if (!sym.Children.Contains("--label-column-name"))
                {
                    return "Option required : --label-column-name";
                }

                return null;
            });

            return newCommand;

            Option TrainFile() =>
                new Option(new List<string>() { "--trainfile", "-t" }, "File name in the blob container to train with.",
                new Argument<string>());

            Option MlTask() =>
                new Option(new List<string>() { "--ml-task", "--mltask", "--task", "-T" }, "Type of ML task to perform. Current supported tasks: regression, binary-classification, multiclass-classification.",
                new Argument<string>().FromAmong(GetMlTaskSuggestions()));

            Option LabelName() =>
                new Option(new List<string>() { "--label-column-name", "-n" }, "Name of the label (target) column to predict.",
                new Argument<string>());

            // TODO: not possible to support for now because ONNX is broken for header-less CSV
            //Option LabelColumnIndex() =>
            //    new Option(new List<string>() { "--label-column-index", "-i" }, "Index of the label (target) column to predict.",
            //    new Argument<uint>());

            Option MaxExplorationTime() =>
                new Option(new List<string>() { "--max-exploration-time", "-x" }, "Maximum time in seconds for exploring models with best configuration.",
                new Argument<uint>(defaultValue: 60*30));

            Option Verbosity() =>
                new Option(new List<string>() { "--verbosity", "-V" }, "Output verbosity choices: q[uiet], m[inimal] (by default) and diag[nostic].",
                new Argument<string>(defaultValue: "m").FromAmong(GetVerbositySuggestions()));

            Option Name() =>
                new Option(new List<string>() { "--name", "-N" }, "Name for the output project or solution to create. ",
                new Argument<string>());

            Option OutputPath() =>
                new Option(new List<string>() { "--output-path", "-o" }, "Location folder to place the generated output. The default is the current directory.",
                new Argument<DirectoryInfo>(defaultValue: new DirectoryInfo(".")));

            //Option HasHeader() =>
            //    new Option(new List<string>() { "--has-header", "-H" }, "Specify true/false depending if the dataset file(s) have a header row.",
            //    new Argument<bool>(defaultValue: true));

            // This is a temporary hack to work around having comma separated values for argument. This feature needs to be enabled in the parser itself.
            //Option IgnoreColumns() =>
            //    new Option(new List<string>() { "--ignore-columns", "-I" }, "Specify the columns that needs to be ignored in the given dataset.",
            //    new Argument<List<string>>(symbolResult =>
            //    {
            //        try
            //        {
            //            List<string> valuesList = new List<string>();
            //            foreach (var argument in symbolResult.Arguments)
            //            {
            //                if (!string.IsNullOrWhiteSpace(argument))
            //                {
            //                    var values = argument.Split(",", StringSplitOptions.RemoveEmptyEntries);
            //                    valuesList.AddRange(values);
            //                }
            //            }
            //            if (valuesList.Count > 0)
            //                return ArgumentResult.Success(valuesList);

            //        }
            //        catch (Exception)
            //        {
            //            return ArgumentResult.Failure($"Unknown exception occured while parsing argument for --ignore-columns :{string.Join(' ', symbolResult.Arguments.ToArray())}");
            //        }

            //        //This shouldn't be hit.
            //        return ArgumentResult.Failure($"Unknown error while parsing argument for --ignore-columns");
            //    })
            //    {
            //        Arity = ArgumentArity.OneOrMore,
            //    });

            Option Workspace() =>
                new Option(new List<string>() { "--workspace", "-w" }, "todo.",
                new Argument<string>());

            Option ComputeTarget() =>
                new Option(new List<string>() { "--computetarget", "-C" }, "todo.",
                new Argument<string>());

            Option SubscriptionId() =>
                          new Option(new List<string>() { "--subscriptionId", "-s" }, "Azure subscription to use.",
                          new Argument<string>());

            Option ResourceGroup() =>
                          new Option(new List<string>() { "--resourceGroup", "-r" }, "todo.",
                          new Argument<string>());

            Option Experiment() =>
                          new Option(new List<string>() { "--experiment", "-e" }, "Existing experiment name to run AutoML in.",
                          new Argument<string>());
        }

        private static string[] GetMlTaskSuggestions()
        {
            return new[] { "classification", "regression" };
        }

        private static string[] GetVerbositySuggestions()
        {
            return new[] { "q", "m", "diag" };
        }

        private static string[] GetCacheSuggestions()
        {
            return new[] { "on", "off", "auto" };
        }
    }
}
