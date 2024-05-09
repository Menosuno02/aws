using Amazon.S3;
using Amazon.S3.Model;

namespace MvcCoreAWSS3.Services
{
    public class ServiceStorageS3
    {

        // VAMOS A RECIBIR EL NOMBRE DEL BUCKET A
        // PARTIR DE APPSETTINGS.JSON
        private string BucketName;

        // LA CLASE/INTERFACE PARA LOS BUCKETS
        // SE LLAMA IAmazonS3 Y TAMBIÉN LA VAMOS
        // A RECIBIR MEDIANTE INYECCIÓN
        private IAmazonS3 ClientS3;

        public ServiceStorageS3(IConfiguration configuration, IAmazonS3 clientS3)
        {
            this.ClientS3 = clientS3;
            this.BucketName = configuration.GetValue<string>("AWS:BucketName");
        }

        // COMENZAMOS SUBIENDO UN FICHERO AL BUCKET
        // NECESITAMOS EL NOMBRE, STREAM
        public async Task<bool> UploadFileAsync(string fileName, Stream stream)
        {
            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = this.BucketName,
                Key = fileName,
                InputStream = stream
            };
            // PARA EJECUTARLO, DEBEMOS HACER UNA
            // PETICIÓN AL CLIENT S3 Y NOS DEVOLVERÁ UN
            // RESPONSE DEL MISMO TIPO QUE EL REQUEST
            PutObjectResponse response = await this.ClientS3.PutObjectAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        // MÉTODO PARA ELIMINAR FICHERO DEL BUCKET
        public async Task<bool> DeleteFileAsync(string fileName)
        {
            // PODEMOS TAMBIÉN HACER PETICIONES SIN
            // NECESIDAD DE REQUESR
            DeleteObjectResponse response = await this.ClientS3.DeleteObjectAsync(this.BucketName, fileName);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        // PARA RECUPERAR TODOS LOS FICHEROS (URLS) SE
        // REALIZA MEDIANTE VERSIONES. AUNQUE NO TENGAMOS
        // HABILITADO EL CONTROL DE VERSIONES, LAS Keys
        // SIEMPRE VAN POR VERSION
        public async Task<List<string>> GetVersionsFileAsync()
        {
            // PRIMERO RECUPERAMOS UNA RESPUESTA CON TODAS
            // LAS VERSIONES A PARTIR DE UN BUCKET
            ListVersionsResponse response = await this.ClientS3.ListVersionsAsync(this.BucketName);
            // EXTRAEMOS TODAS LAS KEYS DE NUESTROS FICHEROS
            List<string> keyFiles = response.Versions
                .Select(x => x.Key).ToList();
            return keyFiles;
        }

        public async Task<Stream> GetFileAsync(string fileName)
        {
            GetObjectResponse response = await
                this.ClientS3.GetObjectAsync
                (this.BucketName, fileName);
            return response.ResponseStream;
        }
    }
}
