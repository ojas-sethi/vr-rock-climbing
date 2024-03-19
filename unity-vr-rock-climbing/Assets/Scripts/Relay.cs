using System.Collections;
using System.Collections.Generic;
<<<<<<< Updated upstream
=======
using System;
using System.Text;
>>>>>>> Stashed changes
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;

<<<<<<< Updated upstream
=======
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

>>>>>>> Stashed changes
public class Relay : MonoBehaviour
{
	[SerializeField]
	private short maxPlayers = 4;
<<<<<<< Updated upstream
	private string joinCode;
	public TMPro.TextMeshProUGUI textMeshProUGUI;

	private async void Start()
	{
		await UnityServices.InitializeAsync();
=======
	public string joinCode = "Hi";
	public TMPro.TextMeshProUGUI textMeshProUGUI;
	
	private Lobby currentLobby;

	private async void Start()
	{
		var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		var stringChars = new char[8];
		var random = new System.Random();

		for (int i = 0; i < stringChars.Length; i++)
		{
			stringChars[i] = chars[random.Next(chars.Length)];
		}

		var randomUserId = new String(stringChars);
		var authProfile = new InitializationOptions().SetProfile(randomUserId);
		
		await UnityServices.InitializeAsync(authProfile);
>>>>>>> Stashed changes

		AuthenticationService.Instance.SignedIn += () =>
		{
			Debug.Log("Signed In" + AuthenticationService.Instance.PlayerId);
		};
		await AuthenticationService.Instance.SignInAnonymouslyAsync();
	}

	public async void AllocateRelay()
	{
		try
		{
			Debug.Log("Host - Creating an allocation.");

			// Important: Once the allocation is created, you have ten seconds to BIND
			Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers-1); // takes number of connections allowed as argument. you can add a region argument

			joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
<<<<<<< Updated upstream
			textMeshProUGUI.text = joinCode;
=======
			textMeshProUGUI.text = "Hosting: " + joinCode;
>>>>>>> Stashed changes
			Debug.Log(joinCode);

			RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
			NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
<<<<<<< Updated upstream
=======
			
			
			CreateLobbyOptions lobbyOptions = new CreateLobbyOptions();
			lobbyOptions.IsPrivate = false;
			lobbyOptions.Data = new Dictionary<string, DataObject>();
			DataObject dataObject = new DataObject(visibility: DataObject.VisibilityOptions.Public, value: joinCode);
			lobbyOptions.Data.Add("JOIN_CODE", dataObject);
			
			currentLobby = await Lobbies.Instance.CreateLobbyAsync("Lobby Name", maxPlayers, lobbyOptions);
			
>>>>>>> Stashed changes

			NetworkManager.Singleton.StartHost();
		} 
		catch (RelayServiceException e)
		{
			Debug.LogError(e.Message);
		}
	}

<<<<<<< Updated upstream
	public async void JoinRelay(string joinCode)
	{
		try
		{
			Debug.Log("Joining Relay with " + joinCode);
			JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
=======
	public async void JoinRelay()
	{
		try
		{
			Debug.Log("trying to join");
			
			currentLobby = await Lobbies.Instance.QuickJoinLobbyAsync();
			
			Debug.Log(currentLobby.Data["JOIN_CODE"].Value);
			
			string lobbyJoinCode = currentLobby.Data["JOIN_CODE"].Value;
			textMeshProUGUI.text = "Joining: " + lobbyJoinCode;
			
			Debug.Log("Joining Relay with " + joinCode);
			JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(lobbyJoinCode);
>>>>>>> Stashed changes

			RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
			NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

			NetworkManager.Singleton.StartClient();
		}
		catch (RelayServiceException e)
		{
			Debug.LogError(e.Message);
<<<<<<< Updated upstream
=======
			//joinCCode.text = "testing error";
			//joinCCode.text = e.Message + "\n";
>>>>>>> Stashed changes
		}
	}
}
