using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using ApiAWSPersonas.Models;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ApiAWSPersonas;

public class Functions
{
    public List<Persona> personasList;

    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Functions()
    {
        this.personasList = new List<Persona>();
        Persona p = new Persona
        {
            IdPersona = 1,
            Nombre = "Adrian",
            Email = "adrian@gmail.com",
            Edad = 20
        };
        this.personasList.Add(p);
        p = new Persona
        {
            IdPersona = 2,
            Nombre = "Paco",
            Email = "paco@gmail.com",
            Edad = 26
        };
        this.personasList.Add(p);
        p = new Persona
        {
            IdPersona = 3,
            Nombre = "Elena",
            Email = "elena@gmail.com",
            Edad = 19
        };
        this.personasList.Add(p);
        p = new Persona
        {
            IdPersona = 4,
            Nombre = "Sofia",
            Email = "sofia@gmail.com",
            Edad = 22
        };
        this.personasList.Add(p);
        p = new Persona
        {
            IdPersona = 5,
            Nombre = "Marta",
            Email = "marta@gmail.com",
            Edad = 31
        };
        this.personasList.Add(p);
    }


    /// <summary>
    /// A Lambda function to respond to HTTP Get methods from API Gateway
    /// </summary>
    /// <remarks>
    /// This uses the <see href="https://github.com/aws/aws-lambda-dotnet/blob/master/Libraries/src/Amazon.Lambda.Annotations/README.md">Lambda Annotations</see> 
    /// programming model to bridge the gap between the Lambda programming model and a more idiomatic .NET model.
    /// 
    /// This automatically handles reading parameters from an APIGatewayProxyRequest
    /// as well as syncing the function definitions to serverless.template each time you build.
    /// 
    /// If you do not wish to use this model and need to manipulate the API Gateway 
    /// objects directly, see the accompanying Readme.md for instructions.
    /// </remarks>
    /// <param name="context">Information about the invocation, function, and execution environment</param>
    /// <returns>The response as an implicit <see cref="APIGatewayProxyResponse"/></returns>
    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/")]
    public IHttpResult Get(ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        string json = JsonConvert.SerializeObject(this.personasList);
        return HttpResults.Ok(json);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/find/{id}")]
    public IHttpResult Find(int id, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        Persona persona = this.personasList[id];
        string json = JsonConvert.SerializeObject(persona);
        return HttpResults.Ok(json);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Post, "/post")]
    public IHttpResult Post([FromBody] Persona persona, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'POST' Request");
        string json = JsonConvert.SerializeObject(persona);
        return HttpResults.Ok(json);
    }
}
