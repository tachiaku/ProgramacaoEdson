using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Networked] public NetworkBool IsCollected { get; set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<NetworkObject>().HasInputAuthority)
        {
            if (IsCollected) return;

            // Pega a referência do jogador local para saber quem pontuou
            PlayerRef localPlayer = other.GetComponent<NetworkObject>().InputAuthority;

            // Chama o RPC no dono do objeto (StateAuthority)
            RPC_Dono(localPlayer);
        }
    }


    // RpcSources.All: qualquer um chama | RpcTargets.StateAuthority: executa só no dono
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_Dono(PlayerRef collector)
    {
        // Este código roda APENAS na máquina de quem tem autoridade sobre o item
        if (IsCollected) return; // Proteção contra coletas duplas
        IsCollected = true;


         // Aqui você busca o script de movimentação/stats do 'collector' para somar o ponto
        // Como o dono do item está chamando, a mudança será replicada para todos
        NetworkObject playerObj = Runner.GetPlayerObject(collector);
        if (playerObj != null)
        {
            PlayerMoviment player = playerObj.GetComponent<PlayerMoviment>();
            if (player != null)
            {
player.RPC_AddScore(1); // Chama o RPC para adicionar pontos, garantindo que a lógica de score fique no script do jogador
            }
        }
        Runner.Despawn(Object); // O dono pode dar Despawn com segurança
    }
