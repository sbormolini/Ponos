# aks-todo-app
Pratice app to get involved with CosmosDB and Blazor.

```csharp
public class LogHandler : RequestHandler 
{     
    public override async Task<ResponseMessage> SendAsync(RequestMessage request, CancellationToken cancellationToken) 
    { 
        Console.WriteLine($"[{request.Method.Method}]\t{request.RequestUri}"); 
        ResponseMessage response = await base.SendAsync(request, cancellationToken); 
        
        Console.WriteLine($"[{Convert.ToInt32(response.StatusCode)}]\t{response.StatusCode}"); 
        return response; 
    } 
}
```
