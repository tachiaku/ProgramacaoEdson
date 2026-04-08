using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[Networked] public int Score { get; set; }

[Rpc(RpcSources.All, RpcTargets.StateAuthority)]
public void RPC_AddScore(int points)
{
    Score += points;
}

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;

   public void PlayerJoined(PlayerRef player)
   {
	   if(player == Runner.LocalPlayer)
	   {
	NetworkObject objetoDaRede = Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
	Runner.SetPlayerObject(player, objetoDaRede);	   }
   }
}
//throw new System.NotImplementedException();