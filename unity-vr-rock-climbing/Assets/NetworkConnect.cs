using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;

public class NetworkConnect : MonoBehaviour
{
	public int maxConnection = 20;
	public UnityTransport transport;
	public string joinCode;
	public TMPro.TextMeshProUGUI textMeshProUGUI;
	public TMPro.TMP_InputField inputTextMeshPro;
	
	private async void Awake()
	{
		await UnityServices.InitializeAsync();
		await AuthenticationService.Instance.SignInAnonymouslyAsync();
	}
	
    public async void Create()
	{
		Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
		string newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
		textMeshProUGUI.text = newJoinCode;
		Debug.Log(newJoinCode);
		
		transport.SetHostRelayData(allocation.RelayServer.IpV4,(ushort) allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);
		
		NetworkManager.Singleton.StartHost();
	}
	
	public void ClientInput()
	{
		joinCode = inputTextMeshPro.text;
	}
	
	public async void Join()
	{
		
		JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
		
		transport.SetClientRelayData(allocation.RelayServer.IpV4,(ushort) allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);
		
		NetworkManager.Singleton.StartClient();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Create();
		}
	}
}
