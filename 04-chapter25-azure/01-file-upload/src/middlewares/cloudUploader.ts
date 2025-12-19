import { randomUUID } from 'node:crypto';
import type { RequestHandler } from 'express';
import { BlobServiceClient } from '@azure/storage-blob';

const connStr = process.env.AZURE_CONNECTION_STRING;

if (!connStr) {
  console.log('Missing Azure Connection String in .env file?');
  process.exit(1);
}

const blobServiceClient = BlobServiceClient.fromConnectionString(connStr);
const containerName = 'images';
const containerClient = blobServiceClient.getContainerClient(containerName);

// Upload an image
const cloudUploader: RequestHandler = async (req, res, next) => {
  try {
    const filePath = req.image!.filepath;
    const blobName = `${randomUUID()}-${filePath}`;

    const blockBlobClient = containerClient.getBlockBlobClient(blobName);

    await blockBlobClient.uploadFile(filePath);

    const imageUrl = blockBlobClient.url;

    // res.json({ message: 'Testing!' });

    req.body.image = imageUrl;

    next();
  } catch (error) {
    next(error);
  }
};

export default cloudUploader;
