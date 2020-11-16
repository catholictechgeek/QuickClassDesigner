using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;
using System.Composition.Hosting;
using Microsoft.CodeAnalysis.Workspaces;

namespace QuickClassDesigner
{
    public static class ValidateHelper
{
        private static DocumentInfo scriptDocumentInfo;
        private static CompletionService service;
        private static Document scriptDocument;
        //private static SourceText scriptCode;
        private static AdhocWorkspace workspace;
        private static ProjectId project;
        private static DocumentId lastid;

        static ValidateHelper()
        {
            //we have to build the trees of namespaces and classes for easy access for searching
            var host = MefHostServices.Create(MefHostServices.DefaultAssemblies);
            workspace = new AdhocWorkspace(host);
            /*
            var projectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Create(), "MyProject", "MyProject", LanguageNames.CSharp);
            var project = workspace.AddProject(projectInfo);
            var document = workspace.AddDocument(project.Id, "MyFile.cs", SourceText.From(""));
            */


            //var scriptCode = "Guid.N";

            var compilationOptions = new CSharpCompilationOptions(
               OutputKind.DynamicallyLinkedLibrary,
               usings: new[] { "System", "System.Collections.Generic", "System.Text" });

            var scriptProjectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Create(), "Script", "Script", LanguageNames.CSharp, isSubmission: false)
               .WithMetadataReferences(new[]
               {
       MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
               })
               .WithCompilationOptions(compilationOptions);

            var scriptProject = workspace.AddProject(scriptProjectInfo);
            project = scriptProject.Id;
            /*
            scriptDocumentInfo = DocumentInfo.Create(
                DocumentId.CreateNewId(scriptProject.Id), "Script",
                sourceCodeKind: SourceCodeKind.Script,
                loader: TextLoader.From(TextAndVersion.Create(SourceText.From(scriptCode), VersionStamp.Create())));
            scriptDocument = workspace.AddDocument(scriptDocumentInfo);
            */


            // cursor position is at the end
            //var position = scriptCode.Length - 1;

            //var completionService = CompletionService.GetService(scriptDocument);

            //service = CompletionService.GetService(scriptDocument);

            //var results = await completionService.GetCompletionsAsync(scriptDocument, position);
        }

        private static void replaceDocument(DocumentInfo newdoc)
        {
            if(lastid != null)
            {
                workspace.CloseDocument(lastid);
            }
            scriptDocument = workspace.AddDocument(scriptDocumentInfo);
            lastid = newdoc.Id;
        }

        private static void prepareScript(string code)
        {
            scriptDocumentInfo = DocumentInfo.Create(
                DocumentId.CreateNewId(project), "Script",
                sourceCodeKind: SourceCodeKind.Script,
                loader: TextLoader.From(TextAndVersion.Create(SourceText.From(code), VersionStamp.Create())));
            replaceDocument(scriptDocumentInfo);
            service = CompletionService.GetService(scriptDocument);
        }

        public static Task<bool> validateNamespace(string s)
        {
            //var namespaces = Assembly.GetAssembly(typeof(List<>)).GetModules();
            //return false;
            var position = s.Length - 1;
            //scriptCode = SourceText.From(s);
            return service.GetCompletionsAsync(scriptDocument, position).ContinueWith(f =>
            {

                return false;
            });
        }

        public static bool validateClassOrInterface(string s)
        {
            return (System.Reflection.TypeInfo.GetType(s) != null);
        }

        public static bool validateInterfaces(string s)
        {
            var splits = s.Trim().Split(',');
            var valid = false;
            for(int i = 0; i < splits.Length; i++)
            {
                valid = validateClassOrInterface(s);
            }
            return valid;
        }

        public static bool validateAccessModifier(AccessLevel property, AccessLevel @class)
        {
            if(property > @class)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Task<System.Collections.Immutable.ImmutableArray<CompletionItem>> getSuggestions(string s)
        {
            var position = s.Length - 1;
            //scriptCode = SourceText.From(s);
            prepareScript(s);
            return service.GetCompletionsAsync(scriptDocument, position).ContinueWith(f =>
            {
                var returning = f.Result.Items;
                //returing[0].DisplayText
                return service.FilterItems(scriptDocument, returning, s);
            });
        }

        /*
        public static string getFullTypeName(Type t)
        {
            string fullname = t.Name;

            if(t.IsGenericType)
            {
                
            }
            return fullname;
        }
        */

        
}
}
