﻿namespace CustomWebServer.Infrastructure
{
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    public abstract class Controller 
    {
        public const string DefaultPath = @"{0}\Resources\{1}.html";
        public const string ContentPlaceholder = "{{{content}}}";

        protected Controller()
        {
            this.ViewData = new Dictionary<string, string>
            {
                ["anonymousDisplay"] = "none",
                ["authenticatedDisplay"] = "flex",
                ["showError"] = "none"
            };
        }

        protected abstract string ApplicationDirectory { get; }

        protected IDictionary<string, string> ViewData { get; private set; }
        
        protected IHttpResponse FileViewResponse(string fileName)
        {
            var result = this.ProcessFileHtml(fileName);

            if (this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
                {
                    result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }
            
            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        protected IHttpResponse RedirectResponse(string route)
        {
            return new RedirectResponse(route);
        }

        protected void DisplayError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["error"] = errorMessage;
        }

        protected bool ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(model, context, results, true) == false)
            {
                foreach (var result in results)
                {
                    if (result != ValidationResult.Success)
                    {
                        this.DisplayError(result.ErrorMessage);
                        return false;
                    }
                }
            }

            return true;
        }

        private string ProcessFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath, this.ApplicationDirectory, "layout"));

            var fileHtml = File
                .ReadAllText(string.Format(DefaultPath, this.ApplicationDirectory, fileName));

            var result = layoutHtml.Replace(ContentPlaceholder, fileHtml);

            return result;
        }
    }
}
