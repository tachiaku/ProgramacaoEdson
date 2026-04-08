using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MovimentoController : NetworkBehaviour
{
    public CharacterController characterController;
    public float velocidade = 50f;
    public Animator animator;
    public int points = 0;

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if(HasStateAuthority)
        {
            float horizontal = Input.GetAxis("Horizontal");      
            float vertical = Input.GetAxis("Vertical");

            Vector3 direcao = new Vector3 (horizontal, 0, vertical);
           
            if(direcao.magnitude > 0.1f)
            {
                #region forma de mvimentação
               //characterController.Move(direcao * velocidade * Runner.DeltaTime); //movimento do personagem
                //transform.rotation = Quaternion.LookRotation(direcao); //rotacao do personagem
                #endregion

                #region segunda forma de movimentação
                //movimento do personagem
                characterController.Move(transform.forward * vertical * velocidade * Runner.DeltaTime);
                //rotaçaõ personagem
                float velocidadeRotacao = velocidade * 1f;
                transform.Rotate(new Vector3(0, horizontal * velocidade * Runner.DeltaTime, 0));
                #endregion

                animator.SetBool("podeAndar", true);
               
            }

            else
            {
                                animator.SetBool("podeAndar", false);

            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            points++;
            Debug.Log("Doce coletado: aumento de nível de açúcar em + points !");
        }
    }

}
