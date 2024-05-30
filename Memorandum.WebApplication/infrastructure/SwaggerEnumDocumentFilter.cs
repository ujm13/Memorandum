using Memorandum.Common;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Memorandum.WebApplication.infrastructure
{
    public class SwaggerEnumDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // add enum descriptions to result models
            foreach (var property in swaggerDoc.Components.Schemas)
            {
                var propertyEnums = property.Value.Enum;
                if (propertyEnums is { Count: > 0 })
                {
                    property.Value.Description += DescribeEnum(propertyEnums, property.Key);
                }
            }

            if (swaggerDoc.Paths.Count <= 0)
            {
                return;
            }

            // add enum descriptions to input parameters
            foreach (var pathItem in swaggerDoc.Paths.Values)
            {
                DescribeEnumParameters(pathItem.Parameters);

                var affectedOperations = new List<OperationType>
                { OperationType.Get, OperationType.Post, OperationType.Put, OperationType.Patch };

                foreach (var operation in pathItem.Operations)
                {
                    if (affectedOperations.Contains(operation.Key))
                    {
                        DescribeEnumParameters(operation.Value.Parameters);
                    }
                }
            }
        }

        private static void DescribeEnumParameters(IList<OpenApiParameter>? parameters)
        {
            if (parameters is null)
            {
                return;
            }

            foreach (var param in parameters)
            {
                if (param.Schema.Reference != null)
                {
                    var enumType = GetEnumTypeByName(param.Schema.Reference.Id);
                    var names = Enum.GetNames(enumType!).ToList();

                    param.Description += string.Join(", ", names.Select(name =>
                    {
                        var @enum = (Enum)Enum.Parse(enumType!, name);
                        return $"{Convert.ToInt32(@enum)} - {@enum.GetDescription()}";
                    }).ToList());
                }
            }
        }

        private static Type? GetEnumTypeByName(string enumTypeName)
        {
            if (string.IsNullOrEmpty(enumTypeName))
            {
                return null;
            }

            try
            {
                return AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .Single(x => x.FullName != null
                                 && x.Name == enumTypeName);
            }
            catch (InvalidOperationException e)
            {
                throw new Exception(
                    $"SwaggerDoc: Can not find a unique Enum for specified typeName '{enumTypeName}'. Please provide a more unique enum name.");
            }
        }

        private static string? DescribeEnum(IEnumerable<IOpenApiAny> enums, string propertyTypeName)
        {
            var enumType = GetEnumTypeByName(propertyTypeName);

            if (enumType is null)
            {
                return null;
            }

            var parsedEnums = new List<OpenApiInteger>();
            foreach (var @enum in enums)
            {
                if (@enum is OpenApiInteger enumInt)
                {
                    parsedEnums.Add(enumInt);
                }
            }

            return string.Join(", ",
                parsedEnums.Select(x => $"{x.Value} - {((Enum)Enum.ToObject(enumType, x.Value)).GetDescription()}"));
        }
    }
}
