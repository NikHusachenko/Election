using Microsoft.AspNetCore.SignalR;

namespace Election.Api.Hubs;

public sealed class ElectionHub : Hub
{
    public Task SyncVotes(int candidateId) => Clients.Group(GetGroupId(candidateId)).SendAsync(GetClientMethod(candidateId), candidateId);
    public Task Subscribe(int candidateId) => Groups.AddToGroupAsync(Context.ConnectionId, GetGroupId(candidateId));
    public Task Unsubscribe(int candidateId) => Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupId(candidateId));
    
    private string GetGroupId(int candidateId) => $"candidate_{candidateId}";
    private string GetClientMethod(int candidateId) => $"handleVoteChanged_{candidateId}";
}