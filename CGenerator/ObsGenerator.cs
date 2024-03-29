﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace CGenerator
{
    [Generator]
    public class ObsGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var libraryDirectory = ((SourceFileResolver)((Microsoft.CodeAnalysis.CSharp.CSharpCompilation)context.Compilation).Options.SourceReferenceResolver).BaseDirectory;
            var projectDirectory = Path.Combine(libraryDirectory, "..\\");
            var codeDirectory = Path.Combine(projectDirectory, "obs-studio\\libobs\\");
            var mainHeaderFile = Path.Combine(codeDirectory, "obs.h");

            if (!File.Exists(mainHeaderFile))
            {
                return;
            }

            var headerData = File.ReadAllText(mainHeaderFile).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            headerData = headerData.Where(x => x.StartsWith("EXPORT ")).ToArray();

            Debug.WriteLine(headerData);
//            // begin creating the source we'll inject into the users compilation
//            var sourceBuilder = new StringBuilder(@"
//using System;
//namespace HelloWorldGenerated
//{
//    public static class HelloWorld
//    {
//        public static void SayHello() 
//        {
//            Console.WriteLine(""Hello from generated code!"");
//            Console.WriteLine(""The following syntax trees existed in the compilation that created this program:"");
//");

//            // using the context, get a list of syntax trees in the users compilation
//            var syntaxTrees = context.Compilation.SyntaxTrees;

//            // add the filepath of each tree to the class we're building
//            foreach (SyntaxTree tree in syntaxTrees)
//            {
//                sourceBuilder.AppendLine($@"Console.WriteLine(@"" - {tree.FilePath}"");");
//            }

//            // finish creating the source to inject
//            sourceBuilder.Append(@"
//        }
//    }
//}");

//            // inject the created source into the users compilation
//            context.AddSource("helloWorldGenerator", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif 
        }
    }
}
