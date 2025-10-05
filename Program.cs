using System.ClientModel;
using Microsoft.Extensions.AI;
using OpenAI.Chat;

IChatClient chatClient = new ChatClient(
     "gpt-4.1-mini",
     new ApiKeyCredential(""),
     new OpenAI.OpenAIClientOptions{Endpoint = new Uri("https://models.github.ai/interface")}
).AsIChatClient();

Console.WriteLine("GPT-4.1-Mini Chat Client- Type 'exit' to quit");
Console.WriteLine();

List<Microsoft.Extensions.AI.ChatMessage> chatHistory = new();

while (true)
{
    Console.Write("User: ");
    string userInput = Console.ReadLine();
    if (string.IsNullOrEmpty(userInput))
    {

        continue;
    }
    if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }
    chatHistory.Add(new Microsoft.Extensions.AI.ChatMessage(ChatRole.User, userInput));

    Console.WriteLine("GPT-4.1-Mini is typing...");
    string assistantResponse = string.Empty;

    await foreach (var response in chatClient.GetStreamingResponseAsync(chatHistory))
    {
        Console.Write(response.Text);
        assistantResponse += response.Text;
    }
    chatHistory.Add(new Microsoft.Extensions.AI.ChatMessage(ChatRole.Assistant, assistantResponse));
    Console.WriteLine(); 
}
