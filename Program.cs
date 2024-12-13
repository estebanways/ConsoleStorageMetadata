using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;

namespace ConsoleStorageMetadata
{
    internal class Program
    {
        private const string blobServiceEndpoint = "";
        private const string storageAccountName = "";
        private const string storageAccountKey = "";
        static void Main(string[] args)
        {
            StorageSharedKeyCredential accountCredentials = new 
                       StorageSharedKeyCredential(storageAccountName, storageAccountKey);

            BlobServiceClient serviceClient = new BlobServiceClient(new Uri(blobServiceEndpoint), accountCredentials);

            // Consultar la metadata del Storage
            AccountInfo info = serviceClient.GetAccountInfo();

            Console.WriteLine("Nombre de la cuenta " + storageAccountName);
            Console.WriteLine("Tipo de la cuenta " + info.AccountKind);
            Console.WriteLine("Sku de la cuenta " + info.SkuName);

            // Enumerar los containers de la cuenta
            BlobContainerClient containerClient;

            foreach (BlobContainerItem container in serviceClient.GetBlobContainers())
            {
                Console.WriteLine("Container: " + container.Name);
                containerClient = serviceClient.GetBlobContainerClient(container.Name);
                foreach (BlobItem blob in containerClient.GetBlobs())
                {
                    Console.WriteLine("Blob: " + blob.Name);
                }
            }
            containerClient = serviceClient.GetBlobContainerClient("vector-graphics");
            //containerClient.CreateIfNotExists();
            BlobClient blobClient = containerClient.GetBlobClient("graph.svg");
            Console.WriteLine("Blob Name: " + blobClient.Name);
            Console.WriteLine("Blob URI: " + blobClient.Uri);

        }
    }
}
