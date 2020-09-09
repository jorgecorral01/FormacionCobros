using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Charge.Activity.Service.swagger {
    public class SwaggerFileUploadOperation : IOperationFilter {

        public void Apply(Operation operation, OperationFilterContext context) {
            if(operation.OperationId.ToLower() == "apivaluesuploadput") {
                operation.Parameters.Clear();
                operation.Parameters.Add(new NonBodyParameter {
                    Name = "file",
                    In = "formData",
                    Description = "Upload File",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("multipart/form-data");
            }
        }
    }
}
