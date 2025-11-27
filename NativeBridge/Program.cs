using System;
using System.IO;
using System.Text;
using System.Text.Json;

class NativeBridge
{
    static void Main()
    {
        var stdin = Console.OpenStandardInput();
        var stdout = Console.OpenStandardOutput();

        while (true)
        {
            try
            {
                byte[] lengthBytes = new byte[4];
                int bytesRead = stdin.Read(lengthBytes, 0, 4);
                if (bytesRead == 0) break;

                int length = BitConverter.ToInt32(lengthBytes, 0);
                byte[] buffer = new byte[length];
                stdin.Read(buffer, 0, length);

                string inputJson = Encoding.UTF8.GetString(buffer);
                Console.Error.WriteLine($"[NativeBridge] Received: {inputJson}");

                var responseObj = new { status = "received", echo = inputJson };
                string responseJson = JsonSerializer.Serialize(responseObj);
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseJson);
                byte[] responseLength = BitConverter.GetBytes(responseBytes.Length);

                stdout.Write(responseLength, 0, 4);
                stdout.Write(responseBytes, 0, responseBytes.Length);
                stdout.Flush();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[NativeBridge] Error: {ex.Message}");
                break;
            }
        }
    }
}
