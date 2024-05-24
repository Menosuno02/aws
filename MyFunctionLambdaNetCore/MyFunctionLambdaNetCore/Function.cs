using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using MyFunctionLambdaNetCore.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Net;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MyFunctionLambdaNetCore;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public string FunctionHandler(ModeloUpdate model, ILambdaContext context)
    {
        if (model.IdEmpleado == null)
        {
            return JsonConvert.SerializeObject(new
            {
                status = 400,
                mensaje = "Debe proporcionar un Id de Empleado"
            });
        }

        // Connection string to the SQL database
        string connectionString = @"Data Source=sqlpaco2825.database.windows.net;Initial Catalog=AZURETAJAMAR;User ID=adminsql;Encrypt=True;Password=Admin123";

        string mensaje = "";
        using (SqlConnection cn = new SqlConnection(connectionString))
        {
            cn.Open();
            string sqlUpdate = "UPDATE EMP SET SALARIO = SALARIO + 1 WHERE EMP_NO = @id";
            using (SqlCommand com = new SqlCommand(sqlUpdate, cn))
            {
                com.Parameters.AddWithValue("@id", model.IdEmpleado);
                com.ExecuteNonQuery();
            }

            string sqlSelect = "SELECT * FROM EMP WHERE EMP_NO = @id";
            using (SqlCommand com = new SqlCommand(sqlSelect, cn))
            {
                com.Parameters.AddWithValue("@id", model.IdEmpleado);
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        mensaje = $"El empleado {reader["APELLIDO"]} con oficio {reader["OFICIO"]} ha incrementado su salario a {reader["SALARIO"]}";
                    }
                    else
                    {
                        mensaje = $"No existe el empleado con ID {model.IdEmpleado}";
                    }
                }
            }
        }

        return JsonConvert.SerializeObject(new
        {
            status = 200,
            mensaje = mensaje
        });
    }
}
