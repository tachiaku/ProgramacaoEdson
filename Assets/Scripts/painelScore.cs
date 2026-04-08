using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class painelScore : MonoBehaviour
{
    public Text textoTela;

    void Update()
    {
        AtualizarPlacar();
    }

    void AtualizarPlacar()
    {
        string placar = "=== PLACAR ===\n";
        NetworkObject[] todosObjetos = FindObjectsOfType<NetworkObject>();
        int numeroPlayer = 1;
        
        foreach (NetworkObject networkObj in todosObjetos)
        {
            PlayerMoviment player = networkObj.GetComponent<PlayerMoviment>();

            if (player != null)
            {
                string marcador = networkObj.HasInputAuthority ? " (VOCÊ)" : "";
                placar += $"Player {numeroPlayer}{marcador}: {player.Score} pontos\n";
                numeroPlayer++;
            }
        }

        textoTela.text = placar;
    }
}
